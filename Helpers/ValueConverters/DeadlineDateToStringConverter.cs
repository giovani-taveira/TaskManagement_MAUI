using System.Globalization;

namespace TaskManagement.Helpers.ValueConverters
{
    public class DeadlineDateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime?)value;
            return date.Value.ToString("dd/MM/yyyy") ?? "Sem Prazo";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
