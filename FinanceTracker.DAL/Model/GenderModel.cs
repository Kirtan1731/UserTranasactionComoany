using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.DAL.Model
{
    public class GenderModel
    {
        [Key]
        public byte GenderId { get; set; }
        public string Gender { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}