using SQLite;

namespace VtsMockClient.Domain.Models
{
    public class PersonDTO
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string FullName { get; set; }

        public float VacationDays { get; set; }

        public float SickDays { get; set; }

        public float Overtime { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}