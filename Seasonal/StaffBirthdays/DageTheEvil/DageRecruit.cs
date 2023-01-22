/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class DageRecruitStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CompleteDageRecruit();

        Core.SetOptions(false);
    }

    public void CompleteDageRecruit()
    {
        if (!Core.isSeasonalMapActive("dagerecruit"))
            return;
        if (Core.isCompletedBefore(8575))
            return;

        Story.PreLoad(this);

        Bot.Drops.Start();

        //Pop Goes the Makai
        Story.KillQuest(8556, "dagerecruit", "Dark Makai");

        //Dispel Spell
        Story.KillQuest(8557, "dagerecruit", "Dreadfiend");
        Story.MapItemQuest(8557, "dagerecruit", 9883, 4);

        //Dreadfiend Demolition
        Story.KillQuest(8558, "dagerecruit", "Dreadfiend");

        //Graython Located
        Story.MapItemQuest(8559, "dagerecruit", 9884);

        //Defeat Graython
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(8560, "dagerecruit", "Graython");

        //Island Sightseeing
        Story.KillQuest(8561, "dagerecruit", "Scared Wildcat");
        Story.MapItemQuest(8561, "dagerecruit", 9885);

        //Lure Crafted
        Story.KillQuest(8562, "dagerecruit", "Dreadfiend");

        //Lure Charged
        Story.KillQuest(8563, "dagerecruit", "Scared Wildcat");

        //Place the Lure
        Story.MapItemQuest(8564, "dagerecruit", 9886);

        //Defeat Nuckelavee
        Story.KillQuest(8565, "dagerecruit", "Nuckelavee");

        //Bloody the Fiends
        Story.KillQuest(8566, "dagerecruit", "Bloodfiend");

        //Unstable Energies
        Story.KillQuest(8567, "dagerecruit", "Bloodfiend");

        //Plant the Bombs
        Story.MapItemQuest(8568, "dagerecruit", 9887, 4);

        //Those Infernal Fiends
        Story.KillQuest(8569, "dagerecruit", "Infernal Fiend");

        //Defeat Smaras
        Story.KillQuest(8570, "dagerecruit", "Smaras");

        //Understanding Yokai
        Story.KillQuest(8571, "dagerecruit", "Funa-yurei");

        //Covering Our Scent
        Story.KillQuest(8572, "dagerecruit", "Funa-yurei");
        Story.MapItemQuest(8572, "dagerecruit", 9888, 4);

        //Can't Escape the Shadows
        Story.KillQuest(8573, "dagerecruit", "Shadow Clone");

        //Last of the Defenses
        Story.KillQuest(8574, "dagerecruit", "Shadow Clone");

        //Defeat Hebimaru
        Story.KillQuest(8575, "dagerecruit", "Hebimaru");
    }
}
