using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningStatsWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearningStatsWeb.Pages
{
    public class PickCompareModel : PageModel
    {
        public List<string> SessionNames { get; set; }

        public void OnGet()
        {
            MLPerfService service = HttpContext.RequestServices.GetService(typeof(MLPerfService)) as MLPerfService;

            SessionNames = service.GetSessionNames();
        }

        public IActionResult OnPost()
        {
            string selectA = Request.Form["selectA"];
            string selectB = Request.Form["selectB"];

            return new RedirectToPageResult("DoCompare", new { selectA = selectA, selectB = selectB }); 
            //Response.Redirect($"DoCompare?selectA={selectA}&selectB={selectB}"); 
        }
    }
}