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
        }

        #endregion

        #region Fields

        public Rider SelectedRider { get; set; }
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

        public ICommand LoadTableCommand { get; set; }
        public ICommand AddRowCommand { get; set; }
        public ICommand DeleteRowCommand { get; set; }
        public ICommand KeyPressedCommand { get; set; }

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

        private void Edit(object parameter)
        {

            using (var entity = new SpeedwayEntities())
            {

            }
        }

        private void AddRowInRiders()
        {
            Riders.Add(new Rider());
        }

        private void DeleteRowFromRiders()
        {
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
