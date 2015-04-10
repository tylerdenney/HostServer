using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace HostServer
{
    public partial class Form1 : Form
    {
        cHost host;
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("View Most Ordered");
            comboBox1.Items.Add("View Average Party Size");
            comboBox1.Items.Add("View Average Order Cost");
          
            host = new cHost();
            host.Initialize(listbox_incoming_orders, listbox_incoming_requests);
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            cHost.ConfirmRequest(listbox_incoming_requests.SelectedIndex);
            listbox_incoming_requests.Items.Remove(listbox_incoming_requests.SelectedItem);
        }

        private void btb_removeorder_Click(object sender, EventArgs e)
        {
            cHost.ConfirmRequest(listbox_incoming_orders.SelectedIndex);
            listbox_incoming_orders.Items.Remove(listbox_incoming_orders.SelectedItem);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cHost.ExportOrdersToDatabase();
            MessageBox.Show("Successfully Entered Orders");
        }

        private void listbox_incoming_orders_SelectedIndexChanged(object sender, EventArgs e)
        {
            detailed_order.Items.Clear();
            double cost = 0;
            string[] orderinfo = listbox_incoming_orders.Text.Split(':');
            if (orderinfo.Length > 1)
            {
                detailed_order.Items.Insert(0, "Date: " + orderinfo[1]);
                detailed_order.Items.Insert(1, "Party Size: " + orderinfo[2]);
                detailed_order.Items.Insert(2, "Table Number: " + orderinfo[3]);
                cost = Convert.ToDouble(orderinfo[4]);
                cost = Math.Round(cost, 2);
                detailed_order.Items.Insert(3, "Cost: $" + cost);
                detailed_order.Items.Insert(4, "Order: " + orderinfo[5]);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            pastorderbox.Items.Clear();
            List<cHostOrder> pastorders = host.FindPastOrders();
            for(int i = 0; i < pastorders.Count(); ++i)
            {
                pastorderbox.Items.Insert(0,pastorders[i].GetDate());
            }
        }

        private void pastorderbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pastorderbox.SelectedIndex != -1)
            {
                pastorderdetails.Items.Clear();

                List<cHostOrder> pastorders = host.FindPastOrders();

                pastorderdetails.Items.Add("Date:" + pastorders[pastorderbox.SelectedIndex].GetDate());
                pastorderdetails.Items.Add("Party Size: " + pastorders[pastorderbox.SelectedIndex].GetPartySize());
                pastorderdetails.Items.Add("Table Number: " + pastorders[pastorderbox.SelectedIndex].GetTableNum());
                pastorderdetails.Items.Add("Cost: " + pastorders[pastorderbox.SelectedIndex].GetCost());
                pastorderdetails.Items.Add("Order: ");
                List<iHostItem> items = pastorders[pastorderbox.SelectedIndex].GetItems();
                for (int i = 0; i < items.Count(); ++i)
                {
                    pastorderdetails.Items.Add(items[i].GetItemName());

                }
            }
        }
        private void CreateGraph(ZedGraphControl zg1)
        {
            // get a reference to the GraphPane
  
            GraphPane myPane = zg1.GraphPane;


            // Set the Titles
            myPane.Title.Text = "My Test Bar Graph";
            myPane.XAxis.Title.Text = "Label";
            myPane.YAxis.Title.Text = "My Y Axis";

            // Make up some random data points
            string[] labels = { "Panther", "Lion", "Cheetah", 
                      "Cougar", "Tiger", "Leopard" };
            double[] y = { 100, 115, 75, 22, 98, 40 };
            double[] y2 = { 90, 100, 95, 35, 80, 35 };
            double[] y3 = { 80, 110, 65, 15, 54, 67 };
            double[] y4 = { 120, 125, 100, 40, 105, 75 };

            // Generate a red bar with "Curve 1" in the legend
            BarItem myBar = myPane.AddBar("Curve 1", null, y,
                                                        Color.Red);
            myBar.Bar.Fill = new Fill(Color.Red, Color.White,
                                                        Color.Red);

            // Generate a blue bar with "Curve 2" in the legend
            myBar = myPane.AddBar("Curve 2", null, y2, Color.Blue);
            myBar.Bar.Fill = new Fill(Color.Blue, Color.White,
                                                        Color.Blue);

            // Generate a green bar with "Curve 3" in the legend
            myBar = myPane.AddBar("Curve 3", null, y3, Color.Green);
            myBar.Bar.Fill = new Fill(Color.Green, Color.White,
                                                        Color.Green);

            // Generate a black line with "Curve 4" in the legend
            LineItem myCurve = myPane.AddCurve("Curve 4",
                  null, y4, Color.Black, SymbolType.Circle);
            myCurve.Line.Fill = new Fill(Color.White,
                                  Color.LightSkyBlue, -45F);

            // Fix up the curve attributes a little
            myCurve.Symbol.Size = 8.0F;
            myCurve.Symbol.Fill = new Fill(Color.White);
            myCurve.Line.Width = 2.0F;

            // Draw the X tics between the labels instead of 
            // at the labels
            myPane.XAxis.MajorTic.IsBetweenLabels = true;

            // Set the XAxis labels
            myPane.XAxis.Scale.TextLabels = labels;
            // Set the XAxis to Text type
            myPane.XAxis.Type = AxisType.Text;

            // Fill the Axis and Pane backgrounds
            myPane.Chart.Fill = new Fill(Color.White,
                  Color.FromArgb(255, 255, 166), 90F);
            myPane.Fill = new Fill(Color.FromArgb(250, 250, 255));

            // Tell ZedGraph to refigure the
            // axes since the data have changed
            zg1.AxisChange();
        }
        private void SetupGraphForMostOrdered(List<iHostItem> mitems)
        {
            zedGraphControl1.ResetText();
            GraphPane myPane = zedGraphControl1.GraphPane;

            // Set the Titles
            myPane.Title.Text = "Most Ordered Items";
            myPane.XAxis.Title.Text = "Items";
            myPane.YAxis.Title.Text = "Frequency";

            string[] labels = { mitems[0].GetItemName(), mitems[1].GetItemName(), mitems[2].GetItemName() };
            double[] y1 = { mitems[0].GetCount() };
            double[] y2 = { mitems[1].GetCount()};
            double[] y3 = { mitems[2].GetCount() };

            BarItem myBar = myPane.AddBar(mitems[0].GetItemName(), null, y1,
                                                      Color.Red);
            myBar.Bar.Fill = new Fill(Color.Red, Color.White,
                                                        Color.Red);
            myBar = myPane.AddBar(mitems[1].GetItemName(), null, y2, Color.Blue);
            myBar.Bar.Fill = new Fill(Color.Blue, Color.White,
                                                        Color.Blue);

            // Generate a green bar with "Curve 3" in the legend
            myBar = myPane.AddBar(mitems[2].GetItemName(), null, y3, Color.Green);
            myBar.Bar.Fill = new Fill(Color.Green, Color.White,
                                                        Color.Green);


            // Draw the X tics between the labels instead of 
            // at the labels
            myPane.XAxis.MajorTic.IsBetweenLabels = true;

            // Set the XAxis to Text type
            myPane.XAxis.Type = AxisType.Text;

            // Fill the Axis and Pane backgrounds
            myPane.Chart.Fill = new Fill(Color.White,
                  Color.FromArgb(255, 255, 166), 90F);
            myPane.Fill = new Fill(Color.FromArgb(250, 250, 255));

            // Tell ZedGraph to refigure the
            // axes since the data have changed
            zedGraphControl1.AxisChange();
            
        }
        private void SetupGraphForMostOrderedByDay(List<iHostItem>[] mitems)
        {
            zedGraphControl1.ResetText();
            GraphPane myPane = zedGraphControl1.GraphPane;
            string[] labels = { "Mon", "Tues", "Wed", "Thurs", "Fri", "Sat", "Sun" };
            double[] y = { mitems[0][0].GetCount(), 115, 75, 22, 98, 40 };//red
            double[] y2 = { mitems[0][1].GetCount(), 100, 95, 35, 80, 35 };
            double[] y3 = { mitems[0][2].GetCount(), 65, 15, 54, 67 };


            // Generate a red bar with "Curve 1" in the legend
            BarItem myBar = myPane.AddBar("Curve 1", null, y,
                                                        Color.Red);
            myBar.Bar.Fill = new Fill(Color.Red, Color.White,
                                                        Color.Red);

            // Generate a blue bar with "Curve 2" in the legend
            myBar = myPane.AddBar("Curve 2", null, y2, Color.Blue);
            myBar.Bar.Fill = new Fill(Color.Blue, Color.White,
                                                        Color.Blue);

            // Generate a green bar with "Curve 3" in the legend
            myBar = myPane.AddBar("Curve 3", null, y3, Color.Green);
            myBar.Bar.Fill = new Fill(Color.Green, Color.White,
                                                        Color.Green);

           
            myPane.XAxis.MajorTic.IsBetweenLabels = true;

            // Set the XAxis to Text type
            myPane.XAxis.Type = AxisType.Text;

            // Fill the Axis and Pane backgrounds
            myPane.Chart.Fill = new Fill(Color.White,
                  Color.FromArgb(255, 255, 166), 90F);
            myPane.Fill = new Fill(Color.FromArgb(250, 250, 255));

            // Tell ZedGraph to refigure the
            // axes since the data have changed
            zedGraphControl1.AxisChange();

        }
        private void SetupGraphForAvgValue(double[] values, string valuename)
        {
            GraphPane myPane = zedGraphControl1.GraphPane;

            // Set the Titles
            myPane.Title.Text = "Average " + valuename;
            myPane.XAxis.Title.Text = "Day";
            myPane.YAxis.Title.Text = "Frequency";

            string[] labels = { "Mon", "Tues", "Wed", "Thurs", "Fri", "Sat", "Sun" };
            double[] y1 = values;


            BarItem myBar = myPane.AddBar(valuename, null, y1,
                                                      Color.Red);
            myBar.Bar.Fill = new Fill(Color.Red, Color.White,
                                                        Color.Red);

            myPane.XAxis.MajorTic.IsBetweenLabels = true;

            // Set the XAxis labels
            myPane.XAxis.Scale.TextLabels = labels;
            // Set the XAxis to Text type
            myPane.XAxis.Type = AxisType.Text;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            zedGraphControl1.GraphPane.GraphObjList.Clear();
            zedGraphControl1.GraphPane.CurveList.Clear();
            if (comboBox1.SelectedIndex == 0)
            {
                List<iHostItem> mitems = host.FindMostOrderedItems();
                if (mitems != null)
                    SetupGraphForMostOrdered(mitems);
                else
                    MessageBox.Show("Not enough info in db");

            }
            if (comboBox1.SelectedIndex == 1)
            {
                //double partysize = host.findAveragePartySize();
                //SetupGraphForAvgValue(partysize, "Party Size");
                double[] partysize = host.findAveragePartySizes();
                SetupGraphForAvgValue(partysize, "Party Sizes");


            }
            if (comboBox1.SelectedIndex == 2)
            {
                double ordercost = host.FindAverageOrderCost();
                //SetupGraphForAvgValue(ordercost, "Order Cost");
                double[] costs = host.findAverageOrderCosts();
                SetupGraphForAvgValue(costs, "Average Costs");
            }
        }
    }
}
