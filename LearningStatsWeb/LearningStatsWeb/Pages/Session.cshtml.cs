using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningStatsWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearningStatsWeb.Pages
{
    public class SessionModel : PageModel
    {
        public List<float> SessionValues { get; set; }

        public string SessionName { get; set; }

        public void OnGet()
        {
            string sessionName = HttpContext.Request.Query["sessionname"];

            MLPerfService service = HttpContext.RequestServices.GetService(typeof(MLPerfService)) as MLPerfService;

            SessionValues = service.GetValues(sessionName);
            SessionName = sessionName;
        }
    }
}