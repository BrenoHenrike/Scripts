//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class ThatStory
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
        if (Core.isCompletedBefore(7179))
            return;

        Story.PreLoad(this);
		
		//Yeti Witual 7167
		if (!Story.QuestProgression(7167))
        {
            Core.EnsureAccept(7167);
            Core.Logger("Doing Quest: [7167] - \"Yeti Witual\"", "QuestProgression");
            Core.GetMapItem(6790, 8, "that");
			Core.HuntMonster("that", "Candy Goblin", "Witch Twuffles", 8);
            Core.EnsureComplete(7167);
            Core.Logger("Completed Quest: [7167] - \"Yeti Witual\"", "TryComplete");
        }
        else Core.Logger("Already Completed: [7167] - \"Yeti Witual\"", "QuestProgression");
		
		//Get the Fur 7168
		Story.KillQuest(7168, "that", "Moglinster");
		
		//Vapor Needed 7169
		Story.KillQuest(7169, "that", "Mystcroft Ghost");
		
		//Trap the Flame 7170
		Story.KillQuest(7170, "that", "Will o' The Wisp");
		
		//Down the Well 7171
		Story.MapItemQuest(7171, "that", 6791, 1);
		
		//Cweepie-cwawlie Time 7172
		Story.KillQuest(7172, "that", new[] { "Well Spider ", "Congealed Fear" });
		
		//Gwoop Destruction 7173
		Story.KillQuest(7173, "that", "Congealed Fear");
		
		//Seawch for Bubble 7174
		if (!Story.QuestProgression(7174))
        {
            Core.EnsureAccept(7174);
            Core.Logger("Doing Quest: [7174] - \"Seawch for Bubble\"", "QuestProgression");
            Core.GetMapItem(6792, 6, "that");
			Core.GetMapItem(6793, 1, "that");
            Core.EnsureComplete(7174);
            Core.Logger("Completed Quest: [7174] - \"Seawch for Bubble\"", "TryComplete");
        }
        else Core.Logger("Already Completed: [7174] - \"Seawch for Bubble\"", "QuestProgression");
		
		//Free Bubble! 7175
		if (!Story.QuestProgression(7175))
        {
            Core.EnsureAccept(7175);
            Core.Logger("Doing Quest: [7175] - \"Free Bubble!\"", "QuestProgression");
            Core.HuntMonster("that", "Congealed Fear", "Skeleton Key", 8);
			Core.GetMapItem(6794, 1, "that");
            Core.EnsureComplete(7175);
            Core.Logger("Completed Quest: [7175] - \"Free Bubble!\"", "TryComplete");
        }
        else Core.Logger("Already Completed: [7175] - \"Free Bubble!\"", "QuestProgression");
		
		//Echo... echo... echo 7176
		Story.KillQuest(7176, "that", "Echo of That");
		
		//Gather Hope 7177
		Story.KillQuest(7177, "that", "Shattered Hope");
		
		//Bweak the Blacklights 7178
		Story.KillQuest(7178, "that", "Blacklights");
		
		//Get Wid of THAT 7179
		Story.KillQuest(7179, "that", "That");
    }
}