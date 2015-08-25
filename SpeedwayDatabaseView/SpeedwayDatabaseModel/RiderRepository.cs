using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SpeedwayDAL;

namespace SpeedwayDatabaseModel
{
    public class RiderRepository : BaseRepository, IRepository<Rider>
    {
        private readonly DbSet<Rider> _riders;
        private readonly List<SqlParameter> _params = new List<SqlParameter>();
        private readonly StringBuilder _query = new StringBuilder();

        public RiderRepository()
        {
            _riders = Context.Riders;
        }

        public void Add(Rider record)
        {
            _riders.Add(record);
        }

        public void Delete(Rider record)
        {
            _riders.Remove(record);
        }

        public void Update(Rider record)
        {
            _riders.Attach(record);
            var entry = Context.Entry(record);
            entry.State = EntityState.Modified;
        }

        public RiderRepository ById(int id, bool flag)
        {
            if (!flag) return this;
            AddAndOperator();
            _query.Append(" id = @id");
            _params.Add(new SqlParameter("@id", id));
            return this;
        }

        public RiderRepository ByFirstName(string firstName, bool flag)
        {
            if (!flag) return this;
            AddAndOperator();
            _query.Append(" firstName = @firstName");
            _params.Add(new SqlParameter("@firstName", firstName));
            return this;
        }

        public RiderRepository ByLastName(string lastName, bool flag)
        {
            if (!flag) return this;
            AddAndOperator();
            _query.Append(" lastName = @lastName");
            _params.Add(new SqlParameter("@lastName", lastName));
            return this;
        }

        public RiderRepository ByCountry(string country, bool flag)
        {
            if (!flag) return this;
            AddAndOperator();
            _query.Append(" country = @country");
            _params.Add(new SqlParameter("@country", country));
            return this;
        }

        public RiderRepository ByBirthDate(DateTime birthDate, bool flag)
        {
            if (!flag) return this;
            AddAndOperator();
            _query.Append(" birthDate = @birthDate");
            _params.Add(new SqlParameter("@birthDate", birthDate.ToShortDateString()));
            return this;
        }

        public RiderRepository ByTeamId(int? teamId, bool flag)
        {
            if (!flag) return this;
            AddAndOperator();
            _query.Append(" teamId = @teamId");
            _params.Add(new SqlParameter("@teamId", teamId));
            return this;
        }

        public IEnumerable<Rider> GetRecords()
        {
            var query = $"SELECT * FROM riders WHERE{_query};";
            var anwser = _riders.SqlQuery(query, _params).ToList();
            _query.Clear();
            _params.Clear();
            return anwser;
        }

        private bool QueryIsNotNull() => _query.Length > 0;

        private void AddAndOperator()
        {
            if (QueryIsNotNull()) _query.Append(" AND");
        }
    }
}
