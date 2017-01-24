using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamGridSelectedItems.Models
{
    public class Company : ObservableObject
    {
        public Company()
        {
            Products = new BindableCollection<Product>();
        }

        public Company(IEnumerable<Product> products)
        {
            Products = new BindableCollection<Product>(products);
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetField(ref _name, value); }
        }

        private Uri _uri;

        public Uri Uri
        {
            get { return _uri; }
            set { SetField(ref _uri, value); }
        }

        private BindableCollection<Product> _products;

        public BindableCollection<Product> Products
        {
            get { return _products; }
            set { SetField(ref _products, value); }
        }
    }
}
