//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Hollowborn/HollowbornPaladin/LetsGetYouASuit.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
using RBot;

public class IGotYourBackAndYourTop
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreHollowborn HB = new CoreHollowborn();
    public LetsGetYouASuit HBPal = new LetsGetYouASuit();
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        Bot.Player.LoadBank();

        HBPaladinHelmet();

        Core.SetOptions(false);
    }

    public void HBPaladinHelmet()
    {
        if (Core.CheckInventory("Hollowborn Paladin Helmet"))
            return;
        HB.HardcoreContract();
        HBPal.HBPaladin();
        Farm.IcestormArena(85);

        Core.AddDrop("Dark Aura Gem", "Enchantment Rune", "Shadow Dragon Soul", "Hollowborn Paladin Helmet");
        Core.EnsureAccept(7558);
        Core.BuyItem("necropolis", 410, "Templar's Helm of Light");
        Farm.DoomwoodREP(6);
        Core.BuyItem("necropolis", 408, "Destiny Cloak");
        if (!Core.CheckInventory("Dark Aura Gem"))
        {
            if (!Core.CheckInventory("Enchanted Victory Blade"))
            {
                Core.BuyItem("river", 1213, "Silver Victory Blade");
                if (!Core.CheckInventory("Enchantment Rune"))
                {
                    Core.EnsureAccept(4811);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("graveyard", "Skeletal Viking", "Ravenwing Scroll");
                    Core.HuntMonster("graveyard", "Skeletal Warrior", "Unseen Eye", 3);
                    Core.HuntMonster("graveyard", "Big Jack Sprat", "Shard of Diamond Blade", 5);
                    Core.EnsureComplete(4811);
                }
                Core.BuyItem("river", 1213, "Enchanted Victory Blade");
            }
            Core.EnsureAccept(4812);
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("graveyard", "Skeletal Warrior", "Broken Dream Catcher", 10);
            Core.EnsureCompleteChoose(4812, new[] {"Dark Aura Gem"});
        }
        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("necrocavern", "Shadow Dragon", "Shadow Dragon Soul", 1, false);
        Core.HuntMonster("temple", "Cryptkeeper Lich", "Cryptkeeper Lich's Head");
        HB.HumanSoul(200);
        Core.EnsureComplete(7558);
        return;
    }
}