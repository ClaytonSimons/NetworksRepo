using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace NetworksLab3CompressionServer
{
    class HTTPS
    {
        static String indexfile = "index.html";
        TcpListener server;
        static String root;
        int port;
        TcpClient browser;
        public HTTPS(TcpClient tcpc)
        {
            browser = tcpc;
        }
        public HTTPS(String rootlib, int thePort)
        {
            root = rootlib;
            port = thePort;
            try
            {
                //bind and start server
                server = new TcpListener(port);
                Console.WriteLine("Accepting connections on port " + thePort);
                Console.WriteLine("Document Root: " + root);
                server.Start();
                while(true)
                {
                    HTTPS https = new HTTPS(server.AcceptTcpClient());
                    Thread serverThread = new Thread(new ThreadStart(https.Process));
                    serverThread.Start();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e + " " + e.StackTrace);
            }
        }
        public void Process()
        {
            String contentType;
            String httpVersion = "";
            String fileName;
            try
            {
                Console.WriteLine("HTTP request sent");

                StreamWriter outstream = new StreamWriter(browser.GetStream());
                StreamReader instream = new StreamReader(browser.GetStream());

                String request = instream.ReadLine();

                Console.WriteLine(request);

                //split
                String[] st = request.Split();

                String method = st[0];
                if(method == "GET")
                {
                    String file = st[1];

                    if (file.EndsWith("/")) 
                        file += indexfile;

                    contentType = GuessContentTypeFromName(file);

                    if(st.Length > 2)
                    {
                        httpVersion = st[2];
                    }
                    //go through rest
                    while ((request = instream.ReadLine()) != null)
                    {
                        //Display lines
                        Console.WriteLine(request);
                        if (request.Trim().Equals("")) break;
                    }

                    try
                    {
                        fileName = root + file;

                        FileStream fileStream = new FileStream(fileName, FileMode.Open);

                        byte[] Data = new byte[(int)fileStream.Length];

                        fileStream.Read(Data, 0, Data.Length);
                        fileStream.Close();

                        if(httpVersion.StartsWith("HTTP/"))
                        {
                            SendHeader(outstream, "HTTP/1.0 200 OK/r/n", contentType);
                            outstream.Write("Content-Length: " + Data.Length + "/r/n/r/n");
                        }

                        BufferedStream byteStream = new BufferedStream(browser.GetStream());
                        byteStream.Write(Data, 0, Data.Length);

                        byteStream.Close();
                    }
                    catch(FileNotFoundException)
                    {
                        //can't find the file-send a 404 code
                        if (httpVersion.StartsWith("HTTP/"))
                        {
                            SendHeader(outstream,"HTTP/1.0 404 File Not Found\r\n","text/html");
                        }
                        //Send some html to the client describing error
                        outstream.Write("\r\n<HTML><BODY><H1>HTTP Error 404: ");
                        outstream.Write("File Not Found</H1></BODY></HTML>\r\n");
                        outstream.Close();
                    }
                }
                else
                {
                    if(httpVersion.StartsWith("HTTP/"))
                    {
                        SendHeader(outstream, "HTTP/1.0 501 Not Implemented/r/n", "text/hrml");
                    }
                    outstream.Write("/r/n<HTML><BODY><H1>HTTP Error 501: ");
                    outstream.Write("Not Implemented</H1></BODY></HTML>/r/n");
                    outstream.Close();
                }
                browser.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e + " " + e.StackTrace);
            }

        }
        public String GuessContentTypeFromName(String name)
        {

            //Determine the MIME type from the specified file extension
            if (name.EndsWith(".html")
            || name.EndsWith(".htm")) return "text/html";
            else if (name.EndsWith(".txt")) return "text/plain";
            else if (name.EndsWith(".gif")) return "image/gif";
            else if (name.EndsWith(".jpg")
            || name.EndsWith(".jpeg")) return "image/jpeg";
            else return "text/plain";

        }
        public void SendHeader(StreamWriter ostream,String code,String contentType)
        {
            DateTime now = DateTime.Now;
            ostream.Write(code);
            ostream.Write("Date: " + now + "\r\n");
            ostream.Write("Server: HTTPServer 1.0\r\n");
            ostream.Write("Content-type: " + contentType + "\r\n");
        }
    }
}
