using EmployeeManagement.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class RecordController : Controller
    {
        private static string url = "https://localhost:44351/";
        // GET: Record
        public async Task<ActionResult> Index()
        {
            List<EmpViewModel> listemp = new List<EmpViewModel>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseMsg = await client.GetAsync("Employee/GetAllEmp");
                if (responseMsg.IsSuccessStatusCode)
                {
                    var emplist = responseMsg.Content.ReadAsStringAsync().Result;
                    listemp = JsonConvert.DeserializeObject<List<EmpViewModel>>(emplist);
                }
            }
            return View(listemp);
        }
        public async Task<ActionResult> Edit(int? empid)
        {
            EmpViewModel empViewModel = new EmpViewModel();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseMsg = await client.GetAsync("Employee/GetEmpByID?empid=" + empid);
                if (responseMsg.IsSuccessStatusCode)
                {
                    var res = responseMsg.Content.ReadAsStringAsync().Result;
                    empViewModel = JsonConvert.DeserializeObject<EmpViewModel>(res);
                }
            }
            return View(empViewModel);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(EmpViewModel empViewModel)
        {
            string custommsg = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseMsg = await client.PostAsJsonAsync("Employee/AddEmployee",empViewModel);
                if (responseMsg.IsSuccessStatusCode)
                {
                    var res = responseMsg.Content.ReadAsStringAsync().Result;
                    custommsg = JsonConvert.DeserializeObject<string>(res);
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Update(EmpViewModel empViewModel)
        {
            string custommsg = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseMsg = await client.PutAsJsonAsync("Employee/UpdateEmp?empid="+ empViewModel.EmpID, empViewModel);
                if (responseMsg.IsSuccessStatusCode)
                {
                    var res = responseMsg.Content.ReadAsStringAsync().Result;
                    custommsg = JsonConvert.DeserializeObject<string>(res);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        //public ActionResult DeleteRecord()
        //{
        //    return View();
        //}
        //[HttpPost]
        public async Task<ActionResult> DeleteRecord(int? empid)
        {
            string custommsg = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseMsg = await client.DeleteAsync("Employee/Delete?empid=" + empid);
                if (responseMsg.IsSuccessStatusCode)
                {
                    var res = responseMsg.Content.ReadAsStringAsync().Result;
                    custommsg = JsonConvert.DeserializeObject<string>(res);
                }
            }
            return RedirectToAction("Index");
        }
        
    }
}