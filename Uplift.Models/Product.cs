using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Uplift.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Article No")]
        public string ArticleNo { get; set; }
        [Display(Name = "Manufacture Code")]
        public string ManufactureCode { get; set; }
        [Display(Name = "Barcode")]
        public string Barcode { get; set; }
        public double Weight { get; set; }
        public string Description { get; set; }
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        [Required]
        [Display(Name = "B2C Price")]
        public double B2CPrice { get; set; }
        [Display(Name = "B2B Price")]
        public double B2BPrice { get; set; }
        [Display(Name = "Premium Price")]
        public double PremiumPrice { get; set; }
        [Display(Name = "Distributor Price")]
        public double DistributorPrice { get; set; }
        [Required]
        [Display(Name = "Purchase Price")]
        public double PurchasePrice { get; set; }
        [Display(Name = "Polo Price")]
        public double PurchasePriceA { get; set; }
        [Display(Name = "Thomas Price")]
        public double PurchasePriceB { get; set; }
        [Display(Name = "New Supplier Price")]
        public double PurchasePriceC { get; set; }
        public string Note { get; set; }
        [Display(Name = "Stock Limit")]
        public int StockLimit { get; set; }
        [Display(Name = "Min Reorder Quantity ")]
        public int ReorderQuantity { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public int FrequencyId { get; set; }
        [ForeignKey("FrequencyId")]
        public Frequency Frequency { get; set; }
    }
}
