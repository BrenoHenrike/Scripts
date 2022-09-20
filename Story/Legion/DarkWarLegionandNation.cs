//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;

public class DarkWarLegionandNation
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public Core13LoC LOC => new();
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DarkWarLegion();
        DarkWarNation();

        Core.SetOptions(false);
    }

    public void DarkWarLegion()
    {
        if (Core.isCompletedBefore(8588))
            return;

        Story.PreLoad(this);

        // Pop Goes the Makai
        Story.KillQuest(8556, "dagerecruit", "Dark Makai");

        // Dispel Spell
        Story.MapItemQuest(8557, "dagerecruit", 9883, 4);
        Story.KillQuest(8557, "dagerecruit", "Dreadfiend");

        // Dreadfiend Demolition
        Story.KillQuest(8558, "dagerecruit", "Dreadfiend");

        // Graython Located
        Story.MapItemQuest(8559, "dagerecruit", 9884);

        // Defeat Graython
        Story.KillQuest(8560, "dagerecruit", "Graython");

        // Island Sightseeing
        Story.MapItemQuest(8561, "dagerecruit", 9885);
        Story.KillQuest(8561, "dagerecruit", "Scared Wildcat");

        // Lure Crafted
        Story.KillQuest(8562, "dagerecruit", "Dreadfiend");

        // Lure Charged
        Story.KillQuest(8563, "dagerecruit", "Scared Wildcat");

        // Place the Lure
        Story.MapItemQuest(8564, "dagerecruit", 9886);

        // Defeat Nuckelavee
        Story.KillQuest(8565, "dagerecruit", "Nuckelavee");

        // Bloody the Fiends
        Story.KillQuest(8566, "dagerecruit", "Bloodfiend");

        // Unstable Energies
        Story.KillQuest(8567, "dagerecruit", "Bloodfiend");

        // Plant the Bombs
        Story.MapItemQuest(8568, "dagerecruit", 9887, 4);

        // Those Infernal Fiends
        Story.KillQuest(8569, "dagerecruit", "Infernal Fiend");

        // Defeat Smaras
        Story.KillQuest(8570, "dagerecruit", "Smaras");

        // Understanding Yokai
        Story.KillQuest(8571, "dagerecruit", "Funa-yurei");

        // Covering Our Scent
        Story.KillQuest(8572, "dagerecruit", "Funa-yurei");
        Story.MapItemQuest(8572, "dagerecruit", 9888, 4);

        // Can't Escape the Shadows
        Story.KillQuest(8573, "dagerecruit", "Shadow Clone");

        // Last of the Defenses
        Story.KillQuest(8574, "dagerecruit", "Shadow Clone");

        // Defeat Hebimaru
        Story.KillQuest(8575, "dagerecruit", "Hebimaru");

        // Scorched Earth
        Story.KillQuest(8576, "dagerecruit", new[] { "Dark Makai", "Dreadfiend", "Bloodfiend", "Infernal Fiend" });

        // Nation Badges / Mega Nation BadgesDreadfiend
        Story.KillQuest(8585, "darkwarlegion", "Dreadfiend");

        // A Nation Defeated
        Story.KillQuest(8586, "darkwarlegion", "Dreadfiend");

        // ManSlayer? More Like ManSLAIN
        Story.KillQuest(8587, "darkwarlegion", "Manslayer Fiend");

        // Defeat Dirtlicker            
        Story.KillQuest(8588, "darkwarlegion", "Dirtlicker");
    }

    public void DarkWarNation()
    {
        if (Core.isCompletedBefore(8583))
            return;

        LOC.Vath();

        // Legion Badges
        Story.KillQuest(8578, "darkwarnation", "High Legion Inquisitor");

        // Doomed Legion Warriors
        Story.KillQuest(8580, "darkwarnation", "Legion Doomknight");

        // Undead Legion Dread
        Story.KillQuest(8581, "darkwarnation", "Legion Dread Knight");

        // Defeat War
        Story.KillQuest(8582, "darkwarnation", "War");

        // The Traitor           
        Story.KillQuest(8583, "darkwarnation", "Dage the Evil");
    }

}
