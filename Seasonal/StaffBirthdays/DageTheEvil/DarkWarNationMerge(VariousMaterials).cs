//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/StaffBirthdays/DageTheEvil/DageRecruit.cs
using Skua.Core.Interfaces;

public class DarkWarNationMerge
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
        if (!Core.isSeasonalMapActive("darkwarnation"))
            return;
        //Needed AddDrop
        Core.AddDrop("Nation Defender Medal", "Nation Trophy", "Nation War Banner", "Spoils of War");

        while (!Bot.ShouldExit && !Core.CheckInventory(new[] { "Nation Defender Medal", "Nation Trophy", "Nation War Banner", "Spoils of War" }, 300))
        {
            
            //Nation Defender Medal - Legion Badges, Mega Legion Badges
            if (!Core.CheckInventory("Nation Defender Medal", 300))
            {
                Core.EnsureAccept(8578);
                Core.EnsureAccept(8579);
                Core.HuntMonster("darkwarnation", "Legion Doomknight", "Legion Badge", 5);
                Core.HuntMonster("darkwarnation", "Legion Doomknight", "Mega Legion Badge", 3);
                while (!Bot.ShouldExit && Core.CheckInventory("Legion Badge", 5))
                    Core.EnsureComplete(8578);
                while (!Bot.ShouldExit && Core.CheckInventory("Mega Legion Badge", 3))
                    Core.EnsureComplete(8579);
            }

            //Nation Trophy - Doomed Legion Warriors
            if (!Core.CheckInventory("Nation Trophy", 300))
            {
                Core.EnsureAccept(8580);
                Core.HuntMonster("darkwarnation", "Legion Doomknight", "Legion Doomed", 5);
                Core.EnsureComplete(8580);
            }

            //Nation War Banner - Undead Legion Dread
            if (!Core.CheckInventory("Nation War Banner", 300))
            {
                Core.EnsureAccept(8581);
                Core.HuntMonster("darkwarnation", "Legion Dread Knight", "Legion's Dread", 5);
                Core.EnsureComplete(8581);
            }

            //Spoils of War - Defeat War
            if (!Core.CheckInventory("Spoils of War", 300))
            {
                Core.EnsureAccept(8582);
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("darkwarnation", "War", "War Defeated");
                Core.EnsureComplete(8582);
            }
        }
    }
}