using System.Numerics;

namespace Mava
{
    internal class SoundNotifier
    {
        public int Count;
        WMPLib.WindowsMediaPlayer Player;
        public SoundNotifier()
        {
            Player = new WMPLib.WindowsMediaPlayer();

            GetCount();
        }
        public void PlaySound(string soundName)
        {
            if (Mava_Prime.Config.SilentMode) return;
            Player.URL = soundName;
            Player.controls.play();
        }
        void GetCount()
        {
            try
            {
                // Only get files that begin with the letter "c".
                string[] dirs = Directory.GetFiles(Mava_Prime.soundspath, "*.mp3");
                //Console.WriteLine("The number of files starting with c is {0}.", dirs.Length);
                Count = dirs.Length;
                /*foreach (string dir in dirs)
                {
                    Console.WriteLine(dir);
                }*/
            }
            catch (Exception e)
            {
                Console.WriteLine("Can't reach Sounds/ folder: {0}", e.ToString());
            }
        }
    }
}
