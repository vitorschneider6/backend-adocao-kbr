using System.Text.Json.Serialization;

namespace Adocao.Models;

public class Animal
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public double? Peso { get; set; }
    public string Sobre { get; set; }
    public bool Ativo { get; set; }
    public string Local { get; set; }
    public string Porte { get; set; }
    public string Sexo { get; set; }
    
    
    public int FotoId { get; set; }
    public List<Foto> Fotos { get; set; }
    public int EspecieId { get; set; }
    public Especie Especie { get; set; }
    public int RacaId { get; set; }
    public Raca Raca { get; set; }
    [JsonIgnore]
    public List<SolicitacaoAdocaoAnimais> Solicitacoes { get; set; }
}