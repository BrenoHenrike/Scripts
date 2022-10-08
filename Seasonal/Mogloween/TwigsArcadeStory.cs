//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class TwigsArcadeStory
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
        if (Core.isCompletedBefore(6579))
            return;

        Story.PreLoad(this);

        //Stop the Hurting 6568
        Story.KillQuest(6568, "twigsarcade", new[] { "Scotty Sneevil", "Clucky Moo" });
		
        //Gather the Goods 6569
        if (!Story.QuestProgression(6569))
        {
            Core.EnsureAccept(6569);
            Core.Logger("Doing Quest: [6569] - \"Gather the Goods\"", "QuestProgression");
            Core.HuntMonster("twigsarcade", "Scotty Sneevil", "Wires", 5);
			Core.HuntMonster("twigsarcade", "Scotty Sneevil", "Small Screw", 16);
            Core.GetMapItem(6070, 3, "twigsarcade");
			Core.GetMapItem(6069, 3, "twigsarcade");
            Core.EnsureComplete(6569);
            Core.Logger("Completed Quest: [6569] - \"Gather the Goods\"", "TryComplete");
        }
        else Core.Logger("Already Completed: [6569] - \"Gather the Goods\"", "QuestProgression");
		
        //Spooky Casings 6570
		Story.KillQuest(6570, "twigsarcade", "Scotty Sneevil");
		
		//Do a Goo-d Job 6571
		Story.KillQuest(6571, "twigsarcade", "Ectoplasm");
		
		//Plop Plop Fizz Fizz 6572
		Story.MapItemQuest(6572, "twigsarcade", 6071, 1);
		
		//Test the Ghost box 6573
		Story.KillQuest(6573, "twigsarcade", new[] { "Scotty Sneevil", "Clucky Moo" });
		
		//Get Wid of CWEEPY TWIG 6574
		Story.KillQuest(6574, "twigsarcade", "Cweepy Twig");
		
		//Cranky Spirits 6575
		Story.KillQuest(6575, "twigsarcade", "Spirit Residue");
		
		//It's Too S-warm 6576
		Story.KillQuest(6576, "twigsarcade", "Swarmer");
		
		//Get into the Panic Room 6577
		Story.KillQuest(6577, "twigsarcade", "Cweepy Twilly");
		
		//Smash the Wall 6578
		Story.MapItemQuest(6578, "twigsarcade", 6072, 1);
		
		//Scary Baby 6579
		Story.KillQuest(6579, "twigsarcade", "Baby");
    }
}