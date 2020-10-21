using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(Product product)
        {
            var objFromDB = _db.Product.FirstOrDefault(s => s.Id == product.Id);
            objFromDB.Name = product.Name;
            objFromDB.ArticleNo = product.ArticleNo;
            objFromDB.ManufactureCode = product.ManufactureCode;
            objFromDB.Barcode = product.Barcode;
            objFromDB.Weight = product.Weight;
            objFromDB.Description = product.Description;
            objFromDB.ImageUrl = product.ImageUrl;
            objFromDB.Quantity = product.Quantity;
            objFromDB.B2CPrice = product.B2CPrice;
            objFromDB.B2BPrice = product.B2BPrice;
            objFromDB.PremiumPrice = product.PremiumPrice;
            objFromDB.DistributorPrice = product.DistributorPrice;
            objFromDB.PurchasePrice = product.PurchasePrice;
            objFromDB.PurchasePriceA = product.PurchasePriceA;
            objFromDB.PurchasePriceB = product.PurchasePriceB;
            objFromDB.PurchasePriceC = product.PurchasePriceC;
            objFromDB.Note = product.Note;
            objFromDB.StockLimit = product.StockLimit;
            objFromDB.ReorderQuantity = product.ReorderQuantity;
            objFromDB.CategoryId = product.CategoryId;
            objFromDB.FrequencyId = product.FrequencyId;
            _db.SaveChanges();
        }
    }

}
