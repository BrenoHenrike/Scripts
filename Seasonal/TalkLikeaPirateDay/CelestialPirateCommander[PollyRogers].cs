//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class CelestialPirateCommander
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public bool DontPreconfigure = true;
    public string OptionsStorage = "Pet only or All";
    public List<IOption> Options = new List<IOption>()
    {
        new Option<bool>("PetOnly", "Do you want to get the pet only?", "Whether to farm only the pet or everthing", false),
        CoreBots.Instance.SkipOptions,
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetCPC(Bot.Config.Get<bool>("PetOnly"));

        Core.SetOptions(false);
    }

    public string[] Rewards = {
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

    public void GetCPC(bool PetOnly = true)
    {
        if (PetOnly && Core.CheckInventory("Polly Roger"))
        {
            Core.Logger($"You already have Polly Roger");
            return;
        }
        int i = 1;
        Core.AddDrop(Rewards);
        Core.EquipClass(ClassType.Solo);
        while (!Bot.ShouldExit && !Core.CheckInventory(Rewards, toInv: false))
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

            if (Bot.Config.Get<bool>("PetOnly"))
            {
                Core.EnsureCompleteChoose(7713, new[] { "Polly Roger" });
                return;
            }
            else
            {
                Core.EnsureCompleteChoose(7713);
                Core.ToBank(Rewards);
                Core.Logger($"Completed x{i++}");
            }
        }
        Core.Logger($"You already have all the drops");
    }
}
