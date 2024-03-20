using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace Mava
{
    internal class Command
    {
        public List<VirtualKeyCode> ids { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string ParentCommand { get; set; }
        Random Rand;

        public Command(string idstring, string name, string filePath = "", string parentCommand = "")
        {
            Rand = new Random();
            ids = new List<VirtualKeyCode>();
            string[] tmps = idstring.Split(' ');
            for (int i = 0; i < tmps.Length; i++)
            {
                this.ids.Add((VirtualKeyCode)Int32.Parse(tmps[i]));
            }
            this.Name = name;
            this.FilePath = filePath;
            this.ParentCommand = parentCommand;
        }
        public Command() { }
        public void Execute()
        {
            for (int i = 0; i < ids.Count; i++)
            {
                Mava_Prime.KBController.Transfer(ids[i]);
                if (Mava_Prime.Config.RandomizeTimings)
                {
                    Thread.Sleep(Rand.Next(50, 100));
                }
                else Thread.Sleep(50);
            }
            if (FilePath != "" && FilePath != null) Mava_Prime.Notifier.PlaySound(Mava_Prime.soundspath + FilePath);
        }
    }
}
