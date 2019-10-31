using System;
using System.Collections.Generic;
using System.Text;

namespace App172S.Models
{
    public class Product : ModelObject
    {
        string name;
        int unitPrice;

        public Product(string name, int unitPrice)
        {
            this.name = name;
            this.unitPrice = unitPrice;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }
    }

    
}
