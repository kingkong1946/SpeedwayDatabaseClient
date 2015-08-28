using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
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

        private BaseRepository _table;

        /// <summary>
        /// Table selected from TabControl
        /// </summary>
        private string _selectedTable = "riders";
        public string SelectedTable
        {
            get { return _selectedTable; }
            set
            {
                _selectedTable = value;
                OnPropertyChanged("SelectedTable");
            }
        }

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
                StartContext();
                _table.Start();
                _table.Update(SelectedRider);
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
            finally
            {
                _table?.Dispose();
            }
        }

        private bool IsSelected(object parameter)
        {
            return SelectedRider != null;
        }

        private void Load()
        {
            Riders = new ObservableCollection<Rider>();
            IEnumerable<object> riders = null;
            try
            {
                StartContext();
                _table.Start();
                riders = _table.GetRecords().ToList();
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
            finally
            {
                _table?.Dispose();
            }

            if (riders == null) return;
            foreach (var item in riders.OfType<Rider>())
            {
                Riders.Add(item);
            }
        }

        private void AddRow()
        {
            var rider = new Rider();
            Riders.Add(rider);
            try
            {
                StartContext();
                _table.Start();
                _table.Add(rider);
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
            finally
            {
                _table?.Dispose();
            }
        }

        private void DeleteRow()
        {
            try
            {
                StartContext();
                _table.Start();
                _table.Delete(SelectedRider);
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
            finally
            {
                _table?.Dispose();
            }
            Riders.Remove(SelectedRider);
        }

        private void StartContext()
        {
            switch (SelectedTable.ToLower())
            {
                case "riders":
                    _table = new RiderRepository();
                    break;

                default:
                    throw new ArgumentException("Invalid name of table");
            }
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
