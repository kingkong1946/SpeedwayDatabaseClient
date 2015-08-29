using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeedwayDAL;

namespace SpeedwayDatabaseModel
{
    public abstract class BaseRepository : IDisposable
    {
        protected SpeedwayEntities Context;

        public void Start()
        {
            Context = new SpeedwayEntities();
        }

        public void Upload()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
            Context = null;
        }

        public void Close()
        {
            Dispose();
        }

        public abstract void Add(object record);
        public abstract void Delete(object record);
        public abstract void Update(object record);
        public abstract IEnumerable<object> GetRecords();
    }
}
