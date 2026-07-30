using myshop.BLL.DTO;

namespace myshop.MVC.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<CategoryDto> Categories { set; get; }
        public IEnumerable<ProductDto> Products { set; get; }

    }
}
