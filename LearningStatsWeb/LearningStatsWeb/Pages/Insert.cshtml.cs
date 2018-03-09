using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningStatsWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearningStatsWeb.Pages
{
    public class InsertModel : PageModel
    {
        public void OnGet()
        {
            MLPerfService service = HttpContext.RequestServices.GetService(typeof(MLPerfService)) as MLPerfService;

            int step = int.Parse(HttpContext.Request.Query["step"].First());
            float value = float.Parse(HttpContext.Request.Query["value"].First());
            string sessionName = (string)HttpContext.Request.Query["sessionname"].First(); 

            service.InsertValue(step, value, DateTime.Now, sessionName);
        }
    }
}