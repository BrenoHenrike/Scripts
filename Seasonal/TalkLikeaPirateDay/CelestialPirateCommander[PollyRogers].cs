//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class CelestialPirateCommander
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetCPC(true);

        Core.SetOptions(false);
    }

    private string[] Rewards = {
        "Celestial Pirate Commander",
        "Celestial Commander's Hat",
        "Celestial Commander's Locks",
        "Celestial Commander's Locks + Hat",
        "Celestial Commander's Wings",
        "Celestial Commander's Back Blade",
        "Celestial Commander's Wings+ Blade",
        "Celestial Commander's Sword",
        "Celestial Commander's Hat + Morph",
        "Celestial Commander's Morph + Locks",
        "Celestial Commander's Plank",
        "Polly Roger"
    };

    public void GetCPC(bool OnlyPolly)
    {
        if (OnlyPolly && Core.CheckInventory("Polly Roger"))
            return;
        if (!OnlyPolly && Core.CheckInventory(Rewards))
            return;

        Core.AddDrop(Rewards);
        while (!Core.CheckInventory(Rewards))
        {
            Core.EnsureAccept(7713);
            Core.HuntMonster("frozenlair", "Legion Lich Lord", "Sapphire Orb", 5, false, publicRoom: true);
            Core.HuntMonster("lostruinswar", "Diabolical Warlord", "Rumors of the Celestial Commander", 5, false, publicRoom: true);
            Core.HuntMonster("iceplane", "Animus of Ice", "Starlit Journal Page 1 Scraps", 10, false);
            Core.HuntMonster("ivoliss", "Ivoliss", "Starlit Journal Page 2 Scraps", 10, false, publicRoom: true);
            Core.HuntMonster("thevoid", "Nightbane", "Starlit Journal Page 3 Scraps", 10, false, publicRoom: true);
            Core.HuntMonster("extinction", "Ultra SN.O.W.", "Starlit Journal Page 4 Scraps", 10, false, publicRoom: true);
            Core.HuntMonster("starsinc", "Empowered Prime", "Map of the Celestial Seas", 1, false, publicRoom: true);
            Core.HuntMonster("underlair", "ArchFiend DragonLord", "Coffer of the Stars", 1, false, publicRoom: true);
            if (!Core.CheckInventory("Polly Roger"))
                Core.EnsureComplete(7713, 56776);
            else Core.EnsureCompleteChoose(7713, Rewards);
            Bot.Sleep(Core.ActionDelay);
            if (OnlyPolly && Core.CheckInventory("Polly Roger"))
                break;
        }
    }
}