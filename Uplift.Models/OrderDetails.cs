using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Uplift.Models
{
   public class OrderDetails
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderheaderId { get; set; }
        [ForeignKey("OrderheaderId")]
        public OrderHeader OrderHeader { get; set; }
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
