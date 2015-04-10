using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HostServer
{
    class cHostOrder
    {
        private string date;
        private string day;
        private double cost;
        private int tablenum;
        private int partysize;
        private List<iHostItem> items;
        private DateTime realdate;
        public string GetDate()
        {
            return date;
        }
        public DateTime GetRealDate()
        {
            return realdate;
        }
        public string GetDay()
        {
            return day;
        }
        public double GetCost()
        {
            return cost;
        }
        public int GetTableNum()
        {
            return tablenum;
        }
        public int GetPartySize()
        {
            return partysize;
        }
        public List<iHostItem> GetItems()
        {
            return items;
        }
        public cHostOrder(string[] message_from_client)
        {
            items = new List<iHostItem>();
            //0 = ORDER
            //1 = date
            //2 = party size
            //3 = table number
            //4 = total cost
            //5 = order comma separated.
            if (message_from_client.Length == 6)
            {
                date = message_from_client[1];
                string[] datearray = date.Split('-');
                string newdate = datearray[1] + '/' + datearray[0] + '/' + datearray[2];
                newdate = newdate.Trim();
                realdate = Convert.ToDateTime(newdate);
                day = realdate.ToString("ddd");
                partysize = Convert.ToInt32(message_from_client[2]);
                tablenum = Convert.ToInt32(message_from_client[3]);
                cost = Convert.ToDouble(message_from_client[4]);
                string[] _items = message_from_client[5].Split(',');
                for (int i = 0; i < _items.Count(); ++i)
                {
                    string name = _items[i];
                    items.Add(new iHostItem(name));
                }
            }
            else
                date = "INVALID";
           
        }
        public cHostOrder(string _date, int _partysize, int _tablenum, double _cost, List<iHostItem> _items)
        {
            date = _date;
            partysize = _partysize;
            tablenum = _tablenum;
            cost = _cost;
            cost = Math.Round(cost, 2);

            items = new List<iHostItem>(_items);
        }
    }
}
