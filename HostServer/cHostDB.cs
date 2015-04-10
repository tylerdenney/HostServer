using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

using System.IO;
namespace HostServer
{
    class cHostDB
    {
        public const int MOST_ORDERED = 3;

        private const int MENU_SIZE = 7;
        private string[] names = new string[] { "Turkey Club", "Fish Tacos", "Chicken Teriyaki", "SeaFood Supreme", "Raspberry Lemonade", "Sprite", "Coke" };
        private string[] descriptions = new string[] { "Turkey sandwich on wheat bread with swiss cheese", "Two Ahi Tuna tacos on crispy corn tortilla", "Chicken teriyaki. Enough said.", "Yumm seafood", "Lemonade with whole raspberries", "Refreshing soft drink Sprite", "Refreshing soft drink Coke" };
        private double[] prices = new double[] { 6.99, 7.99, 7.99, 10.50, 2.99, 1.50, 1.50 };
        private int[] fooditem = new int[] { 1, 1, 1, 1, 0, 0, 0 };
        private string CreateTableITEMS = "CREATE TABLE Items ( " +
                                                    "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                                    "Name varchar(50), " +
                                                    "Description varchar(100), " +
                                                    "Cost REAL, " +
                                                    "FoodItem INT, " +
                                                    "ItemCount INT);";
        private string CreatTableORDERS = "CREATE TABLE Orders ( " +
                                                "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                                "PartySize INT, " +
                                                "TableNum INT, " +
                                                "Cost REAL, " +
                                                "Date varchar(10), "+
                                                "Day varchar(4));";
        private string CreateTableORDER_ITEMS = "CREATE TABLE Order_Items ( " +
                                                "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                                "OrderID INT, " +
                                                "ItemID INT); "; /* +
                                                "FOREIGN KEY(OrderID) REFERENCES Order(id), " +
                                                "FOREIGN KEY(ItemID) REFERENCES Item(id))";*/
        public void CreateDB()
        {
            if (!File.Exists("Restaurant.db"))
            {
                SQLiteConnection.CreateFile("Restaurant.db");
                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=Restaurant.db;Version=3;");
                m_dbConnection.Open();
                
                SQLiteCommand createtablecommand = new SQLiteCommand(CreateTableITEMS, m_dbConnection);
                createtablecommand.ExecuteNonQuery();

                SQLiteCommand createtablecommand2 = new SQLiteCommand(CreatTableORDERS, m_dbConnection);
                createtablecommand2.ExecuteNonQuery();

                SQLiteCommand createtablecommand3 = new SQLiteCommand(CreateTableORDER_ITEMS, m_dbConnection);
                createtablecommand3.ExecuteNonQuery();

                
                

                for (int i = 0; i < MENU_SIZE; ++i)
                {
                    string insertitems = "INSERT INTO Items(Name, Description, Cost, FoodItem, ItemCount) VALUES (@name, @desc, @cost, @fooditem, " + 0 +" )";
                    SQLiteCommand cmd = new SQLiteCommand(insertitems, m_dbConnection);
                    cmd.Parameters.Add(new SQLiteParameter("@name", names[i]));
                    cmd.Parameters.Add(new SQLiteParameter("@desc", descriptions[i]));
                    cmd.Parameters.Add(new SQLiteParameter("@cost", prices[i]));
                    cmd.Parameters.Add(new SQLiteParameter("@fooditem", fooditem[i]));
                    cmd.ExecuteNonQuery();
                }



            }

            
        }
        public bool InsertOrders(List<cHostOrder> orders)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=Restaurant.db;Version=3;");
            m_dbConnection.Open();
            //Insert Order Information into database.
            for (int i = 0; i < orders.Count(); ++i)
            {
                SQLiteCommand cmd = new SQLiteCommand(m_dbConnection);
                string command = "INSERT INTO Orders(PartySize, TableNum, Cost, Date, Day) VALUES (@PartySize, @TableNum, @Cost, @Date, @Day)";
                cmd.CommandText = command;
                cmd.Parameters.Add(new SQLiteParameter("@PartySize", orders[i].GetPartySize()));
                cmd.Parameters.Add(new SQLiteParameter("@TableNum", orders[i].GetTableNum()));
                cmd.Parameters.Add(new SQLiteParameter("@Cost", orders[i].GetCost()));
                cmd.Parameters.Add(new SQLiteParameter("@Date", orders[i].GetDate()));
                cmd.Parameters.Add(new SQLiteParameter("@Day", orders[i].GetDay()));
                cmd.ExecuteNonQuery();
                long current_order_id = m_dbConnection.LastInsertRowId;

                List<iHostItem> items = new List<iHostItem>(orders[i].GetItems());
                List<int> itemids = new List<int>();
                //Update count of items that have been ordered.
                for(int j = 0; j < items.Count(); ++j)
                {
                    SQLiteCommand itemcmd = new SQLiteCommand(m_dbConnection);
                    string itemcommand = "UPDATE Items SET ItemCount = ItemCount + 1 WHERE Name=@itemname";
                    itemcmd.CommandText = itemcommand;
                    itemcmd.Parameters.Add(new SQLiteParameter("@itemname", items[j].GetItemName()));
                    int itemsupdated = itemcmd.ExecuteNonQuery();
                        //get the item id and add it to the list.
                    itemids.Add(Array.IndexOf(names, items[j].GetItemName())+1);
                        
                    

                }
                for (int k = 0; k < itemids.Count(); ++k)
                {
                    SQLiteCommand order_items_command = new SQLiteCommand(m_dbConnection);
                    string order_items_commandtext = "INSERT INTO Order_Items(OrderID, ItemID) VALUES (@OrderID, @ItemID)";
                    order_items_command.CommandText = order_items_commandtext;
                    order_items_command.Parameters.Add(new SQLiteParameter("@OrderID", current_order_id));
                    order_items_command.Parameters.Add(new SQLiteParameter("@ItemID", itemids[k]));
                    order_items_command.ExecuteNonQuery();
                }
                items.Clear();
                itemids.Clear();
                
            }
            m_dbConnection.Close();
            return true;
        }
        public List<cHostOrder> GetOrders()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=Restaurant.db;Version=3;");
            m_dbConnection.Open();
            int current_row = 1;
            List<cHostOrder> pastorders = new List<cHostOrder>();
            string commandtext = "SELECT * FROM ORDERS";
            SQLiteCommand ordercmd = new SQLiteCommand(commandtext, m_dbConnection);
            SQLiteDataReader orderreader = ordercmd.ExecuteReader();
            List<iHostItem> items = new List<iHostItem>();

