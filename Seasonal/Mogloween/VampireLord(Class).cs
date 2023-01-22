/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class VampireLord
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.Add("Blood Moon Token");
        Core.SetOptions();

        GetClass(true);

        Core.SetOptions(false);
    }

    public void GetClass(bool rankUpClass = false)
    {
        if (!Core.isSeasonalMapActive("mogloween"))
            return;
        if (Core.CheckInventory(41575, toInv: false))
            return;

        Core.FarmingLogger("Blood Moon Token", 300);
        Bot.Drops.Add("Blood Moon Token");
        Core.EquipClass(ClassType.Solo);

        while (!Bot.ShouldExit && !Core.CheckInventory("Blood Moon Token", 300))
        {

            while (!Bot.ShouldExit && Core.CheckInventory("Black Blood Vial", 1) && Core.CheckInventory("Moon Stone", 1))
            {
                Bot.Sleep(Core.ActionDelay);
                Core.ChainComplete(Core.IsMember ? 6060 : 6059);
                Bot.Wait.ForPickup("Blood Moon Token");
                if (Core.CheckInventory("Blood Moon Token", 300))
                    break;
            }

            //to keep track:
            Core.EnsureAccept(Core.IsMember ? 6060 : 6059);

            Core.KillMonster("bloodmoon", "r12a", "Left", "Black Unicorn", "Black Blood Vial", 100, isTemp: false);
            Core.KillMonster("bloodmoon", "r4a", "Left", "Lycan Guard", "Moon Stone", 100, isTemp: false);
        }
        Core.BuyItem("mogloween", 1477, "Vampire Lord", shopItemID: 5459);

        if (rankUpClass)
            Adv.rankUpClass("Vampire Lord");
    }
}
