using System;
using System.Globalization;
using System.Threading;
using NUnit.Framework;

namespace Program.Tests
{
    [TestFixture]
    public class PeriodTests
    {
        [Test]
        public void GetInstance_EmptyArgsArray_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Period.GetInstance(new string[] { }));
        }

        [Test]
        public void GetInstance_FirstDateBiggerThanSecond_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Period.GetInstance(new[] {"12.05.2018", "29.07.2017"}));
        }

        [Test]
        public void GetInstance_GivenInvalidDateInArgs_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => Period.GetInstance(new[] {"43.05.2016", "29.07.2017"}));
            Assert.Throws<FormatException>(() => Period.GetInstance(new[] {"14.13.2016", "29.07.2017"}));
        }

        [Test]
        public void GetInstance_GivenTwoProperDates_ReturnInstanceWithProperInitializedtProperties()
        {
            var expected1 = new DateTime(2016, 5, 12);
            var expected2 = new DateTime(2017, 7, 29);
            var period = Period.GetInstance(new[] {"12.05.2016", "29.07.2017"});
            Assert.AreEqual(expected1, period.StartingDate);
            Assert.AreEqual(expected2, period.EndingDate);
        }

        [Test]
        public void GetInstance_InvalidDateStringInArgs_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => Period.GetInstance(new[] {"asdasfas", "1243456"}));
        }

        [Test]
        public void GetInstance_MoreThanTwoStringsInArgs_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                Period.GetInstance(new[] {"12.05.2017", "29.07.2017", "29.10.2018"}));
        }

        [Test]
        public void GetInstance_OneStringInArgs_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Period.GetInstance(new[] {"12.05.2017"}));
        }

        [Test]
        public void ToString_DifferentSystemCultures_ReturnExpectedString()
        {
            string[] cultures = {"pl-PL", "fr-FR", "en-US", "en-GB", "ja-JP"};
            string[] expected =
                {"12.05-29.07.2017", "12/05-29/07/2017", "5/12-7/29/2017", "12/05-29/07/2017", "05/12-2017/07/29"};
            var dateRange = Period.GetInstance(new[] {"12.05.2017", "29.07.2017"});
            for (var i = 0; i < cultures.Length; i++)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(cultures[i]);
                Assert.AreEqual(expected[i], dateRange.ToString());
            }
        }

        [Test]
        public void ToString_GivenTwoDatesWithSameMonthAndYear_ExpectedString()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pl-PL");
            var expected = "12-29.05.2017";
            var period = Period.GetInstance(new[] {"12.05.2017", "29.05.2017"});
            Assert.AreEqual(expected, period.ToString());
        }

        [Test]
        public void ToString_GivenTwoDatesWithSameYear_ExpectedString()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pl-PL");
            var expected = "12.05-29.07.2017";
            var period = Period.GetInstance(new[] {"12.05.2017", "29.07.2017"});
            Assert.AreEqual(expected, period.ToString());
        }

        [Test]
        public void ToString_GivenTwoDifferentDates_ExpectedString()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pl-PL");
            var expected = "12.10.2015-29.05.2017";
            var period = Period.GetInstance(new[] {"12.10.2015", "29.05.2017"});
            Assert.AreEqual(expected, period.ToString());
        }

        [Test]
        public void ToString_GivenTwoSameDates_ExpectedString()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pl-PL");
            var expected = "12.05.2017";
            var period = Period.GetInstance(new[] {"12.05.2017", "12.05.2017"});
            Assert.AreEqual(expected, period.ToString());
        }
    }
}