using Adocao.Models;

namespace Adocao.ViewModels.Animal;

public class ListAnimalsViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Sexo { get; set; }
    public string Local { get; set; }
    public List<Foto> Imagens { get; set; }
    
}