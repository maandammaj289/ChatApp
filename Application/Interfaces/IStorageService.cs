using System.IO;
using System.Threading.Tasks;


namespace ChatApp.Application.Interfaces
{
    public interface IStorageService
    {
        Task<string> UploadAsync(Stream stream, string fileName, string contentType);
    }
}