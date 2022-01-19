//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs

//cs_include Scripts/CoreFile(Or folder)/Filename.cs

using RBot;

public class HedgeMaze
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDailys Dailys = new CoreDailys();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        HedgeMaze_Questline();

        Core.SetOptions(false);
    }

    public void HedgeMaze_Questline()
    {
        //Quest1:
            Core.KillQuest(QuestID: 5298, MapName: "hedgemaze", MonsterName: "Knight's Reflection");
        //Quest2:
            Core.EnsureAccept(5299);
            Core.Jump("r11", "Bottom"); 
            Bot.Sleep(1500);          
            Core.GetMapItem(itemID: 4679, quant: 1, map: "hedgemaze");
            Core.KillQuest(QuestID: 5299, MapName: "hedgemaze", MonsterNames: new[] { "Mirrored Shard", "Hedge Goblin", "Minotaur" } );
        //Quest3:
            Core.KillQuest(QuestID: 5300, MapName: "hedgemaze", MonsterName: "Knight's Reflection");
        //Quest4:
            Core.EnsureAccept(5301);
            Core.Jump("r11", "Bottom");       
            Core.GetMapItem(itemID: 4680, quant: 1, map: "hedgemaze");
            Core.EnsureComplete(5301);
    }
}