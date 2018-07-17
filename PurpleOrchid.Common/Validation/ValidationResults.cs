using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PurpleOrchid.Common.Serialization;

namespace PurpleOrchid.Common.Validation
{
    public class ValidationResults
    {
        private readonly List<ValidationResult> _validationResults = new List<ValidationResult>();

        public bool IsValid => _validationResults.All(x => x.IsValid);
        public IEnumerable<ValidationResult> Results => _validationResults;

        public void Add(ValidationResult result)
        {
            _validationResults.Add(result);
        }

        public void AddRange(ValidationResults results)
        {
            _validationResults.AddRange(results.Results);
        }

        public void Add(string name, string message, bool isValid = false, string value = "")
        {
            Add(new ValidationResult(name, value, isValid, message));
        }


        public override string ToString()
        {
            return Json.Serialize(new
            {
                ValidationErrors = _validationResults.Where(x => !x.IsValid)
            });
        }
    }
}