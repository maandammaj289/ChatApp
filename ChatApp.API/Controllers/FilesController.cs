using System.IO;
using System.Threading;
using System.Threading.Tasks;

using ChatApp.Application.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ChatApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FilesController : ControllerBase
    {
        private readonly IStorageService _storage;
        public FilesController(IStorageService storage) { _storage = storage; }


        [HttpPost]
        //[RequestSizeLimit(50_000_000)]
        public async Task<IActionResult> Upload(IFormFile file, CancellationToken ct)
        {
            await using var stream = file.OpenReadStream();
            var url = await _storage.UploadAsync(stream, file.FileName, file.ContentType);
            return Ok(new { url });
        }
    }
}