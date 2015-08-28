using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedwayDatabaseModel
{
    public interface IRepository
    {
        void Add(object record);
        void Delete(object record);
        void Update(object record);
        IEnumerable<object> GetRecords();
    }
}
