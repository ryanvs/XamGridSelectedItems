using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamGridSelectedItems.Models
{
    public class Product : ObservableObject
    {
        public Product()
        {
            Versions = new BindableCollection<ProductVersion>();
        }

        public Product(IEnumerable<ProductVersion> versions)
        {
            Versions = new BindableCollection<ProductVersion>(versions);
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetField(ref _name, value); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { SetField(ref _description, value); }
        }

        private BindableCollection<ProductVersion> _versions;

        public BindableCollection<ProductVersion> Versions
        {
            get { return _versions; }
            set { SetField(ref _versions, value); }
        }

    }
}
