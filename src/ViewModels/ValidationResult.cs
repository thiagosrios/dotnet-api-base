using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace ApiBase.ViewModels
{
    public class ValidationResult
    {
        public string Message { get; set; }
        public int Status { get; set; }
        public List<ValidationError> Errors { get; set; }

        public ValidationResult(ModelStateDictionary modelState)
        {
            Status = 422;
            Message = "Ocorreram um ou mais erros de validação";
            Errors = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                .ToList();
        }
    }
}
