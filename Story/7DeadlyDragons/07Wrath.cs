//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;
public class Wrath
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        WrathSaga();

        Core.SetOptions(false);
    }

    public void WrathSaga()
    {
        if (Core.isCompletedBefore(6120))         
            return;

        // Decimate the Horder
        Story.KillQuest(6110, "wrath", new[] {"Bone Terror", "Fishbones"});
        // Douse the Flames
        Story.KillQuest(6111, "wrath", "Dark Fire");
        // Grenades of AWE
        Story.MapItemQuest(6112, "wrath", 5541, 6);
        Story.KillQuest(6112, "wrath", "Bone Terror|Fishbones|Undead Pirate");
        // The Final Ingredient
        if (!Story.QuestProgression(6113))
        {
            Core.EnsureAccept(1075);
            Core.HuntMonster("doomwood", "Doomwood Ectomancer", "Dried Wasabi Powder", 4, true);
            Core.GetMapItem(428, 1, "lightguard");
            Core.EnsureComplete(1075);
            Core.AddDrop("Holy Wasabi");
            Core.EnsureComplete(6113);
        }
        // Count Not to Four
        Story.MapItemQuest(6114, "wrath", 5542, 6);
        // Talk to Captain Rhubarb
         Story.MapItemQuest(6115, "wrath", 5540, 1);
        // Find the Manifest
        Story.MapItemQuest(6116, "wrath", 5544, 1);
        Story.KillQuest(6116, "wrath", "Undead Pirate");
        // Get the Cargo Hold Key
        Story.KillQuest(6117, "wrath", new[] {"Mutineer", "Mutineer"});
        // Find the Jewel
        Story.MapItemQuest(6118, "wrath", 5545, 1);
        // Defeat Droghor the Screecher!
        Story.KillQuest(6119, "wrath", "Droghor");
        // Defeat Gorhorath!
        Story.KillQuest(6120, "wrath", "Gorgorath");
    }
}
