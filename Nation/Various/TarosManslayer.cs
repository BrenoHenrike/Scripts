/*
name: TarosManslayer
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/Various/PurifiedClaymoreOfDestiny.cs
using Skua.Core.Interfaces;
public class TarosManslayer
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNation Nation = new();
    public PurifiedClaymoreOfDestiny PCoD = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GuardianTaro(!Core.IsMember);

        Core.SetOptions(false);
    }

    private string[] Rewards = { "Taro's Manslayer", "Taro Blademaster Guardian" };

    public void GuardianTaro(bool ManslayerOnly = true)
    {

        Core.OneTimeMessage("Dark Makai Rune/Sigil Solution", "Randomizing location for \"Dark Makai\"\n" +
        "as the drop can randomly stop showing up", forcedMessageBox: false);

        if (ManslayerOnly)
        {
            Rewards = new[] { "Taro's Manslayer" };
        }

        if (Core.CheckInventory(Rewards))
            return;

        Core.AddDrop(Rewards);

        if (ManslayerOnly)
        {
            Core.HuntMonster("tercessuinotlim", "Taro Blademaster", "Taro's Manslayer", isTemp: false);
        }
        else
        {
            Farm.GoodREP();
            PCoD.GetPCoD();

            while (!Bot.ShouldExit && !Core.CheckInventory(Rewards))
            {
                Core.EnsureAccept(1111);
                Nation.FarmGemofNulgath(10);
                Nation.ResetSindles();
                while (!Bot.ShouldExit && !Core.CheckInventory("Dark Makai Rune"))
                    foreach (var mapInfo in new[] { ("tercessuinotlim", "m1"), (Core.IsMember ? "Nulgath" : "evilmarsh", "Field1") })
                    {
                        Core.Join(mapInfo.Item1, mapInfo.Item2, "Left");
                        while (!Bot.ShouldExit && Core.IsMonsterAlive(1, useMapID: true))
                        {
                            Core.Sleep();
                            Bot.Combat.Attack("*");
                        }
                    }
                Bot.Wait.ForPickup("Dark Makai Rune");

                Core.EnsureCompleteChoose(1111, Rewards);
                Core.Sleep();
            }
        }
    }
}
