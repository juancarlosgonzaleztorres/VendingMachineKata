using System.Collections.Generic;

namespace VendingMachineBusinessLogic
{
    public interface ICoinHandler
    {
        bool NotEnoughChange { get; }
        void InsertCoin(CoinTypes type, int number);
        void EmptyCoins();
        Dictionary<CoinTypes, int> GetMoneyReturned(decimal amount);        
        decimal GetValueOfMoney(Dictionary<CoinTypes, int> moneyReturned);
        bool IsValid(ICoin usCoin);
    }
}