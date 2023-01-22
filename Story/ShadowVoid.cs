/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs

using Skua.Core.Interfaces;

public class ShadowVoid
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        ShadowVoidQuests();

        Core.SetOptions(false);
    }

    public void ShadowVoidQuests()
    {
        if (!Bot.Quests.IsUnlocked(7123))
        {
            Core.Logger("Quests are locked You Should Finish Lords Of Chaos story script first");
            return;
        }
        if (Core.isCompletedBefore(7131))
        {
            Core.Logger("You have already completed Shadowvoid storyline");
            return;
        }

        Story.PreLoad(this);

        //Pieces of Light 7123
        Story.KillQuest(7123, "ShadowVoid", "Light Matter");

        //Cloak of Silk 7124
        Story.KillQuest(7124, "ShadowVoid", "Shadowsprite");

        //Bridge of Shadows 7125
        Story.KillQuest(7125, "ShadowVoid", "Light Matter");

        //Clear the Shadowfiends 7126
        Story.KillQuest(7126, "ShadowVoid", "Shadowfiend");

        //Feed the Doomblade 7127
        Story.KillQuest(7127, "ShadowVoid", "Shadowfiend");

        //Build Another Bridge 7128
        Story.KillQuest(7128, "ShadowVoid", new[] { "Shadowfiend", "Light Matter" });

        //Find Safiria 7129
        Story.MapItemQuest(7129, "ShadowVoid", 6765);

        //Dissolve the Chains 7130
        Story.MapItemQuest(7130, "ShadowVoid", 6766);
        Story.KillQuest(7130, "ShadowVoid", "Light Matter");

        //Fragment of Doom 7131
        Story.KillQuest(7131, "ShadowVoid", "Fragment of Doom");
    }
}
