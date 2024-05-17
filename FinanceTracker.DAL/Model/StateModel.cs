using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.DAL.Model
{
    public class StateModel
    {
        [Key]
        public byte StateId { get; set; }
        public string State { get; set; }
        public byte CountryId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}