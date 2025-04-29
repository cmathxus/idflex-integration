using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        var session = HttpContext.Session.GetString("SessionToken");

        if (string.IsNullOrEmpty(session))
        {
            return RedirectToAction("Index", "Login"); 
        }

        ViewBag.Session = session;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AbrirSecBox()
    {
        var session = HttpContext.Session.GetString("SessionToken"); 

        if (string.IsNullOrEmpty(session))
        {
            return RedirectToAction("Index", "Login");
        }

        var client = new HttpClient();

        var comando = new
        {
            actions = new[]
            {
                new {
                    action = "sec_box",
                    parameters = "id=65793,reason=3,timeout=3000"
                }
            }
        };

        var json = JsonConvert.SerializeObject(comando);
        var request = new HttpRequestMessage(HttpMethod.Post, $"http://192.168.0.129/execute_actions.fcgi?session={session}")
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        await client.SendAsync(request);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> FazerLogout()
    {
        var session = HttpContext.Session.GetString("SessionToken"); 

        if (string.IsNullOrEmpty(session))
        {
            return RedirectToAction("Index", "Login");
        }

        var client = new HttpClient();

        var request = new HttpRequestMessage(HttpMethod.Post, $"http://192.168.0.129/logout.fcgi?session={session}");

        await client.SendAsync(request);
        return RedirectToAction("Index", "Login");
    }
}
