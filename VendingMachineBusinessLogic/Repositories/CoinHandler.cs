using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachineBusinessLogic
{
    public class CoinHandler
    {

        int quarters;
        int dimes;
        int nickels;
        decimal loadedAmount;
        QuarterFeatures quarterValue;
        DimeFeatures dimeValue;
        NickelFeatures nickelValue;
        public CoinHandler()
        {
            nickelValue = new NickelFeatures();
            dimeValue = new DimeFeatures();
            quarterValue = new QuarterFeatures();
        }

        public void AddCoins(USCoinTypes type, int number)
        {
            switch (type)
            {
                case USCoinTypes.Nickel:
                    nickels += number;
                    break;
                case USCoinTypes.Dime:
                    dimes += number;
                    break;
                case USCoinTypes.Quarter:
                    quarters += number;
                    break;
                case USCoinTypes.Penny:
                    break;
                default:
                    break;
            }
            updateAmount();
        }

        private void updateAmount()
        {
            loadedAmount = nickels * nickelValue.Value + dimes * dimeValue.Value + quarters * quarterValue.Value;
        }

        public void RemoveCoins(USCoinTypes type, int number)
        {
            switch (type)
            {
                case USCoinTypes.Nickel:
                    nickels -= number;
                    break;
                case USCoinTypes.Dime:
                    dimes -= number;
                    break;
                case USCoinTypes.Quarter:
                    quarters -= number;
                    break;
                case USCoinTypes.Penny:
                    break;
                default:
                    break;
            }
            updateAmount();
        }

        public bool NotEnoughChange => ((dimes < 1 && nickels < 1) || nickels < 3 ) ? true : false;        

        public Dictionary<USCoinTypes, int> GetMoneyReturned(decimal amount)
        {
            var coinsReturned = new Dictionary<USCoinTypes, int>
            {
                { USCoinTypes.Quarter, 0}, { USCoinTypes.Dime, 0 }, { USCoinTypes.Nickel, 0 }
            };
            
            while (amount>0)
            {
                if (amount >= quarterValue.Value && quarters > 0)
                {
                    quarters--;
                    amount -= quarterValue.Value;
                    coinsReturned[USCoinTypes.Quarter]++;
                }
                else if (amount >= dimeValue.Value && dimes > 0)
                {
                    dimes--;
                    amount -= dimeValue.Value;
                    coinsReturned[USCoinTypes.Dime]++;
                }
                else if (amount >= nickelValue.Value && nickels > 0)
                {
                    nickels--;
                    amount -= nickelValue.Value;
                    coinsReturned[USCoinTypes.Nickel]++;
                }
                else
                    break;
            }

            return coinsReturned;
        }

        public void EmptyCoins()
        {
            quarters = dimes = nickels = 0;
        }
    }
}