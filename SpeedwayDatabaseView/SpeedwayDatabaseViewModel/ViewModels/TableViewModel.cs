using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using SpeedwayDatabaseModel;
using SpeedwayDatabaseViewModel.Commands;
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
            SearchCommand = new RelayCommand(o => Search());
            OnClosingCommand = new RelayCommand(o => OnClosing());
            SaveCommand = new RelayCommand(o => Save());

            CreateContext();
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

        public dynamic Context { get; private set; }

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
        public ICommand SearchCommand { get; }

        /// <summary>
        /// Occurs when app is closing
        /// </summary>
        public ICommand OnClosingCommand { get; }

        /// <summary>
        /// Occurs when user click save button
        /// </summary>
        public ICommand SaveCommand { get; }
        #endregion

        #region Methods

        public SearchViewModel CreateSearchViewModel() => new SearchViewModel(this);

        private void Save()
        {
            Context?.Save();
        }

        private void OnClosing()
        {
            Context?.Dispose();
        }

        private void Search()
        {
            //TODO Implement
        }

        private bool IsSelected(object parameter)
        {
            return SelectedItem != null;
        }

        private void Load()
        {
            try
            {
                Collection = Context.GetLocal();
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }

        private void Add()
        {
            dynamic item = CreateRecord();
            dynamic table = Collection;
            table.Add(item);
        }

        private void Delete()
        {
            dynamic item = SelectedItem;
            dynamic table = Collection;
            table.Remove(item);
        }

        private void CreateContext()
        {
            switch (_currentRepository.ToLower())
            {
                case "riders":
                    Context = new RiderRepository();
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

        #endregion
    }
}
