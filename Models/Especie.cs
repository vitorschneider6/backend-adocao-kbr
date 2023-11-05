using System.Text.Json.Serialization;

namespace Adocao.Models;

public class Especie
{
    public int Id { get; set; }
    public string Nome { get; set; }
    [JsonIgnore]
    public List<Raca> Racas { get; set; }
    [JsonIgnore]
    public List<Animal> Animais { get; set; }
}