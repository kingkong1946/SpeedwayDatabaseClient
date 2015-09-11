using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public RiderRepository()
        {
            _riders = Context.Riders;
        }

        public RiderRepository ById(int id, bool flag)
        {
            if (!flag) return this;
            AddAndOperator();
            Query.Append(" id = @id");
            Params.Add(new SqlParameter("@id", id));
            return this;
        }

        public RiderRepository ByFirstName(string firstName, bool flag)
        {
            if (!flag) return this;
            AddAndOperator();
            Query.Append(" firstName = @firstName");
            Params.Add(new SqlParameter("@firstName", firstName));
            return this;
        }

        public RiderRepository ByLastName(string lastName, bool flag)
        {
            if (!flag) return this;
            AddAndOperator();
            Query.Append(" lastName = @lastName");
            Params.Add(new SqlParameter("@lastName", lastName));
            return this;
        }

        public RiderRepository ByCountry(string country, bool flag)
        {
            if (!flag) return this;
            AddAndOperator();
            Query.Append(" country = @country");
            Params.Add(new SqlParameter("@country", country));
            return this;
        }

        public RiderRepository ByBirthDate(DateTime birthDate, bool flag)
        {
            if (!flag) return this;
            AddAndOperator();
            Query.Append(" birthDate = @birthDate");
            Params.Add(new SqlParameter("@birthDate", birthDate.ToShortDateString()));
            return this;
        }

        public RiderRepository ByTeamId(int? teamId, bool flag)
        {
            if (!flag) return this;
            AddAndOperator();
            Query.Append(" teamId = @teamId");
            Params.Add(new SqlParameter("@teamId", teamId));
            return this;
        }

        public IEnumerable<Rider> GetRecords()
        {
            var query = $"SELECT * FROM riders{Query};";
            var anwser = _riders.SqlQuery(query, Params.Count == 0 ? null : Params).ToList();
            Query.Clear();
            Params.Clear();
            return anwser;
        }

        public ObservableCollection<Rider> GetLocal()
        {
            _riders.Load();
            return _riders.Local;
        }
    }
}
