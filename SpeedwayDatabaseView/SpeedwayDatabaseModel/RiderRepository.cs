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
    public class RiderRepository : BaseRepository<Rider>
    {
        private readonly DbSet<Rider> _riders;

        public RiderRepository()
        {
            _riders = Context.Riders;
        }

        public RiderRepository ById(int id)
        {
            AddAndOperator();
            Query.Append(" id = @id");
            Params.Add(new SqlParameter("@id", id));
            return this;
        }

        public RiderRepository ByFirstName(string firstName)
        {
            AddAndOperator();
            Query.Append(" firstName = @firstName");
            Params.Add(new SqlParameter("@firstName", firstName));
            return this;
        }

        public RiderRepository ByLastName(string lastName)
        {
            AddAndOperator();
            Query.Append(" lastName = @lastName");
            Params.Add(new SqlParameter("@lastName", lastName));
            return this;
        }

        public RiderRepository ByCountry(string country)
        {
            AddAndOperator();
            Query.Append(" country = @country");
            Params.Add(new SqlParameter("@country", country));
            return this;
        }

        public RiderRepository ByBirthDate(DateTime birthDate)
        {
            AddAndOperator();
            Query.Append(" birthDate = @birthDate");
            Params.Add(new SqlParameter("@birthDate", birthDate.ToShortDateString()));
            return this;
        }

        public RiderRepository ByTeamId(int? team_Id)
        {
            AddAndOperator();
            Query.Append(" team_Id = @team_Id");
            Params.Add(new SqlParameter("@team_Id", team_Id));
            return this;
        }

        public override IEnumerable<Rider> GetRecords()
        {
            var query = $"SELECT * FROM riders{Query};";
            var parameters = Params.Count == 0 ? null : Params.ToArray();
            var anwser = parameters == null ? _riders.SqlQuery(query).ToList() : _riders.SqlQuery(query, parameters).ToList();
            Query.Clear();
            Params.Clear();
            return anwser;
        }

        public override void Save(IEnumerable<Rider>[] table)
        {
            foreach (var result in table[0].Select(rider => Context.Entry(rider)))
            {
                result.State = EntityState.Added;
            }
            foreach (var result in table[1].Select(rider => Context.Entry(rider)))
            {
                result.State = EntityState.Deleted;
            }
            foreach (var result in table[2].Select(rider => Context.Entry(rider)))
            {
                result.State = EntityState.Modified;
            }
            base.Save(table);
        }
    }
}
