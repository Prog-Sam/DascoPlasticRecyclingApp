using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class FileUploadController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        var uploadsFolder = Path.Combine("D:\\", "UploadFolder");

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
