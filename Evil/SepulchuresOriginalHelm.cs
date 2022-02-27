//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/Doomwood/AQWZombies.cs
//cs_include Scripts/Story/MysteriousDungeon.cs

using RBot;

public class SepulchuresOriginalHelm
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new CoreAdvanced();
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


        if (Core.CheckInventory("Necrotic Sword of Doom"))
            Core.ChainComplete(5455);
        if (Core.CheckInventory("Sepulchure's DoomKnight Armor"))
            Core.ChainComplete(5456);
        Core.SmartKillMonster(5457, "necrodungeon", "Doom Overlord", completeQuest: true);
        Core.SmartKillMonster(5458, "swordhavenfalls", "Chaos Lord Alteon", completeQuest: true);
        Core.SmartKillMonster(5459, "shadowstrike", "Sepulchuroth", completeQuest: true);
        Core.SmartKillMonster(5460, "Shadowfall", "Shadow of the Past", completeQuest: true);
        Core.SmartKillMonster(5461, "shadowrealmpast", "*", completeQuest: true);
        Bot.Wait.ForDrop("Gravelyn's DoomFire Token");
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