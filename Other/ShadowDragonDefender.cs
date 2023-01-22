/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Story/7DeadlyDragons/Extra/HatchTheEgg.cs
//cs_include Scripts/Other/MysteriousEgg.cs
using Skua.Core.Interfaces;

public class GetSDD
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public HatchTheEgg Egg = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        ShadowDragonDefender();

        Core.SetOptions(false);
    }

    public void ShadowDragonDefender()
    {
        if (Core.CheckInventory("Shadow Dragon Defender"))
            return;

        Egg.Hatch();

        Core.BuyItem("mysteriousegg", 1728, "Shadow Dragon Defender");
        Core.ToBank("Manticore Cub Pet");
    }
}
