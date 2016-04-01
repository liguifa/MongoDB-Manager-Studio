using MMS.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.MongoDB
{
    public class Brown
    {

        private readonly RunCommand mRunSpace;

        public Brown(RunCommand runSpace)
        {
            this.mRunSpace = runSpace;
        }
        public MongoDBTree GetMongoDBTree()
        {
            MMS.Command.Command showCmd = CommandFactory.Create(CommandType.Show, new CommandParameter());
            List<string> outStr = this.mRunSpace.Run(showCmd);
            return this.BuildTree(outStr);
        }

        private MongoDBTree BuildTree(List<string> dbs)
        {
            MongoDBTree tree = new MongoDBTree();
            tree.Name = "127.0.0.1";
            tree.NodeType = MongoDBTreeNodeType.Server;
            tree.Children = new List<MongoDBTree>();

            MongoDBTree docMenu = new MongoDBTree();
            docMenu.Name = "Document";
            docMenu.NodeType = MongoDBTreeNodeType.Menu;
            docMenu.Children = new List<MongoDBTree>();
            foreach (string db in dbs)
            {
                MongoDBTree dbTree = new MongoDBTree();
                dbTree.Name = db;
                dbTree.NodeType = MongoDBTreeNodeType.Docmenu;
                docMenu.Children.Add(dbTree);
            }
            tree.Children.Add(docMenu);

            return tree;
        }
    }
}
