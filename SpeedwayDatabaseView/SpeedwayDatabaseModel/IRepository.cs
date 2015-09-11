using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedwayDatabaseModel
{
    interface IRepository<T> where T: class 
    {
        IEnumerable<T> GetRecords();
        ObservableCollection<T> GetLocal();
    }
}
