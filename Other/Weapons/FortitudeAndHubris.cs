//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class FandH
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        FortitudeAndHubris();

        Core.SetOptions(false);
    }

    public void FortitudeAndHubris()
    {
        Core.AddDrop("Zorbak's Secret G-Rave Key", "Sword's Cost", "Shards of the Sword",
        "Hubris's Final Blade Shard", "Hubris' Magic Essence", "Hubris", "Fortitude's Blade Shards",
        "Fortitude's Magic Essence", "Fortitude", "Fortitude + Hubris");
        if (Core.CheckInventory("Fortitude + Hubris"))
            return;
        Core.EquipClass(ClassType.Farm);
        if (!Bot.Quests.IsUnlocked(6594))
        {
            Core.EnsureAccept(6593);
            Core.KillMonster("stalagbite", "Enter", "Spawn", "*", "Super Specific Rock");
            Core.EnsureComplete(6593);
        }
        if (!Bot.Quests.IsUnlocked(6595))
        {
            Core.EnsureAccept(6594);
            Core.KillMonster("pines", "Mountain", "Left", "Red Shell Turtle", "Turtle Shell");
            Core.KillMonster("pines", "Mountain", "Left", "Pine Grizzly", "Coffee Beans of Awakeness");
            Core.KillMonster("pines", "Llama", "Left", "Pine Troll", "String Fibers", 5);
            Core.GetMapItem(6114, map: "tavern");
            Core.EnsureCompleteChoose(6594);
        }
        if (!Bot.Quests.IsUnlocked(6596))
        {
            Core.EnsureAccept(6595);
            Core.KillMonster("river", "End", "Left", "*", "Pisces Shield Pieces", 20);
            Core.EnsureComplete(6595);
        }
        if (!Bot.Quests.IsUnlocked(6598))
        {
            Core.EnsureAccept(6596);
            Core.KillMonster("maul", "r3", "Down", "*", "Zorbak's Secret G-Rave Key", isTemp: false);
            Core.KillMonster("maul", "r3", "Down", "*", "Ebil Limbs", 5);
            Core.GetMapItem(6116, map: "maul");
            Core.EnsureCompleteChoose(6596);
        }
        if (!Bot.Quests.IsUnlocked(6599))
        {
            Core.EnsureAccept(6598);
            Core.KillMonster("shadowrealm", "r11", "Left", "*", "ENED+D Lance");
            Core.EnsureComplete(6598);
        }
        if (!Bot.Quests.IsUnlocked(6600))
        {
            Core.EnsureAccept(6599);
            Core.KillMonster("dragontown", "r9", "Right", "*", "Time Traveled Sword");
            Core.EnsureComplete(6599);
        }
        if (!Bot.Quests.IsUnlocked(6601))
        {
            Core.EnsureAccept(6600);
            Core.KillMonster("mountfrost", "War", "Left", "*", "Golems Defeated", 20);
            Core.GetMapItem(6115, map: "david");
            Core.EnsureCompleteChoose(6600);
        }
        if (!Bot.Quests.IsUnlocked(6602))
        {
            Core.EnsureAccept(6601);
            Core.KillMonster("northpointe", "r12", "Left", "Wyvern", "Soul of the Sword");
            Core.EnsureComplete(6601);
        }
        if (!Bot.Quests.IsUnlocked(6603))
        {
            Core.EnsureAccept(6602);
            Core.BuyItem("museum", 1653, "Sword's Cost");
            Core.EnsureComplete(6602);
        }
        if (!Bot.Quests.IsUnlocked(6604))
        {
            Core.EnsureAccept(6603);
            Core.KillMonster("razorclaw", "r3", "Left", "*", "Shards of the Sword", 30, false);
            Core.EnsureComplete(6603);
        }
        if (!Bot.Quests.IsUnlocked(6605))
        {
            Core.EnsureAccept(6604);
            Core.KillMonster("doomwood", "r6a", "Left", "*", "Hubris's Final Blade Shard", isTemp: false);
            Core.HuntMonster("trigoras", "Trigoras", "Hubris' Handle");
            Core.KillMonster("styx", "r4", "Left", "*", "Hubris' Magic Essence", 50, false);
            Core.EnsureComplete(6604);
        }
        if (!Bot.Quests.IsUnlocked(6606))
        {
            Core.EnsureAccept(6605);
            Core.HuntMonster("iceplane", "Enfield", "Fortitude's Handle");
            Story.UpdateQuest(4614);
            Core.KillMonster("mummies", "Enter", "Spawn", "*", "Fortitude's Blade Shards", 100, false);
            Core.KillMonster("banished", "r14", "Left", "*", "Fortitude's Magic Essence", 50, false);
            Core.EnsureComplete(6605);
        }
        Core.EnsureAccept(6606);
        Core.HuntMonster("skytower", "Aspect of Good", "Aspect of Good");
        Core.HuntMonster("skytower", "Aspect of Evil", "Aspect of Evil");
        Core.EnsureComplete(6606);
    }
}