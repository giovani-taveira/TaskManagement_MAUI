using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Helpers.CustomDataAnnotations
{
    public class DateTimeGreaterThanOrEqualToday : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var dateTime = value as DateTime?;
            return dateTime >= DateTime.Today.Date;
        }
    }
}
