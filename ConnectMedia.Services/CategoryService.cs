using ConnectMedia.Common.IRepositories;
using ConnectMedia.Common.IServices;
using ConnectMedia.Common.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace ConnectMedia.Services
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository _buildingRepository { get; set; }
        IConfiguration _configuration;
        readonly ILogger<CategoryService> _logger;
        public CategoryService(ILogger<CategoryService> logger, ICategoryRepository buildingRepository, IConfiguration configuration)
        {
            _logger = logger;
            this._buildingRepository = buildingRepository;
            this._configuration = configuration;
        }
        public List<Category> CategoryList(int userId)
        {
            List<Category> buildings = _buildingRepository.CategoryList(userId);

            return buildings;
        }
        public Category CategoryDetailById(int id)
        {
            Category building = _buildingRepository.CategoryDetailById(id);
            return building;
        }
        public string AddEditCategory(Category building)
        {
            string message = _buildingRepository.AddEditCategory(building);
            return message;
        }
        public string Delete(int id, int CurrentUserId)
        {
            string message = _buildingRepository.Delete(id, CurrentUserId);
            return message;
        }

    }
}
