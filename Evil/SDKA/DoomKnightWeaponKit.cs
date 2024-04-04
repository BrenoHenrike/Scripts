/*
name: DoomKnight Weapon Kit
description: This script will farm DoomKnight Weapon Kit.
tags: doomknight, doomknightwk, weapon kit, sdka, evil, corrupt spirit orb, ominous aura, grumpy warhammer
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
public class DoomKnightWeaponKit
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreSDKA SDKA = new CoreSDKA();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        SDKA.DoomKnightWK("Ominous Aura", 10000);

        Core.SetOptions(false);
    }
}
