using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedwayDatabaseViewModel
{
    public class SearchViewModel : BaseViewModel
    {
        #region Fields

        private static string[] DateFormats = {"dd/MM/yyyy", "dd/M/yyyy"};

        private int _Id;
        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                OnPropertyChanged("ID");
            }
        }

        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                _FirstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set
            {
                _LastName = value;
                OnPropertyChanged("LastName");
            }
        }

        private DateTime _BirthDate;
        public DateTime BrithDate
        {
            get { return _BirthDate; }
            set
            {
                _BirthDate = value;
                OnPropertyChanged("BrithDate");
            }
        }

        private string _Country;
        public string Country
        {
            get { return _Country; }
            set
            {
                _Country = value;
                OnPropertyChanged("Country");
            }
        }

        private int _TeamId;
        public int TeamId
        {
            get { return _TeamId; }
            set
            {
                _TeamId = value;
                OnPropertyChanged("TeamId");
            }
        }

        #endregion
    }
}
