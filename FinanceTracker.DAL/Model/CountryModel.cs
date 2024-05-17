using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.DAL.Model
{
    public class CountryModel
    {
        [Key]
        public byte CountryId { get; set; }
        public string Country { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}