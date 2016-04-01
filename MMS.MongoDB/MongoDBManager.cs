using MMS.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.MongoDB
{
    public class MongoDBManager
    {
        public static MongoDBManager mMongoDB;
        private static readonly object syncRoot = new object();
        RunCommand runSpace = new RunCommand();

        private MongoDBManager()
        {

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
            return new Brown(runSpace);
        }

        public ConnecHandle GetConnect()
        {
            return new ConnecHandle(runSpace);
        }
    }
}
