/*
name: Acolyte[Mem]
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class Acolyte
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreStory Story = new();
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAcolyte();

        Core.SetOptions(false);
    }

    public void GetAcolyte(bool rankUpClass = true)
    {
        if (!Core.IsMember)
            return;

        if (Core.CheckInventory("Acolyte"))
            return;

        if (Core.CheckInventory("Healer"))
        {
            Core.BuyItem("trainers", 176, "Healer");
            Adv.RankUpClass("Healer");
            Core.BuyItem("trainers", 177, "Acolyte");
            if (rankUpClass)
                Adv.RankUpClass("Acolyte");
        }
    }
}
