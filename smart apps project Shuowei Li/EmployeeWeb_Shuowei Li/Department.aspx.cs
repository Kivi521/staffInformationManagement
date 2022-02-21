using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Linq;
namespace EmployeeWeb
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string depdocPath = Path.Combine("department.xml");
            departmentXml.DocumentContent = File.ReadAllText(Server.MapPath(depdocPath));
            string depxsltPath = Path.Combine("department.xslt");
            departmentXml.TransformSource = Server.MapPath(depxsltPath);
        }
    }
}