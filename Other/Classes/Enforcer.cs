/*
name: Enforcer Class
description: This script farms the Enforcer class.
tags: dwakel, mithril man, crashsite, class
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Enforcer
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAdvanced Adv = new();
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetClass();
        Core.SetOptions(false);
    }

    public void GetClass(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Enforcer"))
        {
            Core.Logger("You already own Enforcer class.");
            return;
        }

        Core.AddDrop("Enforcer");

        // Quadrolithium (122)
        Story.KillQuest(122, "crashsite", "Dwakel Blaster");

        // Dam Balloons (123)
        Story.KillQuest(123, "crashsite", "Dwakel Blaster");

        // Bumper Bolts (124)
        Story.KillQuest(124, "crashsite", "Dwakel Blaster");

        // Mithril Man Batteries (125)
        Story.KillQuest(125, "crashsite", "Mithril Man");

        // ProtoSartorium Parts (126)
        Story.KillQuest(126, "crashsite", "ProtoSartorium");

        if (rankUpClass)
            Adv.RankUpClass("Enforcer");
    }
}
