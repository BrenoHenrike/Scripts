/*
name: Blood Ancient (Member) Class
description: This script farms the Blood Ancient class.
tags: ancient, class, vitae
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Safiria[Member].cs
//cs_include Scripts/Other/MergeShops/BloodAncientMerge.cs
using Skua.Core.Interfaces;

public class BloodAncient
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAdvanced Adv = new();
    private BloodAncientMerge BAM = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetBAnc();
        Core.SetOptions(false);
    }

    public void GetBAnc(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Blood Ancient") || !Core.IsMember)
        {
            Core.Logger(Core.CheckInventory("Blood Ancient") ? "You already own Blood Ancient class." : "Membership is required for this class.");
            if (rankUpClass && Core.IsMember)
                Adv.RankUpClass("Blood Ancient");
            return;
        }

        BAM.BuyAllMerge("Blood Ancient");

        if (rankUpClass)
            Adv.RankUpClass("Blood Ancient");
    }
}
