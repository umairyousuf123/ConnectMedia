using ConnectMedia.Common.Model;
using System.Collections.Generic;

namespace ConnectMedia.Common.IRepositories
{
    public interface ICategoryRepository
    {
        List<Category> CategoryList(int userId);
        Category CategoryDetailById(int id);
        string AddEditCategory(Category category);
        string Delete(int Id, int userId);
    }
}
