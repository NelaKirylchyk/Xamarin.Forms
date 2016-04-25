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

        public object VacationForm { get; set; }

        public void Update(FullVacationDTO newFullVacationDto)
        {
            StartDate = newFullVacationDto.StartDate;
            EndDate = newFullVacationDto.EndDate;
            EmployeeId = newFullVacationDto.EmployeeId;
            ApproverId = newFullVacationDto.ApproverId;
            NoProjectManagerObjections = newFullVacationDto.NoProjectManagerObjections;
            Comment = newFullVacationDto.Comment;
            ConfirmationDocumentAvailable = newFullVacationDto.ConfirmationDocumentAvailable;
            ProcessInstanceId = newFullVacationDto.ProcessInstanceId;
            Status = newFullVacationDto.Status;
            Type = newFullVacationDto.Type;
            VacationForm = newFullVacationDto.VacationForm;
        }
    }
}
