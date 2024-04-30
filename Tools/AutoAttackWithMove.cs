/*
name: Auto Attack with Auto Movement
description: This bot will automatically move to the right locations for Nightmare Carnax and Ultra Dage
tags: nightmare, carnax, ultra, dage, auto, attack, movement, tool
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class AAWithMove
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions(disableClassSwap: true);

        AutoMove();

        Core.SetOptions(false);
    }

    public void AutoMove()
    {
        switch (Bot.Map.Name.ToLower())
        {
            case "darkcarnax":
                Core.AddDrop("Synthetic Viscera");
                Core.Jump("Boss", "Left");
                Bot.Player.SetSpawnPoint();
                Core.RegisterQuests(8872);
                Bot.Options.AttackWithoutTarget = true;
                Bot.Events.RunToArea += moveNightmareCarnax;

                break;

            case "ultradage":
                Core.AddDrop("Dage the Evil Insignia");
                Core.Jump("Boss", "Right");
                Bot.Player.SetSpawnPoint();
                Bot.Events.RunToArea += moveUltraDage;
                break;
        }

        

        while (!Bot.ShouldExit)
            Bot.Combat.Attack("*");

        void _walk((int, int) X, (int, int) Y) => Bot.Player.WalkTo(Bot.Random.Next(X.Item1, X.Item2), Bot.Random.Next(Y.Item1, Y.Item2));

        void moveNightmareCarnax(string zone)
        {
            switch (zone.ToLower())
            {
                case "a":
                    //Move to the right
                    _walk((600, 930), (380, 475));
                    break;
                case "b":
                    //Move to the left
                    _walk((25, 325), (380, 475));
                    break;
                default:
                    //Move to the center
                    _walk((325, 600), (380, 475));
                    break;
            }
        }

        void moveUltraDage(string zone)
        {
            switch (zone.ToLower())
            {
                case "a":
                    //Move to the left
                    _walk((40, 175), (400, 410));
                    break;
                case "b":
                    //Move to the right
                    _walk((760, 930), (410, 415));
                    break;
                default:
                    //Move to the center
                    _walk((480, 500), (300, 420));
                    break;
            }
        }
    }
}
