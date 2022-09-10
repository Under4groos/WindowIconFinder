using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace WpfApp10.Model
{
    public class ImageFinder
    {
        public string Data
        {
            get;set;
        }
        public void _find(string dir)
        {
            try
            {
                if (Directory.Exists(dir))
                {
                    foreach (var item in Directory.GetFiles(dir))
                    {
                        if (Regex.IsMatch(new FileInfo(item).Extension, "(.png)|(.jpeg)|(.gif)|(.bmp)|(.ico)|(.bmp)"))
                        {
                            Console.WriteLine(item);
                            Data += item + "\n";
                        }
                    }
                    foreach (var item in Directory.GetDirectories(dir))
                    {
                        _find(item);
                    }

                }
            }
            catch { }

        }
//      
    }
}
