using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class LoginController : Controller
{
    private readonly HttpClient _httpClient;

    public LoginController()
    {
        _httpClient = new HttpClient();
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(string usuario, string senha)
    {
        var loginData = new
        {
            login = usuario,
            password = senha
        };

        var json = JsonSerializer.Serialize(loginData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync("http://192.168.0.129/login.fcgi", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode && responseBody.Contains("session"))
            {
                var jsonDoc = JsonDocument.Parse(responseBody);
                var token = jsonDoc.RootElement.GetProperty("session").GetString();

                HttpContext.Session.SetString("SessionToken", token);

                System.Console.WriteLine($"O token é {token}");
                return RedirectToAction("Index","Dashboard");

            }

            ViewBag.Erro = "Login inválido.";
        }
        catch (Exception ex)
        {
            ViewBag.Erro = $"Erro de conexão com o equipamento: {ex.Message}";
        }

        return View();
    }
}