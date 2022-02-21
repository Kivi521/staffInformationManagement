using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Xml.Linq;
using System.Web.Script;
using System.Web.Script.Services;
using Employees;
using System.Threading.Tasks;


namespace EmployeeWeb
{
    /// <summary>
    /// Summary description for EmployeeWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class EmployeeWS : System.Web.Services.WebService
    {
        private Dictionary<string, string> departmentDict;

        public Dictionary<string, string> populateDepartment()
        {
            string pathWithFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "department.xml");

            XDocument doc = XDocument.Load(pathWithFileName);
            // XDocument doc = XDocument.Load("department.xml");
            XElement departmentRoot = doc.Root;
            XNamespace ns = doc.Root.GetDefaultNamespace();

            departmentDict = new Dictionary<string, string>(); //{ { 0, "General" },{ 1 ,  "InformaAon Technology" },{ 2, "Accounting" }, { 3, "MarkeAng" },{ 4, "Human Resources" } };
            foreach (XElement element in departmentRoot.Elements())
            {
                XAttribute id = element.Attribute("id");
                string departId = id.Value;

                XElement department = element.Element(ns + "name");
                string depart = department.Value;
                if (id != null && department != null)
                {
                    departmentDict.Add(departId, depart);
                    departmentDict.Add(depart, departId);
                    //departmentDict[id] = department;
                }
            }
            return departmentDict;
        }

        public int generalId()
        {
            //XDocument doc = XDocument.Load("employees.xml");
            string pathWithFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "employees.xml");
            XDocument doc = XDocument.Load(pathWithFileName);
            List<Employee> employees = new List<Employee>();
            XElement root = doc.Root;
            int newId = 0;
            foreach (XElement element in root.Elements())
            {

                XAttribute Id = element.Attribute("id");
                Console.WriteLine(Id);
                int id = int.Parse(Id.Value);
                if (newId < id)
                {
                    newId = id;
                }
            }
            Console.WriteLine(newId);
            return (newId + 1);

        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public List<Employee> GetEmployees()
        {

            Dictionary<string, string> departDict = populateDepartment();
            //departmentDict
            // XDocument doc = XDocument.Load("employees.xml");
            string pathWithFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "employees.xml");
            XDocument doc = XDocument.Load(pathWithFileName);
            XNamespace ns = doc.Root.GetDefaultNamespace();
            List<Employee> employees = new List<Employee>();
            XElement root = doc.Root;

            foreach (XElement element in root.Elements())
            {
                Employee employee = new Employee();
                XAttribute Id = element.Attribute("id");
                if (Id != null)
                {
                    employee.Id = int.Parse(Id.Value);
                }

                XElement IdEle = element.Element(ns + "id");
                if (IdEle != null)
                {
                    string id = IdEle.Value;
                    employee.Id = int.Parse(id);
                }

                XElement Name = element.Element(ns + "name");
                if (Name != null)
                {
                    string name = Name.Value;
                    employee.Name = name;
                }

                XElement Phone = element.Element(ns + "phone");
                if (Phone != null)
                {
                    string phone = Phone.Value;
                    employee.Phone = int.Parse(phone);
                }

                XElement Department = element.Element(ns + "department");
                if (Department != null)
                {
                    string departmentId = Department.Value;
                    string department = departDict[departmentId];
                    employee.Department = department;
                }

                XElement Street = element.Element(ns + "street");
                if (Street != null)
                {
                    string street = Street.Value;
                    employee.Street = street;
                }

                XElement City = element.Element(ns + "city");
                if (City != null)
                {
                    string city = City.Value;
                    employee.City = city;
                }

                XElement State = element.Element(ns + "state");
                if (State != null)
                {
                    string state = State.Value;
                    employee.State = state;

                }

                XElement Zip = element.Element(ns + "zip");
                if (Zip != null)
                {
                    string zip = Zip.Value;
                    employee.Zip = int.Parse(zip);
                }

                XElement Country = element.Element(ns + "country");
                if (Country != null)
                {
                    string country = Country.Value;
                    employee.Country = country;
                }

                //Console.WriteLine(employee.Id);   
                employees.Add(employee);

            }
            //foreach(Employee e in employees)
            //{
            //    Console.WriteLine(e.Id + e.Name);
            //}

