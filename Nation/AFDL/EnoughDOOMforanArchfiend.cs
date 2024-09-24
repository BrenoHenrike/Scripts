/*
name: EnoughDOOMforanArchfiend
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/AFDL/WillpowerExtraction.cs
//cs_include Scripts/Nation/AFDL/NulgathDemandsWork.cs
//cs_include Scripts/Nation/Various/GoldenHanzoVoid.cs
using Skua.Core.Interfaces;

public class EnoughDOOMforanArchfiend
{
    public static IScriptInterface Bot => IScriptInterface.Instance;
    public static CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreNation Nation = new();

    public WillpowerExtraction WillpowerExtraction = new();
    public NulgathDemandsWork NulgathDemandsWork = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.BankingBlackList.AddRange(new[] {"ArchFiend DoomLord", "Undead Essence", "Chaorruption Essence",
            "Essence Potion", "Essence of Klunk", "Living Star Essence", "Bone Dust", "Undead Energy"});
        Core.SetOptions();

        AFDL();

        Core.SetOptions(false);
    }

    public void AFDL()
    {
        if (Core.CheckInventory("ArchFiend DoomLord", toInv: false))
            return;

        string[] NDWRequiredItems = { "DoomLord's War Mask", "ShadowFiend Cloak", "Locks of the DoomLord", "Doomblade of Destruction" };
        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop("ArchFiend DoomLord", "Undead Essence", "Chaorruption Essence",
            "Essence Potion", "Essence of Klunk", "Living Star Essence", "Bone Dust",
            "Undead Energy", "DoomLord's War Mask", "ShadowFiend Cloak", "Locks of the DoomLord", "Doomblade of Destruction");

        // Quest Accept Requirements: "DoomLord's War Mask", "ShadowFiend Cloak", "Locks of the DoomLord", "Doomblade of Destruction" 
        Nation.FarmUni13(1);
        Nation.ApprovalAndFavor(0, 1);
        NulgathDemandsWork.NDWQuest(NDWRequiredItems);

        //Quest Turnin Items:
        NulgathDemandsWork.NDWQuest(new[] { "Unidentified 35" });
        WillpowerExtraction.Unidentified34(4);
        Nation.FarmBloodGem(10);
        Nation.FarmVoucher(false);
        Nation.EssenceofNulgath(100);
        Farm.BattleUnderB("Undead Essence", 1000);
        Core.EnsureAccept(5260);
        Core.EquipClass(ClassType.Farm);
        Core.HuntMonster("orecavern", "Chaorrupted Good Soldier", "Chaorruption Essence", 75, false);
        Core.HuntMonster("starsinc", "Living Star", "Living Star Essence", 100, false);

        Core.BuyItem("yulgar", 16, "Aelita's Emerald");
        Adv.BuyItem("alchemyacademy", 2115, "Essence Potion", 5, 9770); // see if this works
                                                                        // if (!Core.CheckInventory("Essence Potion", 5))
                                                                        // {
                                                                        //     Farm.Gold(12500000);
                                                                        //     Core.BuyItem("alchemyacademy", 2115, "Gold Voucher 500k", 25);
                                                                        //     Core.BuyItem("alchemyacademy", 2115, "Essence Potion", 5, 9770);
                                                                        //     Bot.Wait.ForItemBuy();
                                                                        // }

        Nation.ApprovalAndFavor(0, 5000);
        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("evilwardage", "Klunk", "Essence of Klunk", isTemp: false);

        //Quest Turnin
        Core.ChainComplete(5260);
        Bot.Wait.ForPickup("ArchFiend DoomLord");
    }
}
