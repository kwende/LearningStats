using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningStatsWeb.DataContracts;
using LearningStatsWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearningStatsWeb.Pages
{
    public class SessionModel : PageModel
    {
        public List<float> SessionValues { get; set; }

        public string SessionName { get; set; }

        public string GitHash { get; set; }

        public string Notes { get; set; }

        public void OnGet()
        {
            string sessionName = HttpContext.Request.Query["sessionname"];

            MLPerfService service = HttpContext.RequestServices.GetService(typeof(MLPerfService)) as MLPerfService;

            SessionValues = service.GetSessionValues(sessionName);
            SessionName = sessionName;

            SessionProperties sessionProperties = service.GetSessionProperties(sessionName);

            if (sessionProperties != null)
            {
                Notes = sessionProperties.Notes;
                GitHash = sessionProperties.GitHash;
            }
        }

        public void OnPost()
        {
            MLPerfService service = HttpContext.RequestServices.GetService(typeof(MLPerfService)) as MLPerfService;

            string notes = Request.Form["notes"];
            string gitHash = Request.Form["gitHash"];
            string sessionName = Request.Form["sessionName"];

            service.InsertMetaData(sessionName, gitHash, notes);

            SessionValues = service.GetSessionValues(sessionName);
            SessionName = sessionName;
            GitHash = gitHash;
            Notes = notes;
        }
    }
}