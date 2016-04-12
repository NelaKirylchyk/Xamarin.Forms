using System;
using EpamVTSClient.Core.Enums;

namespace VtsMockClient.Domain.Models
{
    public class ShortVacationInfo
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ApproverFullName { get; set; }

        public VacationStatus Status { get; set; }

        public VacationType Type { get; set; }
    }
}
