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
    /// PL: Odpowiada za komunikacje z tabelą Riders
    /// EN: Is responsible for communication with Riders
    /// </summary>
    public class RidersViewModel : INotifyPropertyChanged
    {
        #region Constructors

        public RidersViewModel()
        {
            LoadTableCommand = new RelayCommand(o => Load());
            AddRowCommand = new RelayCommand(o => AddRowInRiders());
            DeleteRowCommand = new RelayCommand(o => DeleteRowFromRiders(), CanDelete);
            UploadCommand = new RelayCommand(o => UploadAsync());
        }

        #endregion

        #region Fields

        private const int _MergeAll = 3;

        public int MergeAll => _MergeAll;

        public string ErrorMessage { get; set; } = "OK";
        public Rider SelectedRider { get; set; }
        private ObservableCollection<Rider> _riders;
        public ObservableCollection<Rider> Riders
        {
            get { return _riders; }
            set
            {
                _riders = value;
                OnPropertyChanged("Riders");
                UploadAsync();
            }
        }

        #endregion

        #region Commands

        public ICommand LoadTableCommand { get; set; }
        public ICommand AddRowCommand { get; set; }
        public ICommand DeleteRowCommand { get; set; }
        public ICommand UploadCommand { get; set; }
        //public ICommand EditRowCommand { get; set; }

        #endregion

        #region Private Methods

        private bool CanDelete(object parameter)
        {
            return SelectedRider != null;
        }

        private void Load()
        {
            Riders = new ObservableCollection<Rider>();
            using (var entity = new SpeedwayEntities())
            {
                var riders = entity.Riders;
                foreach (var rider in riders)
                {
                    Riders.Add(rider);
                }
            }
        }

        private async void UploadAsync()
        {
            using (var entity = new SpeedwayEntities())
            {
                try
                {
                    await entity.SaveChangesAsync();
                }
                catch (Exception exception)
                {
                    ErrorMessage = exception.Message;
                }
            }
        }

        private void AddRowInRiders()
        {
            var rider = new Rider()
            {
                BirthDate = DateTime.Now,
                Country = "Unknown",
                FirstName = "Unknown",
                LastName = "Unknown"
            };

            Riders.Add(rider);
            using (var context = new SpeedwayEntities())
            {
                var riders = context.Riders;
                riders.Add(rider);
                context.SaveChanges();
            }
        }

        private void DeleteRowFromRiders()
        {
            using (var context = new SpeedwayEntities())
            {
                var riders = context.Riders;
                riders.Attach(SelectedRider);
                riders.Remove(SelectedRider);
                context.SaveChanges();
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
