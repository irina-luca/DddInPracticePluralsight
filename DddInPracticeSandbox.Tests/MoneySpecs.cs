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

            Assert.Equal(2, sum.OneDollarCount);
            Assert.Equal(4, sum.TenCentCount);
        }
    }
}
