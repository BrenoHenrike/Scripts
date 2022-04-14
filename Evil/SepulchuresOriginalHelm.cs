//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/Doomwood/AQWZombies.cs
//cs_include Scripts/Story/ThroneofDarkness/07bStranger(MysteriousDungeon).cs

using RBot;

public class SepulchuresOriginalHelm
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreFarms Farm = new CoreFarms();
    public CoreDailies Daily = new();
    public Core13LoC LOC => new Core13LoC();
    public MysteriousDungeon MystDung => new MysteriousDungeon();
    public AQWZombies Zombie = new AQWZombies();
    public string[] GravelynsDoomFireTokenItems = { "Empowered Essence", "Gravelyn's Blessing", "Painful Memory Bubble", "Burning Passion Flame", "Father's Sorrowful Tear", "Gravelyn's DoomFire Token", "Necrotic Sword of Doom", "Sepulchure's DoomKnight Armor" };

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        if (Core.CheckInventory("Sepulchure's Original Helm"))
            return;

        Story.PreLoad();

        Core.AddDrop("Sepulchure's Original Helm");
        if (Core.CheckInventory("Sepulchure's DoomKnight Armor"))
        {
            Core.BuyItem("shadowfall", 1642, "Sepulchure's Original Helm");
            Core.SetOptions(false);
        }

        Core.ChangeAlignment(Alignment.Evil);
        Farm.EvilREP(10);
        Farm.Experience(70);
        Zombie.Storyline();
        MystDung.Storyline();
        LOC.Hero();
        Core.EnsureAccept(6555);
        if (!Core.CheckInventory(new[] { "Lore's Champion Seal", "Gravelyns DoomFire Token", "Royal ShadowScythe Blade" }))
        {
            GravelynsDoomFireToken();
            RoyalShadowScytheBlade();
            Core.Relogin();
            Core.BuyItem(Bot.Map.Name, 993, "Lore's Champion Seal");
        }
        Core.EnsureComplete(6555);
        Bot.Wait.ForDrop("Sepulchure's Original Helm");
    }

    public void GravelynsDoomFireToken(int quant = 1)
    {
        if (Core.CheckInventory("Gravelyn's DoomFire Token", quant))
            return;

        Core.AddDrop(GravelynsDoomFireTokenItems);

        while (!Core.CheckInventory("Gravelyn's DoomFire Token", quant))
        {
            while (!Core.CheckInventory("Gravelyn's Blessing"))
            {
                if (Core.CheckInventory("Necrotic Sword of Doom"))
                    Core.ChainComplete(5455);
                else if (Core.CheckInventory("Sepulchure's DoomKnight Armor"))
                    Core.ChainComplete(5456);
                else Core.EnsureAccept(5457);
                Core.HuntMonster("necrodungeon", "Doom Overlord", "Essence of the Doomlord", isTemp: false);
                Core.EnsureComplete(5457);
                Bot.Wait.ForPickup("Gravelyn's Blessing");
            }
            Core.EnsureAccept(5458, 5459, 5460, 5461);
            Core.HuntMonster("swordhavenfalls", "Chaos Lord Alteon", "Doomed Memories", isTemp: false);
            Core.EnsureComplete(5458);
            Bot.Wait.ForPickup("Painful Memory Bubble");
            Core.HuntMonster("shadowstrike", "Sepulchuroth", "Sepulchuroth's Undying Flam", isTemp: false);
            Core.EnsureComplete(5459);
            Bot.Wait.ForPickup("Burning Passion Flame");
            Core.HuntMonster("Shadowfall", "Shadow of the Past", "Father's Anger", isTemp: false);
            Core.EnsureComplete(5460);
            Bot.Wait.ForPickup("Father's Sorrowful Tear");
            Core.HuntMonster("shadowrealmpast", "*", "Empowered Essence", 13, isTemp: false);
            Core.EnsureComplete(5461);
            Bot.Wait.ForPickup("Gravelyn's DoomFire Token");
        }
    }

    public void RoyalShadowScytheBlade()
    {
        if (Core.CheckInventory("Royal ShadowScythe Blade"))
            return;

        Farm.Gold(1000000);
        Farm.EvilREP(10);
        Core.BuyItem("shadowfall", 1639, "Royal ShadowScythe Blade");
    }

}