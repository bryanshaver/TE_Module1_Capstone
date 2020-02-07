using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone
{
    abstract public class Products
    {
        public string SlotLocation { get; set; }

        public string ProductName { get; set; }

        public string Price { get; set; }

        public string Type { get; set; }

        public string Display { get; set; }

        public int Amount { get; set; } = 5;

        public int ItemSales { get; set; } = 0;

    }
}
