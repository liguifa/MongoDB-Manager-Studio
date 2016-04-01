using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.MongoDB
{
    public class MongoDBTree
    {
        public string Name { get; set; }

        public MongoDBTreeNodeType NodeType { get; set; }

        public List<MongoDBTree> Children { get; set; }
    }

    public enum MongoDBTreeNodeType
    {
        Server,
        Menu,
        Docmenu,
        List
    }
}
