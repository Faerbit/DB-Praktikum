using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Configuration;

namespace Praktikum_MVC
{


    static public class PrakHelpers
    {
        static public string profTitelHelper(string titel)
        {
            string tmp = titel;
            string _short = tmp;
            string _long = "";
            int index = tmp.IndexOf("Prof.");
            while (index != -1)
            {
                tmp = tmp.Remove(index, "Prof.".Length);
                _long += "Professor ";
                index = tmp.IndexOf("Prof.");
            }
            index = tmp.IndexOf("Dr.");
            while (index != -1)
            {
                tmp = tmp.Remove(index, "Dr.".Length);
                _long += "Doktor ";
                index = tmp.IndexOf("Dr.");
            }
            index = tmp.IndexOf("rer. nat.");
            while (index != -1)
            {
                tmp = tmp.Remove(index, "rer. nat.".Length);
                _long += "rerum naturalium ";
                index = tmp.IndexOf("rer. nat.");
            }
            index = tmp.IndexOf("Ing.");
            while (index != -1)
            {
                tmp = tmp.Remove(index, "Ing.".Length);
                _long += "Ingenieur ";
                index = tmp.IndexOf("Ing.");
            }
            return @"<abbr title=""" + _long + @""">" + _short + "</abbr>";
        }

    }
}