//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class CoreMogloween
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Core.RunCore();

        Core.SetOptions(false);
    }

    public void SagaName()
    {
        if (Core.isCompletedBefore(1363))
            return;

        Story.PreLoad(this);
        //Candy Craze 95
        Story.KillQuest(95, "mogloween", "Pumpkinhead Fred");

        //Chocolate Goodness 96
        Story.KillQuest(96, "mogloween", "Pumpkinhead Fred");

        //Chilling Costume 93
        Story.KillQuest(93, "mogloween", "Ghostly Sheet");

        //Candy Basket 94
        Story.KillQuest(94, "mogloween", "Pumpkinhead Fred");

        //Lollipop Potion 97
        Story.KillQuest(97, "mogloween", "Jack-O-Doom");

        //Mystery Candy 98
        if (!Story.QuestProgression(98))
        {
            Core.EnsureAccept(98);
            Core.Join("mogloween", "House2", "Center", ignoreCheck: true);
            Bot.Send.Packet($"%xt%zm%ia%1%flourish%FloorTrap%Open%");
            Core.KillMonster("mogloween", "Pit4", "Center", 107, "Mystery Candy", 5);
            Core.EnsureComplete(98);
        }

        //Can't have enough 99
        if (!Story.QuestProgression(99))
        {
            Core.EnsureAccept(99);
            Core.Join("mogloween", "House2", "Center");
            Bot.Send.Packet($"%xt%zm%ia%1%flourish%FloorTrap%Open%");
            Core.KillMonster("mogloween", "Pit4", "Center", 107, "Mystery Candy", 5);
            Core.EnsureComplete(99);
        }

        //Squash the King 391
        Story.KillQuest(391, "mogloween", "Great Pumpkin King");

        //A Bone To Pick 392
        Story.KillQuest(392, "mogloweengrave", "Graveyard Soldier");

        //Were are the Wolves 393
        Story.KillQuest(393, "mogloweengrave", "Graveyard Werewolf");

        //Killer, Chiller, Thriller Here Tonight 394
        Story.KillQuest(394, "mogloweengrave", "Thriller");

        //Candy Shop Cutscene 395
        Story.MapItemQuest(395, "candycorn", 69);

        //Clear A Path 396
        Story.KillQuest(396, "candyshop", "Dark Moglinster");

        //Kanthalite-D 397
        Story.KillQuest(397, "candyshop", "Sugarrush Ghoul");

        //Myx It Up 398
        Story.KillQuest(398, "candyshop", "Super Moglinster");

        //Candy Conundrum 399
        Story.KillQuest(399, "candyshop", "Dark Moglinster");

        //Boos Clues 400
        if (!Story.QuestProgression(400))
        {
            Core.EnsureAccept(400);
            Core.HuntMonster("voltabolt", "Moglinsterbot", "Fluoride Element");
            Core.HuntMonster("voltabolt", "Moglinsterbot", "Stainless Steel Veneers");
            Core.HuntMonster("voltabolt", "Nightmare Dentist Chair", "Robotic Drill Bit");
            Core.HuntMonster("voltabolt", "Nightmare Dentist Chair", "Fill-up's Screwdriver");
            Core.EnsureComplete(400);
        }

        //What’s Up Doc? 401
        Story.KillQuest(401, "voltabolt", "Dental Driller");

        //Investigate the Farmhouse 871
        Story.MapItemQuest(871, "candycorn", 193);

        //Children of Chaos 872
        Story.KillQuest(872, "candycorn", "Candy Kid");

        //Where is that Barn Key? 873
        Story.KillQuest(873, "candycorn", "Candy Kid");

        //Investigate the Barn 874
        Story.MapItemQuest(874, "candycorn", 194);

        //Malik-EYE is so Grounded 875
        Story.KillQuest(875, "candycorn", "Malik-EYE");

        //Clearing the Candy Corn Field  876
        Story.KillQuest(876, "candycorn", "Field Guardian");

        //So Sick of EYE-Sac 877
        Story.KillQuest(877, "candycorn", "EYE-Sac");

        //She Who Walks Behind The Stalks 878
        Story.KillQuest(878, "candycorn", "Stalkwalker");

        //Take a Bite Out of Crime 1355
        Story.KillQuest(1355, "pie", "Gourdo");

        //Skullberry Picking 1356
        Story.MapItemQuest(1356, "pie", 654, 10);

        //Can I Axe You a Question 1357
        Story.KillQuest(1357, "pie", "Gourdo");

        //Don't Forge-t 1358
        Story.MapItemQuest(1358, "pie", 655);

        //A Monstrous Appetite 1359
        if (!Story.QuestProgression(1359))
        {
            Core.EnsureAccept(1359);
            Core.HuntMonster("pie", "Gourdo", "Pie Defended", 13);
            Core.HuntMonster("pie", "Gourdo", "Friend Avenged", 2);
            Core.EnsureComplete(1359);
        }

        //Undead End in Sight 1360
        Story.MapItemQuest(1360, "pie", 656);

        //Missing Servant 1361
        Story.MapItemQuest(1361, "pie", 658);

        //Remember Your Quest 1362
        Story.KillQuest(1362, "pie", "Gourdo");

        //Head to WillowCreek! 1363
        Story.MapItemQuest(1363, "willowcreek", 657);

    }
}
