using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningStatsWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearningStatsWeb.Pages
{
    public class DoCompareModel : PageModel
    {
        public List<float> SessionAValues { get; set; }
        public List<float> SessionBValues { get; set; }

        public string SessionAName { get; set; }
        public string SessionBName { get; set; }

        public int MaximumLength { get; set; }

        public void OnGet()
        {
            SessionAName = Request.Query["selectA"];
            SessionBName = Request.Query["selectB"];

            MLPerfService service = HttpContext.RequestServices.GetService(typeof(MLPerfService)) as MLPerfService;

            SessionAValues = service.GetSessionValues(SessionAName);
            SessionBValues = service.GetSessionValues(SessionBName); 

            if(SessionAValues.Count > SessionBValues.Count)
            {
                MaximumLength = SessionAValues.Count; 
            }
            else
            {
                MaximumLength = SessionBValues.Count; 
            }
        }
    }
}