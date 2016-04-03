using Common.Logger;
using MMS.Command;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MMS.MongoDB
{
    public class Brown
    {
        private static readonly Logger mLog = Logger.GetInstance(MethodBase.GetCurrentMethod().DeclaringType);
        private MongoServer mServer;

        public Brown(MongoServer server)
        {
            this.mServer = server;
        }
        public MongoDBTree GetMongoDBTree()
        {
            try
            {
                List<string> dbs = this.mServer.GetDatabaseNames().ToList();
                mLog.Info("Get database success,db count:[{0}]", dbs.Count().ToString());
                return this.BuildTree(dbs);
            }
            catch (Exception e)
            {
                mLog.Error("An error has occurred in the get databases.error:{0}", e.ToString());
                throw new Exception("获取数据库列表失败.");
            }
        }

        private MongoDBTree BuildTree(List<string> dbs)
        {
            try
            {
                mLog.Info("Start build mongodb database tree.");
                MongoDBTree tree = new MongoDBTree();
                tree.Name = mServer.Instance.Address.ToString();
                tree.NodeType = MongoDBTreeNodeType.Server;
                tree.Children = new List<MongoDBTree>();
                foreach (string db in dbs)
                {
                    mLog.Info("build database:[{0}]", db);
                    MongoDBTree dbTree = new MongoDBTree();
                    dbTree.Name = db;
                    dbTree.NodeType = MongoDBTreeNodeType.Docmenu;
                    dbTree.Children = new List<MongoDBTree>();
                    MongoDatabase database = this.mServer.GetDatabase(db);
                    List<string> collections = database.GetCollectionNames().ToList();
                    mLog.Info("get collections count:{0}", collections.Count.ToString());
                    foreach (string item in collections)
                    {
                        mLog.Info("build collection:[{0}]", item);
                        MongoDBTree itemTree = new MongoDBTree();
                        itemTree.Name = item;
                        itemTree.NodeType = MongoDBTreeNodeType.Docmenu;
                        dbTree.Children.Add(itemTree);
                    }
                    tree.Children.Add(dbTree);
                }
                return tree;
            }
            catch (Exception e)
            {
                mLog.Error("An error has occurred in the build mongodb database tree,error:{0}", e.ToString());
                throw;
            }
        }
    }
}
