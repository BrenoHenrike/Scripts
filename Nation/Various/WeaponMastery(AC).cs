/*
name: Weapon Mastery (AC)
description: This bot does the Weapon Mastery (AC) quest of the "Evolved Warlord Orb", until both the hammer and axe are obtained.
tags: evolved, warlord, orb, hammer, axe, nation, nulgath, underfiend
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;

public class WeaponMasteryAC
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreNation Nation = new();
    private CoreFarms Farm = new();
    private Core13LoC LoC = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.SetOptions();

        GetWM();

        Core.SetOptions(false);
    }

    public void GetWM()
    {
        if (Core.CheckInventory(new[] { "Evolved Warlord Hammer", "Evolved Warlord Axe" }))
        {
            Core.Logger($"Weapon Mastery archieved!");
            return;
        }

        if (!Core.CheckInventory(new[] { 33366, 33199 }, any: true))
        {
            Core.Logger($"Evolved Warlord Orb not found!");
            return;
        }

        LoC.Xiang();
        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop("Evolved Warlord Axe", "Evolved Warlord Hammer");

        Core.FarmingLogger("Evolved Warlord Hammer", 1);
        Core.FarmingLogger("Evolved Warlord Axe", 1);

        // 4786 - Members
        // 4784 - Ac
        while (!Bot.ShouldExit && !Core.CheckInventory(new[] { "Evolved Warlord Hammer", "Evolved Warlord Axe" }))
        {
            Core.EnsureAccept(Core.CheckInventory(33366, toInv: false) ? 4786 : 4784);
            Nation.FarmUni13(1);
            Nation.SwindleBulk(10);
            Nation.FarmDarkCrystalShard(10);
            Nation.FarmTotemofNulgath(1);
            Nation.FarmGemofNulgath(10);

            if (!Core.CheckInventory("Underfriend Blade of Nulgath"))
            {
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonsterMapID("mirrorportal", 1, "Mirror Realm Token", 10, false);

                Farm.Gold(100000);
                Core.BuyItem("mirrorportal", 618, "Underfriend Blade of Nulgath");
            }
            Core.EnsureComplete(Core.CheckInventory(33366, toInv: false) ? 4786 : 4784);
            Bot.Wait.ForPickup("Evolved Warlord Hammer");
            Bot.Wait.ForPickup("Evolved Warlord Axe");
        }
        Core.CancelRegisteredQuests();
    }
}
//Made by ToxlcChain
