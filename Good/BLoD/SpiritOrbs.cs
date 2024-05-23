/*
name: Spirit Orbs
description: This script farms the max quantity of Spirit Orbs.
tags: spirit, orbs, BLOD, blinding, light, destiny, good, soul searching
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
using Skua.Core.Interfaces;

public class SpiritOrbs
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreBLOD BLOD = new();
    public CoreStory Story = new();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BLOD.SpiritOrb(65000);

        Core.SetOptions(false);
    }
}
