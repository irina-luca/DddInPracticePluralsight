using System;
using System.Linq;
using static DddInPracticeSandbox.Logic.Money;

namespace DddInPracticeSandbox.Logic
{
    public /*sealed*/ class SnackMachine : Entity
    {
        public virtual Money MoneyInside { get; /*private*/protected set; } = None; // The syntax "= None;" is part of property initializers in C# 6 (instead of the commented out ctor below).
        public virtual Money MoneyInTransaction { get; /*private*/protected set; } = None; // The syntax "= None;" is part of property initializers in C# 6 (instead of the commented out ctor below).

        //public SnackMachine()
        //{
        //    MoneyInside = None;
        //    MoneyInTransaction = None;
        //}

        public virtual void InsertMoney(Money money)
        {
            var allowedCoinsAndNotes = new Money[] {Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar};
            if (!allowedCoinsAndNotes.Contains(money))
                throw new InvalidOperationException();

            MoneyInTransaction += money;
        }

        public virtual void ReturnMoney()
        {
            MoneyInTransaction = None; // We do not go for a solution like calling some method e.g. .Clear(), as we don't want to mutate the state, but respect the immutability, therefore we set it to a brand new object.
        }

        public virtual void BuySnack()
        {
            MoneyInside += MoneyInTransaction;

            MoneyInTransaction = None;
        }
    }
}
