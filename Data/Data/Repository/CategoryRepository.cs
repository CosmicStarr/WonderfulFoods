﻿using Data.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WonderfulFoods.Data.Data;

namespace Data.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetListforDropDown()
        {
            return _db.GetCategories.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            });
        }

        public void Update(Category category)
        {
            var objFromDb = _db.GetCategories.FirstOrDefault(g => g.Id == category.Id);
            objFromDb.Name = category.Name;
            objFromDb.DisplayOrder = category.DisplayOrder;
            _db.SaveChanges();
        }
    }
}
