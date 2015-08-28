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
    public class RiderRepository : BaseRepository
    {
        #region Fields

        private readonly DbSet<Rider> _riders;
        private readonly List<SqlParameter> _params = new List<SqlParameter>();
        private readonly StringBuilder _query = new StringBuilder();

        #endregion

        #region Constructors

        public RiderRepository()
        {
            _riders = Context.Riders;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds record to local table
        /// </summary>
        /// <param name="record">Record to add</param>
        public override void Add(object record)
        {
            Rider rider;
            TryCast(record, out rider);
            _riders.Add(rider);
        }

        /// <summary>
        /// Delete record from local table
        /// </summary>
        /// <param name="record">Record to delete</param>
        public override void Delete(object record)
        {
            Rider rider;
            TryCast(record, out rider);
            _riders.Remove(rider);
        }

        /// <summary>
        /// Update record into local table
        /// </summary>
        /// <param name="record">Record to update</param>
        public override void Update(object record)
        {
            Rider rider;
            TryCast(record, out rider);
            _riders.Attach(rider);
            var entry = Context.Entry(rider);
            entry.State = EntityState.Modified;
        }

        /// <summary>
        /// Return collection of records that comply with specifed parameters
        /// </summary>
        /// <returns>Collection of records</returns>
        public override IEnumerable<object> GetRecords()
        {
            var query = $"SELECT * FROM riders{_query};";
            var anwser = _riders.SqlQuery(query, _params.Count == 0 ? null : _params).ToList();
            _query.Clear();
            _params.Clear();
            return anwser;
        }

        /// <summary>
        /// Add ID parameter to query
        /// </summary>
        /// <param name="id">Parameter</param>
        /// <param name="flag">Allow to add parameter</param>
        /// <returns>Current instance</returns>
        public RiderRepository ById(int id, bool flag)
        {
            if (!flag) return this;
            AddAndOperator();
            _query.Append(" id = @id");
            _params.Add(new SqlParameter("@id", id));
            return this;
        }

        /// <summary>
        /// Add first name parameter to query
        /// </summary>
        /// <param name="firstName">Parameter</param>
        /// <param name="flag">Allow to add parameter</param>
        /// <returns>Current instance</returns>
        public RiderRepository ByFirstName(string firstName, bool flag)
        {
            if (!flag) return this;
            AddAndOperator();
            _query.Append(" firstName = @firstName");
            _params.Add(new SqlParameter("@firstName", firstName));
            return this;
        }

        /// <summary>
        /// Add last name parameter to query
        /// </summary>
        /// <param name="lastName">Parameter</param>
        /// <param name="flag">Allow to add parameter</param>
        /// <returns>Current instance</returns>
        public RiderRepository ByLastName(string lastName, bool flag)
        {
            if (!flag) return this;
            AddAndOperator();
            _query.Append(" lastName = @lastName");
            _params.Add(new SqlParameter("@lastName", lastName));
            return this;
        }

        /// <summary>
        /// Add country parameter to query
        /// </summary>
        /// <param name="country">Parameter</param>
        /// <param name="flag">Allow to add parameter</param>
        /// <returns>Current instance</returns>
        public RiderRepository ByCountry(string country, bool flag)
        {
            if (!flag) return this;
            AddAndOperator();
            _query.Append(" country = @country");
            _params.Add(new SqlParameter("@country", country));
            return this;
        }

        /// <summary>
        /// Add birth date parameter to query
        /// </summary>
        /// <param name="birthDate">Parameter</param>
        /// <param name="flag">Allow to add parameter</param>
        /// <returns>Current instance</returns>
        public RiderRepository ByBirthDate(DateTime birthDate, bool flag)
        {
            if (!flag) return this;
            AddAndOperator();
            _query.Append(" birthDate = @birthDate");
            _params.Add(new SqlParameter("@birthDate", birthDate.ToShortDateString()));
            return this;
        }

        /// <summary>
        /// Add team ID parameter to query
        /// </summary>
        /// <param name="teamId">Parameter</param>
        /// <param name="flag">Allow to add parameter</param>
        /// <returns>Current instance</returns>
        public RiderRepository ByTeamId(int? teamId, bool flag)
        {
            if (!flag) return this;
            AddAndOperator();
            _query.Append(" teamId = @teamId");
            _params.Add(new SqlParameter("@teamId", teamId));
            return this;
        }

        #endregion

        #region Private Methods

        private bool QueryIsNotNull() => _query.Length > 0;

        private void AddAndOperator()
        {
            _query.Append(QueryIsNotNull() ? " AND" : " WHERE");
        }

        private static void TryCast(object record, out Rider rider)
        {
            rider = record as Rider;
            if (rider == null) throw new InvalidCastException("Can't cast object to Rider.");
        }

        #endregion
    }
}
