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
            this.ConnectMongoDB();
        }

        private void ConnectMongoDB()
        {
            MongoDBTree tree = MongoDBManager.GetInstance().GetBrown().GetMongoDBTree();
            List<ExplorerItem> items = new List<ExplorerItem>();
            items.Add(this.ParseMongoDBTree(tree));
            this.explorer.UpdateSource(items);
        }

        public ExplorerItem ParseMongoDBTree(MongoDBTree tree)
        {
            ExplorerItem m = new ExplorerItem();
            m.Text = tree.Name;
            m.Type = ExplorerItemType.Docmenut;
            if (tree.NodeType == MongoDBTreeNodeType.Server)
            {
                m.Type = ExplorerItemType.Server;
            }
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
