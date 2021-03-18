using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Models
{
    public class UserModel
    {
        public String UserName { get; set; }

        public String Password { get; set; }

        public String PasswordConfirm { get; set; }

        public int Id { get; set; }

        public String UserTrida { get; set; }

        public String UserJmeno { get; set; }

        public String UserPrijmeni { get; set; }
    }
}
