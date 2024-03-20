using Mava;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace Mava
{
    internal class KBExController
    {
        InputSimulator Emulator;
        public KBExController()
        {
            Emulator = new InputSimulator();
        }
        public void Transfer(VirtualKeyCode code)
        {
            Emulator.Keyboard.KeyPress(code);
        }
    }
}
