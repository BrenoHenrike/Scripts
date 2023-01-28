/*
name:  Moglin Pets Daily
description:  Moglin Pets
tags: daily, moglin pets, pet
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class MoglinPets
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Daily.MoglinPets();

        Core.SetOptions(false);
    }
}
