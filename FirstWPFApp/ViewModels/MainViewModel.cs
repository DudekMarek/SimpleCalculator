using FirstWPFApp.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FirstWPFApp.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private string _screenVal;

        private string operation;

        private decimal? firstNumber;

        private decimal? secondNumber;

        private bool isRsultOnScrean = false;

        public string ScreenVal
        {
            get { return _screenVal; }
            set 
            {
                _screenVal = value; 
                OnPropertyChanged();
            }
        }

        public ICommand AddNumberCommand { get; set; }
        public ICommand AddOperationCommand { get; set; }
        public ICommand ClearScreenCommand { get; set; }
        public ICommand GetResultCommand { get; set; }

        public MainViewModel()
        {
            AddNumberCommand = new RelayCommand(AddNumber);
            AddOperationCommand = new RelayCommand(AddOperation);
            ClearScreenCommand = new RelayCommand(ClearScreen);
            GetResultCommand = new RelayCommand(GetResult);

        }
        

        private void AddNumber(object obj)
        {
            string num = obj.ToString();
            ScreenVal += num;
        }

        private void AddOperation(object obj)
        {
            if (!isRsultOnScrean)
            {
                operation = obj.ToString();
                firstNumber = decimal.Parse(ScreenVal);
                ScreenVal = null;
            }
            else
            {
                firstNumber = decimal.Parse(ScreenVal);
                ScreenVal = null;
                operation = obj.ToString();
                isRsultOnScrean = true;
            }
        }
        private void ClearScreen(object obj)
        {
            ScreenVal = null;
        }

        private void GetResult(object obj)
        {
            secondNumber = decimal.Parse(ScreenVal);
            isRsultOnScrean = true;

            switch (operation)
            {
                case "*":
                    ScreenVal = (firstNumber * secondNumber).ToString();
                    break;
                case "+":
                    ScreenVal = (firstNumber + secondNumber).ToString();
                    break;
                case "/":
                    if(secondNumber == 0)
                    {
                        ScreenVal = "Error";
                    }
                    else
                    {
                        ScreenVal = (firstNumber / secondNumber).ToString();
                    }
                    break;
                case "-":
                    ScreenVal = (firstNumber - secondNumber).ToString();
                    break;
            }
        }

        private void ClearResult()
        {
            if (isRsultOnScrean)
            {
                ScreenVal = null;
                operation = null;
                firstNumber = null;
                secondNumber = null;

                isRsultOnScrean = false;
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
