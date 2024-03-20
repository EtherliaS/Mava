using Mava;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mava
{
    internal class UserInterface
    {
        public UserInterface() { }
        public async static Task TerminalInterface()
        {
            while (Mava_Prime.RunState)
            {
                Console.WriteLine("Mava flight assistant [v{0}]", Mava_Prime.version);
                Console.WriteLine("Status:");
                Console.Write("Silent mode: ");
                Console.WriteLine(Mava_Prime.Config.SilentMode ? "[on]" : "[off]");
                Console.Write("RandTimings mode: ");
                Console.WriteLine(Mava_Prime.Config.RandomizeTimings ? "[on]" : "[off]");
                Console.WriteLine("Cmds:");
                Console.WriteLine("0. RandTimings: " + (Mava_Prime.Config.RandomizeTimings ? "Disable" : "Enable"));
                Console.WriteLine("1. Silent: " + (Mava_Prime.Config.SilentMode ? "Disable" : "Enable"));
                Console.WriteLine("2. Add Macros");
                Console.WriteLine("3. Remove Macros");
                Console.WriteLine("4. Current list");
                Console.WriteLine("5. Exit");

                string rl = Console.ReadLine();
                switch (rl)
                {
                    case "0":
                        {
                            Mava_Prime.Config.RandomizeTimings = !Mava_Prime.Config.RandomizeTimings;
                            Console.Clear();
                            break;
                        }
                    case "1":
                        {
                            Mava_Prime.Config.SilentMode = !Mava_Prime.Config.SilentMode;
                            Console.Clear();
                            break;
                        }
                    case "2":
                        {
                            Console.Write("Enter voice cmd name \n(Example: \"flaps\" or \"turn flaps\")\n>>> ");
                            string cmd = Console.ReadLine().ToLower();
                            Console.Write("Enter keys id (Example: \"205\" or \"220 75\")\n>>> ");
                            string ids = Console.ReadLine();
                            Console.Write("Enter file name in Sounds dir (optional)\n>>> ");
                            string fpath = Console.ReadLine().ToLower();
                            Console.Write("Enter parent command (optional)\n>>> ");
                            string parentcmd = Console.ReadLine().ToLower();
                            Mava_Prime.Mapping.AddMapping(ids, cmd, fpath, parentcmd);
                            await JsonManager.WriteJsonMappingAsync();
                            await JsonManager.WriteJsonConfigurationAsync();
                            Console.Clear();
                            Mava_Prime.Recognizer.Restart(true);
                            break;
                        }
                    case "3":
                        {
                            Console.Write("Enter id\n>>> ");
                            int __id = Int32.Parse(Console.ReadLine());
                            Mava_Prime.Mapping.RemoveMapping(__id);
                            Console.Clear();
                            Mava_Prime.Recognizer.Restart(true);
                            break;
                        }
                    case "4":
                        {
                            Mava_Prime.Mapping.PrintCurrentList();
                            Console.WriteLine("\nPress any key...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                    case "5":
                        {
                            Mava_Prime.RunState = false;
                            break;
                        }
                    default: { Console.Clear(); Console.WriteLine("???"); break; }
                }

            }
        }
    }
}
