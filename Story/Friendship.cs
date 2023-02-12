/*
name: Friendship Story
description: This will complete the Friendship storyline.
tags: story, quest, friendship, greyguard, battleodium, NPC
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
        bool waitForPacket = false;
        if (!Story.QuestProgression(9102))
        {
            Core.EnsureAccept(9102);
            Bot.Events.ExtensionPacketReceived += friendshipPacketReader;
            Core.JumpWait();

            while (!Bot.TempInv.Contains("Friendship Formed"))
            {
                SendWaitedPacket($"%xt%zm%friendshipInfo%{Bot.Map.RoomID}%Maya%");
                SendWaitedPacket($"%xt%zm%friendshipTalk%{Bot.Map.RoomID}%");
                SendWaitedPacket($"%xt%zm%friendshipChoice%{Bot.Map.RoomID}%1%");
            }

            Bot.Events.ExtensionPacketReceived -= friendshipPacketReader;
            Core.EnsureComplete(9102);
        }

        // Darkness Distress (9103)
        Story.MapItemQuest(9103, "greyguard", 11205);
        Story.KillQuest(9103, "greyguard", new[] { "Fearweaver", "Darkbark", "Twilighteeth", "Maulignant", "Gloombloom", "Carcass Creeper" });

        // Your New FF (9104)
        if (!Story.QuestProgression(9104))
        {
            Core.EnsureAccept(9104);
            Bot.Events.ExtensionPacketReceived += friendshipPacketReader;
            Core.JumpWait();

            while (!Bot.TempInv.Contains("Greyguard Friendship Formed"))
            {
                SendWaitedPacket($"%xt%zm%friendshipInfo%{Bot.Map.RoomID}%Xing%");
                SendWaitedPacket($"%xt%zm%friendshipTalk%{Bot.Map.RoomID}%");
                SendWaitedPacket($"%xt%zm%friendshipChoice%{Bot.Map.RoomID}%1%");
            }

            Bot.Events.ExtensionPacketReceived -= friendshipPacketReader;
            Core.EnsureComplete(9104);
        }

        // Enguard Greenguard (9105)
        Story.KillQuest(9105, "greyguard", "Carcass Creeper");

        // Not all is Well (9106)
        Story.KillQuest(9106, "greyguard", "Odium");

        void friendshipPacketReader(dynamic packet)
        {
            string type = packet["params"].type;
            dynamic data = packet["params"].dataObj;
            if (type is not null and "json")
            {
                string cmd = data.cmd.ToString();
                switch (cmd)
                {
                    case "friendshipInfo":
                    case "friendshipTalk":
                    case "friendshipChoice":
                        waitForPacket = true;
                        break;
                }
            }
        }

        void SendWaitedPacket(string packet)
        {
            waitForPacket = false;
            Bot.Send.Packet(packet);
            Bot.Wait.ForTrue(() => waitForPacket, 30);
        }
    }
}
