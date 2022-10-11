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
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNation Nation = new();

    public WillpowerExtraction WillpowerExtraction = new WillpowerExtraction();
    public NulgathDemandsWork NulgathDemandsWork = new NulgathDemandsWork();

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

        // string[] NDWRequiredItems = { "DoomLord's War Mask", "ShadowFiend Cloak", "Locks of the DoomLord", "Doomblade of Destruction" };
        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop("ArchFiend DoomLord", "Undead Essence", "Chaorruption Essence",
            "Essence Potion", "Essence of Klunk", "Living Star Essence", "Bone Dust",
            "Undead Energy", "DoomLord's War Mask", "ShadowFiend Cloak", "Locks of the DoomLord", "Doomblade of Destruction");

        // Quest Accept Requirements: "DoomLord's War Mask", "ShadowFiend Cloak", "Locks of the DoomLord", "Doomblade of Destruction" 
        NulgathDemandsWork.NDWQuest();

        //Quest Turnin Items:
        WillpowerExtraction.Unidentified34(4);
        NulgathDemandsWork.NDWQuest(new[] {"Unidentified 35"});
        Nation.ContractExchange(ChooseReward.BloodGemoftheArchfiend);
        Nation.FarmUni13();
        Nation.ApprovalAndFavor(0, 5000);
        Nation.EssenceofNulgath(100);

        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("evilwardage", "Klunk", "Essence of Klunk", isTemp: false);
        Core.EquipClass(ClassType.Farm);
        Farm.BattleUnderB("Undead Essence", 1000);

        Nation.FarmVoucher(false);
        Nation.FarmBloodGem(10);

        Core.BuyItem("yulgar", 16, "Aelita's Emerald");

        if (!Core.CheckInventory("Essence Potion", 5))
        {
            Farm.Gold(12500000);
            Core.BuyItem("alchemyacademy", 2115, "Gold Voucher 500k", 25);
            Core.BuyItem("alchemyacademy", 2115, "Essence Potion", 5, 1, 9770);
            Bot.Wait.ForItemBuy();
        }
        Core.EnsureAccept(5260);

        Core.KillMonster("orecavern", "r3", "Up", "*", "Chaorruption Essence", 75, false);
        Core.HuntMonster("starsinc", "Living Star", "Living Star Essence", 100, false);

        //Quest Turnin
        if (!Bot.Quests.CanComplete(5260))
            Core.Relogin();
        Core.ChainComplete(5260);
        Bot.Wait.ForPickup("ArchFiend DoomLord");
    }
}