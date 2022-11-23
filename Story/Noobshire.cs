//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Noobshire
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        doAll();
        Core.SetOptions(false);
    }

    public void doAll()
    {
        Story.PreLoad(this);
        RolithQuests();
        LowellCatQuests();
    }

    public void RolithQuests()
    {
        if (Core.isCompletedBefore(118))
            return;

        //Defend the Rats 32
        Story.KillQuest(32, "noobshire", "Kittarian Mouse Eater");

        //Rats in the Cradle 33
        Story.MapItemQuest(33, "noobshire", 11, 6);
        Story.KillQuest(33, "noobshire", "Horc Noob");

        //Short in Spoons 35
        Story.KillQuest(35, "noobshire", "Kittarian Mouse Eater");

        //Short in Forks 36
        Story.KillQuest(36, "noobshire", "Kittarian Mouse Flayer");

        //Letter Interception 37
        Story.KillQuest(37, "noobshire", "Horc Noob");

        //Not A Noob 38
        Story.KillQuest(38, "noobshire", "Horc Trainer");

        //Missing King 39
        Story.MapItemQuest(39, "noobshire", 12);
        Story.KillQuest(39, "noobshire", "Horc Noob");

        // [[[Tutor]]]

        // Fighting Exercise 117 
        Story.KillQuest(117, "Tutor", "Horc Noob");

        // Tougher Monsters 118
        Story.KillQuest(118, "Tutor", "Horc Tutor Trainer");
    }

    public void LowellCatQuests()
    {
        if (!Core.IsMember)
            return;

        if (Core.isCompletedBefore(2196))
            return;

        //PORKON RIPOFF 2188
        Story.KillQuest(2188, "orctown", "General Porkon");

        //GRILL THE LOCALS 2189
        Story.KillQuest(2189, "noobshire", "Horc Noob");

        //DOGEAR THE CAT BURGLAR 2190
        if (!Story.QuestProgression(2190))
        {
            Core.EnsureAccept(2190);
            Core.HuntMonster("newbie", "Dogear", "Front Wheel Location");
            Core.HuntMonster("newbie", "Dogear", "Frame Location");
            Core.HuntMonster("newbie", "Dogear", "Back Wheel Location");
            Core.EnsureComplete(2190);
        }

        //BLOODTUSK CATWALK 2191
        Story.MapItemQuest(2191, "bloodtusk", 1270);

        //You scratch me back... 2192
        Story.KillQuest(2192, "bloodtusk", "Horc Boar Scout");

        //...And me scratch yours 2193
        Story.KillQuest(2193, "bloodtusk", "Rhison");

        //Sign here 2194
        Story.MapItemQuest(2194, "noobshire", 1271);

        //THE FRAME UP 2195
        Story.KillQuest(2195, "sandport", "Horc Sell-Sword");

        //THAT DARN CAT 2196
        Story.KillQuest(2196, "giant", "Giant Cat");
    }
}
