//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts\Projects W.I.P\Adventure Quest Worlds Zombies.cs
//cs_include Scripts\Projects W.I.P\MysteriousDungeon.cs

using RBot;

public class SepulchuresOriginalHelm
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDailys Dailys = new CoreDailys();
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
        Core.AddDrop("Sepulchure's Original Helm");
        if (Core.CheckInventory("Sepulchure's DoomKnight Armor"))
        {
            Core.BuyItem("shadowfall", 1642, "Sepulchure's Original Helm");
            Core.SetOptions(false);
        }

        Core.SwitchAlignment(Alignment.Evil);
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
            Core.BuyItem(Bot.Map.Name, 993, "Lore's Champion Seal");
        }
        Core.EnsureComplete(6555);
        Bot.Wait.ForDrop("Sepulchure's Original Helm");
    }

    public void GravelynsDoomFireToken()
    {
        if (Core.CheckInventory("Gravelyn's DoomFire Token"))
            return;


        Core.AddDrop(GravelynsDoomFireTokenItems);


        while (!Core.CheckInventory("Gravelyn's DoomFire Token"))
        {
            // A Loyal Servant: Necrotic Sword of Doom
            if (Core.CheckInventory("Necrotic Sword of Doom") && !Core.CheckInventory("Gravelyn's Blessing") && !Core.CheckInventory("Gravelyn's DoomFire Token"))
                Core.ChainComplete(5455);

            // A Loyal Servant: Sepulchure's DoomKnight
            if (Core.CheckInventory("Sepulchure's DoomKnight Armor") && !Core.CheckInventory("Gravelyn's Blessing") && !Core.CheckInventory("Gravelyn's DoomFire Token"))
                Core.ChainComplete(5456);

            // Let Darkness Enter your Heart
            if (!Core.CheckInventory("Gravelyn's Blessing") || Core.CheckInventory("Gravelyn's DoomFire Token"))
                Core.SmartKillMonster(5457, "necrodungeon", "Doom Overlord", completeQuest: true);

            // Find me some Doom
            if (!Core.CheckInventory("Painful Memory Bubble") || Core.CheckInventory("Gravelyn's DoomFire Token"))
                Core.SmartKillMonster(5458, "swordhavenfalls", "Chaos Lord Alteon", completeQuest: true);

            // Abyssal
            if (!Core.CheckInventory("Burning Passion Flame") || Core.CheckInventory("Gravelyn's DoomFire Token"))
                Core.SmartKillMonster(5459, "shadowstrike", "Sepulchuroth", completeQuest: true);

            // Memories of The Past
            if (!Core.CheckInventory("Father's Sorrowful Tear") || Core.CheckInventory("Gravelyn's DoomFire Token"))
                Core.SmartKillMonster(5460, "Shadowfall", "Shadow of the Past", completeQuest: true);

            // The Summoning
            if (!Core.CheckInventory("Empowered Essence", 13) || Core.CheckInventory("Gravelyn's DoomFire Token"))
                Core.SmartKillMonster(5461, "shadowrealmpast", "*", completeQuest: true);
            Bot.Wait.ForDrop("Gravelyn's DoomFire Token");
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