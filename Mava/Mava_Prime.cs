#pragma warning disable CA1416 // Проверка совместимости платформы
using System;
using System.Speech.Recognition;
using System.Text.RegularExpressions;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;
using System.Text.Json;
using Mava;
using System.ComponentModel.Design;
namespace Mava
{
    class Mava_Prime
    {
        public const string version = "1.0.3";
        public const string cfgpath = "Config.json";
        public const string mappath = "Mapping.json";
        public const string soundspath = @"Sounds/";
        public static bool RunState = true;
        public static Recognizer Recognizer;
        public static KBExController KBController;
        public static Configuration Config;
        public static KeyboardMapping Mapping;
        public static SoundNotifier Notifier;
        static async Task LoadDependencies()
        {
            Console.Clear();
            Directory.CreateDirectory("Sounds/");
            Directory.CreateDirectory("Memes/");
            Console.WriteLine("Loading files...");
            FileInfo fileInfoconf = new("Config.json");
            FileInfo fileInfomap = new("Mapping.json");
            if (fileInfoconf.Exists)
            {
                Config = await JsonManager.ReadJsonConfigurationAsync();
                Console.WriteLine("Found Config.json");
            }
            else
            {
                Config = new(false, false);
                Console.WriteLine("Config.json not found, loading defaults");
            }
            if (fileInfomap.Exists)
            {
                Mapping = await JsonManager.ReadJsonMappingAsync();
                Console.WriteLine("Found Mapping.json");
            }
            else
            {
                Mapping = new();
                Console.WriteLine("Mapping.json not found, loading defaults");
            }
            Console.Write("Recognizer initialization... ");
            Recognizer = new();
            Console.Write("Done!\nController initialization... ");
            KBController = new();
            Console.Write("Done!\nSoundNotifier initialization... ");
            Notifier = new();
            Console.WriteLine("Done!");
            Console.WriteLine("Loaded {0} sounds.\n", Notifier.Count);
            //meme part
            Random random = new();
            var tmp = Directory.GetFiles(@"Memes/", "*.mp3");
            if (tmp.Length > 0 ) Notifier.PlaySound(@tmp[random.Next(1, tmp.Length)]);
        }
        static async Task Main(string[] args)
        {
            await LoadDependencies();

            Recognizer.StartRecognize();
            Console.WriteLine("Done\nSetting up Interface...");

            await UserInterface.TerminalInterface();

            Console.WriteLine("Saving files...");
            await JsonManager.WriteJsonMappingAsync();
            await JsonManager.WriteJsonConfigurationAsync();
            Console.WriteLine("See u again <3");
            Environment.Exit(0);
        }
    }
}
#pragma warning restore CA1416 // Проверка совместимости платформы