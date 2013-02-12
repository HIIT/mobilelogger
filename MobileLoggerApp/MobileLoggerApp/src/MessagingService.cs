using MobileLoggerApp.src.mobilelogger.model;
using System;
using System.ComponentModel;
using System.Net;
using System.Text;

namespace MobileLoggerApp.src
{
    public class MessagingService// : DispatcherTimer
    {   
        public BackgroundWorker bgworker;

        public MessagingService()
        {
            InitializeBackgroundWorker();
            //this.Interval = new TimeSpan(0,0,1);
        }

        private void InitializeBackgroundWorker()
        {
            bgworker = new BackgroundWorker();
            bgworker.DoWork += new DoWorkEventHandler(AsyncSendMessages);
            bgworker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(SendMessagesWorkComplete);
        }

        /// <summary>
        /// Tries to send all items in the phone DB to the server
        /// </summary>
        /// <returns></returns>
        public void SendMessages()
        {
            Console.WriteLine("SendMessages");
            System.Diagnostics.Debug.WriteLine("SendMessages");
            this.bgworker.RunWorkerAsync();        
        }

        /// <summary>
        /// Fired when bgworker.RunWorkerAsync() is called
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void AsyncSendMessages(object sender, EventArgs args) 
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            Console.WriteLine("event working");
            System.Diagnostics.Debug.WriteLine("event working");
            Console.WriteLine(sender.ToString() + " " + args.ToString());
            using (LogEventDataContext logDBContext = new LogEventDataContext(MainPage.ConnectionString))
            {
                
                if (!logDBContext.DatabaseExists()) return;
                foreach (LogEvent e in logDBContext.GetLogEvents())
                {
                    SendMessage(e);
                }
            }
        }

        /// <summary>
        /// Fired when bgworker.RunWorkerAsync() finishes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void SendMessagesWorkComplete(object sender, EventArgs args) 
        {
            Console.WriteLine("event work complete");
            Console.WriteLine(sender.ToString() + " " + args.ToString());
            System.Diagnostics.Debug.WriteLine("finished send messages work");
            System.Diagnostics.Debug.WriteLine(sender.ToString() + " " + args.ToString());
        }

        //get checksum
        //send data to server
        //wait for answer checksum
        //compare checksums
        //if equal, sending was ok, remove from DB
        //else keep in DB and continue
        private void SendMessage(LogEvent e)
        {
            String checksum = DeviceTools.CalculateSHA1(e.sensorEvent);

            //create request message
            Message message = Message.Create(e.relativeUrl, e.sensorEvent, "PUT");
            message.BeginGetRequestStream(result =>
            {
                FinishWebRequest(result, message, checksum);
            }, null); // Don't need the state here any more
        }

        //validates the response based on the checksums
        private void FinishWebRequest(IAsyncResult result, Message request, String checksum)
        {
            using (HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result))
            {
                System.IO.Stream receiveStream = response.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                System.IO.StreamReader readStream = new System.IO.StreamReader(receiveStream, encode);
                
                Char[] read = new Char[256];
                // Reads 256 characters at a time.     
                int count = readStream.Read(read, 0, 256);
                Console.WriteLine("HTML...\r\n");
                while (count > 0)
                {
                    // Dumps the 256 characters on a string and displays the string to the console.
                    String str = new String(read, 0, count);
                    Console.Write(str);
                    count = readStream.Read(read, 0, 256);
                }
                Console.WriteLine("");

                // Releases the resources of the response.
                response.Close();
                // Releases the resources of the Stream.
                readStream.Close();

                //validate response checksum
            }
        }
    }
}
