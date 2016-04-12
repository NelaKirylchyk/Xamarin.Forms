using System;
using System.Linq;
using EpamVTSClient.DAL.Models;
using SQLite;
using VtsMockClient.Domain.Models;

namespace EpamVTSClient.DAL.Services.OfflineService
{
    public class LoginOfflineDbService : ILoginOfflineDBService
    {
        private readonly SQLiteConnection _connection;
        //public LoginOfflineDbService(IConnectionManager connectionManager)
        //{
        //    _connection = connectionManager.GetSqLiteAsyncConnection();
        //}

        public LoginOfflineDbService(SQLiteConnection connection)
        {
            _connection = connection;
        }

        private PersonDTO GetUserOffline(string username, string password)
        {
            try
            {
                var user = _connection.Table<PersonDTO>().FirstOrDefault(r => r.Password == password && r.Email == username);
                if (user != null)
                {
                    return user;
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        public Person SignInIfExist(string userName, string password)
        {
            PersonDTO person = GetUserOffline(userName, password);
            return person != null ? new Person()
            {
                Id = person.Id,
                Credentials = new PersonCredentials()
                {
                    Password = person.Password,
                    Email = person.Email
                },
                SickDays = person.SickDays,
                VacationDays = person.VacationDays,
                Overtime = person.Overtime
            } : null;
        }

        public void SaveUserIfNotExist(Person person)
        {
            try
            {
                PersonDTO personDto = new PersonDTO()
                {
                    Password = person.Credentials.Password,
                    Id = person.Id,
                    Email = person.Credentials.Email,
                    FullName = person.FullName,
                    Overtime = person.Overtime,
                    SickDays = person.SickDays,
                    VacationDays = person.VacationDays
                };
                PersonDTO user = _connection.Table<PersonDTO>().FirstOrDefault(r => r.Id == personDto.Id);
                if (user == null)
                {
                    _connection.Insert(personDto);
                }
            }
            catch (Exception e)
            {

            }
        }
    }

    public interface ILoginOfflineDBService
    {
        Person SignInIfExist(string userName, string password);
        void SaveUserIfNotExist(Person person);
    }
}