            return employees;

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public string InsertEmployee( String name, int phone, String department, String street, String city, String state, int zip, String country)
        {
            
            Dictionary<string, string> departDict = populateDepartment();
            //XDocument doc = XDocument.Load("employees.xml");
            string pathWithFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "employees.xml");
            XDocument doc = XDocument.Load(pathWithFileName);
            XElement EmployeeRoot = doc.Root;
            int id = generalId();
            Console.WriteLine(id);

            XElement addEmployeeElement = new XElement("employee");
            XElement addIdElement = new XElement("id", id);
            XElement addNameElement = new XElement("name", name);
            XElement addPhoneElement = new XElement("phone", phone);

            //users insert an department name
            string departmentId = departDict[department];
            XElement addDepartmentElement = new XElement("department", departmentId);

            XElement addStreetElement = new XElement("street", street);
            XElement addCityElement = new XElement("city", city);
            XElement addStateElement = new XElement("state", state);
            XElement addZipElement = new XElement("zip", zip);
            XElement addCountryElement = new XElement("country", country);

            addEmployeeElement.SetAttributeValue("id", id);
            addEmployeeElement.Add(addIdElement, addNameElement, addPhoneElement, addDepartmentElement, addStreetElement, addCityElement, addStateElement, addZipElement, addCountryElement);
            EmployeeRoot.Add(addEmployeeElement);
            //doc.Save("employees.xml");
            doc.Save(pathWithFileName);
            return "Yeah!! your Insertion is done.!";
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public string UpdateEmployee(string id, String name, int phone, String department, String street, String city, String state, int zip, String country)
        {
            Dictionary<string, string> departDict = populateDepartment();
            //XDocument doc = XDocument.Load("employees.xml");
            string pathWithFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "employees.xml");
            XDocument doc = XDocument.Load(pathWithFileName);
            XElement EmployeeRoot = doc.Root;
            XElement selectedElement = EmployeeRoot.Descendants("employee").Where(el => el.Element("id").Value == id).FirstOrDefault();
            if (selectedElement != null)
            {

                XElement selectedId = selectedElement.Element("id");
                selectedId.Value = id;
                XElement selectedName = selectedElement.Element("name");
                selectedName.Value = name;
                XElement selectedPhone = selectedElement.Element("phone");
                selectedPhone.Value = phone.ToString();
                //users insert an department name
                string departmentId = departDict[department];
                XElement selectedDepartment = selectedElement.Element("department");
                selectedDepartment.Value = departmentId;
                XElement selectedStreet = selectedElement.Element("street");
                selectedStreet.Value = street;
                XElement selectedCity = selectedElement.Element("city");
                selectedCity.Value = city;
                XElement selectedState = selectedElement.Element("state");
                selectedState.Value = state;
                XElement selectedZip = selectedElement.Element("zip");
                selectedZip.Value = zip.ToString();
                XElement selectedCountry = selectedElement.Element("country");
                selectedCountry.Value = country;
                doc.Save(pathWithFileName);
            }
            else
            {
                return "employee id is invalid.";
            }
            return "Congratulations! your new employee data is saved successfully";
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public string DeleteEmployee(string id)
        {
            //XDocument doc = XDocument.Load("employees.xml");
            string pathWithFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "employees.xml");
            XDocument doc = XDocument.Load(pathWithFileName);
            XElement EmployeeRoot = doc.Root;
            XElement selectedElement = EmployeeRoot.Descendants("employee").Where(el => el.Element("id").Value == id).FirstOrDefault();
            if (selectedElement != null)
            {
                Console.WriteLine(selectedElement);
                selectedElement.Remove();
                doc.Save(pathWithFileName);

            }
            else
            {
                return "Employee id is invalid.";
            }

            return "Woohoo! The employee data has been deleted!";
        }

    }
}
