using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeedwayDAL;

namespace SpeedwayDatabaseModel
{
    public class BaseRepository : IDisposable
    {
        protected readonly SpeedwayEntities Context = new SpeedwayEntities();

        public virtual void Save()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
