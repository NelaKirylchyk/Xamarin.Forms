using System;
using EpamVTSClient.Core.Enums;
using SQLite;

namespace EpamVTSClient.DAL.Models.DTOModels
{
    public class VacationDTO
    {
        [PrimaryKey]
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int EmployeeId { get; set; }

        public VacationStatus Status { get; set; }

        public VacationType Type { get; set; }
    }
}