            while (orderreader.Read())
            {
                string itemcommandtext = "Select Name from Items join Order_Items on Items.id = Order_Items.Itemid join Orders on Order_Items.orderID = Orders.id where Orders.id = @orderid";
                SQLiteCommand itemcommand = new SQLiteCommand(itemcommandtext, m_dbConnection);
                itemcommand.Parameters.Add(new SQLiteParameter("@orderid", current_row));
                SQLiteDataReader itemreader = itemcommand.ExecuteReader();
                while(itemreader.Read())
                {
                    items.Add(new iHostItem((string)itemreader["Name"]));
                }
               
                cHostOrder neworder = new cHostOrder((string)orderreader["Date"], (int)orderreader["PartySize"], (int)orderreader["TableNum"], (double)orderreader["Cost"], items);
                pastorders.Add(neworder);
                current_row++;
                items.Clear();
            }


            m_dbConnection.Close();
            return pastorders;
        }
        public List<iHostItem> GetMostOrderedItems()
        {
            List<iHostItem> mitems = new List<iHostItem>(MOST_ORDERED);
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=Restaurant.db;Version=3;");
            m_dbConnection.Open();
            string commandtext = "SELECT Name, ItemCount FROM Items ORDER BY ItemCount DESC LIMIT " + MOST_ORDERED ;
            SQLiteCommand ordercmd = new SQLiteCommand(commandtext, m_dbConnection);
            SQLiteDataReader orderreader = ordercmd.ExecuteReader();
            while (orderreader.Read())
            {
                mitems.Add(new iHostItem((string)orderreader["Name"],(int)orderreader["ItemCount"]));
            }


            m_dbConnection.Close();
            return mitems;
        }
        public List<iHostItem>[] GetMostOrderedItemsByDay()
        {
            string[] days = { "Mon", "Tues", "Wed", "Thurs", "Fri", "Sat", "Sun" };
            List<iHostItem>[] mitems = new List<iHostItem>[7];
            
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=Restaurant.db;Version=3;");
            m_dbConnection.Open();
            for (int i = 0; i < 7; ++i)
            {
                mitems[i] = new List<iHostItem>();
                string commandtext = "SELECT Name, ItemCount FROM Items join Order_Items on Items.id = Order_Items.itemID join Orders on Order_Items.orderID = Orders.id WHERE Day = @day ORDER BY ItemCount LIMIT 3";
                SQLiteCommand cmd = new SQLiteCommand(commandtext, m_dbConnection);
                cmd.Parameters.Add(new SQLiteParameter("@day", days[i]));

                SQLiteDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (rdr != null)
                        mitems[i].Add(new iHostItem((string)rdr["Name"], (int)rdr["ItemCount"]));
                }
                if (mitems[i].Count() < 3)
                {
                    while(mitems[i].Count() < 3)
                        mitems[i].Add(new iHostItem("", 0));

                }
            }
            return mitems;
        }



        public double GetAverageCost()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=Restaurant.db;Version=3;");
            m_dbConnection.Open();
            string commandtext = "select AVG(Cost) from Orders";
            SQLiteCommand cmd = new SQLiteCommand(commandtext, m_dbConnection);
            SQLiteDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
                return (double)rdr["AVG(Cost)"];
            else
                return 0;
        }
        public double GetAveragePartySize()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=Restaurant.db;Version=3;");
            m_dbConnection.Open();
            string commandtext = "select AVG(PartySize) from Orders";
            SQLiteCommand cmd = new SQLiteCommand(commandtext, m_dbConnection);
            SQLiteDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            return (double)rdr["AVG(PartySize)"];
        }
        public double[] GetAverageOrderCosts()
        {
            double[] ps = new double[7];
            string[] days = { "Mon", "Tues", "Wed", "Thu", "Fri", "Sat", "Sun" };
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=Restaurant.db;Version=3;");
            m_dbConnection.Open();
            for(int i = 0; i < 7; ++i)
            {
                string commandtext = "select AVG(Cost) from Orders WHERE Day = @day";
                SQLiteCommand cmd = new SQLiteCommand(commandtext, m_dbConnection);
                cmd.Parameters.Add(new SQLiteParameter("@day", days[i]));

                try
                {

                    SQLiteDataReader rdr = cmd.ExecuteReader();
                    rdr.Read();
                    ps[i] = (double)rdr["AVG(Cost)"];
                }
                catch (Exception e)
                {
                    ps[i] = 0;
                }
            }
            return ps;
        }
        public double[] GetAveragePartySizes()
        {
            double[] ps = new double[7];
            string[] days = { "Mon", "Tues", "Wed", "Thu", "Fri", "Sat", "Sun" };
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=Restaurant.db;Version=3;");
            m_dbConnection.Open();
            for(int i = 0; i < 7; ++i)
            {
                string commandtext = "select AVG(PartySize) from Orders WHERE Day = @day";
                SQLiteCommand cmd = new SQLiteCommand(commandtext, m_dbConnection);
                cmd.Parameters.Add(new SQLiteParameter("@day", days[i]));

                try
                {

                    SQLiteDataReader rdr = cmd.ExecuteReader();
                    rdr.Read();
                    ps[i] = (double)rdr["AVG(PartySize)"];
                }
                catch (Exception e)
                {
                    ps[i] = 0;
                }
            }
            return ps;

        }
    }
}
