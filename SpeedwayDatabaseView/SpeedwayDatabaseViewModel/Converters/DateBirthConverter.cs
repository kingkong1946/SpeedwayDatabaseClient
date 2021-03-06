﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace SpeedwayDatabaseViewModel.Converters
{
    public class DateBirthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var currValue = value as DateTime?;
            var result = currValue?.ToString("d");
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime returnValue;
            try
            {
                returnValue = DateTime.ParseExact("d", value.ToString(), culture);
            }
            catch (FormatException e)
            {
                throw e;
            }
            return returnValue;
        }
    }
}
