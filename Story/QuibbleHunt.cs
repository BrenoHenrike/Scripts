/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class QuibbleHunt
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
        if (Core.isCompletedBefore(5891))
            return;

        Story.PreLoad(this);

        //Follow Those Fairies! 5878
        Story.KillQuest(5878, "quibblehunt", "Void Larva");

        //Aerodu Assault 5879
        Story.KillQuest(5879, "quibblehunt", new[] { "Aerodu Defense Machine", "Nimbo" });

        //Void Travel Battle 5880
        Story.KillQuest(5880, "quibblehunt", "Void Elemental");

        //Lost to Entropy? 5881
        Story.KillQuest(5881, "quibblehunt", "Entropy Dragon");

        //Void Travel Battle 5882
        Story.KillQuest(5882, "quibblehunt", "Void Elemental");

        //The Master Quest 5883
        Story.KillQuest(5883, "quibblehunt", new[] { "Jimmy the Eye-Pod", "Master Twang" });

        //Void Travel Battle 5884
        Story.KillQuest(5884, "quibblehunt", "Void Elemental");

        //Crystallize This 5885
        Story.KillQuest(5885, "quibblehunt", "RogueFiend");

        //Void Travel Battle 5886
        Story.KillQuest(5886, "quibblehunt", "Void Elemental");

        //Nauts and Double-crosses 5887
        Story.KillQuest(5887, "quibblehunt", new[] { "Braken Tentacle", "Jellyfish", "Braken Tentacle" });

        //Void Travel Battle 5888
        Story.KillQuest(5888, "quibblehunt", "Void Elemental");

        //Key to the Mystery 5889
        Story.MapItemQuest(5889, "quibblehunt", 5311);

        //Get the Key 5890
        Story.KillQuest(5890, "quibblehunt", "Clawg");

        //A Void the Dricken 5891
        Story.KillQuest(5891, "quibblehunt", "Dricken");

    }
}
