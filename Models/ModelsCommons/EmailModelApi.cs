using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModelsCommons
{
    public class EmailModelApi
    {

        public IList<ToUser> toUser { get; set; }
        public string subject { get; set; }
        public bool isHTMLBody { get; set; }
        public string body { get; set; }
    }
    public class ToUser
    {
        public string nombre { get; set; }
        public string email { get; set; }

    }
}
