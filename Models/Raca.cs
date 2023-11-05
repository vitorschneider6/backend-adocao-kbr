using System.Text.Json.Serialization;

namespace Adocao.Models;

public class Raca
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int EspecieId { get; set; }
    public Especie Especie { get; set; }
    [JsonIgnore]
    public List<Animal> Animais { get; set; }
}