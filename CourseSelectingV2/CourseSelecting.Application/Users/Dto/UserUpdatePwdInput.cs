using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSelecting.Users.Dto
{
    public class UserUpdatePwdInput
    {
        public string Password { get; set; }

        public string NewPassword { get; set; }

        public string TNewPassword { get; set; }
    }
}
