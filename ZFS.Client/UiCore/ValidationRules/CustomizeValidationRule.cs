using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using ZFS.Client.LogicCore.Enums;
using ZFS.Client.LogicCore.UserAttribute;

namespace ZFS.Client.UiCore.ValidationRules
{
    public class CustomizeValidationRule : ValidationRule
    {
        public ValidationType validationType { get; set; } = ValidationType.None;
        public string errorMessage { get; set; } = string.Empty;
        public int minLength { get; set; }
        public int maxLength { get; set; }


        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string regex = string.Empty;
            if (validationType != ValidationType.None&&validationType!= ValidationType.Str)
                regex = GetEnumAttrbute.GetDescription(validationType).Caption;

            if (!string.IsNullOrWhiteSpace(regex))
            {
                Regex re = new Regex(regex);
                if (re.IsMatch((value ?? "").ToString()))
                {
                    return ValidationResult.ValidResult;
                }
                else
                    return new ValidationResult(false, errorMessage);
            }
            else
            {
                string input = (value ?? "").ToString();
                if (input.Length < minLength || input.Length > 10)
                {
                    return new ValidationResult(false, errorMessage);
                }
                return ValidationResult.ValidResult;
            }
        }
    }
}
