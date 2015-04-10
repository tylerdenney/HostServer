using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HostServer
{
    class iHostItem
    {
        private string ItemName;
        private int count;
        public string GetItemName()
        {
            return ItemName;
        }
        public iHostItem(string name)
        {
            ItemName = name;
        }
        public iHostItem(string name, int _count)
        {
            ItemName = name;
            count = _count;
        }
        public int GetCount()
        {

            return count;
        }
        public void SetCount(int _count)
        {
            count = _count;
        }
    }
}
