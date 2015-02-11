using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Voter
    {
        public string VoterID { set; get; }
        public string VoterName { set; get; }
        public string VoterAddress { set; get; }
        public string VoterDob { set; get; }
        public int ServiceCount { set; get; }
    }
}