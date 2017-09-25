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
            NumberOfSidesOptions = new ObservableCollection<SideOption>(Enumerable.Range(2, MaxNumberOfSides - 1)
                    .Select(s => new SideOption
                    {
                        SideDisplayName = "D" + s,
                        NumberOfSides = s
                    }));
            NumberOfDiceOptions = new ObservableCollection<int>(Enumerable.Range(1, MaxNumberOfDice));
            NumberOfSides = NumberOfSidesOptions.First();
            NumberOfDice = 1;
        }

        public ICommand RollDiceCommand { get; }

        public int MaxNumberOfSides => 1000;

        public int MaxNumberOfDice => 50;

        public ObservableCollection<SideOption> NumberOfSidesOptions { get; }

        public ObservableCollection<int> NumberOfDiceOptions { get; }

        private SideOption _numberOfSides;
        public SideOption NumberOfSides
        {
            get { return _numberOfSides; }
            set
            {
                _numberOfSides = value;
                RaisePropertyChanged();

                if (_numberOfDice != 0 && _numberOfSides.NumberOfSides != 0)
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
                    NumberOfSides = NumberOfSides.NumberOfSides,
                };

                Dice.Add(dice);
            }
        }

        private void ChangeDiceSides()
        {
            foreach(var dice in Dice)
            {
                dice.NumberOfSides = NumberOfSides.NumberOfSides;
                dice.SideUp = 1;
            }
        }
    }
}
