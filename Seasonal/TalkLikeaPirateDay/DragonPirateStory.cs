//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class DragonPirateStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DragonPirate();

        Core.SetOptions(false);
    }

    public void DragonPirate()
    {
        if (Core.isCompletedBefore(8275))
            return;

        Story.PreLoad(this);

        // 8268 They Be Draconian
        Story.KillQuest(8268, "dragonpirate", "Pirate Draconian");

        // 8269 Pirate Pals
        Story.KillQuest(8269, "dragonpirate", "Pirate Wyvern");

        // 8270 Commandeer The Vessel
        Story.MapItemQuest(8270, "dragonpirate", 8930);
        Story.KillQuest(8270, "dragonpirate", "Pirate Draconian");

        // 8271 There Be Dragon
        Story.KillQuest(8271, "dragonpirate", "Dragon Pirate");

        // 8272 Man the Cannon
        Story.KillQuest(8272, "dragonpirate", "Dragon Gunner");

        // 8273 Boarding Party
        Story.MapItemQuest(8273, "dragonpirate", 8931);
        Story.KillQuest(8273, "dragonpirate", "Dragon Pirate");

        // 8274 The Mates First
        Story.KillQuest(8274, "dragonpirate", "Pirate Draconian");

        // 8275 Captain Scalebeard
        Story.KillQuest(8275, "dragonpirate", "Scalebeard");
    }
}