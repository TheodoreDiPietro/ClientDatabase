using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp4
{
    public class Customer
    {
        public int Id { get; set; }

        [Required] [MaxLength(20)] public string FirstName { get; set; }

        [Required] [MaxLength(20)] public string LastName { get; set; }

        public int ShippingAddress { get; set; }

        //[ForeignKey("ShippingAddress")]
        [Required]
        public Address SpAddress { get; set; }

        public int BillingAddress { get; set; }

        //[ForeignKey("BillingAddress")]
        [Required]
        public Address BlAddress { get; set; }
    }
}