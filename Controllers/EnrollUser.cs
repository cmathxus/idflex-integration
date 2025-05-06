using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using IntegracaoIDFlex.Models;

public class EnrollUser : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> EnrollNewUser(EnrollUserViewModel model)
    {
        
        TempData["Id"] = model.Id;
        var session = HttpContext.Session.GetString("SessionToken");

        if (string.IsNullOrEmpty(session))
        {
            return RedirectToAction("Index", "Dashboard");
        }

        var client = new HttpClient();

        var comando = new
        {
            @object = "users",
            values = new[] {
            new {
                id = model.Id,
                name = model.Name,
                registration = "",
                password = "",
                salt = ""
            }
        }
        };

        var json = JsonConvert.SerializeObject(comando);

        var request = new HttpRequestMessage(HttpMethod.Post, $"http://192.168.0.129/create_objects.fcgi?session={session}")
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        await client.SendAsync(request);
        return RedirectToAction("Index", "TestBiometria");

}
}