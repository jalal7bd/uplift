using System;
using System.Collections.Generic;
using System.Text;

namespace Uplift.Models.ViewModels
{
    public class CartViewModel
    {
        public IList<Product> ProductList { get; set; }
        public OrderHeader OrderHeader { get; set; }

    }
}
