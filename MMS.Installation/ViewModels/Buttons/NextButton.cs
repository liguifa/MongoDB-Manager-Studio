using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MMS.Installation
{
    public class NextButton : Button
    {
        public NextButton()
        {
            this.Text = "下一步";
            this.IsEnable = false;
            this.ButtonVisiblity = Visibility.Visible;
            this.Command = new NextCommand();
        }
    }

    public class NextCommand : ICommand
    {

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            Navigation.GetInstance().Next();
        }
    }
}
