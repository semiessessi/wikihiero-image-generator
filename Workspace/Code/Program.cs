using System;
using System.IO;

namespace WHIG
{
    class Program
    {
        public static int TargetSizePixels = 40;
        public static string OutputPath = ".\\out\\";
        public static string InputPath = ".";
        public static bool SimpleMapping = false;
        public static bool GenerateBase64JS = false;

        static void Main(string[] args)
        {
            if (HandleHelp(args))
            {
                return;
            }

            GatherParameters(args);

            OutputImages.GenerateSimpleFromInputPath();
            if(SimpleMapping) // done
            {
                Finish();
                return;
            }

            OutputImages.GenerateReplacementsAndStacks();

            Finish();
        }

        private static void Finish()
        {
            if (GenerateBase64JS)
            {
                Console.WriteLine("Outputting JS lookup table...");
                File.WriteAllText(Path.Combine(OutputPath, "out.js"), OutputBase64JS.CreateJS());
            }

            OutputImages.FinalReport();
        }

        private static bool HandleHelp(string[] args)
        {
            foreach (string arg in args)
            {
                string lower = arg.ToLower();
                if ((lower == "/?")
                    || (lower == "-?")
                    || (lower == "--?")
                    || (lower == "/h")
                    || (lower == "-h")
                    || (lower == "--h")
                    || (lower == "help") // TODO: if there is a path called help?
                    || (lower == "/help")
                    || (lower == "-help")
                    || (lower == "--help"))
                {
                    DisplayHelp();
                    return true;
                }
            }

            //if (args.Length == 0)
            //{
            //    Console.WriteLine("Error: insufficient parameters provided");
            //    DisplayHelp();
            //}

            return false;
        }

        private static void GatherParameters(string[] args)
        {
            foreach (string arg in args)
            {
                string lower = arg.ToLower();
                if (lower.StartsWith("-s")
                    || lower.StartsWith("-size")
                    || lower.StartsWith("--s")
                    || lower.StartsWith("--size")
                    || lower.StartsWith("/s")
                    || lower.StartsWith("/size"))
                {
                    if (lower.Contains("="))
                    {
                        string[] split = lower.Split("=");
                        TargetSizePixels = (split.Length > 1) ? Convert.ToInt32(split[1]) : TargetSizePixels;
                    }
                    else
                    {
                        // TODO:
                        Console.WriteLine("missing '=' in argument: \"" + arg + "\"");
                    }
                }
                else if (arg.StartsWith("-o")
                    || arg.StartsWith("-output")
                    || arg.StartsWith("--o")
                    || arg.StartsWith("--output")
                    || arg.StartsWith("/o")
                    || arg.StartsWith("/output"))
                {
                    if (arg.Contains("="))
                    {
                        string[] split = arg.Split("=");
                        OutputPath = (split.Length > 1) ? split[1] : OutputPath;
                    }
                    else
                    {
                        // TODO: just trim the start instead...
                        Console.WriteLine("missing '=' in argument: \"" + arg + "\"");
                    }
                }
                else if (arg.StartsWith("-js")
                    || arg.StartsWith("--js")
                    || arg.StartsWith("/js"))
                {
                    GenerateBase64JS = true;
                }
            }
        }

        private static void DisplayHelp()
        {
            Console.WriteLine("TODO: ...");
        }
    }
}
