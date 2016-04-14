﻿using System.Collections.Generic;
using System.Threading.Tasks;
using EpamVTSClient.DAL.Models;

namespace EpamVTSClient.DAL.Services
{
    public interface IVacationListWebService
    {
        Task<IEnumerable<ShortVacationInfo>> GetShortVacationsAsync(int userId);
    }
}