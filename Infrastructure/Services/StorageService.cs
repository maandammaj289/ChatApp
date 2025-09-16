using System.IO;
using System.Threading.Tasks;

using ChatApp.Application.Interfaces;


namespace ChatApp.Infrastructure.Services
{
    // Stub: Replace with S3/Azure Blob implementation
    public class StorageService : IStorageService
    {
        public async Task<string> UploadAsync(Stream stream, string fileName, string contentType)
        {
            // TODO: store and return public URL
            var path = Path.Combine(Path.GetTempPath(), fileName);
            using var fs = File.Create(path);
            await stream.CopyToAsync(fs);
            return $"file://{path}";
        }
    }
}