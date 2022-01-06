//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Hollowborn/HollowbornPaladin/LetsGetYouASuit.cs
//cs_include Scripts/Hollowborn/HollowbornPaladin/IGotYourBackAndYourTop.cs
//cs_include Scripts/Hollowborn/HollowbornPaladin/TheDarkSacrifice.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Chaos/DrakathArmorBot.cs
//cs_include Scripts/Chaos/AscendedDrakathGear.cs
//cs_include Scripts/Story/TowerOfDoom.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
//cs_include Scripts/Story/Artixpointe.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
using RBot;

public class ThePostSummoning
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreHollowborn HB = new CoreHollowborn();
    public TheDarkSacrifice HBPalAxe = new TheDarkSacrifice();
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        Bot.Player.LoadBank();

        GetAll();

        Core.SetOptions(false);
    }

    public string[] PostSummoningItems =
    {
        "Classic Hollowborn Paladin Armor",
        "Hollowborn Paladin Visor",
        "Hollowborn Paladin Hood",
        "Hollowborn Templar Helm",
        "Hollowborn Paladin Cape",
        "Hollowborn Paladin Cloak",
        "Dual Hollowborn Shadows of Fate",
        "Hollowborn Daimyo Battlepet",
        "Hollowborn Daimyo"
    };

    public void GetAll()
    {
        if (Core.CheckInventory(PostSummoningItems))
            return;
        HB.HardcoreContract();
        HBPalAxe.HBShadowOfFate();
        Farm.IcestormArena();

        Core.AddDrop(PostSummoningItems);
        while (!Core.CheckInventory(PostSummoningItems))
        {
            Core.EnsureAccept(7560);
            Core.HuntMonster("shadowblast", "Carnage", "Shadow Seal", 1, false);
            HB.HumanSoul(50);
            Core.EnsureCompleteChoose(7560, PostSummoningItems);
            Bot.Wait.ForPickup("*");
        }
        Core.ToBank(PostSummoningItems);
        return;
    }
}