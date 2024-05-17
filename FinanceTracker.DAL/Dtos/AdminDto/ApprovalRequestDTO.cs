using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceTracker.DAL.DTO
{
    public class ApprovalRequestDto
    {
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public byte UserStatusId { get; set; }
        public string EmailId { get; set; }
        public string? RejectionReason { get; set; }
    }
}