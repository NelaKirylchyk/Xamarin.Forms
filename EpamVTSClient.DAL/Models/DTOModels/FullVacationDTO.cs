using System;
using EpamVTSClient.Core.Enums;

namespace EpamVTSClient.DAL.Models.DTOModels
{
    public class FullVacationDTO
    {
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

        public string VacationForm { get; set; }

        public void Update(VacationInfo vacationNewInfo)
        {
            this.StartDate = vacationNewInfo.StartDate;
            this.EndDate = vacationNewInfo.EndDate;
            this.EmployeeId = vacationNewInfo.EmployeeId;
            this.ApproverId = vacationNewInfo.ApproverId;
            this.NoProjectManagerObjections = vacationNewInfo.NoProjectManagerObjections;
            this.Comment = vacationNewInfo.Comment;
            this.ConfirmationDocumentAvailable = vacationNewInfo.ConfirmationDocumentAvailable;
            this.ProcessInstanceId = vacationNewInfo.ProcessInstanceId;
            this.Status = vacationNewInfo.Status;
            this.Type = vacationNewInfo.Type;
            this.VacationForm = (string) vacationNewInfo.VacationForm;
        }
    }
}
