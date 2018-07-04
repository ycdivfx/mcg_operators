using System;
using System.Linq;
using System.Reflection;
using NoiseExtension;
using ViperEngine;

namespace MCG.ExtraOperators
{
    internal static class ViperOpExtensions
    {
        /// <summary>
        /// Register any public static method found on a class, with proper DisplayName, Category and Description.
        /// Supports nested categories.
        /// </summary>
        /// <param name="opType">Class with operator methods.</param>
        /// <param name="category">Base category. Normally left as null.</param>
        public static void RegisterWithDescription(Type opType, string category=null)
        {
#if MAX2017
            ViperOp.RegisterStaticMethods(opType);
#else
            var categoryAttr = opType.GetCustomAttributes(typeof(CategoryAttribute), true);
            var tmpCategory = ((CategoryAttribute)categoryAttr.FirstOrDefault())?.Text ?? string.Empty;
            if (string.IsNullOrWhiteSpace(category)) category = tmpCategory;
            else if (!string.IsNullOrWhiteSpace(tmpCategory)) category += "." + tmpCategory;

            var methods = opType.GetMethods(BindingFlags.Static | BindingFlags.Public);
            foreach (var info in methods)
            {
                // Handle description and subcategories
                var opCategory = category;
                var subCategoryAttr = info.GetCustomAttributes(typeof(CategoryAttribute), true);
                var subCategory = ((CategoryAttribute)subCategoryAttr.FirstOrDefault())?.Text ?? string.Empty;

                var displayNameAttr = info.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                var displayName = ((DisplayNameAttribute)displayNameAttr.FirstOrDefault())?.Name ?? string.Empty;

                var descriptionAttr = info.GetCustomAttributes(typeof(DescriptionAttribute), true);
                var description = ((DescriptionAttribute)descriptionAttr.FirstOrDefault())?.Text ?? string.Empty;

                if (string.IsNullOrWhiteSpace(opCategory)) opCategory = subCategory;
                else if (!string.IsNullOrWhiteSpace(subCategory)) opCategory += "." + subCategory;

                var viperMethodOp = new ViperMethodOp(info);

                if (!string.IsNullOrWhiteSpace(opCategory))
                    viperMethodOp.Category = opCategory;
                if (!string.IsNullOrWhiteSpace(displayName))
                    viperMethodOp.DisplayName = displayName;
                if (!string.IsNullOrWhiteSpace(description))
                    viperMethodOp.Description = description;

                ViperContext.OpContext.RegisterOp(viperMethodOp);

            }

            var nestedTypes = opType.GetNestedTypes();
            foreach (var type in nestedTypes)
            {
                RegisterWithDescription(type, category);
            }
#endif
        }
    }
}