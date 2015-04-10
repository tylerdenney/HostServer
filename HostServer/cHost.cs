using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HostServer
{
    class cHost
    {
        static cStatEngine statengine = null;
        TcpClient client = null;
        bool listening = false;

        CheckedListBox incoming_orders = null;
        CheckedListBox incoming_requests = null;
        String[] message_from_client;
        string raw_message;
        public static List<cHost> clients = new List<cHost>();
        public static List<cHostOrder> orders = new List<cHostOrder>();
        //basic ctor
        public cHost()
        {

        }
        public cHost(TcpClient tcpc)
        {
            client = tcpc;

        }
        public static void ExportOrdersToDatabase()
        {
            statengine.EnterOrders(orders);
            orders.Clear();
        }

        public static void ConfirmRequest(int client)
        {
            if (client != -1)
            {
                clients[client].SetState(false);
                clients.RemoveAt(client);
            }
        }

        public void SetState(bool state)
        {
            listening = state;
        }
        public void ListenForOrders(CheckedListBox orders, CheckedListBox requests)
        {


            TcpListener server = null;
            try
            {
                Int32 portNumber = 9999;
                IPAddress localip = IPAddress.Parse("10.103.5.145");
                server = new TcpListener(localip, portNumber);
                server.Start();

                //Echo server loops forever, listening for clients
                for (; ; )
                {
                    //Accept the pending client connection and return a client 
                    //initialized for communication
                    //This method will block until a connection is made
                    cHost es = new cHost(server.AcceptTcpClient());
                    es.SetState(true);
                    clients.Add(es);

                    //Allow this conversation to run in a new thread
                    // Thread serverThread = new Thread(new ThreadStart(es.Conversation));
                    // serverThread.Start();
                    Console.WriteLine("Creating thread");
                    Thread conversationthread = new Thread(() => es.Conversation(orders, requests));
                    conversationthread.Start();

                    //Loop back up and wait for another client
                    //Another thread is servicing this client
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e + " " + e.StackTrace);
            }
            finally
            {
                //Release the port and stop the server
                server.Stop();
            }
        }
        public void UpdateRequestList(object sender, EventArgs e)
        {

            incoming_requests.Items.Insert(0, "Table " + message_from_client[1]);
            Application.DoEvents();


        }
        public void UpdateOrderList(object sender, EventArgs e)
        {
            incoming_orders.Items.Insert(0, raw_message);
            Application.DoEvents();
        }
        private void ConfirmOrder()
        {
            //0 = ORDER
            //1 = date
            //2 = party size
            //3 = table number
            //4 = total cost
            //5 = order comma separated.
            cHostOrder _order = new cHostOrder(message_from_client);
            if (_order.GetDate() != "INVALID")
            {
                //lock until it is done
                Monitor.Enter(orders);
                orders.Add(_order);

            }

        }

        //This is run as a thread for each client connected to the server.
        public void Conversation(CheckedListBox orders, CheckedListBox requests)
        {
            incoming_orders = orders;
            incoming_requests = requests;
            NetworkStream stream;
            try
            {
                byte[] bytes = new byte[5000];
                string data;
                Console.WriteLine("Connection accepted.");
                stream = client.GetStream();

                int i = stream.Read(bytes, 0, bytes.Length);
                while (i != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine(String.Format("Received: {0}, press 1 to confirm", data));

                    //Update UI
                    raw_message = data;
                    String[] splitdata = data.Split(':');
                    message_from_client = splitdata;
                    if (splitdata[0].Equals("REQUEST"))
                    {
                        orders.Invoke(new EventHandler(UpdateRequestList));
                    }
                    else if (splitdata[0].Equals("ORDER"))
                    {

                        orders.Invoke(new EventHandler(UpdateOrderList));
                    }



                    //hostresponse = Console.ReadLine();

                    while (listening == true) { }

                    if (splitdata[0].Equals("ORDER"))//create the order object
                    {
                        ConfirmOrder();

                    }

                    byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);
                    stream.Write(newline, 0, newline.Length);

                    stream.Flush();


                    Console.WriteLine(String.Format("Sent: {0} \n", data));



                    i = 0;
                }
                stream.Close();



            }
            catch (Exception e)
            {
                Console.WriteLine(e + " " + e.StackTrace);
            }

            //If things go bad, close all connections to client.
            finally
            {
                //if (stream != null) stream.Close();
                if (client != null) client.Close();
            }
        }

        public void Initialize(CheckedListBox orders, CheckedListBox requests)
        {
            Thread listeningthread = new Thread(() => ListenForOrders(orders, requests));
            listeningthread.Start();

            statengine = new cStatEngine();
        }


        public List<cHostOrder> FindPastOrders()
        {
            return statengine.FindPastOrders();
        }

        public List<iHostItem> FindMostOrderedItems()
        {
            return statengine.FindMostOrderedItems();
        }
        public List<iHostItem>[] FindMOstOrderedItemsByDay()
        {
            return statengine.FindMostOrderedItemsByDay();
        }

        public double findAveragePartySize()
        {
            return statengine.FindAvgPartySize();
        }
        public double[] findAveragePartySizes()
        {
            return statengine.FindAvgPartySizes();
        }
        public double[] findAverageOrderCosts()
        {
            return statengine.FIndAvgOrderCosts();
        }
        public double FindAverageOrderCost()
        {
            return statengine.FindAvgOrderCost();
        }
    }
}
