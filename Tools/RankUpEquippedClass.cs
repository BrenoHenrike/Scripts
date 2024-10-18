/*
name: RankUpEquippedClass
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class RankUpEquippedClass
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions(disableClassSwap: true);

        DoRankUpEquippedClass();

        Core.SetOptions(false);
    }

    public void DoRankUpEquippedClass()
    {
        InventoryItem? currentClass = Bot.Player.CurrentClass;
        if (currentClass == null)
        {
            Core.Logger("No class is currently equipped.");
            return;
        }

        Adv.RankUpClass(currentClass.Name);
    }
}
