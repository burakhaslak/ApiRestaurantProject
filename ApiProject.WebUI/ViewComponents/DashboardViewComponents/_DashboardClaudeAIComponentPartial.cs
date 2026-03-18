using Microsoft.AspNetCore.Mvc;

namespace ApiProject.WebUI.ViewComponents.DashboardViewComponents
{
    public class _DashboardClaudeAIComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
