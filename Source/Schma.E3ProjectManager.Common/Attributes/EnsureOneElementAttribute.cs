using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Schma.E3ProjectManager.Common.Attributes
{
    public class EnsureOneElementAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var collection = value as ICollection;
            if (collection == null) return false;
            return collection.Count > 0;
        }
    }
}
