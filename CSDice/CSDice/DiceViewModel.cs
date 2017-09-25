using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSDice
{
    public class DiceViewModel : ViewModelBase
    {
        private Random _randomNumber;

        public DiceViewModel()
        {
            SideUp = 1;
            _randomNumber = new Random();
        }

        private int _numberOfSides;
        public int NumberOfSides
        {
            get { return _numberOfSides; }
            set
            {
                _numberOfSides = value;
                RaisePropertyChanged();
            }
        }

        private int _sideUp;
        public int SideUp
        {
            get { return _sideUp; }
            set
            {
                _sideUp = value;
                RaisePropertyChanged();
            }
        }

        public void RollDice()
        {
            SideUp = _randomNumber.Next(1, NumberOfSides + 1);
        }
    }
}
