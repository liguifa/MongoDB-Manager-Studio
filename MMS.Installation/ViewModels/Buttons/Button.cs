using MMS.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MMS.Installation
{
    public abstract class Button : BaseINotifyPropertyChanged
    {
        private string mText = String.Empty;
        public string Text { get { return this.mText; } set { this.mText = value; OnPropertyChanged("Text"); } }

        private bool mIsEnable = false;
        public bool IsEnable { get { return this.mIsEnable; } set { this.mIsEnable = value; OnPropertyChanged("IsEnable"); } }

        private Visibility mButtonVisiblity = Visibility.Visible;
        public Visibility ButtonVisiblity { get { return this.mButtonVisiblity; } set { this.mButtonVisiblity = value; OnPropertyChanged("ButtonVisiblity"); } }

        private ICommand mCommand = null;
        public ICommand Command { get { return this.mCommand; } set { this.mCommand = value; OnPropertyChanged("Command"); } }
    }
}
