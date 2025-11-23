using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

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
            }
        }
        return Ok(new { message = "Files uploaded successfully.", fileNames = torrentfileNameList });
  
    }
}
