using System.Text.Json.Serialization;

namespace Adocao.Models;

public class Foto
{
    public int FotoId { get; set; }
    public int AnimalId { get; set; }
    public string Base64Image { get; set; }
    [JsonIgnore]
    public Animal Animal { get; set; }

}