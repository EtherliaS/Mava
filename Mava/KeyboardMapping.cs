using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace Mava
{
    internal class KeyboardMapping
    {
        public List<Command> MappingList { get; set; }
        public KeyboardMapping()
        {
            MappingList = new();
        }
        public void AddMapping(string _idstring, string _name, string _filepath, string _parentcommand = "")
        {
            for (int i = 0; i < MappingList.Count; i++)
            {
                if (MappingList[i].Name == _name)
                {
                    Console.WriteLine("Already exists");
                    return;
                }
            }
            MappingList.Add(new Command(_idstring, _name, _filepath, _parentcommand));
        }
        public void RemoveMapping(int id)
        {
            MappingList.RemoveAt(id);
        }
        public void ExecuteByName(string _name)
        {
            for (int i = 0; i < MappingList.Count; i++)
            {
                if (MappingList[i].Name == _name) { MappingList[i].Execute(); return; }
            }
        }
        public void ExecuteByID(int id)
        {
            MappingList[id].Execute(); return;
        }
        public void PrintCommandInfo(int _id)
        {
            Console.WriteLine("\n[{0}]:", _id);
            Console.WriteLine("{0} : {1}", MappingList[_id].Name, String.Join(", ", MappingList[_id].ids.ToArray()));
            Console.WriteLine("Parent command: {0}", MappingList[_id].ParentCommand);
            Console.WriteLine("File: {0}", MappingList[_id].FilePath);
        }
        public void PrintCurrentList()
        {
            Console.Clear();
            for (int i = 0; i < MappingList.Count; i++)
            {
                PrintCommandInfo(i);
            }
        }
    }
}
