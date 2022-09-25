//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Tercessuinotlim
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        JadziaQuests();

        Core.SetOptions(false);
    }

    public void JadziaQuests()
    {
        if (Core.isCompletedBefore(8474))
            return;

        Story.PreLoad(this);

        //Starting off Small 8469
        if (!Story.QuestProgression(8469))
        {
            Core.EnsureAccept(8469);
            Core.HuntMonster("Tercessuinotlim", "Dark Makai", "Makai Captured", 15);
            Core.HuntMonster("ChaosLab", "Chaorrupted Moglin", "Chaorrupted Mutation", 5);
            Core.HuntMonster("RedDeath", "RedDeath Moglin", "RedDeath Mutation", 5);
            Core.HuntMonster("RedDeath", "Swamp Wraith", "Wraith Mutation", 5);
            Core.EnsureComplete(8469);
        }

        //Those Who Don’t Learn… 8470
        if (!Story.QuestProgression(8470))
        {
            Core.EnsureAccept(8470);
            Core.HuntMonster("Underworld", "Revontheus", "Revontheus Reviewed");
            Core.HuntMonster("Underworld", "Bloodfiend", "Bloodfiend Browbeaten", 20);
            Core.HuntMonster("Underworld", "Dreadfiend of Nulgath", "Dreadfiend Disciplined", 20);
            Core.HuntMonster("Underworld", "Dilligas", "Dilligas Demoted");
            Core.HuntMonster("Underworld", "Klunk", "Klunk Kriticized");
            Core.EnsureComplete(8470);
        }

        //Chibification Nation 8471
        if (!Story.QuestProgression(8471))
        {
            Core.EnsureAccept(8471);
            Core.HuntMonster("Pastelia", "Chaos Queen Beleen", "Insanely Pink Energy");
            Core.HuntMonster("Pastelia", "Happy Cloud", "Fluffy Pink Energy", 7);
            Core.HuntMonster("Pastelia", "Cutie Makai", "Cutsey Pink Energy", 8);
            Core.HuntMonster("SewerPink", "Cutie Grumbley", "Slimy Pink Energy", 5);
            Core.EnsureComplete(8471);
        }

        //A Little Fiendly Sparring 8472
        if (!Story.QuestProgression(8472))
        {
            Core.EnsureAccept(8472);
            Core.HuntMonster($"Fiendshard", "Dirtlicker", "Dirtlicker’s Dignity Decimated");
            Core.HuntMonster($"Tercessuinotlim", "Death's Head", "Death’s Head Demolished");
            Core.HuntMonster("ShadowBlast", "CaesarIsTheDark|Carnage", "Shadowblaster Shamed", 10);
            Core.HuntMonster("QuibbleHunt", "Skew", "Skew Straightened Out");
            Core.HuntMonster("ShadowBlast", "Crag and Bamboozle", "Crag and Bamboozle Cowering");
            Core.HuntMonster("ShadowBlast", "Grimlord Boss", "Grimlord Left Groveling");
            Core.HuntMonster("tercessuinotlim", "Void Monk", "Void Warrior Vanquished", 10);
            Core.EnsureComplete(8472);
        }

        //Working Shard or Shardly Working? 8473
        if (!Story.QuestProgression(8473))
        {
            Core.EnsureAccept(8473);
            Core.HuntMonster($"Fiendshard", "Fiend Shard", "Dirtlicker’s Shard Shaving", 3);
            Core.HuntMonster($"Fiendshard", "Nulgath's Fiend Shard", "Nulgath’s Shard Shaving", 3);
            Core.HuntMonster("QuibbleHunt", "RogueFiend", "Roguefiend Crystal Shaving", 5);
            Core.EnsureComplete(8473);
        }

        //The Wanderer’s Words 8474
        if (!Story.QuestProgression(8474))
        {
            Core.EnsureAccept(8474);
            Core.HuntMonster("hachiko", "Dai Tengu", "101 Proof Blade Oil");
            Core.HuntMonster("Kitsune", "Kitsune", "Private Reserve Sake");
            Core.HuntMonster("Tercessuinotlim", "Taro Blademaster", "The Tale of Taro");
            Core.EnsureComplete(8474);
        }

    }
}
