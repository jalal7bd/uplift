﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository.IRepository
{
   public interface IProductRepository: IRepository<Product>
    {
        void Update(Product product);
    }
}
