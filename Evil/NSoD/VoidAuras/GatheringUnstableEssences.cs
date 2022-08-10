//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Good/BLOD/CoreBLOD.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Evil/NSoD/CoreNSOD.cs
using RBot;

public class GatheringUnstableEssences
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreNSOD NSoD = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        NSoD.GatheringUnstableEssences();

        Core.SetOptions(false);
    }
}
