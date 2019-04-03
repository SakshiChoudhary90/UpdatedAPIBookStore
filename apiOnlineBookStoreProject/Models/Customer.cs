using coreBookStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace apiOnlineBookStoreProject.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string BillingAddress { get; set; }
        public long ZipCode { get; set; }
        public long Contact { get; set; }
        public bool ShippingAddress { get; set; }
        public bool SaveInformation { get; set; }
        public string PaymentType { get; set; }
        public List<Order> Orders { get; set; }
         


    }
}
