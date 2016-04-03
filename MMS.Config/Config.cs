using Common.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MMS.Config
{
    public class Config
    {
        public string MongodPath { get; set; }

        private static readonly Logger mLog = Logger.GetInstance(MethodBase.GetCurrentMethod().DeclaringType);
        private static Config config;
        private static readonly object syncRoot = new object();

        private Config()
        {
            this.InitMMSConfig();
        }

        public static Config GetInstance()
        {
            if (config == null)
            {
                lock (syncRoot)
                {
                    if (config == null)
                    {
                        config = new Config();
                    }
                }
            }
            return config;
        }

        private void InitMMSConfig()
        {
            string mmsConfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "mms.config");
            if (!File.Exists(mmsConfig))
            {
                mLog.Error("An error has occurred in the init mmsconfig file,error:not fount file \"{0}\"。", mmsConfig);
                throw new FileNotFoundException(String.Format("没有找到文件{0}.", mmsConfig));
            }
            try
            {
                XElement config = XElement.Load(mmsConfig);
                this.MongodPath = config.Element("mongod").Attribute("path").Value;
            }
            catch (Exception e)
            {
                mLog.Error("An error has occurred in the init mmsconfig file,error:{0}", e.ToString());
                throw;
            }
        }
    }
}
