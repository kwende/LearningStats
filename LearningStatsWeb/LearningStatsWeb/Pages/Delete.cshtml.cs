using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningStatsWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearningStatsWeb.Pages
{
    public class DeleteModel : PageModel
    {
        public string SessionToDelete { get; set; }

        public void OnGet()
        {
            SessionToDelete = Request.Query["name"]; 
        }

        public IActionResult OnPost()
        {
            string sessionToDelete = Request.Form["toDelete"]; 

            MLPerfService service = HttpContext.RequestServices.GetService(typeof(MLPerfService)) as MLPerfService;

            service.DeleteSession(sessionToDelete);

            return new RedirectToPageResult("Index");
        }
    }
}