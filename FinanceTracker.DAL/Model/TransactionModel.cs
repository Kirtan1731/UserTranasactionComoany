using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.DAL.Model
{
    public class TransactionModel
    {
        [Key]
        public int TransactionId { get; set; }
        public short CategoryId { get; set; }
        public string? Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

    }
}