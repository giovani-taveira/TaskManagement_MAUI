using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.MVVM.Models.DomainObjects
{
    public static class Validations
    {
        public static void ValidateGuidIsNotNull(Guid? value, string message)
        {
            if (!value.HasValue)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateIsNullOrEmpty(string value, string message)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateLength(string value, int maxLength, string message)
        {
            var length = value.Trim().Length;
            if (length > maxLength)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateLengthAllowingNull(string value, int maxLength, string message)
        {
            if (string.IsNullOrEmpty(value)) return;

            var length = value.Trim().Length;
            if (length > maxLength)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateIsGreaterThanZero(int value, string message)
        {
            if (value < 0)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateDateTimeIsNotMinOrMaxValue(DateTime? value, string message)
        {
            if (value.HasValue && (value == DateTime.MinValue || value == DateTime.MaxValue))
            {
                throw new DomainException(message);
            }
        }
    }
}
