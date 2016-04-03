using Common.Cryptogram;
using Common.Logger;
using MMS.Command;
using MMS.Config;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MMS.MongoDB
{
    public class MongoDBManager
    {
        private static readonly Logger mLog = Logger.GetInstance(MethodBase.GetCurrentMethod().DeclaringType);
        public static MongoDBManager mMongoDB;
        private static readonly object syncRoot = new object();
        private MongoServer mServer;

        private MongoDBManager()
        {
            this.RunMongod();
        }

        private void RunMongod()
        {
            string mongod = String.Empty;
            try
            {
                mongod = Config.Config.GetInstance().MongodPath;
                mLog.Info("Start mongod,file:{0}.", mongod);
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo();
                process.StartInfo.FileName = mongod;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.OutputDataReceived += Process_OutputDataReceived;
                process.Start();
                process.BeginOutputReadLine();
                Thread.Sleep(2000);
                mLog.Info("Start mongod success.");
            }
            catch (FileNotFoundException e)
            {
                mLog.Error("An error has occurred in the run mongod,error {0}", e.ToString());
                throw;
            }
            catch (Exception e)
            {
                mLog.Error("An error has occurred in the run mongod,error {0}", e.ToString());
                throw new Exception(String.Format("未知异常,{0}", e.Message));
            }
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            mLog.Info(e.Data);
        }

        public void Connect(string address, string port, string username, string password)
        {
            try
            {
                mLog.Info("Start conect mongodb,address:[{0}],port:[{1}],username:[{2}],password:[{3}]", address, port, username, Base64.Encrypt(password));
                string strconn = String.Format("mongodb://{0}:{1}", address, port);
                this.mServer = MongoServer.Create(strconn);
                if (this.mServer == null)
                {
                    throw new Exception("连接失败.");
                }
                mLog.Info("Connect mongodb success.");
            }
            catch (Exception e)
            {
                mLog.Error("An error has occurred in the run mongod,error {0}", e.ToString());
                throw new Exception("MongoDB 连接失败,请检查连接数据是否正确.");
            }
        }

        public static MongoDBManager GetInstance()
        {
            if (mMongoDB == null)
            {
                lock (syncRoot)
                {
                    if (mMongoDB == null)
                    {
                        mMongoDB = new MongoDBManager();
                    }
                }
            }
            return mMongoDB;
        }

        public Brown GetBrown()
        {
            return new Brown(this.mServer);
        }

        public void CreateCollection(string databaseName, string collectionName)
        {
            try
            {
                mLog.Info("Start create collection:database:[{0}],collection:[{0}]", databaseName, collectionName);
                MongoDatabase database = this.mServer.GetDatabase(databaseName);
                database.CreateCollection(collectionName);
            }
            catch(Exception e)
            {

            }

        }
    }
}
