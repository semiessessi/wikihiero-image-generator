﻿using System;
using System.CommandLine;
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
        public static bool OptiPNG = false;

        private static Option<string> OutputOption = new Option<string>(
            name: "--output",
            description: "The path to write the images to",
            getDefaultValue: () => ".\\out\\");
        private static Option<int> SizeOption = new Option<int>(
            name: "--size",
            description: "Font size to use for the glyphs",
            getDefaultValue: () => 40);
        private static Option<bool> JSOption = new Option<bool>(
            name: "--js",
            description: "Font size to use for the glyphs",
            getDefaultValue: () => false);
        private static Option<bool> OptiPNGOption = new Option<bool>(
             name: "--optipng",
             description: "Font size to use for the glyphs",
             getDefaultValue: () => false);

        static int Main(string[] args)
        {
            RootCommand rootCommand = new RootCommand("Generate wikihiero images from hieroglyph images");
            JSOption.AddAlias("-js");
            OptiPNGOption.AddAlias("-optipng");
            OutputOption.AddAlias("-output");
            OutputOption.AddAlias("--o");
            OutputOption.AddAlias("-o");
            SizeOption.AddAlias("-size");
            SizeOption.AddAlias("--s");
            SizeOption.AddAlias("-s");
            rootCommand.AddOption(JSOption);
            rootCommand.AddOption(OptiPNGOption);
            rootCommand.AddOption(OutputOption);
            rootCommand.AddOption(SizeOption);

            rootCommand.SetHandler(
                (js, optimise, output, size) =>
                {
                    GenerateBase64JS = js;
                    OptiPNG = optimise;
                    OutputPath = output;
                    TargetSizePixels = size;
                    OutputImages.GenerateSimpleFromInputPath();
                    if (SimpleMapping) // done
                    {
                        Finish();
                        return;
                    }

                    OutputImages.GenerateReplacementsAndRotations();
                    OutputImages.GenerateLigaturesAndStacks();

                    Finish();
                },
                JSOption, OptiPNGOption, OutputOption, SizeOption);

            return rootCommand.InvokeAsync(args).Result;
        }

        private static void Finish()
        {
            if(OptiPNG)
            {
                Console.WriteLine("Optimising generated files...");
                OutputImages.RunOptiPNG();
            }
            if (GenerateBase64JS)
            {
                Console.WriteLine("Outputting JS lookup table...");
                File.WriteAllText(Path.Combine(OutputPath, "out.js"), OutputBase64JS.CreateJS(OptiPNG));
            }

            OutputImages.FinalReport();
        }
    }
}
