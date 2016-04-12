using System;
using EpamVTSClient.Core.Enums;
using SQLite;

namespace VtsMockClient.Domain.Models
{
    public class VacationInfo
    {
        [PrimaryKey]
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int EmployeeId { get; set; }

        public int ApproverId { get; set; }

        public bool NoProjectManagerObjections { get; set; }

        public string Comment { get; set; }

        public bool ConfirmationDocumentAvailable { get; set; }

        public string ProcessInstanceId { get; set; }

        public VacationStatus Status { get; set; }

        public VacationType Type { get; set; }
    }
}
