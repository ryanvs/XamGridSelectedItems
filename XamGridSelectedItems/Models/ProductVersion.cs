using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamGridSelectedItems.Models
{
    public class ProductVersion : ObservableObject
    {
        private string _version;

        public string Version
        {
            get { return _version; }
            set { SetField(ref _version, value); }
        }

        private DateTime? _releaseDate;

        public DateTime? ReleaseDate
        {
            get { return _releaseDate; }
            set { SetField(ref _releaseDate, value); }
        }
    }
}
