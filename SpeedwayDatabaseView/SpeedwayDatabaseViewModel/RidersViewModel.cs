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
    /// Is responsible for communication with Riders
    /// </summary>
    public class RidersViewModel : BaseViewModel
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public RidersViewModel()
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
        public string ErrorMessage { get; set; } = "OK";

        /// <summary>
        /// Rider selected from table
        /// </summary>
        private Rider _selectedRider;
        public Rider SelectedRider
        {
            get
            {
                return _selectedRider;
            }
            set
            {
                RowEdited();
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

        private void AddRowInRiders()
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

        private void DeleteRowFromRiders()
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
    }
}
