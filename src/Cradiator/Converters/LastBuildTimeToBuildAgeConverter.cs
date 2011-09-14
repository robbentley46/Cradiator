using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using Cradiator.Model;
using Cradiator.Services;
using Ninject;

namespace Cradiator.Converters
{
    [MarkupExtensionReturnType(typeof(LastBuildTimeToBuildAgeConverter))]
    [ValueConversion(typeof(DateTime), typeof(string))]
    public class LastBuildTimeToBuildAgeConverter : MarkupExtension, IValueConverter
    {
        private readonly IDateTimeNow _dateTimeProvider;

        /// <summary>
        /// xaml insists on this DO NOT USE
        /// </summary>
		public LastBuildTimeToBuildAgeConverter()
        {
        }

        [Inject]
        public LastBuildTimeToBuildAgeConverter(IDateTimeNow dateTimeProvider) //todo ninject IDateTimeNow
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var lastBuildDate = value as DateTime?;
            
            if (lastBuildDate == null || !lastBuildDate.HasValue)
                throw new ArgumentException("value should be a System.DateTime", "parameter");

            var difference = _dateTimeProvider.Now.Subtract(lastBuildDate.Value);

            if (difference.Days > 1)
                return string.Format("[{0} Days]", difference.Days);
            
            if (difference.Days == 1)
                return string.Format("[{0} Day]", difference.Days);

            if (difference.Hours > 1)
                return string.Format("[{0} Hours]", difference.Hours);

            if (difference.Hours == 1)
                return string.Format("[{0} Hour]", difference.Hours);
            
            if (difference.Minutes > 1)
                return string.Format("[{0} Minutes]", difference.Minutes);

            if (difference.Minutes == 1)
                return string.Format("[{0} Minute]", difference.Minutes);

            if (difference.Seconds > 1)
                return string.Format("[{0} Seconds]", difference.Seconds);

            
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Ninjector.Get<LastBuildTimeToBuildAgeConverter>();
        }
    }
}