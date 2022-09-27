//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class SuperDeath
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(8032))
            return;

        Story.PreLoad(this);

        //SMASH a Path 8015
        Story.KillQuest(8015, "SuperDeath", "Shadow Cave Yeti|Shadow Collector");

        //Cold Open the Portal 8016
        Story.MapItemQuest(8016, "SuperDeath", 8330);
        Story.KillQuest(8016, "SuperDeath", new[] { "Shadow Cave Yeti|Shadow Collector", "Shadow Lava Crab", "Shadow Collector", "Shadow Cave Yeti" });

        //VolcaNO Thank You 8017
        Story.KillQuest(8017, "SuperDeath", "Igneous Lava Crab");

        //Return to Cinder 8018
        Story.KillQuest(8018, "SuperDeath", "Cinder Bender");

        //Hottica 8019
        Story.KillQuest(8019, "SuperDeath", "Hottica");

        //Liquidate Liberate 8020
        Story.KillQuest(8020, "SuperDeath", "Slime Collector");

        //Lord of the Mall 8021
        Story.KillQuest(8021, "SuperDeath", "Slime Lord");

        //Electina 8022
        Story.KillQuest(8022, "SuperDeath", "Electina");

        //Carpool Lane 8023
        Story.KillQuest(8023, "SuperDeath", "Rider");

        //The BlasterMaster 8024
        Story.KillQuest(8024, "SuperDeath", "Blaster Master");

        //General Smash 8025
        Story.KillQuest(8025, "SuperDeath", "General Smash");

        //Left In-giRuins 8026
        Story.KillQuest(8026, "SuperDeath", "Cave Yeti");

        //Rock Me Khali May-eus 8027
        Story.KillQuest(8027, "SuperDeath", "Khali May");

        //Charries 8028
        Story.KillQuest(8028, "SuperDeath", "Charries");

        //Shadow Realm Cleansing 8029
        Story.KillQuest(8029, "SuperDeath", "Shadow Cave Bandit|Shadow Goo Pup");

        //Forging the Shadow Key 8030
        Story.KillQuest(8030, "SuperDeath", new[] { "Shadow Mutant", "Shadow Scorpion", "Shadow Cave Bandit", "Shadow Goo Pup" });

        //Charge the Key 8031
        Story.KillQuest(8031, "SuperDeath", new[] { "Hottica", "Electina", "General Smash", "Charries" });

        //Super Death 8032
        Story.KillQuest(8032, "SuperDeath", "Super Death");

    }
}
