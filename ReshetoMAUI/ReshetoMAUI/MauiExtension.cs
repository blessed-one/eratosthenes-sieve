using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESContract;

namespace ReshetoMAUI
{
    public static class MauiExtension
    {
        public static void ChangeColourByState(this Entry entry, State state)
        {
            Color colour = state switch
            {
                State.Unknown => new Color(100, 100, 100),
                State.Good => new Color(0, 255, 0),
                State.Bad => new Color(255, 0, 0),
                _ => new Color(100, 100, 100),
            };

            MainThread.BeginInvokeOnMainThread(() => entry.BackgroundColor = colour);
        }
    }
}
