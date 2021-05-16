using System;
using System.Globalization;
using DddInPracticeSandbox.Logic;
using FluentAssertions;
using Xunit;

namespace DddInPracticeSandbox.Tests
{
    public class MoneySpecs
    {
        [Fact] 
        public void Sum_of_two_money_produces_correct_result()
        {
            var money1 = new Money(1, 2, 3, 4, 5, 6);
            var money2 = new Money(1, 2, 3, 4, 5, 6);

            var sum = money1 + money2;

            sum.OneCentCount.Should().Be(2);
            sum.TenCentCount.Should().Be(4);
            sum.QuarterCount.Should().Be(6);
            sum.OneDollarCount.Should().Be(8);
            sum.FiveDollarCount.Should().Be(10);
            sum.TwentyDollarCount.Should().Be(12);
        }

        [Fact]
        public void Two_money_instances_equal_if_they_have_the_amount()
        {
            var money1 = new Money(1, 2, 3, 4, 5, 6);
            var money2 = new Money(1, 2, 3, 4, 5, 6);

            money1.Should().Be(money2);
            money1.GetHashCode().Should().Be(money2.GetHashCode());
        }

        [Fact]
        public void Two_money_instances_do_not_equal_when_containing_different_amounts()
        {
            var money1 = new Money(1, 2, 3, 4, 5, 6);
            var money2 = new Money(2, 2, 3, 4, 5, 6);

            money1.Should().NotBe(money2);
            money1.GetHashCode().Should().NotBe(money2.GetHashCode());
        }

        // Parameterized test
        [Theory]
        [InlineData(-1, 0, 0, 0, 0, 0)]
        [InlineData(0, -2, 0, 0, 0, 0)]
        [InlineData(0, 0, -3, 0, 0, 0)]
        [InlineData(0, 0, 0, -4, 0, 0)]
        [InlineData(0, 0, 0, 0, -5, 0)]
        [InlineData(0, 0, 0, 0, 0, -6)]
        public void Cannot_create_money_with_negative_value(
            int oneCentCount,
            int tenCentCount,
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount)
        {
            Func<Money> action = () => new Money(oneCentCount, tenCentCount, quarterCount, oneDollarCount, fiveDollarCount, twentyDollarCount);

            action.Should().Throw<InvalidOperationException>();
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0)]
        [InlineData(1, 0, 0, 0, 0, 0, 0.01)]
        [InlineData(1, 2, 0, 0, 0, 0, 0.21)]
        [InlineData(1, 2, 4, 0, 0, 0, 1.21)]
        [InlineData(1, 2, 4, 1, 0, 0, 2.21)]
        [InlineData(1, 2, 4, 1, 2, 0, 12.21)]
        [InlineData(1, 2, 4, 1, 2, 3, 72.21)]
        public void Amount_is_calculated_correctly(
            int oneCentCount,
            int tenCentCount,
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount,
            decimal expectedAmount)
        {
            var money = new Money(oneCentCount, tenCentCount, quarterCount, oneDollarCount, fiveDollarCount, twentyDollarCount);

            money.Amount.Should().Be(expectedAmount);
        }

        [Fact]
        public void Subtraction_of_two_money_produces_correct_result()
        {
            var money1 = new Money(10, 10, 10, 10, 10, 10);
            var money2 = new Money(1, 2, 3, 4, 5, 6);

            var result = money1 - money2;

            result.OneCentCount.Should().Be(9);
            result.TenCentCount.Should().Be(8);
            result.QuarterCount.Should().Be(7);
            result.OneDollarCount.Should().Be(6);
            result.FiveDollarCount.Should().Be(5);
            result.TwentyDollarCount.Should().Be(4);
        }

        [Fact]
        public void Cannot_subtract_more_than_exists()
        {
            var money1 = new Money(0, 1, 0, 0, 0, 0);
            var money2 = new Money(1, 0, 0, 0, 0, 0);

            Action action = () =>
            {
                Money result = money1 - money2;
            };

            action.Should().Throw<InvalidOperationException>();
        }

        [Theory]
        [InlineData(1, 0, 0, 0, 0, 0, "$1")] // 1 cent
        [InlineData(1, 2, 0, 0, 0, 0, "$21")] // 21 cents
        [InlineData(1, 2, 4, 0, 0, 0, "$1.21")]
        [InlineData(1, 2, 4, 1, 0, 0, "$2.21")]
        [InlineData(1, 2, 4, 1, 2, 0, "$12.21")]
        [InlineData(1, 2, 4, 1, 2, 3, "$72.21")]
        public void To_string_should_return_amount_of_money(
            int oneCentCount,
            int tenCentCount,
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount,
            string expectedString)
        {
            var money = new Money(oneCentCount, tenCentCount, quarterCount, oneDollarCount, fiveDollarCount, twentyDollarCount);

            money.ToString().Should().Be(expectedString);
        }
    }
}
