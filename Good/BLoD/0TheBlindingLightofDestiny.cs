/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;

public class TheBlindingLightofDestiny
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreBLOD BLOD = new CoreBLOD();
    public CoreStory Story = new CoreStory();
    public CoreAdvanced Adv = new();
    public Core13LoC LOC => new Core13LoC();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BLOD.DoAll();

        Core.SetOptions(false);
    }
}
