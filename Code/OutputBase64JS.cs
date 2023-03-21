using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHIG
{
#pragma warning disable CA1416 // Validate platform compatibility

    public static class OutputBase64JS
    {
        public static void RegisterData(string name, Image data, bool convertImmediately)
        {
            if (Program.GenerateBase64JS == false)
            {
                return;
            }

            string base64String = "";
            if (convertImmediately)
            {
                MemoryStream m = new();
                data.Save(m, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = m.ToArray();

                // Convert byte[] to Base64 String
                base64String = Convert.ToBase64String(imageBytes);
            }
            base64Data.Add(name, base64String);
        }

        public static string CreateJS(bool loadData)
        {
            return CreateJSWithVariableName(Program.VariableName, loadData);
        }

        public static string CreateJSWithVariableName(string name, bool loadData)
        {
            string js = "var " + name + " = {\n";
            if (loadData == false)
            {
                foreach (KeyValuePair<string, string> pair in base64Data)
                {
                    js += "\t\"" + pair.Key + "\": ";
                    js += "\"" + pair.Value + "\",\n";
                }
            }
            else
            {
                foreach (KeyValuePair<string, string> pair in base64Data)
                {
                    byte[] dataBytes = File.ReadAllBytes(Path.Join(Program.OutputPath, pair.Key));
                    js += "\t\"" + pair.Key + "\": ";
                    js += "\"" + Convert.ToBase64String(dataBytes) + "\",\n";
                }
            }

            js = js.Remove(js.Length - 2, 2); // remove last comma
            js += "\n};\n";

            return js;
        }

        private static Dictionary<string, string> base64Data = new();
    }

#pragma warning restore CA1416 // Validate platform compatibility
}
