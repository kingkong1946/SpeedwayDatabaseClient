using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SpeedwayDatabaseModel;
using SpeedwayDatabaseViewModel.Annotations;
using SpeedwayDAL;

namespace SpeedwayDatabaseViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Contructors

        public BaseViewModel()
        {
            LoadTableCommand = new RelayCommand(o => Load());
            AddRowCommand = new RelayCommand(o => AddRow());
            DeleteRowCommand = new RelayCommand(o => DeleteRow(), IsSelected);
            SearchCommand = new RelayCommand(o => Search());
        }

        #endregion

        #region Fields

        /// <summary>
        /// Number of merge columns for ToolBar and DataGrid
        /// </summary>
        private const int _mergeAll = 3;
        public int MergeAll => _mergeAll;

        /// <summary>
        /// Message error
        /// </summary>
        private string _errorMessage = "OK";

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        /// <summary>
        /// Rider selected from table
        /// </summary>
        private Rider _selectedRider;

        public Rider SelectedRider
        {
            get { return _selectedRider; }
            set
            {
                _selectedRider = value;
                OnPropertyChanged("SelectedRider");
            }
        }

        /// <summary>
        /// Riders Collection 
        /// </summary>
        private ObservableCollection<Rider> _riders;
        public ObservableCollection<Rider> Riders
        {
            get { return _riders; }
            set
            {
                _riders = value;
                OnPropertyChanged("Riders");
            }
        }

        private IRepository<> _table;

        #endregion

        #region Commands

        /// <summary>
        /// Occurs when Rider grid is loading
        /// </summary>
        public ICommand LoadTableCommand { get; set; }

        /// <summary>
        /// Occurs when user added row
        /// </summary>
        public ICommand AddRowCommand { get; set; }

        /// <summary>
        /// Occurs when user deleted row
        /// </summary>
        public ICommand DeleteRowCommand { get; set; }

        /// <summary>
        /// Occurs when user click searh button
        /// </summary>

        public ICommand SearchCommand { get; set; }

        #endregion

        #region Private Methods

        private void Search()
        {

        }

        private void RowEdited()
        {
            try
            {
                using (var context = new RiderRepository())
                {
                    context.Update(SelectedRider);
                }
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }

        private bool IsSelected(object parameter)
        {
            return SelectedRider != null;
        }

        private void Load()
        {
            Riders = new ObservableCollection<Rider>();
            try
            {
                IEnumerable<Rider> riders = null;
                using (var context = new RiderRepository())
                {
                    riders = context.GetRecords();
                }
                foreach (var rider in riders)
                {
                    Riders.Add(rider);
                }
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }

        private void AddRow()
        {
            var rider = new Rider();
            Riders.Add(rider);
            try
            {
                using (var context = new RiderRepository())
                {
                    context.Add(rider);
                }
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }

        private void DeleteRow()
        {
            try
            {
                using (var context = new RiderRepository())
                {
                    context.Delete(SelectedRider);
                }
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
            Riders.Remove(SelectedRider);
        }
        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
