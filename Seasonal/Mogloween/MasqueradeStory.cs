//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class MasqueradeStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(7154))
            return;

        Story.PreLoad(this);

        //Mmm... Cake 7139
		Story.KillQuest(7139, "masquerade", "Harried Waiter");
		
		//Catch the Sprites 7140
		Story.KillQuest(7140, "masquerade", "Garden Sprite");
		
		//Spicy 7141
		Story.KillQuest(7141, "masquerade", "Kitchen Brownie");

        //Things are Getting Hairy 7142
        if (!Story.QuestProgression(7142))
        {
            Core.EnsureAccept(7142);
            Core.Logger("Doing Quest: [7142] - \"Things are Getting Hairy\"", "QuestProgression");
            Core.GetMapItem(6779, 1, "masquerade");
			Core.GetMapItem(6780, 1, "masquerade");
			Core.GetMapItem(6781, 1, "masquerade");
			Core.GetMapItem(6782, 1, "masquerade");
			Core.GetMapItem(6783, 1, "masquerade");
            Core.EnsureComplete(7142);
            Core.Logger("Completed Quest: [7142] - \"Things are Getting Hairy\"", "TryComplete");
        }
        else Core.Logger("Already Completed: [7142] - \"Things are Getting Hairy\"", "QuestProgression");
		
        //Let 'em Loose 7143
		Story.MapItemQuest(7143, "masquerade", 6784, 1);
		
		//Gather Spores 7144
		Story.KillQuest(7144, "masquerade", "Glowshroom");
		
		//Getting Dusty 7145
		Story.KillQuest(7145, "masquerade", "Garden Sprite");
		
		//Gather Moss 7146
		Story.KillQuest(7146, "masquerade", "Fae Treeant");
		
		//Feed Bramblebite 7147
		Story.MapItemQuest(7147, "masquerade", 6785, 1);
		
		//Feed Bramblebite 7148
		if (!Story.QuestProgression(7148))
        {
            Core.EnsureAccept(7148);
            Core.Logger("Doing Quest: [7148] - \"Feed Bramblebite\"", "QuestProgression");
            Core.GetMapItem(6786, 6, "masquerade");
			Core.HuntMonster("masquerade", "Glass Spitter", "Gemseeds", 10);
            Core.EnsureComplete(7148);
            Core.Logger("Completed Quest: [7148] - \"Feed Bramblebite\"", "TryComplete");
        }
        else Core.Logger("Already Completed: [7148] - \"Feed Bramblebite\"", "QuestProgression");
		
		//So Glamourous 7149
		Story.KillQuest(7149, "masquerade", "Garden Sprite");
		
		//Likin' the Lichen 7150
		Story.KillQuest(7150, "masquerade", "Fae Treeant");
		
		//It's Wine Time 7151
		Story.KillQuest(7151, "masquerade", "Wandering Guest");
		
		//Nap Time for Barb 7152
		if (!Story.QuestProgression(7152))
        {
            Core.EnsureAccept(7152);
            Core.Logger("Doing Quest: [7152] - \"Nap Time for Barb\"", "QuestProgression");
            Core.GetMapItem(6789, 1, "masquerade");
			Core.HuntMonster("masquerade", "Scriptkeeper", "Put Barb To Sleep", 1);
            Core.EnsureComplete(7152);
            Core.Logger("Completed Quest: [7152] - \"Nap Time for Barb\"", "TryComplete");
        }
        else Core.Logger("Already Completed: [7152] - \"Nap Time for Barb\"", "QuestProgression");
		
		//Sneak Out 7153
		Story.MapItemQuest(7153, "masquerade", 6787, 1);
		
		//Break Out 7154
		Story.KillQuest(7154, "masquerade", "Aermhar");
    }
}