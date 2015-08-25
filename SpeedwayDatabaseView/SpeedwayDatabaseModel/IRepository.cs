using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedwayDatabaseModel
{
    interface IRepository<T> where T: class 
    {
        void Add(T record);
        void Delete(T record);
        void Update(T record);
        IEnumerable<T> Search();
    }
}
