//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/StaffBirthdays/DageTheEvil/DageRecruit.cs
using Skua.Core.Interfaces;

public class DarkWarLegionMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public DageRecruitStory DageRecruit => new DageRecruitStory();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DageRecruit.CompleteDageRecruit();

        GetMergeItems();

        Core.SetOptions(false);
    }

    public void GetMergeItems()
    {
        if (!Core.isSeasonalMapActive("darkwarlegion"))
            return;
        //Needed AddDrop
        Core.AddDrop("Legion Defender Medal", "Legion Trophy", "Legion War Banner", "Soiled Fiend Crystal");

        while (!Bot.ShouldExit && !Core.CheckInventory(new[] { "Legion Defender Medal", "Legion Trophy", "Legion War Banner", "Soiled Fiend Crystal" }, 300))
        {
            //Legion Defender Medal - Nation Badges, Mega Nation Badges
            if (!Core.CheckInventory("Legion Defender Medal", 300))
            {
                Core.EnsureAccept(8584);
                Core.EnsureAccept(8585);
                Core.HuntMonster("darkwarlegion", "Infernal Fiend", "Nation Badge", 5);
                Core.HuntMonster("darkwarlegion", "Infernal Fiend", "Mega Nation Badge", 3);
                while (!Bot.ShouldExit && Core.CheckInventory("Nation Badge", 5))
                    Core.EnsureComplete(8584);
                while (!Bot.ShouldExit && Core.CheckInventory("Mega Nation Badge", 3))
                    Core.EnsureComplete(8585);
            }

            //Legion Trophy - A Nation Defeated
            if (!Core.CheckInventory("Legion Trophy", 300))
            {
                Core.EnsureAccept(8586);
                Core.HuntMonster("darkwarlegion", "Infernal Fiend", "Nation's Dread", 5);
                Core.EnsureComplete(8586);
            }

            //Legion War Banner - ManSlayer? More Like ManSLAIN
            if (!Core.CheckInventory("Legion War Banner", 300))
            {
                Core.EnsureAccept(8587);
                Core.HuntMonster("darkwarlegion", "Manslayer Fiend", "ManSlayer Slain", 5);
                Core.EnsureComplete(8587);
            }

            //Soiled Fiend Crystal - Defeat Dirtlicker
            if (!Core.CheckInventory("Soiled Fiend Crystal", 300))
            {
                Core.EnsureAccept(8588);
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("darkwarlegion", "Dirtlicker", "Dirtlicker Defeated");
                Core.EnsureComplete(8588);
            }
        }
    }
}