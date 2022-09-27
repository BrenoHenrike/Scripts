//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Manor.cs

using Skua.Core.Interfaces;

public class PoisonForest
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public Manor Manor = new();


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        Manor.StoryLine();

        if (Core.isCompletedBefore(1955))
            return;

        Story.PreLoad(this);

        //Poison Creation 1948
        Story.MapItemQuest(1948, "PoisonForest", 964, 3);
        Story.MapItemQuest(1948, "PoisonForest", 965, 3);
        Story.KillQuest(1948, "PoisonForest", new[] { "Traitor Knight", "Marsh Lurker" });

        //Vegetal Decay 1949
        if (!Story.QuestProgression(1949) || (!Core.CheckInventory("Combustible Krialo Moss") && !Core.isCompletedBefore(1954)))
        {
            Core.EnsureAccept(1949);
            Core.GetMapItem(963, 9, "PoisonForest");
            Core.GetMapItem(971, 1, "PoisonForest");
            Core.HuntMonster("PoisonForest", "Marsh Lurker", "Toxic Gallstones");
            Core.EnsureComplete(1949);
        }

        //Recycle, Rebuild, Rescue 1950
        if (!Story.QuestProgression(1950) || (!Core.CheckInventory("Sour Sap") && !Core.isCompletedBefore(1954)))
        {
            Core.EnsureAccept(1950);
            Core.GetMapItem(966, 1, "PoisonForest");
            Core.HuntMonster("PoisonForest", "Traitor Knight", "Unbent Nail", 6);
            Core.HuntMonster("PoisonForest", "Treeant", "Sturdy Vine", 6);
            Core.EnsureComplete(1950);
        }

        //Corrosive Philtre 1951
        if (!Story.QuestProgression(1951) || (!Core.CheckInventory("Burning Ember") && !Core.isCompletedBefore(1954)))
        {
            Core.EnsureAccept(1951);
            Core.GetMapItem(967, 5, "PoisonForest");
            Core.HuntMonster("PoisonForest", "Burning Loyalist", "Burning Loyalist Ember", 15);
            Core.EnsureComplete(1951);
        }

        //Loyalty Rewarded, Wounds Salved 1952
        Story.KillQuest(1952, "PoisonForest", "Burning Loyalist");

        //Storm the Fort 1953
        if (!Story.QuestProgression(1953) || (!Core.CheckInventory("Flakes of Rust") && !Core.isCompletedBefore(1954)))
        {
            Core.EnsureAccept(1953);
            Core.GetMapItem(968, 1, "PoisonForest");
            Core.HuntMonster("PoisonForest", "Traitor Knight", "Guard Slain", 8);
            Core.EnsureComplete(1953);
        }

        //Termination Tonic 1954
        Story.MapItemQuest(1954, "PoisonForest", 970);
        Story.KillQuest(1954, "PoisonForest", "Traitor Knight");

        //Burn ALL the Things! 1955
        Story.MapItemQuest(1955, "PoisonForest", 969, 11);
        Story.KillQuest(1955, "PoisonForest", "Xavier Lionfang");

    }
}
