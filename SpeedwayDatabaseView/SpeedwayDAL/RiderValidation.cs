using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedwayDAL
{
    public partial class Rider : IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                var error = string.Empty;
                switch (columnName)
                {
                    case "FirstName":
                        if (string.IsNullOrEmpty(FirstName)) error = "First Name can't be null";
                        break;
                    case "LastName":
                        if (string.IsNullOrEmpty(LastName)) error = "Last Name can't be null";
                        break;
                    case "Country":
                        if (string.IsNullOrEmpty(LastName)) error = "Country can't be null";
                        break;
                    //case "BirthDate":
                    //    if (string.IsNullOrEmpty(LastName)) error = "Last name can't be null";
                    //    break;
                }
                return error;
            }
        }

        public string Error => null;
    }
}
