using Microsoft.AspNetCore.Mvc;

namespace YJWebCoreMVC.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IMenuService _menuService;

        public MenuViewComponent(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menu = _menuService.GenerateMenu();
            return View(menu);
        }

    }
}
