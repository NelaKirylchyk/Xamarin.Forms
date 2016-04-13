using System;
using System.Linq;
using System.Threading.Tasks;
using EpamVTSClient.DAL.Models;
using EpamVTSClient.DAL.Models.DTOModels;
using SQLite;

namespace EpamVTSClient.DAL.Services.OfflineService
{
    public class LoginOfflineDbService : ILoginOfflineDBService
    {
        private readonly SQLiteAsyncConnection _connection;

        public LoginOfflineDbService(SQLiteAsyncConnection connection)
        {
            _connection = connection;
        }

        private async Task<PersonDTO> GetUserOffline(string username, string password)
        {
            try
            {
                var user = await _connection.Table<PersonDTO>().Where(r => r.Password == password && r.Email == username).FirstOrDefaultAsync();
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

        public async Task<Person> SignInIfExist(string userName, string password)
        {
            var person = await GetUserOffline(userName, password);
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

        public async Task SaveUserIfNotExist(Person person)
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
                PersonDTO user = await _connection.Table<PersonDTO>().Where(r => r.Id == personDto.Id).FirstOrDefaultAsync();
                if (user == null)
                {
                    await _connection.InsertAsync(personDto);
                }
            }
            catch (Exception e)
            {

            }
        }
    }

    public interface ILoginOfflineDBService
    {
        Task<Person> SignInIfExist(string userName, string password);
        Task SaveUserIfNotExist(Person person);
    }
}
