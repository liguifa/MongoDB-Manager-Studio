using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.UI.Default
{
    public class RightMenu
    {
        public List<RightMenuItem> GetRightMneuByServer()
        {
            List<RightMenuItem> menus = new List<RightMenuItem>();
            RightMenuItem item = new RightMenuItem()
            {
                Text = "新建集合",
                //Command = new NewCollectionCommand()
            };
            menus.Add(item);
            return null;
        }
    }
}
