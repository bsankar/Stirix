using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace StirixLib
{
    public class StirixLib
    {
    }

    public class StirixListener
    {
        public HttpListener stirixListener  { get; set; }

        public void startListner(){
        stirixListener = stirixSingleton.Instance;
        stirixListener.Prefixes.Add("http://*:9090/");
        stirixListener.Start();
            IAsyncResult result;
            Boolean error=false;
       while(!error){
           try{
         result = stirixListener.BeginGetContext(new AsyncCallback(getData), stirixListener);
        Console.WriteLine("Waiting for request to be processed asyncronously.");
           }
            catch(Exception e)
           {
                error=true;
            }
        }
       // result.AsyncWaitHandle.WaitOne();
        Console.WriteLine("Request processed asyncronously.");
        stirixListener.Close();

        }

        public static void getData(IAsyncResult result)
        {
            HttpListener listener = (HttpListener)result.AsyncState;
            // Call EndGetContext to complete the asynchronous operation.
            HttpListenerContext context = listener.EndGetContext(result);
            HttpListenerRequest request = context.Request;
            // Obtain a response object.
            HttpListenerResponse response = context.Response;
            // Construct a response. 
            string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
        }
    }

    public sealed class stirixSingleton
    {
        private static volatile HttpListener instance;
        private static object syncRoot = new Object();

        private stirixSingleton() { }

        public static HttpListener Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new HttpListener();
                    }
                }

                return instance;
            }
        }
    }
 
}
