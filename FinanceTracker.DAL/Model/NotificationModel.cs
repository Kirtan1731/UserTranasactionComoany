using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.DAL.Model
{
    public class NotificationModel
    {
        [Key]
        public int NotificationId { get; set; }
        public byte NotificationContentId { get; set; }
        public bool IsRead { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

    
    }
}