using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostServer
{
    class cStatEngine
    {
        //select Name, Orders.id as "OrderID", Items.id as "ItemID" from Orders join Order_Items on Orders.id = Order_Items.orderID join Items on Order_Items.itemID = items.id

        public double avgordercost = 0.0;
        public double avgpartysize = 0.0;
        private cHostDB db = null;
        public cStatEngine()
        {
            db = new cHostDB();
            db.CreateDB();
        }
        public Double FindAvgOrderCost()
        {
            if (avgordercost == 0.0)
            {
                avgordercost = db.GetAverageCost();
                avgordercost = Math.Round(avgordercost, 2);
            }
            return avgordercost;
        }

        public double FindAvgPartySize()
        {
            if (avgpartysize == 0)
            {
                avgpartysize = db.GetAveragePartySize();
            }
            return avgpartysize;
        }
        public double[] FindAvgPartySizes()
        {
            double[] ps = db.GetAveragePartySizes();
            return ps;
        }
        public double[] FIndAvgOrderCosts()
        {
            double[] costs = db.GetAverageOrderCosts();
            return costs;
        }
        public List<cHostOrder> FindPastOrders()
        {
            return db.GetOrders();
        }
        public List<iHostItem> FindMostOrderedItems()
        {
            List<iHostItem> items = db.GetMostOrderedItems();

            if (items.Count == cHostDB.MOST_ORDERED)
            {
                return items;
            }
            else
                return null;
        }
        public List<iHostItem>[] FindMostOrderedItemsByDay()
        {
            return db.GetMostOrderedItemsByDay();
        }
        public int EnterOrders(List<cHostOrder> orders)
        {
            db.InsertOrders(orders);
            return 0;
        }

    }
}
