using MMS.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Installation
{
    public class MainWindowViewModel : BaseINotifyPropertyChanged
    {
        private static MainWindowViewModel mainWindow;
        private static readonly object syncRoot = new object();

        private MainWindowViewModel()
        {

        }

        public static MainWindowViewModel GetInstance()
        {
            if (mainWindow == null)
            {
                lock (syncRoot)
                {
                    if (mainWindow == null)
                    {
                        mainWindow = new MainWindowViewModel();
                    }
                }
            }
            return mainWindow;
        }

        private BackButton mBackButton = new BackButton();
        public BackButton BackButton { get { return this.mBackButton; } set { this.mBackButton = value; OnPropertyChanged("BackButton"); } }

        private NextButton mNextButton = new NextButton();
        public NextButton NextButton { get { return this.mNextButton; } set { this.mNextButton = value; OnPropertyChanged("NextButton"); } }

        private CancelButton mCancelButton = new CancelButton();
        public CancelButton CancelButton { get { return this.mCancelButton; } set { this.mCancelButton = value; OnPropertyChanged("CancelButton"); } }

        private Navigation mNavigation = Navigation.GetInstance();
        public Navigation Navigation { get { return this.mNavigation; } set { this.mNavigation = value; OnPropertyChanged("Navigation"); } }
    }
}
