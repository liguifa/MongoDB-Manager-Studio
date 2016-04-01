using MMS.Client.ViewModels;
using MMS.Grammar;
using MMS.MongoDB;
using MMS.UI.Default;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace MMS.Client
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MMS.UI.Default.Window
    {
        private GrammarAnalysis ga = new GrammarAnalysis();

        public MainWindow()
        {
            InitializeComponent();
            this.menu.MenuItems = MainWindowViewModel.GetInstance().MenuItems;
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Connection connection = new Connection();
            //connection.Show();
            UI.Default.Table table = new UI.Default.Table();
            table.Source = "[{\"id\":\"1\",\"Name\":\"李贵发\",\"Age\":\"23\"},{\"id\":\"2\",\"Name\":\"张山\",\"Age\":\"63\"},{\"id\":\"3\",\"Name\":\"王二\",\"Age\":\"41\"}]";
            this.tabs.Add("VM5310SQL11-WSS_Context", table);

            UI.Default.Edit edit = new Edit();
            edit.TextChanged += ga.StartAnalysis;
            this.tabs.Add("VM5310SQL11-WSS_Context.Code", edit);
            //table.UpdateContext();
            MMS.UI.Default.Connection connectWindow = new MMS.UI.Default.Connection();
            connectWindow.OKButton_Click += connectWindow_OKClick_Event;
            connectWindow.Show();
        }

        void connectWindow_OKClick_Event()
        {
            MongoDBManager.GetInstance().GetConnect().Connect("", "", "", "");
            MongoDBTree tree = MongoDBManager.GetInstance().GetBrown().GetMongoDBTree();
            List<ExplorerItem> items = new List<ExplorerItem>();
            items.Add(this.ParseMongoDBTree(tree));
            this.explorer.UpdateSource(items);
        }

        public ExplorerItem ParseMongoDBTree(MongoDBTree tree)
        {
            ExplorerItem m = new ExplorerItem();
            m.Text = tree.Name;
            m.Icon = "/MMS.UI;Component/Default/Explorer/Images/Database.ico";
            if (tree.Children != null && tree.Children.Count > 0)
            {
                m.Children = new List<ExplorerItem>();
                foreach (MongoDBTree t in tree.Children)
                {
                    m.Children.Add(this.ParseMongoDBTree(t));
                }
            }
            return m;
        }
    }
}
