using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.DAL;

public class RoleModel
{
    [Key]
    public byte RoleId { get; set; }
    public string Role { get; set; }
    public DateTime CreatedDate { get; set; }
    public int CreatedBy { get; set; }
}
