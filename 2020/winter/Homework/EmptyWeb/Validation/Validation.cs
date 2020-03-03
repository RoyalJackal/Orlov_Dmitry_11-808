using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EmptyWeb.Validation
{
    public static class Validation
    {
        public static List<ValidationResult> Validate(object obj)
        {
            // здесь пробежать по всем полям модели с использованием рефлексии
            // и если они имеют атрибут потомок ValidationAttribute
            // вызвать соответствующий метод IsValid
            var type = obj.GetType();
            var props = type.GetProperties();
            var results = new List<ValidationResult>();
            foreach (var prop in props)
            {
                var valAttributes = prop.GetCustomAttributes<ValidationAttribute>();
                if (valAttributes.Any(attribute => !attribute.IsValid(prop.GetValue(obj))))
                {
                    results.Add(new ValidationResult(false,
                        $"Property {prop.Name} doesn't have a valid value. Try something else."));
                }
                else
                {
                    results.Add(new ValidationResult(true));
                }
            }

            return results;
        }
    }
}