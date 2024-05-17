using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.DAL.Model
{
    public class CityModel
    {
        [Key]
        public int CityId { get; set; }
        public string City { get; set; }
        public byte StateId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}