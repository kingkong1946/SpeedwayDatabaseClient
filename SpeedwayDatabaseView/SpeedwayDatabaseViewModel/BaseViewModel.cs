﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SpeedwayDatabaseViewModel.Annotations;

namespace SpeedwayDatabaseViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Number of merge columns for ToolBar and DataGrid
        /// </summary>
        private const int _MergeAll = 3;

        public int MergeAll => _MergeAll;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
