using System.ComponentModel.DataAnnotations;

namespace HR.API.Models
{
    public class AddressModel
    {
        public int Id { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
