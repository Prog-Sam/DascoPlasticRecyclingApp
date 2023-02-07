using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class FileUploadController : ControllerBase
{
    [HttpGet("{fileName}")]
    public IActionResult GetImage(string fileName)
    {
        try
        {
            var path = Path.Combine(@"D:\Projects\Node\Actual\Dasco-Frontend\public\images", fileName);
            var image = System.IO.File.OpenRead(path);
            return File(image, "image/jpeg");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        var uploadsFolder = Path.Combine(@"D:\Projects\Node\Actual\Dasco-Frontend\public", "images");

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var fileExtension = Path.GetExtension(file.FileName);
        var fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}{fileExtension}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok(filePath);
    }

    [HttpDelete("{filePath}")]
    public IActionResult DeleteFile(string filePath)
    {

        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}
