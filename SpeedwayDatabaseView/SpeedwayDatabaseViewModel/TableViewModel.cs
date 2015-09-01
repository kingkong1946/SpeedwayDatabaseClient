using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using SpeedwayDatabaseModel;
using SpeedwayDatabaseViewModel.Annotations;
using SpeedwayDAL;

namespace SpeedwayDatabaseViewModel
{
    /// <summary>
    /// Is responsible for communication with Collection
    /// </summary>
    public class TableViewModel : BaseViewModel 
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public TableViewModel()
        {
            LoadTableCommand = new RelayCommand(o => Load());
            AddRowCommand = new RelayCommand(o => AddRowInRiders());
            DeleteRowCommand = new RelayCommand(o => DeleteRowFromRiders(), IsSelected);
            SearchCommand = new RelayCommand(o => Search());
        }

        #endregion

        #region Fields

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
                OnPropertyChanged(ErrorMessage);
            }
        }

        /// <summary>
        /// Rider selected from table
        /// </summary>
        private Rider _selectedItem;
        public Rider SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                //RowEdited();
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        /// <summary>
        /// Collection Collection 
        /// </summary>
        private ObservableCollection<Rider> _collection;
        public ObservableCollection<Rider> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                OnPropertyChanged("Collection");
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Occurs when Rider grid is loading
        /// </summary>
        public ICommand LoadTableCommand { get; }

        /// <summary>
        /// Occurs when user added row
        /// </summary>
        public ICommand AddRowCommand { get; }

        /// <summary>
        /// Occurs when user deleted row
        /// </summary>
        public ICommand DeleteRowCommand { get; }

        /// <summary>
        /// Occurs when user click searh button
        /// </summary>

        public ICommand SearchCommand { get; }

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
                    context.Update(SelectedItem);
                }
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }

        private bool IsSelected(object parameter)
        {
            return SelectedItem != null;
        }

        private void Load()
        {
            Collection = new ObservableCollection<T>();
            try
            {
                IEnumerable<Rider> riders = null;
                using (var context = new RiderRepository())
                {
                    riders = context.GetRecords();
                }
                foreach (var rider in riders)
                {
                    Collection.Add(rider);
                }
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }

        private void AddRowInRiders()
        {
            var rider = new Rider();
            Collection.Add(rider);
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

        private void DeleteRowFromRiders()
        {
            try
            {
                using (var context = new RiderRepository())
                {
                    context.Delete(SelectedItem);
                }
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
            Collection.Remove(SelectedItem);
        }
        #endregion
    }
}
