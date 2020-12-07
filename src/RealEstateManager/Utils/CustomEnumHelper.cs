using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RealEstateManager.Utils
{
    public static class CustomEnumHelper
    {
        public static SelectList ToSelectList<TEnum>(TEnum? defaultValue = null)
            where TEnum : struct, Enum
        {
            var enumType = typeof(TEnum);

            if (enumType.FullName == null)
                throw new InvalidOperationException($"Cannot localize Enum: {typeof(TEnum)}.FullName returned null.");

            var enumFullName = enumType.FullName.Replace('.', '_');

            var items = Enum.GetNames(enumType)
                .Select(x => new SelectListItem
                {
                    Text = Localization.GetString($"{enumFullName}_{x}"),
                    Value = Convert.ToInt32(Enum.Parse(enumType, x)).ToString()
                }).ToList();

            return defaultValue.HasValue
                ? new SelectList(items, "Value", "Text", defaultValue)
                : new SelectList(items, "Value", "Text");
        }

        public static SelectList ToSelectList<TEnum>(TEnum defaultValue, IEnumerable<TEnum> disabledValues = null)
            where TEnum : struct, Enum
        {
            var enumType = typeof(TEnum);

            if (enumType.FullName == null)
                throw new InvalidOperationException($"Cannot localize Enum: {typeof(TEnum)}.FullName returned null.");

            var enumFullName = enumType.FullName.Replace('.', '_');

            var items = Enum.GetNames(enumType)
                .Select(x => new SelectListItem
                {
                    Text = Localization.GetString($"{enumFullName}_{x}"),
                    Value = Convert.ToInt32(Enum.Parse(enumType, x)).ToString()
                }).ToList();

            return new SelectList(items, "Value", "Text", defaultValue, disabledValues);
        }

        public static string GetLocalizedName<TEnum>(TEnum value)
            where TEnum : struct, Enum
        {
            var enumType = typeof(TEnum);

            if (enumType.FullName == null)
                throw new InvalidOperationException($"Cannot localize Enum: {typeof(TEnum)}.FullName returned null.");

            var enumFullName = enumType.FullName.Replace('.', '_');

            var enumValueName = Enum.GetName(enumType, value);

            var localizationKey = $"{enumFullName}_{enumValueName}";

            return Localization.GetString(localizationKey);
        }
    }
}
