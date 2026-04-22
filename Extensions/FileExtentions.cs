using Microsoft.AspNetCore.Http;

namespace PustokApp.Extensions;

public static class FileExtensions
{
    public static string SaveFile(this IFormFile file, string webRootPath)
    {
        if (file == null || file.Length == 0)
            return null;

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var imagePath = Path.Combine("assets", "image", "books");
        var path = Path.Combine(webRootPath, imagePath, fileName);

        var directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        return fileName;
    }
}

