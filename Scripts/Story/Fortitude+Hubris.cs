//cs_include Scripts/CoreBots.cs
using RBot;

public class FortitudeHubrisStory
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        FortitudeHubris();
        Core.SetOptions(false);
    }

    public void FortitudeHubris()
    {
        if (Core.CheckInventory("Fortitude + Hubris"))
            return;

        Core.AddDrop("Fortitude + Hubris","Fortitude","Hubris");
        //1 Qualifying Quest
        Core.KillQuest(6593,"dwarfprison","Balboa",false);
        //2 Rest for the Not Very Wicked
        Core.KillQuest(6594,"pines",new[] { "Red Shell Turtle", "Pine Grizzly", "Pine Troll" },false);
        Core.MapItemQuest(6594,"tavern",6114, GetReward : false);
        //3 Pisces Pieces
        Core.KillQuest(6595,"river","kuro",false);
        //4 Be Ebil
        Core.MapItemQuest(6596,"maul",6115, GetReward : false,FollowupIDOverwrite : 1);
        Core.KillQuest(6596,"maul","Creature Creation",false,FollowupIDOverwrite : 1);
        //5 Eternal, Never-Ending Darkness and Death Lance
        Core.KillQuest(6598,"shadowrealmpast","Shadow Lord",false);
        //6 It Takes a Special Brand of Glory
        Core.KillQuest(6599,"dragontown","Chaos Fluffy",false);
        //7 1st Trial
        Core.KillQuest(6600,"kingcoal","Snow Golem",false);
        //8 2nd Trial
        Core.KillQuest(6601,"northpointe","Wyvern",false);
        //9 3rd Trial
        Core.BuyQuest(6602,"museum",1653,"Sword's Cost",1,false);
        //10 4th Trial
        Core.KillQuest(6603,"razorclaw","Enraged Razorclaw",false);
        //11 Hubris
        if(!Bot.Quests.IsUnlocked(6605))
        {
            Core.EnsureAccept(6604);
            Core.HuntMonster("doomwood","Doomwood Ectomancer","Hubris's Final Blade Shard",1,false);
            Core.HuntMonster("styx","Styx Hydra","Hubris' Magic Essence",50,false);
            Core.HuntMonster("trigoras","Trigoras","Hubris's Handle",1);
            Core.EnsureComplete(6604);
        }
        //12 Fortitude
        if(!Bot.Quests.IsUnlocked(6606))
        {
            Core.EnsureAccept(6605);
            Core.HuntMonster("cruxship","Mummy","Fortitude's Blade Shards",100,false);
            Core.HuntMonster("banished","Desterrat Moya","Fortitude's Magic Essence",50,false);
            Core.HuntMonster("iceplane","Enfield","Fortitude's Handle",1);
            Core.EnsureComplete(6605);
        }
        //13 Dual Wielding
        if(!Core.CheckInventory("Fortitude + Hubris"))
        {
            Core.EnsureAccept(6606);
            Core.CheckInventory(new[] {"Fortitude + Hubris","Fortitude","Hubris"});
            Core.HuntMonster("skytower","Aspect of Good","Aspect of Good");
            Core.HuntMonster("skytower","Aspect of Evil","Aspect of Evil");
            Core.EnsureComplete(6606);
        }
    }
}