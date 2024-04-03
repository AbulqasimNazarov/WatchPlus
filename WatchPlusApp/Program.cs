using System.Net;
using System.Text;
using WatchPlusApp.Models;
using WatchPlusApp.Repositories;

HttpListener httpListener = new HttpListener();

var prexif = "http://*:8080/";
httpListener.Prefixes.Add(prexif);

httpListener.Start();


var htmlTemplate = @"<div>
    <p><i>Name: </i>{{name}}</p>
    <p><i>Rate: </i>{{rate}}</p>    
</div>";



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
                var html = await File.ReadAllTextAsync("Views/index.html");

                using var stream = new StreamWriter(client.Response.OutputStream);

                await stream.WriteLineAsync(html);


                break;
            }


        case "/home":
            {


                var repository = new FilmsRepository();
                var data = repository.GetAll();
                StringBuilder sb = new StringBuilder(htmlTemplate.Length * data.Count());
                foreach (var item in data)
                {
                    var html = htmlTemplate
                               .Replace("{{name}}", item.Name)
                               .Replace("{{rate}}", item.Rate);
                    //var html = await File.ReadAllTextAsync("Views/index.html");
                    sb.Append(html);
                }
                client.Response.ContentType = "text/html";
                using var stream = new StreamWriter(client.Response.OutputStream);

                await stream.WriteLineAsync(sb);
                client.Response.StatusCode = (int)HttpStatusCode.OK;

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