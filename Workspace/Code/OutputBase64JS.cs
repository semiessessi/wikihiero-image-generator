using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHIG
{
    public static class OutputBase64JS
    {
        public static void RegisterData(string name, Image data)
        {
            if (Program.GenerateBase64JS == false)
            {
                return;
            }
            
            MemoryStream m = new MemoryStream();
            data.Save(m, System.Drawing.Imaging.ImageFormat.Png);
            byte[] imageBytes = m.ToArray();

            // Convert byte[] to Base64 String
            string base64String = Convert.ToBase64String(imageBytes);
            base64Data.Add(name, base64String);
        }

        public static string CreateJS()
        {
            string js = "var lookup = {\n";
            foreach (KeyValuePair<string, string> pair in base64Data)
            {
                js += "\t\"" + pair.Key + "\": ";
                js += "\"" + pair.Value + "\",\n";
            }

            js.Remove(js.Length - 2, 2); // remove last comma
            js += "\n};\n";

            return js;
        }

        private static Dictionary<string, string> base64Data = new Dictionary<string, string>();
    }
}
