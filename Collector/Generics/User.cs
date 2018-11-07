using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Generics
{
    [Serializable]
    public class User
    {
        public string ID;
        public string ScreenName;
        public string UserName;
        //public String EmojifiedNameJson;

        public string Id_str
        {
            get => ID;

            set
            {
                ID = value;
            }
        }

        public string screen_name
        {
            get => ScreenName;

            set
            {
                ScreenName = value;
            }
        }

        public string name
        {
            get => UserName;

            set
            {
                UserName = value;
            }
        }

        //public string emojified_name
        //{
        //    get => EmojifiedNameJson;

        //    set
        //    {
        //        EmojifiedNameJson = value;
        //    }
        //}

    }
}
