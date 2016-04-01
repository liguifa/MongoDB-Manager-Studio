using MMS.Command;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.MongoDB
{
    public class Brown
    {
        private MongoServer mServer;

        public Brown(MongoServer server)
        {
            this.mServer = server;
        }
        public MongoDBTree GetMongoDBTree()
        {
            List<string> dbs = this.mServer.GetDatabaseNames().ToList();
            return this.BuildTree(dbs);
        }

        private MongoDBTree BuildTree(List<string> dbs)
        {
            MongoDBTree tree = new MongoDBTree();
            tree.Name = "127.0.0.1";
            tree.NodeType = MongoDBTreeNodeType.Server;
            tree.Children = new List<MongoDBTree>();
            foreach (string db in dbs)
            {
                MongoDBTree dbTree = new MongoDBTree();
                dbTree.Name = db;
                dbTree.NodeType = MongoDBTreeNodeType.Docmenu;
                dbTree.Children = new List<MongoDBTree>();
                MongoDatabase database = this.mServer.GetDatabase(db);
                foreach (string item in database.GetCollectionNames())
                {
                    MongoDBTree itemTree = new MongoDBTree();
                    itemTree.Name = item;
                    itemTree.NodeType = MongoDBTreeNodeType.Docmenu;
                    dbTree.Children.Add(itemTree);
                }
                tree.Children.Add(dbTree);
            }
            return tree;
        }
    }
}
