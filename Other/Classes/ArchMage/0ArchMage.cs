//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Farm/BuyScrolls.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQOM.cs
//cs_include Scripts/Other/Classes/ArchMage/CoreArchMage.cs
using Skua.Core.Interfaces;

public class ArchMage
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreArchMage AM = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        AM.GetAM();

        Core.SetOptions(false);
    }
}