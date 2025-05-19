using System;

namespace Application.Services
{
    public class MovesService
    {
        public event Action<int> MovesAmountChanged;

        private int _amount;

        public void SetAmount(int amount)
        {
            _amount = amount;
            MovesAmountChanged?.Invoke(_amount);
        }

        public int GetAmount() => _amount;

        public void Increase()
        {
            _amount++;
            MovesAmountChanged?.Invoke(_amount);
        }
    }
}