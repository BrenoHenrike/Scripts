/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
using System.Collections.Generic;
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class PinpointthePieces_Any
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreSDKA SDKA = new CoreSDKA();

    public string OptionStorage = "Pinpoint_the_Pieces";

    public List<IOption> Options = new List<IOption>()
    {
        new Option<PinpointIDs>("questID", "Quest ID", "ID of the desired Pinpoint quest to do.", PinpointIDs.Dagger)
    };
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Pinpoint();

        Core.SetOptions(false);
    }

    public void Pinpoint()
    {
        Core.EquipClass(ClassType.Farm);
        int i = 1;
        int questID = (int)Bot.Config.Get<PinpointIDs>("questID");
        while (!Bot.ShouldExit)
        {
            SDKA.PinpointthePieces(questID);
            Core.Logger($"Completed x{i++}");
        }
    }
}

public enum PinpointIDs
{
    Dagger = 2181,
    Blade = 2182,
    Broadsword = 2183,
    Scythe = 2184,
    Mace = 2185,
    Bow = 2186
}
