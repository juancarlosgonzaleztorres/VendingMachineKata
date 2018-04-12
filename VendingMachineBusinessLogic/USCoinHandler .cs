using System.Collections.Generic;

namespace VendingMachineBusinessLogic
{
    public class USCoinHandler : ICoinHandler
    {
        int quarters;
        int dimes;
        int nickels;
        decimal loadedAmount;
        USQuarterFeatures quarterValue;
        USDimeFeatures dimeValue;
        USNickelFeatures nickelValue;
        public USCoinHandler()
        {
            nickelValue = new USNickelFeatures();
            dimeValue = new USDimeFeatures();
            quarterValue = new USQuarterFeatures();
        }

        public void InsertCoin(CoinTypes coinType, int number)
        {
            switch (coinType)
            {
                case CoinTypes.USNickel:
                    nickels += number;
                    break;
                case CoinTypes.USDime:
                    dimes += number;
                    break;
                case CoinTypes.USQuarter:
                    quarters += number;
                    break;
                case CoinTypes.USPenny:
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

        public bool NotEnoughChange => ((dimes < 1 && nickels < 1) || nickels < 3 ) ? true : false;        

        public Dictionary<CoinTypes, int> GetMoneyReturned(decimal amount)
        {
            var coinsReturned = new Dictionary<CoinTypes, int>
            {
                { CoinTypes.USQuarter, 0},
                { CoinTypes.USDime, 0 },
                { CoinTypes.USNickel, 0 }
            };
            
            while (amount>0)
            {
                if (amount >= quarterValue.Value && quarters > 0)
                {
                    quarters--;
                    amount -= quarterValue.Value;
                    coinsReturned[CoinTypes.USQuarter]++;
                }
                else if (amount >= dimeValue.Value && dimes > 0)
                {
                    dimes--;
                    amount -= dimeValue.Value;
                    coinsReturned[CoinTypes.USDime]++;
                }
                else if (amount >= nickelValue.Value && nickels > 0)
                {
                    nickels--;
                    amount -= nickelValue.Value;
                    coinsReturned[CoinTypes.USNickel]++;
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

        public decimal GetValueOfMoney(Dictionary<CoinTypes, int> moneyReturned)
        {
            return (moneyReturned == null) ? 0.00m :
                     moneyReturned[CoinTypes.USQuarter] * quarterValue .Value
                   + moneyReturned[CoinTypes.USDime] * dimeValue.Value
                   + moneyReturned[CoinTypes.USNickel] * nickelValue.Value;
        }

        public bool IsValid(ICoin usCoin)
        {
            return (usCoin.Type() == CoinTypes.USNickel ||
                    usCoin.Type() == CoinTypes.USDime ||
                    usCoin.Type() == CoinTypes.USQuarter) ? true : false;
        } 
    }
}