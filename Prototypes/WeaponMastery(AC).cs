/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;

public class WeaponMasteryAC
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();
    public CoreFarms Farm = new();
    public Core13LoC LoC = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);

        Core.SetOptions();

        GetWM();

        Core.SetOptions(false);
    }

    public void GetWM()
    {
        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop("Evolved Warlord Axe", "Evolved Warlord Hammer");
        if (Core.CheckInventory("Evolved Warlord Orb"))
        {
            if (Core.CheckInventory("Evolved Warlord Hammer") && Core.CheckInventory("Evolved Warlord Axe"))
            {
                Core.Logger($"Weapon Mastery archieved!");
                return;
            }
            int i = 1;
            while (!Bot.ShouldExit && !Core.CheckInventory("Evolved Warlord Hammer") || !Core.CheckInventory("Evolved Warlord Axe"))
            {
                Core.EnsureAccept(4784);

                Nation.FarmUni13(1);

                Nation.SwindleBulk(10);

                Nation.FarmDarkCrystalShard(10);

                Nation.FarmTotemofNulgath(1);

                Nation.FarmGemofNulgath(10);

                if (!Core.CheckInventory("Underfriend Blade of Nulgath"))
                {
                    LoC.Xiang();
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(3188);
                    while (!Bot.ShouldExit && !Core.CheckInventory("Mirror Realm Token", 10))
                    {
                        Core.HuntMonsterMapID("mirrorportal", 1);
                        Core.CancelRegisteredQuests();
                    }
                    if (Bot.Player.Gold <= 100000)
                    {
                        Farm.Gold(100000);
                    }
                    Core.BuyItem("mirrorportal", 618, "Underfriend Blade of Nulgath");
                }

                Core.EnsureComplete(4784);
                Core.Logger($"Completed x{i++}");
            }
        }
        else
        {
            Core.Logger($"Evolved Warlord Orb not found!");
            return;
        }
    }
}
//Made by ToxlcChain
