using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.DAL.Model
{
    public class NotificationContentModel
    {
        [Key]
        public byte NotificationContentId { get; set; }
        public string NotificationContent { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}