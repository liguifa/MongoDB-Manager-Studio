﻿using MMS.UI.Common;
using MMS.UI.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.UnInstallation
{
    public class Navigation : BaseINotifyPropertyChanged
    {
        private List<NavigationItem> mItems = new List<NavigationItem>();
        public List<NavigationItem> Items { get { return this.mItems; } }

        private static Navigation navigation;
        private static readonly object syncRoot = new object();

        private int mCurrent = 0;

        private Uri mCurrentPage = null;
        public Uri CurrentPage { get { return this.mCurrentPage; } set { this.mCurrentPage = value; OnPropertyChanged("CurrentPage"); } }

        public static Navigation GetInstance()
        {
            if (navigation == null)
            {
                lock (syncRoot)
                {
                    if (navigation == null)
                    {
                        navigation = new Navigation();
                    }
                }
            }
            return navigation;
        }

        private Navigation()
        {
            NavigationItem confirm = new NavigationItem()
            {
                Text = "确认卸载",
                Status = NavigationType.Process,
                Url = new Uri("/Views/Confirm.xaml", UriKind.Relative)
            };
            NavigationItem unInstall = new NavigationItem()
            {
                Text = "正在卸载",
                Status = NavigationType.Wait,
                Url = new Uri("/Views/Process.xaml", UriKind.Relative)
            };
            NavigationItem finish = new NavigationItem()
            {
                Text = "卸载完成",
                Status = NavigationType.Wait,
                Url = new Uri("/Views/Finish.xaml", UriKind.Relative)
            };
            this.mItems.Add(confirm);
            this.mItems.Add(unInstall);
            this.mItems.Add(finish);
            this.CurrentPage = this.mItems[this.mCurrent].Url;
        }

        public void Next()
        {
            this.mItems[this.mCurrent].Status = NavigationType.Complete;
            this.mCurrent++;
            this.CurrentPage = this.mItems[this.mCurrent].Url;
            this.mItems[this.mCurrent].Status = NavigationType.Process;
            this.UpdateButtonStatus();
        }

        public void Back()
        {
            this.mItems[this.mCurrent].Status = NavigationType.Wait;
            this.mCurrent--;
            this.CurrentPage = this.mItems[this.mCurrent].Url;
            this.mItems[this.mCurrent].Status = NavigationType.Process;
            this.UpdateButtonStatus();
        }

        private void UpdateButtonStatus()
        {
            if (this.mCurrent == 0)
            {
                MainWindowViewModel.GetInstance().BackButton.ButtonVisiblity = System.Windows.Visibility.Hidden;
            }
            else
            {
                MainWindowViewModel.GetInstance().BackButton.ButtonVisiblity = System.Windows.Visibility.Visible;
            }
            if (this.mCurrent >= this.Items.Count - 2)
            {
                MainWindowViewModel.GetInstance().BackButton.IsEnabled = false;
                MainWindowViewModel.GetInstance().NextButton.IsEnabled = false;
            }
            if (this.mCurrent == this.Items.Count - 1)
            {
                MainWindowViewModel.GetInstance().BackButton.ButtonVisiblity = System.Windows.Visibility.Collapsed;
                MainWindowViewModel.GetInstance().NextButton.IsEnabled = true;
                MainWindowViewModel.GetInstance().NextButton.Text = "完  成";
                MainWindowViewModel.GetInstance().NextButton.Command = new FinishCommand();
                MainWindowViewModel.GetInstance().CancelButton.ButtonVisiblity = System.Windows.Visibility.Collapsed;
            }
        }
    }
}
