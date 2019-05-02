using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp_OpenIDConnect_DotNet.Models
{
    public class StudentViewModel
    {
        public Student Student { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}