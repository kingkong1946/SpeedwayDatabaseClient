using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeedwayDAL;

namespace SpeedwayDatabaseModel
{
    public class BaseRepository : IDisposable
    {
        protected readonly SpeedwayEntities Context = new SpeedwayEntities();
        protected readonly List<SqlParameter> Params = new List<SqlParameter>();
        protected readonly StringBuilder Query = new StringBuilder();

        public virtual void Save()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        protected bool QueryIsNotNull() => Query.Length > 0;

        protected void AddAndOperator()
        {
            Query.Append(QueryIsNotNull() ? " AND" : " WHERE");
        }
    }
}
