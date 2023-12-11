using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Utils
{
    class ValidationModels
    {
        public static Tuple<bool, string> Validated(object mdl)
        {
            ValidationContext context = new ValidationContext(mdl, null, null);
            IList<ValidationResult> errors = new List<ValidationResult>();
            StringBuilder message = new StringBuilder();
            if (!Validator.TryValidateObject(mdl, context, errors, true))
            {
                foreach (ValidationResult result in errors)
                {
                    //message.Append("\n");
                    if (!string.IsNullOrWhiteSpace(message.ToString()))
                        message.Append("\n");
                    message.Append(result.ErrorMessage);
                }
            }
            return Tuple.Create(string.IsNullOrWhiteSpace(message.ToString()), message.ToString());
        }

    }
}
