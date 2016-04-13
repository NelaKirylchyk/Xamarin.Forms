using System.Runtime.Serialization;

namespace EpamVTSClient.DAL.Models
{
    [DataContract]
    public class Person
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public float VacationDays { get; set; }

        [DataMember]
        public float SickDays { get; set; }

        [DataMember]
        public float Overtime { get; set; }

        public PersonCredentials Credentials { get; set; }
    }
}