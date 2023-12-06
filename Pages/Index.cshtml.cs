using System;
using System.IO;
using System.Linq;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AutomatedInstagramBot.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        // This is your page's initialization logic
    }

    public void OnTimerTick()
    {
        // This method can be used to trigger your Instagram bot
        // Create an instance of your Instagram bot class
        // InstagramBot instagramBot = new InstagramBot();

        // Call the method that uploads memes to Instagram
        //  instagramBot.UploadMemes();

        // Optionally, you can log the action or handle any errors here

        using (var stream = new FileStream("~/client_secret_644166516897-gc0bk2rjao7ugjnbs49n3dlupt9sjl6b.apps.googleusercontent.com.json", FileMode.Open, FileAccess.Read))
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

                for (int i = 0; i < amountOfMemesTodownload; i++)
                {
                    memes = memesListRequest.Execute().Files;

                    // Download the random image from the "memes" folder
                    var memeToDownload = memes[rand.Next(0, memes.Count)];

                    Console.WriteLine($"Meme Name: {memeToDownload.Name}");
                    Console.WriteLine($"Meme ID: {memeToDownload.Id}");
                    
                    // Download the meme
                    var memeStream = new MemoryStream();
                    service.Files.Get(memeToDownload.Id).Download(memeStream);
                    System.IO.File.WriteAllBytes($"~/memes/{memeToDownload.Name}", memeStream.ToArray());

                    Console.WriteLine("Image downloaded successfully.");

                    var imageId = memeToDownload.Id;
                    var imageName = memeToDownload.Name;

                    try
                    {
                        service.Files.Delete(imageId).Execute();
                        Console.WriteLine($"Meme Name: {imageId}");
                        Console.WriteLine($"Meme ID: {imageName}");
                        Console.WriteLine($"Image deleted successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting image: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine("No images found in the 'memes' folder.");
            }
        }
    }
    /*[HttpPost]
    public ActionResult ProcessData(string name, string email)
    {
        // Process the data or perform any backend tasks
        // ...

        // Return a JSON response back to the frontend
        return Json(new { status = "success", message = "Data received successfully!" });
    }*/
}
