using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.DAL.Model
{
    public class CategoryModel
    {
        [Key]
        public short CategoryId { get; set; }
        public string Category { get; set; }
        public byte CategoryTypeId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}