//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class ArtixWedding
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        ArtixWeddingComplete();

        Core.SetOptions(false);
    }
    public void ArtixWeddingComplete()
    {
        GrimskullAnnex();
        ArtixWeddingQuests();
    }

    public void GrimskullAnnex()
    {
        if (Core.isCompletedBefore(3234))
            return;

        Story.PreLoad(this);

        //Artix Needs a Girlfriend
        Story.ChainQuest(3231);

        //Battle Through the Dungeon 3232
        Core.Join("ArtixWedding");
        Story.KillQuest(3232, "GrimskullAnnex", "Grim Fighter");

        //The Way is Blocked 3233
        Story.KillQuest(3233, "GrimskullAnnex", "Grim Mage");

        //Just a FEW More Monsters... 3234
        Story.KillQuest(3234, "GrimskullAnnex", "Grim Soldier");

    }

    public void ArtixWeddingQuests()
    {
        if (Core.isCompletedBefore(3253))
            return;

        Story.PreLoad(this);

        //Prove Your Worth 3235
        Story.KillQuest(3235, "BattleWedding", "Silver Knight");

        //The Knight is Dark 3236
        Story.KillQuest(3236, "BattleWedding", "Dark Knight");

        //The Most Brutal Battle 3237
        Story.KillQuest(3237, "BattleWedding", "BrutalCorn");

        //Ebilcorp Ninja Nightmare 3238
        Story.KillQuest(3238, "BattleWedding", "EbilCorp Ninja");

        //EbilCorp Drones: On! 3239
        Story.KillQuest(3239, "BattleWedding", "EbilCorp Ninja");

        //EbilCorp Overshadow(scythe)d 3240
        Story.KillQuest(3240, "BattleWedding", "EbilCorp Shadowscythe");

        //Ebilcorp CEOverkill 3241
        Story.KillQuest(3241, "BattleWedding", "Platinum Mech Dragon");

        //Protect the Cake! 3242
        Story.KillQuest(3242, "BattleWedding", "EbilCorp Ninja");

        //Bellhop and They Don't Stop 3243
        Story.KillQuest(3243, "BattleWedding", "Bellhop Human");

        //Dork Knight Rising 3244
        Story.KillQuest(3244, "BattleWedding", "Nightwyvern");

        //Iron Hero... RUSTED! 3245
        Story.KillQuest(3245, "BattleWedding", "Nightwyvern");

        //Crush the Wedding Crasher 3246
        Story.KillQuest(3246, "BattleWedding", "Iron Hero");

        //Hats Off to You 3247
        Story.KillQuest(3247, "BattleWedding", "Bellhop Human");

        //This Boss is a Real Drag(on)! 3248
        Story.KillQuest(3248, "BattleWedding", "Evil Hotel Manager");

        //Eye See Victory! 3249
        Story.KillQuest(3249, "BattleWedding", "Flying Eye");

        //Eye Love Battle 3250
        Story.KillQuest(3250, "BattleWedding", "Jimmy the Eye Heart");

        //Bonus Ending: Brutal Battles 3251
        Story.KillQuest(3251, "BattleWedding", "BrutalCorn");

        //Bonus Ending: Smell of Victory 3252
        Story.KillQuest(3252, "BattleWedding", "Evil Hotel Manager");

        //Bonus Ending: Don't Push the Button 3253
        Story.KillQuest(3253, "BattleWedding", "Platinum Mech Dragon");
    }
}