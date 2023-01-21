using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FormsPlay.Core
{
    public static class Extensions
    {
        public static int GetViewModelNumber(this object obj)
        {
            var type = obj.GetType();
            string name = type.Name;
            MemberInfo[] memberInfo = type.GetMember(name);
            if (memberInfo?.Any() ?? false)
            {
                throw new ArgumentException($"{type.Name} should have only one member info");
            }
            IEnumerable<ViewModelNumber> customAttributes = memberInfo[0].GetCustomAttributes<ViewModelNumber>();
            var attribute = customAttributes.FirstOrDefault();

            if (attribute == null)
            {
                throw new InvalidOperationException($"{type.Name} Enum has no Id attribute");
            }
            return attribute.Number;
        }
    }
}
