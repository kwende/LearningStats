using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningStatsWeb.DataContracts
{
    public class Status
    {
        public int ID { get; set; }
        public double Value { get; set; }
        public int Step { get; set; }
        public DateTime InsertedOn { get; set; }
        public string SessionName { get; set; }
    }
}
