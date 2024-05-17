using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.DAL.Model
{
    public class CategoryTypeModel
    {
        [Key]
        public byte CategoryTypeId { get; set; }
        public string CategoryType { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}