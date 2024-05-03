﻿using ESContract;

namespace ReshetoMAUI
{
    public static class MauiExtension
    {
        public static async Task ChangeColourByState(this Entry entry, State state)
        {
            Color colour = state switch
            {
                State.Unknown => new Color(100, 100, 100),
                State.Good => new Color(0, 255, 0),
                State.Bad => new Color(255, 0, 0),
                _ => new Color(100, 100, 100),
            };
            
            await Task.Run(() =>
            {
                Application.Current?.Dispatcher.DispatchAsync(() => entry.BackgroundColor = colour);
            });
        }
    }
}