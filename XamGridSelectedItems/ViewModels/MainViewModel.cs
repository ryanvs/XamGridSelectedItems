using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamGridSelectedItems.Models;

namespace XamGridSelectedItems.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            TestSelectedItems = new BindableCollection<object>();
            Companies = new BindableCollection<Company>(
                new Company[]
                {
                    new Company()
                    {
                        Name = "Apple",
                        Uri = new Uri("http://www.apple.com"),
                        Products = new BindableCollection<Product>()
                        {
                            new Product
                            {
                                Name = "iPhone",
                                Description = "The phone that changed everything",
                                Versions = new BindableCollection<ProductVersion>()
                                {
                                    new ProductVersion { Version = "5" },
                                    new ProductVersion { Version = "6" },
                                    new ProductVersion { Version = "6S" },
                                },
                            },
                        },
                    },
                    new Company()
                    {
                        Name = "Microsoft",
                        Uri = new Uri("http://www.microsoft.com"),
                        Products = new BindableCollection<Product>()
                        {
                            new Product
                            {
                                Name = "Windows",
                                Description = "The OS that changed everything",
                                Versions = new BindableCollection<ProductVersion>()
                                {
                                    new ProductVersion { Version = "1.0" },
                                    new ProductVersion { Version = "2.0" },
                                    new ProductVersion { Version = "3.0" },
                                    new ProductVersion { Version = "3.1" },
                                },
                            },
                        },
                    }
                });
        }
        private BindableCollection<Company> _companies;

        public BindableCollection<Company> Companies
        {
            get { return _companies; }
            set { SetField(ref _companies, value); }
        }

        private BindableCollection<object> _selectedItems;

        public BindableCollection<object> TestSelectedItems
        {
            get { return _selectedItems; }
            set { SetField(ref _selectedItems, value); }
        }

    }
}
