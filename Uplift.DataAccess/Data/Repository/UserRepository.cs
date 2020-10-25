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
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void LockUser(string userId)
        {
            var UserFromDb = _db.ApplicationUser.FirstOrDefault(u => u.Id == userId);
            UserFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            _db.SaveChanges();
        }

        public void UnLockUser(string userId)
        {
            var UserFromDb = _db.ApplicationUser.FirstOrDefault(u => u.Id == userId);
            UserFromDb.LockoutEnd = DateTime.Now;
            _db.SaveChanges();
        }

    }

}
