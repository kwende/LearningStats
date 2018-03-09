using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningStatsWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearningStatsWeb.Pages
{
    public class IndexModel : PageModel
    {
        public List<string> SessionNames { get; set; }

        public void OnGet()
        {
            MLPerfService service = HttpContext.RequestServices.GetService(typeof(MLPerfService)) as MLPerfService;

            SessionNames = service.GetSessionNames();

            return;
        }
    }
}
