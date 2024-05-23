/*
name: TarosPrismaticManslayers
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/Various/PurifiedClaymoreOfDestiny.cs
//cs_include Scripts/Nation/Various/TarosManslayer.cs
using Skua.Core.Interfaces;
public class TarosPrismaticManslayers
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNation Nation = new();
    public TarosManslayer Taro = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        TemptationTest();

        Core.SetOptions(false);
    }

    private string[] Rewards = { "Taro's Prismatic Manslayer", "Taro's Dual Prismatic Manslayers", "Taro's BattleBlade" };

    public void TemptationTest()
    {
        if (Core.CheckInventory(Rewards) || !Core.IsMember)
        {
            Core.Logger(!Core.IsMember ? "Membership required for the quest `A Test of Temptation`" : "Rewards already in inventory");
            return;
        }

        Core.AddDrop(Rewards);

        Farm.Experience(80);
        Farm.GoodREP();
        Taro.GuardianTaro();

        while (!Bot.ShouldExit && !Core.CheckInventory(Rewards))
        {
            Core.EnsureAccept(8496);
            Nation.SwindleBulk(200);
            Nation.FarmDarkCrystalShard(125);
            Nation.FarmDiamondofNulgath(300);
            Nation.FarmGemofNulgath(75);
            Nation.FarmBloodGem(35);
            Core.EnsureCompleteChoose(8496, Rewards);
            Core.Sleep();
        }
    }
}
