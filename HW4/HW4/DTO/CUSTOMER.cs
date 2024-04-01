using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4.DTO
{
    internal class CUSTOMER : INotifyPropertyChanged, ICloneable
    {
        public string CusID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PhoneNum { get; set; }
        public string CusAddress { get; set; }
        public DateTime DOB { get; set; }
        public string CusImage { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
