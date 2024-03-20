#pragma warning disable CA1416 // Проверка совместимости платформы
using Mava;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace Mava
{
    internal class Recognizer
    {
        public bool running = false;
        public Recognizer()
        {

        }
        public async Task Restart(bool force = false)
        {
            if (force)
            {
                running = false;
                Thread.Sleep(500);
            }
            while (running) { Thread.Sleep(500); }
            await StartRecognize();
        }
        public async Task StartRecognize()
        {
            await Task.Run(() => //!!
            {
                if (running) return;
                running = true;
                using (SpeechRecognitionEngine recognizer = new())
                {
                    Console.WriteLine("Starting recognizer...");
                    //preconfiguration
                    recognizer.SetInputToDefaultAudioDevice();
                    //initialize voice cmds
                    if (Mava_Prime.Mapping.MappingList.Count == 0)
                    {
                        Console.WriteLine("No commands detected");
                        return;
                    }
                    else Console.WriteLine("Processing {0} commands", Mava_Prime.Mapping.MappingList.Count);
                    List<string> buffer1 = new List<string>(Mava_Prime.Mapping.MappingList.Count);
                    foreach (var kek in Mava_Prime.Mapping.MappingList)
                    {
                        buffer1.Add(kek.Name);
                    }
                    Choices commands = new Choices();
                    commands.Add(buffer1.ToArray());
                    GrammarBuilder grammarBuilder = new();
                    grammarBuilder.Culture = new System.Globalization.CultureInfo("en-US");
                    grammarBuilder.Append(commands);
                    // Создание и загрузка грамматики в распознаватель
                    Grammar grammar = new(grammarBuilder);
                    recognizer.LoadGrammar(grammar);


                    //https://stackoverflow.com/questions/3502311/how-to-play-a-sound-in-c-net
                    // Событие, вызываемое при распознавании речи
                    recognizer.SpeechRecognized += (s, e) =>
                    {
                        Console.WriteLine($"Распознанный текст: {e.Result.Text}");
                        Mava_Prime.Mapping.ExecuteByName(e.Result.Text);
                    };

                    // Начало асинхронного распознавания
                    recognizer.RecognizeAsync(RecognizeMode.Multiple);

                    while (running)
                    {
                        Thread.Sleep(500);
                        //Console.WriteLine("+");
                    }

                    // Остановка асинхронного распознавания
                    recognizer.RecognizeAsyncStop();
                }
                running = false;
            });
        }
    }
}
#pragma warning restore CA1416 // Проверка совместимости платформы