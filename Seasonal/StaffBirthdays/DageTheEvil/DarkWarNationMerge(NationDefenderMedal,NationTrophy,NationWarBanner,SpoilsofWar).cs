//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/StaffBirthdays/DageTheEvil/DageRecruit.cs
using RBot;

public class DarkWarNationMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public DageRecruitStory DageRecruit => new DageRecruitStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        DageRecruit.CompleteDageRecruit();

        GetMergeItems();

        Core.SetOptions(false);
    }

    public void GetMergeItems()
    {
        //Needed AddDrop
        Core.AddDrop("Nation Defender Medal", "Nation Trophy", "Nation War Banner", "Spoils of War");

        while (!Core.CheckInventory(new[] { "Nation Defender Medal", "Nation Trophy", "Nation War Banner", "Spoils of War" }, 300))
        {
            //Nation Defender Medal - Legion Badges, Mega Legion Badges
            if (!Core.CheckInventory("Nation Defender Medal", 300))
            {
                Core.EnsureAccept(8578);
                Core.EnsureAccept(8579);
                Core.HuntMonster("darkwarnation", "Legion Doomknight|Legion Dreadmarch|High Legion Inquisitor", "Legion Badge", 5);
                Core.HuntMonster("darkwarnation", "Legion Doomknight|Legion Dreadmarch|High Legion Inquisitor", "Mega Legion Badge", 3);
                while (Core.CheckInventory("Legion Badge", 5))
                    Core.EnsureComplete(8578);
                while (Core.CheckInventory("Mega Legion Badge", 3))
                    Core.EnsureComplete(8579);
            }

            //Nation Trophy - Doomed Legion Warriors
            if (!Core.CheckInventory("Nation Trophy", 300))
            {
                Core.EnsureAccept(8580);
                Core.HuntMonster("darkwarnation", "Legion Doomknight|Legion Fiend Rider", "Legion Doomed", 5);
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