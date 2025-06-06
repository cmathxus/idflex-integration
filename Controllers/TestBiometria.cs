using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using IntegracaoIDFlex.Models;

public class TestBiometria : Controller {
    public IActionResult Index()
    {
        int Id = (int)TempData["Id"];
        ViewBag.Id = Id;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> TesteBiometria(int id)
    {
        var session = HttpContext.Session.GetString("SessionToken");

        if (string.IsNullOrEmpty(session))
        {
            return RedirectToAction("Index", "Dashboard");
        }

        var client = new HttpClient();

        var data = new {
            
            type = "biometry",
            user_id = id,           
            save = true,              
            sync = true,               
            panic_finger = 0 

        };

        var json = JsonConvert.SerializeObject(data);

        var request = new HttpRequestMessage(HttpMethod.Post, $"http://192.168.0.129/remote_enroll.fcgi?session={session}")
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

    try
    {
        var response = await client.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            TempData["Erro"] = $"Erro na requisição: {response.StatusCode} - {responseContent}";
            return RedirectToAction("Index");
        }

        TempData["Sucesso"] = "Cadastro remoto enviado com sucesso!";
        return RedirectToAction("Index", "Dashboard");
    }
    catch (Exception ex)
    {
        TempData["Erro"] = $"Erro inesperado: {ex.Message}";
        return RedirectToAction("Index");
    }
}
}