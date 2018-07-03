using System;
using System.Linq;
using System.Reflection;
using NoiseExtension;
using ViperEngine;

static internal class ViperOpExtensions
{
    public static void RegisterWithDescription(Type staticType, string category=null)
    {
        var categoryAttr = staticType.GetCustomAttributes(typeof(CategoryAttribute), true);
        var tmpCategory = ((CategoryAttribute)categoryAttr.FirstOrDefault())?.Text ?? String.Empty;
        if (String.IsNullOrWhiteSpace(category)) category = tmpCategory;
        else if (!String.IsNullOrWhiteSpace(tmpCategory)) category += "." + tmpCategory;

        var methods = staticType.GetMethods(BindingFlags.Static | BindingFlags.Public);
        foreach (var info in methods)
        {
            // Handle description and subcategories
            var opCategory = category;
            var subCategoryAttr = info.GetCustomAttributes(typeof(CategoryAttribute), true);
            var subCategory = ((CategoryAttribute)subCategoryAttr.FirstOrDefault())?.Text ?? String.Empty;

            var displayNameAttr = info.GetCustomAttributes(typeof(DisplayNameAttribute), true);
            var displayName = ((DisplayNameAttribute)displayNameAttr.FirstOrDefault())?.Name ?? String.Empty;

            var descriptionAttr = info.GetCustomAttributes(typeof(DescriptionAttribute), true);
            var description = ((DescriptionAttribute)descriptionAttr.FirstOrDefault())?.Text ?? String.Empty;

            if (String.IsNullOrWhiteSpace(opCategory)) opCategory = subCategory;
            else if (!String.IsNullOrWhiteSpace(subCategory)) opCategory += "." + subCategory;

            var viperMethodOp = new ViperMethodOp(info);

            if (!String.IsNullOrWhiteSpace(opCategory))
                viperMethodOp.Category = opCategory;
            if (!String.IsNullOrWhiteSpace(displayName))
                viperMethodOp.DisplayName = displayName;
            if (!String.IsNullOrWhiteSpace(description))
                viperMethodOp.Description = description;

            ViperContext.OpContext.RegisterOp(viperMethodOp);
        }

        var nestedTypes = staticType.GetNestedTypes();
        foreach (var type in nestedTypes)
        {
            RegisterWithDescription(type, category);
        }
    }
}