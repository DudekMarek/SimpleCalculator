using FirstWPFApp.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        private List<string> _availableOperations = new List<string> {"/", "*", "-", "+"};
        private DataTable _dataTable = new DataTable();
        private bool _isLastSignAnOperation;
        private bool _isResultOnScrean;

        

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
            AddOperationCommand = new RelayCommand(AddOperation, CanAddOperation);
            ClearScreenCommand = new RelayCommand(ClearScreen);
            GetResultCommand = new RelayCommand(GetResult, CanGetResult);

            _screenVal = "0";
        }

        private bool CanGetResult(object obj) => !_isLastSignAnOperation;

        private bool CanAddOperation(object obj) => !_isLastSignAnOperation;

        private void AddNumber(object obj)
        {
            var number = (string)obj;

            if (ScreenVal == "0" && number != ",") ScreenVal = string.Empty;
            else if (number == "," && _availableOperations.Contains(ScreenVal.Substring(ScreenVal.Length - 1))) number = "0,";
            else if (_isResultOnScrean)
            {
                ScreenVal = String.Empty;
                _isResultOnScrean = false;
            }

            ScreenVal += number;

            _isLastSignAnOperation = false;
        }

        private void AddOperation(object obj)
        {
            var operation = (string)obj;

            ScreenVal += operation;

            _isLastSignAnOperation = true;

            _isResultOnScrean = false;
        }
        private void ClearScreen(object obj)
        {
            ScreenVal = "0";

            _isLastSignAnOperation = false;

            _isResultOnScrean = false;
        }

        private void GetResult(object obj)
        {
            var result = _dataTable.Compute(ScreenVal.Replace(",", "."), "").ToString();

            ScreenVal = result;

            _isResultOnScrean = true;
        }

        

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
