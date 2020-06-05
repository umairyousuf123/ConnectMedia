using ConnectMedia.Common.Model;
using System.Collections.Generic;

namespace ConnectMedia.Common.IServices
{
    public interface ICategoryService
    {
        List<Category> CategoryList(int userId);
        Category CategoryDetailById(int id);
        string AddEditCategory(Category category);
        string Delete(int Id, int userId);
    }
}
