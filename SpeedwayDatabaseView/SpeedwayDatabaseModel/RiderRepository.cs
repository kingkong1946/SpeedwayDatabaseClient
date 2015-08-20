using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedwayDatabaseModel
{
    public class RiderRepository
    {
        public RiderRepository(int id, string firstName, string lastName, string country, int? teamId, string birthDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            TeamId = teamId;
            BirthDate = birthDate;
        }

        public RiderRepository() : this(0, string.Empty, string.Empty, string.Empty, null, string.Empty)
        {
        }

        public int Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public int? TeamId { get; set; }

        private DateTime _birthDate;
        public string BirthDate
        {
            get { return _birthDate.ToShortDateString(); }
            set
            {
                DateTime.TryParseExact(value, _dateFormat, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal,
                    out _birthDate);
            }
        }

        private readonly string[] _dateFormat = { "dd/MM/yyyy", "dd.MM.yyyy", "dd-MM-yyyy" };
    }
}
