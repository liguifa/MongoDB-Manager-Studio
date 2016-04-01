using MMS.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.MongoDB
{
    public class ConnecHandle
    {
        private readonly RunCommand mRunSpace;

        public ConnecHandle(RunCommand runSpace)
        {
            this.mRunSpace = runSpace;
        }

        public bool Connect(string name, string address, string username, string password)
        {
            try
            {
                MMS.Command.Command startCmd = CommandFactory.Create(CommandType.Start, new CommandParameter());
                this.mRunSpace.Start(startCmd);
                MMS.Command.Command shellCmd = CommandFactory.Create(CommandType.Shell, new CommandParameter());
                this.mRunSpace.Shell(shellCmd);
            }
            catch (Exception e)
            {

            }
            return true;
        }

        public bool DisConnect(string name)
        {
            return true;
        }
    }
}
