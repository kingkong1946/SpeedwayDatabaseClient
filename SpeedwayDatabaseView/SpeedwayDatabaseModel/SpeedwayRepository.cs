using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedwayDatabaseModel
{
    public class SpeedwayRepository<T> : BaseRepository
    {
        public T Table { get; }

        public SpeedwayRepository(T table)
        {
            if (table == null) throw new NullReferenceException("Table can't be null");
            Table = table;
        }

        
    }
}
