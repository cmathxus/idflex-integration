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
                name = model.Name,
                registration = "",
                password = "",
                salt = ""
            }
        }
        };

        var actions = new {
            type = "biometry",
            user_id = "1",
            save = "false",
            sync = "true",
            panic_finger = "0"
        };


        var json = JsonConvert.SerializeObject(comando);
        var json1 = JsonConvert.SerializeObject(actions);

        var request = new HttpRequestMessage(HttpMethod.Post, $"http://192.168.0.129/create_objects.fcgi?session={session}")
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        var request1 = new HttpRequestMessage(HttpMethod.Post, $"http://192.168.0.129/remote_enroll.fcgi?session={session}")
        {
            Content = new StringContent(json1, Encoding.UTF8, "application/json")
        };

        await client.SendAsync(request);
        return RedirectToAction("Index", "Dashboard");

        
}
}