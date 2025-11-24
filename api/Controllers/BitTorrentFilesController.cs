using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("multipart/form-data")]
public class BitTorrentFilesController : ControllerBase
{
    [HttpPost("upload")]
    [Consumes("application/x-bittorrent", "multipart/form-data")]
    public async Task<IActionResult> UploadMultiple([FromForm] IFormFileCollection torrentFiles)
    {
        var torrentfileNameList = new List<string>();
        foreach (var torrentFile in torrentFiles)
        {
          if (torrentFile == null || torrentFile.Length == 0)
            {
                return BadRequest("No BitTorrent files uploaded.");
            }
            else
            {
                torrentfileNameList.Add(torrentFile.FileName);
                var requestStream = torrentFile.OpenReadStream();
                var memoryStream = new MemoryStream();
                await requestStream.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();
                foreach(var fileByte in fileBytes)
                {
                    Console.WriteLine(fileByte);
                }
            }
        }
        return Ok(new { message = "Files uploaded successfully.", fileNames = torrentfileNameList });
  
    }
}
