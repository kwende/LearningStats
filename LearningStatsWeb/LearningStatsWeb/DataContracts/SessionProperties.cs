using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningStatsWeb.DataContracts
{
    public class SessionProperties
    {
        public int SessionPropertiesId { get; set; }
        public string SessionName { get; set; }
        public string GitHash { get; set; }
        public string Notes { get; set; }
    }
}
