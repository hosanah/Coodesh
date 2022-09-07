using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Coodesh.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> GetErrors(this ModelStateDictionary modelState)
        {
            var result = new List<string>();
            foreach (var model in modelState.Values)
                result.AddRange(model.Errors.Select(error => error.ErrorMessage));

            return result;
        }
    }
}