using System;
using System.Windows.Data;
using Cradiator.Converters;
using Cradiator.Model;
using NUnit.Framework;
using Shouldly;

namespace Cradiator.Tests.Converters
{
    [TestFixture]
    public class LastBuildTimeToBuildAgeConverter_Tests : ConverterTestBase
    {
        [Test]
        public void can_convert_8_day_old_build()
        {
            ((string)DoConvert(DateTime.Parse("2011-09-06T12:00:00"))).ShouldBe("[8 Days]");
        }
        
        [Test]
        public void can_convert_1_day_old_build()
        {
            ((string)DoConvert(DateTime.Parse("2011-09-13T12:00:00"))).ShouldBe("[1 Day]");
        }

        [Test]
        public void can_convert_6_hour_old_build()
        {
            ((string)DoConvert(DateTime.Parse("2011-09-14T06:00:00"))).ShouldBe("[6 Hours]");
        }
        
        [Test]
        public void can_convert_1_hour_old_build()
        {
            ((string)DoConvert(DateTime.Parse("2011-09-14T11:00:00"))).ShouldBe("[1 Hour]");
        }
        
        [Test]
        public void can_convert_30_minute_old_build()
        {
            ((string)DoConvert(DateTime.Parse("2011-09-14T11:30:00"))).ShouldBe("[30 Minutes]");
        }
        
        [Test]
        public void can_convert_1_minute_old_build()
        {
            ((string)DoConvert(DateTime.Parse("2011-09-14T11:59:00"))).ShouldBe("[1 Minute]");
        }
        
        [Test]
        public void can_convert_30_second_old_build()
        {
            ((string)DoConvert(DateTime.Parse("2011-09-14T11:59:30"))).ShouldBe("[30 Seconds]");
        }

        protected override IValueConverter CreateConverter()
        {
            return new LastBuildTimeToBuildAgeConverter(new DateTimeFake(new DateTime(2011, 09, 14, 12, 00, 00)));
        }

        internal class DateTimeFake : IDateTimeNow
        {
            private readonly DateTime _dateTime;

            public DateTimeFake(DateTime dateTime)
            {
                _dateTime = dateTime;
            }

            public DateTime Now
            {
                get { return _dateTime; }
                set { throw new NotImplementedException(); }
            }
        }
    }
}