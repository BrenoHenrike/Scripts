/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ShadowVoid.cs
using Skua.Core.Interfaces;

public class LightoviaCave
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public ShadowVoid ShadowVoid = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        LightoviaCaveQuests();

        Core.SetOptions(false);
    }

    public void LightoviaCaveQuests()
    {
        if (!Bot.Quests.IsUnlocked(7132))
        {
            if (Bot.Quests.IsUnlocked(7123))
            {
                Core.Logger(
                    "Quests are locked Shadowvoid quests should be completed first. Starting Shadowvoid script");
                ShadowVoid.ShadowVoidQuests();
            }
            else
            {
                Core.Logger(
                    "Quests are locked You Should Finish Lords Of Chaos story script first");
                return;
            }
        }

        if (Core.isCompletedBefore(7136))
            return;

        Story.PreLoad(this);

        //Defeat the Imbalanced 7132
        Story.KillQuest(7132, "LightoviaCave", "Imbalanced Knight|Imbalanced Mage");

        //Free the Lycans 7133
        Story.MapItemQuest(7133, "LightoviaCave", 6767, 6);
        Story.KillQuest(7133, "LightoviaCave", "Imbalanced Mage");

        //Cleanse the Land 7134
        Story.MapItemQuest(7134, "LightoviaCave", 6768, 6);

        //Defeat the Knights 7135
        Story.KillQuest(7135, "LightoviaCave", "Imbalanced Knight");

        //Defeat ArchMage Brentan 7136
        Story.KillQuest(7136, "LightoviaCave", "Evil ArchMage Brentan");
    }
}
