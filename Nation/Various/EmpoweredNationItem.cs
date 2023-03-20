/*
name: Empowered Weapons of Nulgath
description: pick an empowered weapon, if you own teh requirements and 25 insignias, itll farm the empowered ver.
tags: Empowered, nulgath, reavers, bloodletter, overfiend blade, shadow spear, prismatic manslayers, legacy of nulgath, worshipper of nulgath, evovled void armor
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class EmpoweredWeaponsofNulgath
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();

    public string OptionsStorage = "EmpoweredWeaponofN";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<EmpoweredItems>("EmpoweredWep", "Choose Weapon", "Choose, and the bot will Farm the Appropriate item.", EmpoweredItems.Empowered_Overfiend_Blade),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.SetOptions();

        GetEmpoweredItem(Bot.Config.Get<EmpoweredItems>("EmpoweredWep"));

        Core.SetOptions(false);
    }

    public void GetEmpoweredItem(EmpoweredItems Item)
    {
        if (!Core.CheckInventory("Nulgath Insignia", 25))
            Core.Logger("Could not find 25x Nulgath Insignia, stopping.", messageBox: true, stopBot: true);

        Farm.Experience(80);
        Core.AddDrop(Nation.bagDrops);

        foreach (EmpoweredItems item in (EmpoweredItems[])Enum.GetValues(typeof(EmpoweredItems)))
        {
            switch (Bot.Config.Get<EmpoweredItems>("EmpoweredWep"))
            {
                //Empowered Bloodletter 8696
                case EmpoweredItems.Empowered_Bloodletter:
                    if (!Core.CheckInventory("Bloodletter of Nulgath"))
                        Core.Logger($"Missing required items. Bot cannot continue", messageBox: true, stopBot: true);

                    Core.EnsureAccept(8696);
                    Nation.SwindleBulk(350);
                    Nation.FarmDarkCrystalShard(200);
                    Nation.FarmDiamondofNulgath(500);
                    Nation.FarmVoucher(false);
                    Core.EnsureComplete(8696);
                    break;

                //Empowered Evolved Void Armors 1 8700
                case EmpoweredItems.Empowered_Evolved_Fiend:
                case EmpoweredItems.Empowered_Evolved_Void:
                    if (!Core.CheckInventory(new[] { "Evolved Fiend Of Nulgath", "Evolved Void Of Nulgath" }))
                        Core.Logger($"Missing required items. Bot cannot continue", messageBox: true, stopBot: true);

                    Core.EnsureAccept(8700);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("lair", "Red Dragon", "Phoenix Blade", isTemp: false);
                    Core.HuntMonster("Marsh2", "Soulseeker", "Soul Scythe", isTemp: false);
                    Core.HuntMonster("superdeath", "Super Death", "Chaos Tentacles", isTemp: false);
                    Nation.FarmDiamondofNulgath(1000);
                    Nation.FarmTotemofNulgath(30);
                    Farm.ChronoSpanREP(4);
                    Adv.BuyItem("thespan", 435, "Shadow Warrior");
                    Adv.BuyItem("tercessuinotlim", 1951, "Unmoulded Fiend Essence");
                    Core.EnsureComplete(8700);
                    break;

                //Empowered Evolved Void Armors 2 8701
                case EmpoweredItems.Empowered_Evolved_Blood:
                case EmpoweredItems.Empowered_Evolved_Shadow:
                case EmpoweredItems.Empowered_Evolved_Hex:
                    if (!Core.CheckInventory(new[] { "Evolved Hex of Nulgath", "Evolved Shadow of Nulgath", "Evolved Blood of Nulgath" }))
                        Core.Logger($"Missing required items. Bot cannot continue", messageBox: true, stopBot: true);

                    Core.EnsureAccept(8701);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("lair", "Red Dragon", "Phoenix Blade", isTemp: false);
                    Core.HuntMonster("Marsh2", "Soulseeker", "Soul Scythe", isTemp: false);
                    Core.HuntMonster("superdeath", "Super Death", "Chaos Tentacles", isTemp: false);
                    Nation.FarmDiamondofNulgath(1000);
                    Nation.FarmTotemofNulgath(30);
                    Farm.ChronoSpanREP(4);
                    Adv.BuyItem("thespan", 435, "Shadow Warrior");
                    Adv.BuyItem("tercessuinotlim", 1951, "Unmoulded Fiend Essence");
                    Core.EnsureComplete(8701);
                    break;

                //Empowered Legacy of Nulgath 8698
                case EmpoweredItems.Empowered_Legacy_of_Nulgath:
                    if (!Core.CheckInventory("Legacy of Nulgath"))
                        Core.Logger($"Missing required items. Bot cannot continue", messageBox: true, stopBot: true);

                    Core.EnsureAccept(8698);
                    Nation.FarmDiamondofNulgath();
                    Core.EnsureComplete(8698);
                    break;

                //Empowered Overfiend Blade 8693
                case EmpoweredItems.Empowered_Overfiend_Blade:
                    if (!Core.CheckInventory("Overfiend Blade of Nulgath"))
                        Core.Logger($"Missing required items. Bot cannot continue", messageBox: true, stopBot: true);

                    Core.EnsureAccept(8693);
                    Nation.SwindleBulk(200);
                    Nation.FarmDarkCrystalShard(100);
                    Nation.FarmDiamondofNulgath(400);
                    Nation.FarmVoucher(false);
                    Nation.FarmTotemofNulgath(30);
                    Nation.FarmGemofNulgath(80);
                    Core.EnsureComplete(8693);
                    break;

                //Empowered Prismatic Manslayers 8697
                case EmpoweredItems.Empowered_Prismatic_Manslayer:
                case EmpoweredItems.Empowered_Prismatic_Manslayers:
                    if (!Core.CheckInventory(new[] { "Taro's Prismatic Manslayer", "Taro's Dual Prismatic Manslayers" }) || !Core.IsMember)
                        Core.Logger($"Missing required items or your not a member. Bot cannot continue", messageBox: true, stopBot: true);

                    Core.EnsureAccept(8697);
                    Nation.SwindleBulk(400);
                    Nation.FarmDarkCrystalShard(250);
                    Nation.FarmDiamondofNulgath(600);
                    Nation.FarmGemofNulgath(150);
                    Nation.FarmBloodGem(70);
                    Core.EnsureComplete(8697);
                    break;

                //Empowered Shadow Spear 8695
                case EmpoweredItems.Empowered_Shadow_Spear:
                    if (!Core.CheckInventory("Shadow Spear of Nulgath"))
                        Core.Logger($"Missing required items. Bot cannot continue", messageBox: true, stopBot: true);

                    Core.EnsureAccept(8695);
                    Nation.SwindleBulk(350);
                    Nation.FarmDarkCrystalShard(200);
                    Nation.FarmDiamondofNulgath(500);
                    Nation.FarmVoucher(false);
                    break;

                //Empowered Ungodly Reavers 8694
                case EmpoweredItems.Empowered_Ungodly_Reavers:
                    if (!Core.CheckInventory("Ungodly Reavers of Nulgath"))
                        Core.Logger($"Missing required items. Bot cannot continue", messageBox: true, stopBot: true);

                    Core.EnsureAccept(8694);
                    Nation.SwindleBulk(200);
                    Nation.FarmDarkCrystalShard(100);
                    Nation.FarmDiamondofNulgath(400);
                    Nation.FarmVoucher(false);
                    Nation.FarmTotemofNulgath(30);
                    Nation.FarmGemofNulgath(80);
                    Core.EnsureComplete(8694);
                    break;

                //Empowered Worshipper of Nulgath 8699
                case EmpoweredItems.Empowered_Worshipper_of_Nulgath:
                    if (!Core.CheckInventory("Worshipper of Nulgath"))
                        Core.Logger($"Missing required items. Bot cannot continue", messageBox: true, stopBot: true);

                    Core.EnsureAccept(8699);
                    Nation.FarmDiamondofNulgath();
                    Core.EnsureComplete(8699);
                    break;
            }
        }
    }

    public enum EmpoweredItems
    {
        Empowered_Overfiend_Blade,
        Empowered_Ungodly_Reavers,
        Empowered_Shadow_Spear,
        Empowered_Bloodletter,
        Empowered_Prismatic_Manslayer,
        Empowered_Prismatic_Manslayers,
        Empowered_Legacy_of_Nulgath,
        Empowered_Worshipper_of_Nulgath,
        Empowered_Evolved_Void,
        Empowered_Evolved_Fiend,
        Empowered_Evolved_Blood,
        Empowered_Evolved_Hex,
        Empowered_Evolved_Shadow,
    };
}
