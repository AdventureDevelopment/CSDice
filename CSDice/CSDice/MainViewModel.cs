using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CSDice
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            RollDiceCommand = new RelayCommand(RollDice);
            NumberOfSides = 2;
            NumberOfDice = 1;
        }

        public void Initialize()
        {

        }

        public ICommand RollDiceCommand { get; }

        public int MaxNumberOfSides => 1000;

        public int MaxNumberOfDice => 50;

        public IList<int> NumberOfSidesOptions
        {
            get
            {
                return Enumerable.Range(2, MaxNumberOfSides + 1).ToList();
            }
        }

        public IList<int> NumberOfDiceOptions
        {
            get
            {
                return Enumerable.Range(1, MaxNumberOfDice + 1).ToList();
            }
        }

        public int NumberOfSidesAsIndex
        {
            get { return NumberOfSides - 1; }
            set
            {
                NumberOfSides = value + 1;
                RaisePropertyChanged();
            }
        }

        public int NumberOfDiceAsIndex
        {
            get { return NumberOfDice - 1; }
            set
            {
                NumberOfDice = value + 1;
                RaisePropertyChanged();
            }
        }

        private int _numberOfSides;
        public int NumberOfSides
        {
            get { return _numberOfSides; }
            set
            {
                _numberOfSides = value;
                RaisePropertyChanged();

                if (_numberOfDice != 0 && _numberOfSides != 0)
                {
                    ChangeDiceSides();
                }
            }
        }

        private int _numberOfDice;
        public int NumberOfDice
        {
            get { return _numberOfDice; }
            set
            {
                _numberOfDice = value;
                RaisePropertyChanged();

                if (_numberOfDice > 0)
                {
                    ChangeDice();
                }
            }
        }

        public ObservableCollection<DiceViewModel> Dice { get; set; } = new ObservableCollection<DiceViewModel>();

        public void RollDice()
        {
            foreach(var dice in Dice)
            {
                dice.RollDice();
            }
        }

        private void ChangeDice()
        {
            Dice.Clear();

            for(var count = 0; count < NumberOfDice; count++)
            {
                var dice = new DiceViewModel
                {
                    NumberOfSides = NumberOfSides,
                };

                Dice.Add(dice);
            }
        }

        private void ChangeDiceSides()
        {
            foreach(var dice in Dice)
            {
                dice.NumberOfSides = NumberOfSides;
                dice.SideUp = 1;
            }
        }
    }
}
