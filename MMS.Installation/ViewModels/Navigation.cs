using MMS.UI.Common;
using MMS.UI.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Installation
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
            NavigationItem license = new NavigationItem()
            {
                Text = "用户协议",
                Status = NavigationType.Process,
                Url = new Uri("/Views/License.xaml", UriKind.Relative)
            };
            NavigationItem installPath = new NavigationItem()
            {
                Text = "安装路径",
                Status = NavigationType.Wait,
                Url = new Uri("/Views/InstallPath.xaml", UriKind.Relative)
            };
            NavigationItem process = new NavigationItem()
            {
                Text = "软件设置",
                Status = NavigationType.Wait,
                Url = new Uri("/Views/MongoDBSet.xaml", UriKind.Relative)
            };
            NavigationItem install = new NavigationItem()
            {
                Text = "正在安装",
                Status = NavigationType.Wait,
                Url = new Uri("/Views/Process.xaml", UriKind.Relative)
            };
            NavigationItem finish = new NavigationItem()
            {
                Text = "安装完成",
                Status = NavigationType.Wait,
                Url = new Uri("/Views/Finish.xaml", UriKind.Relative)
            };
            this.mItems.Add(license);
            this.mItems.Add(installPath);
            this.mItems.Add(process);
            this.mItems.Add(install);
            this.mItems.Add(finish);

            this.CurrentPage = this.mItems[this.mCurrent].Url;
        }

        public void Next()
        {
            this.mItems[this.mCurrent].Status = NavigationType.Complete;
            this.mCurrent++;
            this.CurrentPage = this.mItems[this.mCurrent].Url;
            this.mItems[this.mCurrent].Status = NavigationType.Process;
        }
    }
}
