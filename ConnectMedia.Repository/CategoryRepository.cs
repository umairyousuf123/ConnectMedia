using ConnectMedia.Common.Database;
using ConnectMedia.Common.IRepositories;
using ConnectMedia.Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectMedia.Repository
{
    public class CategoryRepository: ICategoryRepository
    {
        ConnectMediaDB _db;
        readonly ILogger<CategoryRepository> _logger;
        public CategoryRepository(ILogger<CategoryRepository> logger, ConnectMediaDB db)
        {
            this._db = db;
            this._logger = logger;
        }
        public List<Category> CategoryList(int UserId)
        {
            List<Category> buildings = _db.Category.Where(x => x.IsDel == false).OrderByDescending(x => x.Id).ToList();
            return buildings;
        }
        public Category CategoryDetailById(int Id)
        {
            Category building = _db.Category.SingleOrDefault(b => b.Id == Id && b.IsDel == false);
            return building;
        }
        public string AddEditCategory(Category category)
        {
            string ErrorMessage = string.Empty;
            try
            {
                if (category.Id > 0)
                {
                    _db.Category.Attach(category);
                    _db.Entry(category).State = EntityState.Modified;
                }
                else
                {
                    _db.Category.Add(category);
                }
                int Success = _db.SaveChanges();
                ErrorMessage = (Success > 0) ? "Building added sucessfully " : "Building not added";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
                _logger.LogError(ErrorMessage);

            }
            return ErrorMessage;
        }
        public string Delete(int Id, int CurrentUserId)
        {
            string ErrorMessage = string.Empty;
            Category category = new Category();
            try
            {
                category = _db.Category.SingleOrDefault(x => x.Id == Id && x.IsDel == false);
                category.IsDel = true;
                category.UpdatedOn = DateTime.UtcNow;
                category.UpdatedBy = CurrentUserId;
                _db.Category.Attach(category);
                _db.Entry(category).State = EntityState.Modified;
                int Success = _db.SaveChanges();
                ErrorMessage = (Success > 0) ? "Building deleted sucessfully " : "Building not deleted";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
            }
            return ErrorMessage;
        }
    }
}