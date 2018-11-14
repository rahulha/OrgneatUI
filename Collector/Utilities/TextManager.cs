using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Utilities
{
    public static class TextManager
    {

        public static String CleanTextForCSV(String Text)
        {
            return Text.Replace(",", ";").Replace("\n", "").Replace(Environment.NewLine, "");
        }
    }
}
