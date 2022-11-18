//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class FandH
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        FortitudeAndHubris();

        Core.SetOptions(false);
    }

    public void FortitudeAndHubris()
    {
        if (Core.CheckInventory("Fortitude + Hubris"))
            return;

        Story.PreLoad(this);

        Core.AddDrop(
            "Zorbak's Secret G-Rave Key", 
            "Sword's Cost", 
            "Shards of the Sword", 
            "Hubris's Final Blade Shard",
            "Hubris' Magic Essence", 
            "Hubris", 
            "Fortitude's Blade Shards", 
            "Fortitude's Magic Essence", 
            "Fortitude", 
            "Fortitude + Hubris"
        );

        // Qualifying Quest
        Story.KillQuest(6593, "stalagbite", "Balboa", GetReward: false);

        // Rest for the Not Very Wicked
        Story.MapItemQuest(6594, "tavern", 6114, GetReward: false);
        Story.KillQuest(6594, "pines", new[] { "Red Shell Turtle", "Pine Grizzly", "Pine Troll" });

        // Pisces Pieces
        Story.KillQuest(6595, "river", "Kuro", GetReward: false);

        // Be Ebil
        Story.MapItemQuest(6596, "maul", 6116);
        Story.KillQuest(6596, "maul", "Creature Creation", GetReward: false);
        Core.SellItem("Zorbak's Secret G-Rave Key");

        // Eternal, Never-Ending Darkness and Death Lance
        Farm.Experience(45);
        Story.KillQuest(6598, "shadowrealm", "Shadow Lord", GetReward: false);

        // It Takes a Special Brand of Glory
        Story.KillQuest(6599, "dragontown", "Chaos Fluffy", GetReward: false);

        // 1st Trial
        Story.MapItemQuest(6600, "david", 6115);
        Story.KillQuest(6600, "mountfrost", "Snow Golem", GetReward: false);

        // 2nd Trial
        Story.KillQuest(6601, "northpointe", "Wyvern", GetReward: false);

        // 3rd Trial
        Story.BuyQuest(6602, "museum", 1653, "Sword's Cost", GetReward: false);

        // 4th Trial
        Story.KillQuest(6603, "razorclaw", "Enraged Razorclaw", GetReward: false);

        // Hubris
        if (!Core.CheckInventory("Hubris"))
        {
            Core.EnsureAccept(6604);
            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("doomwood", "r6a", "Right", "Doomwood Ectomancer", "Hubris's Final Blade Shard", isTemp: false);
            Bot.Sleep(2500);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("trigoras", "Trigoras", "Hubris' Handle");
            Core.HuntMonster("styx", "Styx Hydra", "Hubris' Magic Essence", 50, isTemp: false);
            Core.EnsureComplete(6604);
            Bot.Wait.ForPickup("Hubris");
        }

        // Fortitude
        if (!Core.CheckInventory("Fortitude"))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(6605);
            Core.HuntMonster("iceplane", "Enfield", "Fortitude's Handle");
            Bot.Quests.UpdateQuest(4616);
            Core.HuntMonster("mummies", "Mummy", "Fortitude's Blade Shards", 100, isTemp: false);
            Core.HuntMonster("banished", "Desterrat Moya", "Fortitude's Magic Essence", 50, isTemp: false);
            Core.EnsureComplete(6605);
            Bot.Wait.ForPickup("Fortitude");
        }

        // Dual Wielding
        if (!Core.CheckInventory("Fortitude + Hubris"))
        {
            Story.KillQuest(6606, "skytower", new[] { "Aspect of Good", "Aspect of Evil" });
            Bot.Wait.ForPickup("Fortitude + Hubris");
        }
    }
}