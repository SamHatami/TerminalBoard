using System.Globalization;
using System.Windows.Controls;

namespace TerminalBoard.App.UIComponents.ValidationRules;

public class TextBoxNumericValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (float.TryParse((string)value, out float floatValue))
            return ValidationResult.ValidResult;

        if (Int32.TryParse((string)value,out int intValue))
            return ValidationResult.ValidResult;

        return new ValidationResult(false, $"Non integer value");
    }
}