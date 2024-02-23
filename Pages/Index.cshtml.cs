using System;
using System.IO;
using System.Linq;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.SignalR;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.API;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Logger;
using InstagramApiSharp.Classes.Models;
using System.IO;
using System.Linq;
using System.Text;
using System.Net.Http;
using ImageMagick;


namespace AutomatedInstagramBot.Pages;


[Route("api/[controller]")]

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IHubContext<ClockHub> _clockHub;

    public IndexModel(ILogger<IndexModel> logger, IHubContext<ClockHub> clockHub)
    {
        _logger = logger;
        _clockHub = clockHub;
    }

    public async void OnGet()
    {
        // This is your page's initialization logic
        int remainingSeconds = 24 * 60 * 60; // Initial time

        while (remainingSeconds > 0)
        {
            await _clockHub.Clients.All.SendAsync("UpdateTime", remainingSeconds);
            await Task.Delay(1000); // Wait for 1 second
            remainingSeconds--;
        }
    }
    public IActionResult OnPostGetAjax(string name)
    {
        switch (name)
        {
            case "DownloadMemes":
                DownloadMemes();
                break;
                    }
        return new JsonResult("Hello " + name);
    }
    static void DeleteFile(string path)
    {
        string filesPath = @path;

        System.IO.File.Delete(filesPath);

        Console.Write($"file {filesPath} has ben deleted ");
    }
    static void ConvertImage(string inputFilePath, string outputFilePath)
    {
        //  = "downloaded_image.jpg"; // Replace with the path to your downloaded image
        //  = "converted_image.jpg"; // Specify the output path and filename

        using (MagickImage image = new MagickImage(inputFilePath))
        {
            // Convert the image to JPEG format
            image.Write(outputFilePath);
        }

        Console.WriteLine("Image converted successfully.");
    }




    static async Task UploadMemes(string[] names)
    {

        UserSessionData userSession = new UserSessionData
        {
            UserName = "thatmemeguy35",
            Password = "meme35!!"
        };

        IInstaApi api = InstaApiBuilder.CreateBuilder()
        .SetUser(userSession)
        .UseLogger(new DebugLogger(InstagramApiSharp.Logger.LogLevel.All))
        .Build();

        // Log in to Instagram
        var loginResult = await api.LoginAsync();

        if (loginResult.Succeeded)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Logged in successfully!");
            Console.ResetColor();


            for (int i = 0; i < names.Length; i++)
            {
                string name = names[i];
                var MediaImage = new InstaImageUpload
                {
                    Height = 1080,
                    Width = 1080,
                    Uri = $@"wwwroot/memes/{name}"
                    //Uri = @"\Users\student\Desktop\IgBot\memes\meme1.png"//path
                    //    Uri = "\Users\student\Desktop\IgBot\memes\frog.jpg"
                };
                var postResult = await api.MediaProcessor.UploadPhotoAsync(MediaImage,"");//i

                if (postResult.Succeeded)
                {
                    
                    DeleteFile($@"wwwroot/memes/{name}");
                    // DeleteDescription(descriptonePath);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Meme posting failed: {postResult.Info.Message}");
                    Console.ResetColor();
                }

            }

        }
        else
        {
            UploadMemes(names);
        }
    }
public void DownloadMemes()
{
    using (var stream = new FileStream("wwwroot/client_secret_644166516897-gc0bk2rjao7ugjnbs49n3dlupt9sjl6b.apps.googleusercontent.com.json", FileMode.Open, FileAccess.Read))
    {
        GoogleClientSecrets clientSecrets = GoogleClientSecrets.Load(stream);

        // Create an OAuth2.0 credential
        var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            clientSecrets.Secrets,
            new[] { DriveService.Scope.Drive },
            "user",
            System.Threading.CancellationToken.None).Result;

        var service = new DriveService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "DriveConsole",
        });

        // Specify the ID of the "memes" folder
        var memesFolderId = "1FcvGQ-dRsGFFkiwwWyOONUEbO4VWT9hC"; // Replace with the actual folder ID

        // List files in the "memes" folder
        var memesQuery = $"'{memesFolderId}' in parents and mimeType contains 'image/'";
        var memesListRequest = service.Files.List();
        memesListRequest.Q = memesQuery;
        var memes = memesListRequest.Execute().Files;

        int amountOfMemesTodownload = 2;

        if (memes.Count > amountOfMemesTodownload - 1)
        {
            Random rand = new Random();
            string[] imageNames = new string[amountOfMemesTodownload];

            for (int i = 0; i < amountOfMemesTodownload; i++)
            {
                memes = memesListRequest.Execute().Files;

                // Download the random image from the "memes" folder
                var memeToDownload = memes[rand.Next(0, memes.Count)];

                // Console.WriteLine($"Meme Name: {memeToDownload.Name}");
                // Console.WriteLine($"Meme ID: {memeToDownload.Id}");

                var memeStream = new MemoryStream();
                service.Files.Get(memeToDownload.Id).Download(memeStream);
                System.IO.File.WriteAllBytes($"wwwroot/memes/{memeToDownload.Name}", memeStream.ToArray());

                // Console.WriteLine("Image downloaded successfully.");

                var imageId = memeToDownload.Id;
                var imageName = memeToDownload.Name;

                imageNames[i] = imageName.ToString();

                try
                {
                    service.Files.Delete(imageId).Execute();
                    // Console.WriteLine($"Meme Name: {imageId}");
                    // Console.WriteLine($"Meme ID: {imageName}");
                    Console.WriteLine($"Image deleted successfully.");
                }
                catch (Exception ex)
                {
                    // Console.WriteLine($"Error deleting image: {ex.Message}");
                }
            }
                UploadMemes(imageNames);
        }
        else
        {
            // Console.WriteLine("No images found in the 'memes' folder.");
        }
    }
}

    // [HttpPost("myMethod")]
    // public IActionResult MyMethod([FromBody] string data)
    // {
    //     // Your logic here
    //     string result = $"Received data: {data}";
    //     return ;
    // }

}







    /*[HttpPost]
    public ActionResult ProcessData(string name, string email)
    {
        // Process the data or perform any backend tasks
        // ...

        // Return a JSON response back to the frontend
        return Json(new { status = "success", message = "Data received successfully!" });
    }*/

    // [ApiController]
    // [Route("[controller]")]
    // public class ApiController : ControllerBase
    // {
    //     [HttpPost]
    //     public string Get()
    //     {
    //         return "ok ";
    //     }
    // }

