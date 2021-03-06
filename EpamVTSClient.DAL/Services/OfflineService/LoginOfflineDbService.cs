﻿using System;
using System.Threading.Tasks;
using EpamVTSClient.Core.Services;
using EpamVTSClient.DAL.Models;
using EpamVTSClient.DAL.Models.DTOModels;
using SQLite;

namespace EpamVTSClient.DAL.Services.OfflineService
{
    public class LoginOfflineDbService : ILoginOfflineDBService
    {
        private readonly SQLiteAsyncConnection _connection;
        private readonly IMessageDialogService _messageDialogService;

        public LoginOfflineDbService(SQLiteAsyncConnection connection, IMessageDialogService messageDialogService)
        {
            _connection = connection;
            _messageDialogService = messageDialogService;
        }

        private async Task<PersonDTO> GetUserOffline(string username, string password)
        {
            PersonDTO personDto = null;
            try
            {
                personDto = await _connection.Table<PersonDTO>()
                            .Where(r => r.Password == password && r.Email == username)
                            .FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                await _messageDialogService.ShowMessageDialogAsync(e.Message);
            }
            return personDto;
        }

        public async Task<Person> SignInAsync(string userName, string password)
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

        public async Task SaveUserOfflineAsync(Person person)
        {
            try
            {
                PersonDTO newPersonDto = new PersonDTO()
                {
                    Password = person.Credentials.Password,
                    Id = person.Id,
                    Email = person.Credentials.Email,
                    FullName = person.FullName,
                    Overtime = person.Overtime,
                    SickDays = person.SickDays,
                    VacationDays = person.VacationDays
                };
                PersonDTO oldPersonDto = await _connection.Table<PersonDTO>().Where(r => r.Id == newPersonDto.Id).FirstOrDefaultAsync();
                if (oldPersonDto != null)
                {
                    await _connection.DeleteAsync(oldPersonDto);
                    await _connection.InsertAsync(newPersonDto);
                }
            }
            catch (Exception e)
            {
                await _messageDialogService.ShowMessageDialogAsync(e.Message);
            }
        }
    }
}
