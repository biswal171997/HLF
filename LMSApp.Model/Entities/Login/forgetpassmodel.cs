using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
namespace CodeGen.Model.forgetpassmodel
{
    public class forgetpassmodel
    {
       
	   public string vchemail { get; set; }
        public bool emailsent { get; set; }
        public int otp { get; set; }
        public string Domainname { get; set; }
        public string Domainpassword { get; set; }
        public int roleid { get; set; }
        
    }
}

