using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Proyecto_IngSoftware
{
    public static class UIExtension_43BO
    {
        public static void Traducir(this Control control, Dictionary<string, string> dict, string key)
        {
            if (dict != null && dict.ContainsKey(key))
            {
                control.Text = dict[key];
            }
            else
            {

                control.Text = $"[{key}]";
            }
        }
    }
}
