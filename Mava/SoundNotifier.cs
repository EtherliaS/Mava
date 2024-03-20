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
                string[] dirs = Directory.GetFiles(Mava_Prime.soundspath, "*.mp3");
                Count = dirs.Length;
            }
            catch (Exception e)
            {
                Console.WriteLine("Can't reach Sounds/ folder: {0}", e.ToString());
            }
        }
    }
}
