/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;

public class DarkbloodStormKing
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public Core13LoC LOC => new Core13LoC();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetDSK();

        Core.SetOptions(false);
    }

    public void GetDSK(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Darkblood StormKing"))
            return;

        LOC.Lionfang();
        Farm.ThunderForgeREP();

        Core.BuyItem("stormtemple", 544, "Darkblood StormKing", 8079);

        if (rankUpClass)
            Adv.rankUpClass("Darkblood StormKing");
    }
}
