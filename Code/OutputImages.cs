﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHIG
{
    public static class OutputImages
    {
        static OutputImages()
        {
            UndoneSet.UnionWith(FullList);
        }

        public static void GenerateSimpleFromInputPath()
        {
            foreach(string name in UndoneSet)
            {
                string testPath = Path.Join(Program.InputPath, name + ".png");
                if(File.Exists(testPath))
                {
                    if(ImageResize.ProcessImage(testPath, Path.Join(Program.OutputPath, "hiero_" + name + ".png")))
                    {
                        MarkDone(name);
                        continue;
                    }
                }

                testPath = Path.Join(Program.InputPath, "hiero_" + name + ".png");
                if (File.Exists(testPath))
                {
                    if (ImageResize.ProcessImage(testPath, Path.Join(Program.OutputPath, "hiero_" + name + ".png")))
                    {
                        MarkDone(name);
                        continue;
                    }
                }

                if(name.StartsWith("Aa"))
                {
                    string jName = name.Replace("Aa", "J");
                    testPath = Path.Join(Program.InputPath, jName + ".png");
                    if (File.Exists(testPath))
                    {
                        if (ImageResize.ProcessImage(testPath, Path.Join(Program.OutputPath, "hiero_" + name + ".png")))
                        {
                            MarkDone(name);
                            continue;
                        }
                    }

                    testPath = Path.Join(Program.InputPath, "hiero_" + jName + ".png");
                    if (File.Exists(testPath))
                    {
                        if (ImageResize.ProcessImage(testPath, Path.Join(Program.OutputPath, "hiero_" + name + ".png")))
                        {
                            MarkDone(name);
                            continue;
                        }
                    }
                }
            }
        }

        public static void GenerateReplacementsAndStacks()
        {
            foreach (string name in UndoneSet)
            {
                if (Replacements.ContainsKey(name))
                {
                    string testPath = Path.Join(Program.InputPath, Replacements[name] + ".png");
                    if (File.Exists(testPath))
                    {
                        if (ImageResize.ProcessImage(testPath, Path.Join(Program.OutputPath, "hiero_" + name + ".png")))
                        {
                            MarkDone(name);
                            continue;
                        }
                    }

                    testPath = Path.Join(Program.InputPath, "hiero_" + Replacements[name] + ".png");
                    if (File.Exists(testPath))
                    {
                        if (ImageResize.ProcessImage(testPath, Path.Join(Program.OutputPath, "hiero_" + name + ".png")))
                        {
                            MarkDone(name);
                            continue;
                        }
                    }
                }

                if (RotatedLeft.ContainsKey(name))
                {
                    if (ImageResize.ProcessRotateLeft(Path.Join(Program.OutputPath, "hiero_" + RotatedLeft[name] + ".png"), Path.Join(Program.OutputPath, "hiero_" + name + ".png")))
                    {
                        MarkDone(name);
                        continue;
                    }
                }
                
                if (RotatedRight.ContainsKey(name))
                {
                    if (ImageResize.ProcessRotateRight(Path.Join(Program.OutputPath, "hiero_" + RotatedRight[name] + ".png"), Path.Join(Program.OutputPath, "hiero_" + name + ".png")))
                    {
                        MarkDone(name);
                        continue;
                    }
                }

                if (SmallAndTallStacks.ContainsKey(name))
                {
                    if (ImageResize.ProcessSmallAndTallStack(SmallAndTallStacks[name],
                        Program.OutputPath, Path.Join(Program.OutputPath, "hiero_" + name + ".png")))
                    {
                        MarkDone(name);
                        continue;
                    }
                }

                if (TallAndSmallStacks.ContainsKey(name))
                {
                    if (ImageResize.ProcessTallAndSmallStack(TallAndSmallStacks[name],
                        Program.OutputPath, Path.Join(Program.OutputPath, "hiero_" + name + ".png")))
                    {
                        MarkDone(name);
                        continue;
                    }
                }

                if (Stacks.ContainsKey(name))
                {
                    if (ImageResize.ProcessStack(Stacks[name], Program.OutputPath, Path.Join(Program.OutputPath, "hiero_" + name + ".png")))
                    {
                        MarkDone(name);
                        continue;
                    }
                }
            }
        }

        private static void MarkDone(string name)
        {
            DoneSet.Add(name);
            UndoneSet.Remove(name);
            Console.WriteLine("(" + DoneSet.Count + "/" + FullList.Length + ") Generated image for " + name + "...");
        }

        public static void FinalReport()
        {
            if(UndoneSet.Count == 0)
            {
                Console.WriteLine("All images sucessfully generated!");
                return;
            }

            Console.WriteLine(UndoneSet.Count.ToString() + " images could not be generated:");
            foreach (string name in UndoneSet)
            {
                Console.WriteLine("\t" + "hiero_" + name + ".png");
            }
        }

        private static HashSet<string> DoneSet = new HashSet<string>();
        private static HashSet<string> UndoneSet = new HashSet<string>();
        public readonly static string[] FullList = new string[]
            {
                "a&A1",
                "a&b&t",
                "a&D",
                "a&F51a&Z2",
                "a&n&D",
                "a&r&t",
                "a&t&x",
                "A&t",
                "A1&n",
                "A1&r",
                "A1",
                "A10",
                "A11",
                "A113",
                "A12",
                "A12D",
                "A13",
                "A14",
                "A14A",
                "A15",
                "A16",
                "A17",
                "A17A",
                "A18",
                "A19",
                "A1s",
                "A2",
                "A20",
                "A21",
                "A21A",
                "A22",
                "A23",
                "A24",
                "A25",
                "A25A",
                "A25as",
                "A26",
                "A27",
                "A28",
                "A29",
                "A2s",
                "A3",
                "A30",
                "A31",
                "A316",
                "A32",
                "A33",
                "A335",
                "A34",
                "A347",
                "A35",
                "A36",
                "A37",
                "A38",
                "A39",
                "A4",
                "A40",
                "A40s",
                "A41",
                "A42",
                "A43",
                "A44",
                "A45",
                "A46",
                "A47",
                "A48",
                "A49",
                "A5",
                "A50",
                "A51",
                "A52",
                "A53",
                "A54",
                "A55",
                "A56",
                "A59",
                "A6",
                "A7",
                "A8",
                "A9",
                "Aa1",
                "Aa10",
                "Aa11",
                "Aa12",
                "Aa13",
                "Aa14",
                "Aa15",
                "Aa16&m",
                "Aa16",
                "Aa17",
                "Aa18",
                "Aa19",
                "Aa2",
                "Aa20",
                "Aa21",
                "Aa22",
                "Aa23",
                "Aa24",
                "Aa25",
                "Aa26",
                "Aa27",
                "Aa28",
                "Aa29",
                "Aa3",
                "Aa30",
                "Aa30A",
                "Aa31",
                "Aa32",
                "Aa4",
                "Aa40",
                "Aa41",
                "Aa5",
                "Aa6",
                "Aa7",
                "Aa8&D",
                "Aa8",
                "Aa9",
                "b&Hb",
                "b&t",
                "B1",
                "B10",
                "B11",
                "B12",
                "B1s",
                "B2&Z2",
                "B2",
                "B23B",
                "B3",
                "B4",
                "B5",
                "B6",
                "B7",
                "B8",
                "B9",
                "bA&k",
                //"Ba14",
                //"Ba14a",
                //"Ba14as",
                //"Ba14s",
                //"Ba15",
                //"Ba15a",
                //"Ba15as",
                //"Ba15s",
                //"Ba16",
                //"Ba16a",
                //"Ba16as",
                //"Ba16s",
                //"Ba17",
                //"Ba17a",
                //"Ba17as",
                //"Ba17s",
                //"Ba18",
                //"Ba18a",
                //"Ba18as",
                //"Ba18s",
                //"Ba19",
                //"Ba19a",
                //"Ba19as",
                //"Ba19s",
                //"BLACKDOT",
                "C1",
                "C10",
                "C11",
                "C12",
                "C17",
                "C18",
                "C19",
                "C2",
                "C20",
                "C3",
                "C4",
                "C5",
                "C6",
                "C7",
                "C8",
                "C9",
                //"Ca0",
                //"Ca1",
                //"Ca1a",
                //"Ca2",
                //"Ca2a",
                //"Cah1",
                //"Cah1a",
                //"Cah2",
                //"Cah2a",
                //"Cah3",
                //"Cah3a",
                "D&d&t",
                "D&d",
                "D&ra",
                "D&t&N17",
                "D&t&tA",
                "D&t",
                "d&w",
                "D&z&f",
                "D&z",
                "D1",
                "D10",
                "D11",
                "D12",
                "D13",
                "D14",
                "D15",
                "D16",
                "D17",
                "D18",
                "D19",
                "D2",
                "D20",
                "D21",
                "D22",
                "D23",
                "D24",
                "D25",
                "D26",
                "D27",
                "D27A",
                "D28",
                "D29",
                "D3",
                "D30",
                "D31",
                "D32",
                "D33",
                "D34",
                "D34A",
                "D35",
                "D36",
                "D37",
                "D38",
                "D39",
                "D4",
                "D40",
                "D41",
                "D42",
                "D43",
                "D44",
                "D45",
                "D46",
                "D46A",
                "D47",
                "D48",
                "D49",
                "D5",
                "D50",
                "D51",
                "D52&t&r",
                "D52",
                "D53",
                "D54",
                "D55",
                "D56",
                "D57",
                "D58",
                "D58s",
                "D59",
                "D6",
                "D60",
                "D61",
                "D62",
                "D63",
                "D7",
                "D8",
                "D9",
                "E1",
                "E10",
                "E11",
                "E12",
                "E13",
                "E14",
                "E15",
                "E16",
                "E17",
                "E18",
                "E19",
                "E2",
                "E20",
                "E21",
                "E22",
                "E23",
                "E24",
                "E25",
                "E26",
                "E27",
                "E28",
                "E29",
                "E3",
                "E30",
                "E31",
                "E32",
                "E33",
                "E34",
                "E4",
                "E5",
                "E6",
                "E7",
                "E8",
                "E8A",
                "E9",
                "f&n&d",
                "f&r&t",
                "F1",
                "F10",
                "F11",
                "F12",
                "F13",
                "F14",
                "F15",
                "F16",
                "F17",
                "F18",
                "F19",
                "F2",
                "F20",
                "F21",
                "F22",
                "F23",
                "F24",
                "F25",
                "F26",
                "F27",
                "F28",
                "F29",
                "F3",
                "F30",
                "F31",
                "F31s",
                "F32",
                "F33",
                "F34",
                "F35",
                "F36",
                "F37",
                "F37B",
                "F38",
                "F39&Z1",
                "F39",
                "F4",
                "F40",
                "F41",
                "F42",
                "F43",
                "F44",
                "F45",
                "F46",
                "F47",
                "F48",
                "F49",
                "F5",
                "F50",
                "F51",
                "F51A",
                "F51B",
                "F52",
                "F6",
                "F7",
                "F8",
                "F9",
                "G1",
                "G10",
                "G11",
                "G12",
                "G13",
                "G14&t",
                "G14",
                "G15",
                "G16",
                "G17",
                "G17s",
                "G18",
                "G19",
                "G1s",
                "G2",
                "G20",
                "G21",
                "G22",
                "G23",
                "G24",
                "G25",
                "G26",
                "G26A",
                "G27",
                "G28",
                "G29",
                "G3",
                "G30",
                "G31",
                "G32",
                "G33",
                "G34",
                "G35",
                "G36",
                "G37",
                "G38",
                "G39",
                "G4",
                "G40",
                "G41",
                "G42",
                "G43",
                "G43s",
                "G44",
                "G45",
                "G46",
                "G47",
                "G48",
                "G49",
                "G5",
                "G50",
                "G51",
                "G52",
                "G53",
                "G54",
                "G5s",
                "G6",
                "G7",
                "G7A",
                "G7AA",
                "G8",
                "G9",
                "H1",
                "H2",
                "H3",
                "H4",
                "H5",
                "H6",
                "H6A",
                "H7",
                "H8",
                "H8W",
                //"HASH(old)",
                //"HASH",
                //"hatching",
                "Hmw&kA",
                //"H_HASH",
                //"H_SPACE",
                "I1",
                "I10",
                "I11",
                "I12",
                "I13",
                "I14",
                "I15",
                "I1S",
                "I2",
                "I3",
                "I4",
                "I5",
                "I5A",
                "I6",
                "I7",
                "I8",
                "I9",
                "ini&n&n",
                "ini&n",
                "ir&n&f",
                "ir&n&k",
                "ir&r&f",
                "ir&t&f",
                "ir&t&n",
                "K1",
                "K2",
                "K3",
                "K4",
                "K5",
                "K6",
                "K7",
                "L1",
                "L2",
                "L3",
                "L4",
                "L5",
                "L6",
                "L7",
                "m&&t",
                "m&a",
                "m&f",
                "m&n",
                "m&r",
                "m&t",
                "M1",
                "M10",
                "M11",
                "M12",
                "M12s",
                "M13",
                "M14",
                "M15",
                "M16",
                "M17",
                "M17s",
                "M18",
                "M19",
                "M2",
                "M20",
                "M21",
                "M22",
                "M23",
                "M23s",
                "M24",
                "M25",
                "M26",
                "M27",
                "M28",
                "M29",
                "M3",
                "M30",
                "M31",
                "M32",
                "M33",
                "M34",
                "M35",
                "M36",
                "M37",
                "M38",
                "M39",
                "M4",
                "M40",
                "M41",
                "M42",
                "M43",
                "M43A",
                "M44",
                "M5",
                "M6",
                "M7",
                "M8",
                "M9",
                "mn&n&t",
                "mn&n&x",
                "mr&&r&r",
                "mr&r&r",
                "mr&r&t",
                "n&A1",
                "n&D",
                "n&f&n",
                "n&f&t",
                "n&n&a",
                "n&n&f",
                "n&nH",
                "n&nm&m",
                "n&t&f",
                "n&t&k",
                "n&t&t",
                "n&U19&nw",
                "n&w",
                "n&wa&w",
                "n&x&f",
                "n&xAst&t",
                "n&xt",
                "N1",
                "N10",
                "N11",
                "N12",
                "N13",
                "N14",
                "N15",
                "N16",
                "N17",
                "N18",
                "N19",
                "N2",
                "N20",
                "N21",
                "N22",
                "N23",
                "N24",
                "N25",
                "N26",
                "N27",
                "N28",
                "N29",
                "N3",
                "N30",
                "N31",
                "N32",
                "N33",
                "N33A",
                "N33B",
                "N33C",
                "N34",
                "N35",
                "N35A",
                "N35B",
                "N35C",
                "N36",
                "N37",
                "N38",
                "N39",
                "N4",
                "N40",
                "N41",
                "N42",
                "N5",
                "N6",
                "N7",
                "N8",
                "N9",
                "nbAki",
                "nswt&bity",
                "O1",
                "O10",
                "O11",
                "O12",
                "O13",
                "O14",
                "O15",
                "O16",
                "O17",
                "O18",
                "O19",
                "O2",
                "O20",
                "O21",
                "O22",
                "O23",
                "O24",
                "O25",
                "O26",
                "O27",
                "O28",
                "O29",
                "O29V",
                "O3",
                "O30",
                "O31",
                "O32",
                "O33",
                "O34",
                "O35",
                "O36",
                "O37",
                "O38",
                "O39",
                "O4",
                "O40",
                "O41",
                "O42",
                "O43",
                "O44",
                "O45",
                "O46",
                "O47",
                "O48",
                "O49",
                "O5",
                "O50",
                "O51",
                "O6",
                "O7",
                "O8",
                "O9",
                "p&z&f",
                "P1",
                "P10",
                "P11",
                "P13",
                "P1A",
                "P2",
                "P3",
                "P4",
                "P44A",
                "P5",
                "P6",
                "P7",
                "P8",
                "P8H",
                "P9",
                "pr&r&t",
                "Q1",
                "Q2",
                "Q3",
                "Q4",
                "Q5",
                "Q6",
                "Q7",
                //"QUERY",
                //"Q_HASH",
                "r&a&k",
                "r&a&t",
                "r&A1",
                "r&D&d",
                "r&f&n",
                "r&n&f",
                "r&r&t",
                "r&r",
                "r&t",
                "R1",
                "R10",
                "R11",
                "R12",
                "R13",
                "R14",
                "R15",
                "R16",
                "R17",
                "R18",
                "R19",
                "R2",
                "R20",
                "R21",
                "R22",
                "R23",
                "R24",
                "R25",
                "R3",
                "R4",
                "R5",
                "R6",
                "R7",
                "R8",
                "R8A",
                "R9",
                //"REDDOT",
                "S1",
                "S10",
                "S106",
                "S11",
                "S12",
                "S13",
                "S14",
                "S14A",
                "S15",
                "S16",
                "S17",
                "S17A",
                "S18",
                "S19",
                "S2",
                "S20",
                "S21",
                "S22",
                "S23",
                "S24",
                "S25",
                "S26",
                "S27",
                "S28&Z2",
                "S28",
                "S29",
                "S29s",
                "S3",
                "S30",
                "S31",
                "S32",
                "S33",
                "S34",
                "S35",
                "S36",
                "S37",
                "S38",
                "S39",
                "S4",
                "S40",
                "S41",
                "S42",
                "S43",
                "S44",
                "S45",
                "S5",
                "S6",
                "S7",
                "S8",
                "S9",
                //"SPACE",
                "sSm&m",
                "sti&t",
                "stp&s",
                "t&A",
                "t&A19",
                "t&H",
                "t&I12",
                "t&M30",
                "t&nw",
                "t&r&f",
                "t&r",
                "t&s",
                "t&sti",
                "t&T30",
                "T1",
                "T10",
                "T11",
                "T12",
                "T13",
                "T14",
                "T15",
                "T16",
                "T17",
                "T18",
                "T19",
                "T2",
                "T20",
                "T21",
                "T22",
                "T23",
                "T24",
                "T25",
                "T26",
                "T27",
                "T28",
                "T29",
                "T3",
                "T30",
                "T31",
                "T32",
                "T33",
                "T34",
                "T35",
                "T4",
                "T5",
                "T6",
                "T7",
                "T7A",
                "T8",
                "T8A",
                "T8B",
                "T9",
                "T9A",
                "THREE",
                //"Tr_HSPACE",
                //"Tr_SPACE",
                "TWO",
                "U1",
                "U10",
                "U11",
                "U12",
                "U13",
                "U14",
                "U15",
                "U16",
                "U17",
                "U18",
                "U19",
                "U2",
                "U20",
                "U21",
                "U22",
                "U23",
                "U24",
                "U25",
                "U26",
                "U27",
                "U28",
                "U29",
                "U3",
                "U30",
                "U31",
                "U32",
                "U33",
                "U34",
                "U35",
                "U35s",
                "U36",
                "U37",
                "U38",
                "U39",
                "U4",
                "U40",
                "U41",
                "U5",
                "U6",
                "U7",
                "U8",
                "U9",
                "V1",
                "V10",
                "V10A",
                "V11",
                "V11A",
                "V12",
                "V13",
                "V14",
                "V15",
                "V16",
                "V17",
                "V18",
                "V19",
                "V2",
                "V20",
                "V21",
                "V22",
                "V23",
                "V24",
                "V25",
                "V26",
                "V27",
                "V28",
                "V29",
                "V3",
                "V30",
                "V31",
                "V31A",
                "V32",
                "V33",
                "V34",
                "V35",
                "V36",
                "V37",
                "V38",
                "V39",
                "V4",
                "V5",
                "V6",
                "V7",
                "V8",
                "V9",
                "VTHREE",
                //"V_HASH",
                "w&&t",
                "w&t",
                "w&y",
                "W1",
                "W10",
                "W10A",
                "W11",
                "W12",
                "W13",
                "W14",
                "W15",
                "W16",
                "W17",
                "W18",
                "W19",
                "W19s",
                "W2",
                "W20",
                "W21",
                "W22",
                "W23",
                "W24",
                "W25",
                "W3",
                "W4",
                "W5",
                "W6",
                "W7",
                "W8",
                "W9",
                "wa&W&a",
                "wn&n&t",
                "wr&r&t",
                "x&f&t",
                "x&mt&t",
                "x&r&t&Y1",
                "x&r&t",
                "X1",
                "X2",
                "X3",
                "X4",
                "X5",
                "X6",
                "X7",
                "X8",
                "xAswt",
                "Y1&A1",
                "Y1&n&f",
                "Y1",
                "Y1V",
                "Y2",
                "Y3",
                "Y4",
                "Y5",
                "Y6",
                "Y7",
                "Y8",
                "z&A1&Z1",
                "z&A1",
                "z&Ab&b",
                "z&Dr&r",
                "z&w",
                "z&X&k",
                "z&x&r",
                "Z1",
                "Z10",
                "Z11",
                "Z1s",
                "Z2",
                "Z2s",
                "Z2ss",
                "Z3",
                "Z3A",
                "Z3as",
                "Z4",
                "Z4B",
                "Z5",
                "Z6&A1",
                "Z6",
                "Z7",
                "Z8",
                "Z9",
                "Z91",
                "Z92",
                "Z93",
                "Z94",
                "Z95",
                "Z98A"
            };

        public static readonly Dictionary<string, string> Replacements = new Dictionary<string, string>
        {
            { "A1s", "A1" },
            { "A2s", "A2" },
            { "A40s", "A40" },
            { "B1s", "B1" },
            { "D&t&N17", "I11A" },
            { "D58s", "D58" },
            { "F31s", "F31" },
            { "G1s", "G1" },
            { "G5s", "G5" },
            { "G17s", "G17" },
            { "G43s", "G43" },
            { "I1S", "I1" },
            { "M12s", "M12" },
            { "M17s", "M17" },
            { "M23s", "M23" },
            { "nswt&bity", "L2A" },
            { "O29V", "O29A" },
            { "S29s", "S29" },
            { "THREE", "Z2A" },
            { "TWO", "Z4A" },
            { "U35s", "U35" },
            { "VTHREE", "Z3A" },
            { "W19s", "W19" },
            { "Y1V", "Y1A" }
        };

        public static readonly HashSet<string> LargeInStack = new HashSet<string>
        {
            "A1",
            "A21",
            "B1",
            "B2",
            "G43",
            "I10",
            //"Aa1"
        };

        public static readonly Dictionary<string, string> RotatedLeft = new Dictionary<string, string>
        {
            { "P8H", "P8" },
        };

        public static readonly Dictionary<string, string> RotatedRight = new Dictionary<string, string>
        {
            { "T8B", "T8A" },
            { "O29V", "O29" },
        };

        public static readonly Dictionary<string, string[]> SmallAndTallStacks = new Dictionary<string, string[]>
        {
            { "t&A", new string[]{ "X1", "G1" } },
            { "t&A19", new string[]{ "X1", "A19" } },
            { "t&H", new string[]{ "X1", "V28" } },
            { "t&I12", new string[]{ "X1", "I12" } },
            { "t&M30", new string[]{ "X1", "M30" } },
            { "t&s", new string[]{ "X1", "S29" } },
            { "t&sti", new string[]{ "X1", "F29" } },
        };

        public static readonly Dictionary<string, string[]> TallAndSmallStacks = new Dictionary<string, string[]>
        {
            { "sti&t", new string[]{ "F29", "X1" } },
        };

        public static readonly Dictionary<string, string[]> StackOnRow = new Dictionary<string, string[]>
        {
            { "a&b&t", new string[]{ "D36", "D58", "X1" } },
            { "a&r&t", new string[]{ "D36", "D21", "X1" } },
        };

        public static readonly Dictionary<string, string[]> Stacks = new Dictionary<string, string[]>
        {
            { "a&A1", new string[]{ "D36", "A1" } },
            { "a&D", new string[]{ "D36", "I10" } },
            { "a&F51a&Z2", new string[]{ "D36", "F51A", "Z2" } },
            { "a&n&D", new string[]{ "D36", "N35", "I10" } },
            { "A1&n", new string[]{ "A1", "N35" } },
            { "A1&r", new string[]{ "A1", "D21" } },
            { "A113", new string[]{ "A21", "N1" } },
            { "Aa8&D", new string[]{ "Aa8", "I10" } },
            { "B2&Z2", new string[]{ "B2", "Z2" } },
            { "d&w", new string[]{ "D46", "G43" } },
            { "f&n&d", new string[]{ "I9", "N35", "D46" } },
            { "f&r&t", new string[]{ "I9", "D21", "X1" } },
            { "ir&n&f", new string[]{ "D4", "N35", "I9" } },
            { "ir&n&k", new string[]{ "D4", "N35", "V31" } },
            { "ir&r&f", new string[]{ "D4", "D21", "I9" } },
            { "ir&t&f", new string[]{ "D4", "X1", "I9" } },
            { "ir&t&n", new string[]{ "D4", "X1", "N35" } },
            { "mn&n&t", new string[]{ "Y5", "N35", "Z1" } },
            { "mn&n&x", new string[]{ "Y5", "N35", "Aa1" } },
            { "mr&r&r", new string[]{ "U7", "D21", "D21" } },
            { "n&A1", new string[]{ "N35", "A1" } },
            { "n&D", new string[]{ "N35", "I10" } },
            { "n&f&n", new string[]{ "N35", "I9", "N35" } },
            { "n&f&t", new string[]{ "N35", "I9", "X1" } },
            { "n&n&a", new string[]{ "N35", "N35", "D36" } },
            { "n&n&f", new string[]{ "N35", "N35", "I9" } },
            { "n&t&f", new string[]{ "N35", "X1", "I9" } },
            { "n&t&k", new string[]{ "N35", "X1", "V31" } },
            { "n&t&t", new string[]{ "N35", "X1", "X1" } },
            { "n&x&f", new string[]{ "N35", "Aa1", "I9" } },
            { "p&z&f", new string[]{ "Q3", "O34", "I9" } },
            { "n&w", new string[]{ "N35", "G43" } },
            { "r&A1", new string[]{ "D21", "A1" } },
            { "r&a&k", new string[]{ "D21", "D36", "V31" } },
            { "r&a&t", new string[]{ "D21", "D36", "X1" } },
            { "r&f&n", new string[]{ "D21", "I9", "N35" } },
            { "r&n&f", new string[]{ "D21", "N35", "I9" } },
            { "r&r", new string[]{ "D21", "D21" } },
            { "r&t", new string[]{ "D21", "X1" } },
            { "t&r&f", new string[]{ "X1", "D21", "I9" } },
            { "x&f&t", new string[]{ "Aa1", "I9", "X1" } },
            { "x&r&t", new string[]{ "Aa1", "D21", "X1" } },
            { "Y1&A1", new string[]{ "Y1", "A1" } },
            { "Y1&n&f", new string[]{ "Y1", "N35", "I9" } },
            { "z&A1", new string[]{ "O34", "A1" } },
            { "z&x&r", new string[]{ "O34", "Aa1", "D21" } },
            { "z&X&k", new string[]{ "O34", "F32", "V31" } },
            { "z&w", new string[]{ "O34", "G43" } },
            { "Z6&A1", new string[]{ "Z6", "A1" } },
        };
    }
}