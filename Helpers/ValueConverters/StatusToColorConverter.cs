using System.Globalization;
using TaskManagement.Helpers.Enums;

namespace TaskManagement.Helpers.ValueConverters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (string)value;
            StatusEnum statusEnum; 
            Enum.TryParse(status.Replace(" ", "_"), out statusEnum);

            var color = statusEnum switch
            {
                StatusEnum.Ativo => CustomColors.Blue,
                StatusEnum.Concluido => CustomColors.Green,
                StatusEnum.Em_Atraso => CustomColors.Red,
                _ => CustomColors.Black
            };

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
