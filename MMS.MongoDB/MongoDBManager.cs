using MMS.Command;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MMS.MongoDB
{
    public class MongoDBManager
    {
        public static MongoDBManager mMongoDB;
        private static readonly object syncRoot = new object();
        private MongoServer mServer;

        private MongoDBManager()
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = @"C:\Program Files\Mongo\bin\Mongod.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            //process.OutputDataReceived += Process_OutputDataReceived;
            process.Start();
            process.BeginOutputReadLine();
            Thread.Sleep(2000);

            string strconn = "mongodb://127.0.0.1:27017";
            this.mServer = MongoServer.Create(strconn);
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

        //public ConnecHandle GetConnect()
        //{
        //    //return new ConnecHandle();
        //}
    }
}
