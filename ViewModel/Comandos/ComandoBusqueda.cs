using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1.ViewModel.Comandos
{
    public class ComandoBusqueda : ICommand
    {           
        public event EventHandler? CanExecuteChanged;

        ClimaViewModel _cvm;

        public ComandoBusqueda(ClimaViewModel cvm)
        {
            _cvm = cvm;
        }

        public bool CanExecute(object? parameter)
        {
            string q = parameter as string;
            if (string.IsNullOrEmpty(q)) return false;
            else return true;
        }

        public void Execute(object? parameter)
        {
            _cvm.hacerQuery();
        }
    }
}
