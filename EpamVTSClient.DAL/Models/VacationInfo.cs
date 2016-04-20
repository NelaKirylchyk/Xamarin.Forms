using System;
using EpamVTSClient.Core.Enums;
using EpamVTSClient.DAL.Models.DTOModels;
using SQLite;

namespace EpamVTSClient.DAL.Models
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

        public string VacationForm { get; set; }

        public void Update(FullVacationDTO newFullVacationDto)
        {
            this.StartDate = newFullVacationDto.StartDate;
            this.EndDate = newFullVacationDto.EndDate;
            this.EmployeeId = newFullVacationDto.EmployeeId;
            this.ApproverId = newFullVacationDto.ApproverId;
            this.NoProjectManagerObjections = newFullVacationDto.NoProjectManagerObjections;
            this.Comment = newFullVacationDto.Comment;
            this.ConfirmationDocumentAvailable = newFullVacationDto.ConfirmationDocumentAvailable;
            this.ProcessInstanceId = newFullVacationDto.ProcessInstanceId;
            this.Status = newFullVacationDto.Status;
            this.Type = newFullVacationDto.Type;
            this.VacationForm = newFullVacationDto.VacationForm;
        }
    }
}
