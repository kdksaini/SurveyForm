using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataAccess
{
   public class SurveyDetails
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Resume { get; set; }
        public string Education { get; set; }
    }
}
