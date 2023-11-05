using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Adocao.ExtensionMethods;

public static class ModelStateExtension
{
    public static List<string> GetErrors(this ModelStateDictionary modelo)
    {
        List<string> listaErros = new();
        foreach (var modelStateValue in modelo.Values)
        {
            foreach (var erros in modelStateValue.Errors)
            {
                listaErros.Add(erros.ErrorMessage);
            }
        }
        return listaErros;
    }
}