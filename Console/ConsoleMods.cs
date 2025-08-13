using Console;
using GunTemplateee;
using System;
using System.Collections.Generic;
using System.Text;

namespace Console
{
    internal class ConsoleMods
    {
        public static void KickConsolePlayer()
        {
            GunTemplate.StartBothGuns(() =>
            {
                Console.ExecuteCommand("kick", GunTemplate.lockedPlayer.creator.ActorNumber, new object[]
                {

                });
            }, true);
        }
    }
}
