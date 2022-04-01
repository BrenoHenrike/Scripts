//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
//cs_include Scripts/Nulgath/AFDL/WillpowerExtraction.cs
//cs_include Scripts/Nulgath/AFDL/NulgathDemandsWork.cs
using RBot;

public class EnoughDOOMforanArchfiend
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNulgath Nulgath = new CoreNulgath();

    public WillpowerExtraction WillpowerExtraction = new WillpowerExtraction();
    public NulgathDemandsWork NulgathDemandsWork = new NulgathDemandsWork();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nulgath.bagDrops);
        Core.BankingBlackList.AddRange(new[] {"ArchFiend DoomLord", "Undead Essence", "Chaorruption Essence",
            "Essence Potion", "Essence of Klunk", "Living Star Essence", "Bone Dust", "Undead Energy"});
        Core.SetOptions();

        AFDL();

        Core.SetOptions(false);
    }

    public void AFDL()
    {
        NulgathDemandsWork.Unidentified35();

        Core.AddDrop(Nulgath.bagDrops);
        Core.AddDrop("ArchFiend DoomLord", "Undead Essence", "Chaorruption Essence",
            "Essence Potion", "Essence of Klunk", "Living Star Essence", "Bone Dust", "Undead Energy");

        Core.Unbank("DoomLord's War Mask", "ShadowFiend Cloak", "Locks of the DoomLord", "Doomblade of Destruction");

        WillpowerExtraction.Unidentified34(4);

        Nulgath.ContractExchange(ChooseReward.BloodGemoftheArchfiend);

        Nulgath.FarmUni13(1);

        Nulgath.ApprovalAndFavor(0, 5000);

        Nulgath.EssenceofNulgath(100);
        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("evilwardage", "Klunk", "Essence of Klunk", 1, false);
        Core.EquipClass(ClassType.Farm);
        Farm.BattleUnderB("Undead Essence", 1000);

        Nulgath.FarmVoucher(false);

        Nulgath.FarmBloodGem(2);

        Core.BuyItem("digitalyulgar", 16, "Aelita's Emerald");

        while (!Core.CheckInventory("Essence Potion", 5))
        {
            if (!Bot.Player.Factions.Exists(f => f.Name == "Alchemy"))
                Core.Logger("You need at least 1 point in Alchemy for the packets to work, make sure you do 1 potion first in /Join Alchemy.", messageBox: true, stopBot: true);

            Core.HuntMonster("orecavern", "Deathmole", "Arashtite Ore", 2, false);
            Core.HuntMonster("deathsrealm", "Skeleton Fighter", "Necrot", 2, false);
            Farm.AlchemyPacket("Necrot", "Arashtite Ore", AlchemyRunes.Uruz);
            Bot.Player.Pickup("Essence Potion");
        }

        Core.EnsureAccept(5260);

        Core.KillMonster("orecavern", "r3", "Up", "*", "Chaorruption Essence", 75, false);

        Core.HuntMonster("starsinc", "Living Star", "Living Star Essence", 100, false);

        if (!Bot.Quests.CanComplete(5260))
            Bot.Player.Logout();
        Core.EnsureComplete(5260);
        Bot.Player.Pickup("ArchFiend DoomLord");
    }
}