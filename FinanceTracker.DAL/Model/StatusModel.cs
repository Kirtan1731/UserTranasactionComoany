using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.DAL;

public class StatusModel
{
    [Key]
    public byte UserStatusId { get; set; }
    public string UserStatus { get; set; }
    public DateTime CreatedDate { get; set; }
    public int CreatedBy { get; set; }
}
