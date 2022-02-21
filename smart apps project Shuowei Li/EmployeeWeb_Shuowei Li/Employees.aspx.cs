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
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string empdocPath = Path.Combine("employees.xml");
            EmployeesXml.DocumentContent = File.ReadAllText(Server.MapPath(empdocPath));
            string empxsltPath = Path.Combine("employee.xslt");
            EmployeesXml.TransformSource = Server.MapPath(empxsltPath);


        }
    }
}