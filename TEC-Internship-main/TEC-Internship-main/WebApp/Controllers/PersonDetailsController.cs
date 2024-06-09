using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PersonDetailsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync("http://localhost:5229/api/personDetails");
            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                List<PersonDetails> list = JsonConvert.DeserializeObject<List<PersonDetails>>(jstring);
                return View(list);
            }
            else
                return View(new List<PersonDetails>());
        }
        public IActionResult Add()
        {
            PersonDetails personDetails = new PersonDetails();
            return View(personDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Add(PersonDetails personDetails)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var jsonPersonDetails = JsonConvert.SerializeObject(personDetails);
                StringContent content = new StringContent(jsonPersonDetails, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await client.PostAsync("http://localhost:5229/api/personsDetails", content);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "There is an API Error");
                    return View(personDetails);
                }

            }
            else
            {
                return View(personDetails);
            }
        }

        public async Task<IActionResult> Update(int Id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync("http://localhost:5229/api/personDetails/" + Id);
            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                PersonDetails personDetails = JsonConvert.DeserializeObject<PersonDetails>(jstring);
                return View(personDetails);
            }
            else
                return RedirectToAction("Add");
        }
        [HttpPost]
        public async Task<IActionResult> Update(PersonDetails personDetails)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var jsonpersonDetails = JsonConvert.SerializeObject(personDetails);
                StringContent content = new StringContent(jsonpersonDetails, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await client.PutAsync("http://localhost:5229/api/personDetails", content);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(personDetails);
                }
            }
            else
                return View(personDetails);
        }
    }
}
