// File: Utilities/ImageDownloader.cs

using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System;
using WatchPlus.Models; 

namespace WatchPlus.Utilities;

public static class ImageDownloader
{
    public static async Task DownloadImagesAsync(IEnumerable<Film> films, string directory)
    {
        using HttpClient client = new HttpClient();

        foreach (var film in films)
        {
            var imageUrl = film.Image;
            var imageResponse = await client.GetAsync(imageUrl);
            if (imageResponse.IsSuccessStatusCode)
            {
                var imageBytes = await imageResponse.Content.ReadAsByteArrayAsync();
                var extension = Path.GetExtension(imageUrl);
                var imagePath = Path.Combine(directory, $"{film.Id}{extension}");
                await File.WriteAllBytesAsync(imagePath, imageBytes);
            }
        }
    }
}
