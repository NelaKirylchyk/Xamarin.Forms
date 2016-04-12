using System;
using EpamVTSClient.Core.Enums;

namespace EpamVTSClient.BLL.ViewModels
{
    public class VacationViewModel
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ApproverFullName { get; set; }

        public VacationStatus Status { get; set; }

        public VacationType Type { get; set; }
    }
}
