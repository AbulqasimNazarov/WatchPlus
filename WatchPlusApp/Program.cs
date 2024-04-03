using System.Net;
using WatchPlusApp.Models;
using WatchPlusApp.Repositories;

HttpListener httpListener = new HttpListener();

var prexif = "http://*:8080/";
httpListener.Prefixes.Add(prexif);

httpListener.Start();

System.Console.WriteLine($"Server started... {prexif.Replace("*", "localhost")}");
System.Collections.ObjectModel.ObservableCollection<Films> filmList = new System.Collections.ObjectModel.ObservableCollection<Films>();
while (true)
{
    var client = await httpListener.GetContextAsync();
    string? endpoint = client.Request.RawUrl;
    switch (endpoint)
    {
        case "/":
            {




                break;
            }


        case "/home":
            {

                var repository = new FilmsRepository();
                var data = repository.GetAll();
                var html = await File.ReadAllTextAsync("Views/index.html");
                foreach (var item in data)
                {
                    html = html.Replace("{{Name}}", item.Name).Replace("{{Rate}}", item.Rate);
                }
                client.Response.ContentType = "text/html";
                using var stream = new StreamWriter(client.Response.OutputStream);

                await stream.WriteLineAsync(html);

                break;
            }


        default:
            {
                client.Response.ContentType = "text/html";
                using var stream = new StreamWriter(client.Response.OutputStream);
                var html = await File.ReadAllTextAsync("Views/errorPage.html");
                await stream.WriteLineAsync(html);
                break;
            }

    }
    client.Response.Close();
}