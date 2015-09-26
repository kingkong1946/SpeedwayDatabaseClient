using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SpeedwayDatabaseModel;
using SpeedwayDAL;

namespace SpeedwayDatabaseViewModel.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class TableViewModel : BaseViewModel
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public TableViewModel()
        {
            LoadCommand = new RelayCommand(o => Load());
            AddCommand = new RelayCommand(o => Add());
            DeleteCommand = new RelayCommand(o => Delete(), IsSelected);
            //SearchCommand = new RelayCommand(o => Search());
            OnClosingCommand = new RelayCommand(o => OnClosing());
            SaveCommand = new RelayCommand(o => Save());
            EditedCommand = new RelayCommand(o => Edited());
            _modified = new List<object>[3];
            for (int i = 0; i < _modified.Count(); i++)
            {
                _modified[i] = new List<object>();
            }
        }

        #endregion

        #region Fields

        /// <summary>
        /// Message error
        /// </summary>
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        private string _errorMessage = "OK";

        /// <summary>
        /// Rider selected from table
        /// </summary>
        public object SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        private object _selectedItem;

        /// <summary>
        /// Table source
        /// </summary>
        public object Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                OnPropertyChanged(nameof(Collection));
            }
        }
        private object _collection;

        private string _currentRepository = "riders";
        private dynamic _context;
        private readonly List<object>[] _modified;

        #endregion

        #region Commands

        /// <summary>
        /// Occurs when Rider grid is loading
        /// </summary>
        public ICommand LoadCommand { get; }

        /// <summary>
        /// Occurs when user added row
        /// </summary>
        public ICommand AddCommand { get; }

        /// <summary>
        /// Occurs when user deleted row
        /// </summary>
        public ICommand DeleteCommand { get; }

        /// <summary>
        /// Occurs when user click search button
        /// </summary>
        ///public ICommand SearchCommand { get; }

        /// <summary>
        /// Occurs when app is closing
        /// </summary>
        public ICommand OnClosingCommand { get; }

        /// <summary>
        /// Occurs when user click save button
        /// </summary>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// Occurs when user edited cells
        /// </summary>
        public ICommand EditedCommand { get; }
        #endregion

        #region Private Methods

        private void Save()
        {
            var list = CastModifiedList();
            CreateContext();
            try
            {
                _context?.Save(list);
            }
            catch (Exception)
            {
                ErrorMessage = "Can't save table";
            }
            finally
            {
                _context?.Dispose();
            }
        }

        private void OnClosing()
        {
            _context?.Dispose();
        }

        private bool IsSelected(object parameter)
        {
            return SelectedItem != null;
        }

        private void Load()
        {
            CreateContext();
            try
            {
                Collection = _context.GetRecords();
            }
            catch (Exception)
            {
                ErrorMessage = "Can't load table";
            }
            finally
            {
                _context?.Dispose();
            }
        }

        private void Add()
        {
            dynamic item = CreateRecord();
            dynamic table = Collection;
            table.Add(item);
            _modified[0].Add(item);
        }

        private void Delete()
        {
            dynamic item = SelectedItem;
            dynamic table = Collection;
            table.Remove(item);
            _modified[1].Add(item);
        }

        private void Edited()
        {
            if (IfExist(SelectedItem)) return;
            dynamic item = SelectedItem;
            _modified[2].Add(item);
        }

        private bool IfExist(object item)
        {
            return _modified[0].Find(rider => rider == item) != null ||
                   _modified[1].Find(rider => rider == item) != null ||
                   _modified[2].Find(rider => rider == item) != null;
        }

        private void CreateContext()
        {
            switch (_currentRepository.ToLower())
            {
                case "riders":
                    _context = new RiderRepository();
                    break;

                default:
                    ErrorMessage = "Invalid repository.";
                    break;
            }
        }

        private dynamic CreateRecord()
        {
            dynamic record = null;
            switch (_currentRepository.ToLower())
            {
                case "riders":
                    record = new Rider();
                    break;

                default:
                    ErrorMessage = "Invalid type.";
                    break;
            }
            return record;
        }

        private dynamic CastModifiedList()
        {
            dynamic list = null;
            switch (_currentRepository.ToLower())
            {
                case "riders":
                    list = new List<Rider>[3];
                    for (int i = 0; i < _modified.Count(); i++)
                    {
                        list[i] = _modified[i].Select(o => (Rider)o).ToList();
                    }
                    break;

                default:
                    ErrorMessage = "Invalid type.";
                    break;
            }
            return list;
        }

        #endregion
    }
}
