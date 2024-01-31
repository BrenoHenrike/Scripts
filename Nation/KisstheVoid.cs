/*
name: KisstheVoid
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
public class KisstheVoid
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();

    /// <summary>
    /// List of Betrayal Blades
    /// </summary>
    public string[] betrayalBlades =
    {
        "1st Betrayal Blade of Nulgath",
        "2nd Betrayal Blade of Nulgath",
        "3rd Betrayal Blade of Nulgath",
        "4th Betrayal Blade of Nulgath",
        "5th Betrayal Blade of Nulgath",
        "6th Betrayal Blade of Nulgath",
        "7th Betrayal Blade of Nulgath",
        "8th Betrayal Blade of Nulgath"
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        //blades:
        foreach (string item in new[] {
        "1st Betrayal Blade of Nulgath",
        "2nd Betrayal Blade of Nulgath",
        "3rd Betrayal Blade of Nulgath",
        "4th Betrayal Blade of Nulgath",
        "5th Betrayal Blade of Nulgath",
        "6th Betrayal Blade of Nulgath",
        "7th Betrayal Blade of Nulgath",
        "8th Betrayal Blade of Nulgath"
    })
        {
            Core.AddDrop(betrayalBlades);
            Nation.KisstheVoid(1, item);
        }

        //Blood Gems:
        Nation.KisstheVoid();

        Core.SetOptions(false);
    }
}
