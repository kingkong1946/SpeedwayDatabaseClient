using System;
using System.Collections.Generic;
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
            LoadTable = new RelayCommand(o => Load());
            CellEdit = new RelayCommand(o => Edit(o));
        }

        #endregion

        #region Fields

        private List<Rider> _riders = new List<Rider>();
        public List<Rider> Riders
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

        public ICommand LoadTable { get; set; }
        public ICommand CellEdit { get; set; }

        #endregion
        
        private void Load()
        {
            using (var entity = new SpeedwayEntities())
            {
                var riders = entity.Riders;
                Riders = riders.ToList();
            }
        }

        private void Edit(object parameter)
        {
            using (var entity = new SpeedwayEntities())
            {

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
