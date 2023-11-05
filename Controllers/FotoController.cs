using System.Text.RegularExpressions;
using Adocao.Data;
using Adocao.Models;
using Adocao.ViewModels;
using Adocao.ViewModels.Animal;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Adocao.Controllers;
[Authorize]
[ApiController]
public class FotoController : ControllerBase
{
    private readonly AdocaoDevDataContext _context;
    
    
    [HttpPost("v1/animais/upload-image/{id:int}")]
    public async Task<IActionResult> UploadImage(
        [FromRoute] int id,
        [FromBody] UploadImageViewModel model
    )
    {
        var animal = await _context
            .Animais
            .FirstOrDefaultAsync(a => a.Id == id);

        if (animal == null)
        {
            return NotFound(new ResultViewModel<Animal>("Animal não encontrado."));
        }
       
        foreach (var modelBase64Image in model.Base64Images)
        {
            var fileName = $"{Guid.NewGuid().ToString()}.jpg";
            var data = new Regex(@"^data:image\/[a-z]+;base64,")
                .Replace(modelBase64Image, "");
            var bytes = Convert.FromBase64String(data);

            string uri;
            

            try
            {
                var connectionString = Configuration.BlobConnectionString;
                var blobClient = new BlobClient(connectionString, "adocao-project", fileName);

                using (var stream = new MemoryStream(bytes))
                {
                    blobClient.Upload(stream);
                }

                uri = blobClient.Uri.AbsoluteUri;
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor."));
            }
            
            var foto = new Foto
            {
                AnimalId = id,
                Base64Image = uri
            };
            
            try
            {
                _context.Fotos.Add(foto);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<Foto>("Falha interna no servidor"));
            }
        }

        return Ok();
    }


    public FotoController(AdocaoDevDataContext context)
    {
        this._context = context;
    }
}