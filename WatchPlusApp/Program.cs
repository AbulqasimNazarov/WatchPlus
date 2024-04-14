using System.Net;
using System.Text;
using WatchPlusApp.Models;
using WatchPlusApp.Repositories;

HttpListener httpListener = new HttpListener();

var prexif = "http://*:8080/";
httpListener.Prefixes.Add(prexif);

httpListener.Start();



async Task WriteViewAsync(HttpListenerResponse response, string viewName, Dictionary<string, object>? viewValues = null)
{
    response.ContentType = "text/html";
    using var stream = new StreamWriter(response.OutputStream);
    var html = await File.ReadAllTextAsync($"{viewName}.html");
    if (viewValues is not null)
    {
        foreach (var viewValue in viewValues)
        {
            html = html.Replace("{{" + viewValue.Key + "}}", viewValue.Value.ToString());
        }
    }
    await stream.WriteLineAsync(html);
    response.StatusCode = (int)HttpStatusCode.OK;
}

string GetCollectionsAsHtml<T>(IEnumerable<T> collection)
{
    Type type = typeof(T);
    var props = type.GetProperties();
    var sb = new StringBuilder();

    foreach (var item in collection)
    {
        sb.Append("<div>");
        foreach (var prop in props)
        {
            sb.Append($"<p><i>{nameof(prop.Name)}: </i>{prop.GetValue(item)}</p>");

        }
        sb.Append("</div>");
    }
    return sb.ToString();
}


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
                await WriteViewAsync(client.Response, "Views/index");


                break;
            }


        case "/films":
            {
                var repository = new FilmsRepository();
                var data = repository.GetAll();
                var html = GetCollectionsAsHtml<Films>(data);
                await WriteViewAsync(client.Response, "Views/films", new(){
                    {"body", html},
                });

                break;
            }


        default:
            {
                await WriteViewAsync(client.Response, "Views/errorPage");
                break;
            }

    }
    client.Response.Close();
}