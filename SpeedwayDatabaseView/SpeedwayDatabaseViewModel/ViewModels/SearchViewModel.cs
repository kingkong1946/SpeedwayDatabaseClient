using System;
using System.ComponentModel;
using System.Windows.Input;
using SpeedwayDatabaseModel;
using SpeedwayDatabaseViewModel.Commands;

namespace SpeedwayDatabaseViewModel.ViewModels
{
    public class SearchViewModel : BaseViewModel, IDataErrorInfo
    {
        #region Constructors

        public SearchViewModel(TableViewModel tableTableviewModel)
        {
            _tableviewModel = tableTableviewModel;

            SearchCommand = new RelayCommand(o => Search(), o => CanSearch());
        }

        #endregion

        #region Commands

        public ICommand SearchCommand { get; }

        #endregion
        
        #region Properties
        
        private readonly TableViewModel _tableviewModel;

        private int? _id;
        public int? Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        private DateTime? _birthDate;
        public DateTime? BirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }

        private string _country;
        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        private int? _teamId;
        public int? TeamId
        {
            get { return _teamId; }
            set
            {
                _teamId = value;
                OnPropertyChanged(nameof(TeamId));
            }
        }

        #endregion

        private void Search()
        {
            var context = _tableviewModel.CreateAndGetContext();
            try
            {
                if (context == null) return;
                if (Id != null) context.ById(Id.Value);
                if (!string.IsNullOrWhiteSpace(FirstName)) context.ByFirstName(FirstName);
                if (!string.IsNullOrWhiteSpace(LastName)) context.ByLastName(LastName);
                if (BirthDate != null) context.ByBirthDate(BirthDate.Value);
                if (!string.IsNullOrWhiteSpace(Country)) context.ByCountry(Country);
                if (TeamId != null) context.ByTeamId(TeamId.Value);
                var query = context.GetRecords();
                _tableviewModel.Collection = query;
            }
            finally
            {
                context?.Dispose();
            }
        }

        private bool CanSearch()
        {
            return Id == null || string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) ||
                   BirthDate == null || string.IsNullOrWhiteSpace(Country);
        }

        #region IDataErrorInfo

        public string this[string columnName]
        {
            get
            {
                var error = string.Empty;
                switch (columnName)
                {
                    case nameof(FirstName):
                        error = string.IsNullOrWhiteSpace(FirstName) ? "First Name can't be null!" : string.Empty;
                        break;

                    case nameof(LastName):
                        error = string.IsNullOrWhiteSpace(LastName) ? "Last Name can't be null!" : string.Empty;
                        break;

                    case nameof(Country):
                        error = string.IsNullOrWhiteSpace(Country) ? "Country can't be null!" : string.Empty;
                        break;
                }
                return error;
            }
        }

        public string Error { get; }

        #endregion

    }
}
