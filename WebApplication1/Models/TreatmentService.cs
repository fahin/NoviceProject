using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class TreatmentService
    {
        public int Id { set; get; }
        public string VotersID { set; get; }
        public string Dose { set; get; }
        public string BeforAfter { set; get; }
        public string QtyGiven { set; get; }
        public string Note { set; get; }
    }
}