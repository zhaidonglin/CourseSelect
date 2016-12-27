using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSelecting.Education.Dto
{
    public class GetCreditsInput
    {

        public int PageSize { get; set; }

        public int Start { get; set; }
        public string KeyWord { get; set; }
    }
}
