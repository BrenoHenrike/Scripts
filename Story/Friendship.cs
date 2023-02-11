/*
name: Friendship
description: This will complete the Friendship story quest.
tags: friendship, story, greyguard, battleodium
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Friendship
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        CompleteStory();
        Core.SetOptions(false);
    }

    public void CompleteStory()
    {
        if (Core.isCompletedBefore(9106))
            return;

        Story.PreLoad(this);

        // Bad-tleon (9099)
        Story.MapItemQuest(9099, "battleodium", 11202);

        // Hello Darkness My Old Fiend (9100)
        Story.KillQuest(9100, "battleodium", "Diemond");

        // Knock Knock, You’s There (9101)
        Story.MapItemQuest(9101, "battleodium", 11203, 3);

        // Befriend a Friend (9102)
        if (!Story.QuestProgression(9102))
        {
            Core.EnsureAccept(9102);
            while(!Bot.TempInv.Contains("Friendship Formed"))
            {
                Bot.Send.Packet("%xt%zm%friendshipInfo%12681%Maya%");
                Bot.Sleep(1000);
                Bot.Send.Packet("%xt%zm%friendshipTalk%12681%");
                Bot.Sleep(1000);
                Bot.Send.Packet("%xt%zm%friendshipChoice%12681%1%");
                Bot.Sleep(1000);
            }
            Core.EnsureComplete(9102);
        }

        // Darkness Distress (9103)
        Story.MapItemQuest(9103, "greyguard", 11205);
        Story.KillQuest(9103, "greyguard", new[] {"Fearweaver", "Darkbark", "Twilighteeth", "Maulignant", "Gloombloom", "Carcass Creeper"});

        // Your New FF (9104)
        if (!Story.QuestProgression(9104))
        {
            Core.EnsureAccept(9104);
            while(!Bot.TempInv.Contains("Greyguard Friendship Formed"))
            {
                Bot.Send.Packet("%xt%zm%friendshipInfo%13359%Xing%");
                Bot.Sleep(1000);
                Bot.Send.Packet("%xt%zm%friendshipTalk%13359%");
                Bot.Sleep(1000);
                Bot.Send.Packet("%xt%zm%friendshipChoice%13359%1%");
                Bot.Sleep(1000);
            }
            Core.EnsureComplete(9104);
        }

        // Enguard Greenguard (9105)
        Story.KillQuest(9105, "greyguard", "Carcass Creeper");

        // Not all is Well (9106)
        Story.KillQuest(9106, "greyguard", "Odium");
    }
}
