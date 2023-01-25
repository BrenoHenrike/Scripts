/*
name: aggro monster creator
description: This bot will allow you to start a custom AggroMon bot. It also allows you to save it to a file for later.
tags: custom, aggro monster, army
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.ViewModels;
using Skua.Core.Options;
using CommunityToolkit.Mvvm.DependencyInjection;

public class CustomAggroMon
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreArmyLite Army = new();

    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "CustomAggroMon";
    public List<IOption> Options = new List<IOption>()
    {
        new Option<string>("map", "Map Name",
                           "Please provide the map you wish to join."),

        new Option<string>("monsters", "Monster Names*",
                           "Please provide the monster names or IDs you wish to fight.\n" +
                           "Split them with a ,\n" +
                           "Example: \"monster1,monster2,103\" or \"monster1, 102, monster3\"\n" +
                           "* = If left empty, the bot will give you a prompt where you get an overview of all the monsters in the map."),

        new Option<string>("quests", "QuestIDs",
                           "Please provide the quest IDs you wish to have the quest handle (optional).\n" +
                           "Split them with a ,\n" +
                           "Example: \"1234,1235,1236\" or \"1234, 1235, 1236\""),

        new Option<string>("drops", "Drops to pickup",
                           "Please provide the names of the items you wish to automatically pick up (optional).\n" +
                           "Split them with a ,\n" +
                           "Example: \"drop1,drop2,drop3\" or \"drop1, drop2, drop3\""),

        new Option<ClassType>("classtype", "Class Type",
                              "Would you like to use your Solo or Farm Class?",
                              ClassType.Farm),

        new Option<bool>("genFile", "Save to file",
                         "If true, the bot will generate a copy of these settings in the \"Army/Generated\"",
                         false),

        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        sArmy.player7,
        sArmy.player8,
        sArmy.player9,
        sArmy.player10,
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        MakeAggroMon();

        Core.SetOptions(false);
    }

    public void MakeAggroMon()
    {
        string map = Bot.Config.Get<string>("map");
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();
        Core.Join(map);

        var _monData = Bot.Monsters.MapMonsters;
        List<string> _monDataNames = _monData.Select(m => m.Name).ToList();
        string monsters = Bot.Config.Get<string>("monsters");
        if (String.IsNullOrEmpty(monsters) || String.IsNullOrWhiteSpace(monsters))
            monsters = getMonsters();
        string[] monsterList = monsters.Split(',');

        // player correction
        for (int i = 0; i < monsterList.Length; i++)
            monsterList[i] = monsterList[i].Trim().ToLower();

        // splitting ID from names
        List<string> monNames = new();
        List<string> remainder = new();
        foreach (string s in monsterList)
        {
            if (Int32.TryParse(s, out int monID) && Bot.Monsters.TryGetMonster(monID, out var Monster))
                monNames.Add(Monster.Name);
            else remainder.Add(s);
        }

        // finding the right monsters based on name
        List<string> remainder2 = new();
        foreach (string s in remainder)
        {
            if (_monDataNames.Any(x => x.ToLower().Trim() == s))
                monNames.Add(_monDataNames.First(x => x.ToLower().Trim() == s));
            else remainder2.Add(s);
        }

        if (remainder2.Count > 0)
        {
            Core.Logger($"The bot was unable to find the following monster{(remainder2.Count == 1 ? "" : "s")}: " + String.Join('|', remainder2));
        }

        if (monNames.Count == 0)
            Core.Logger("No monsters were found based on your input. The bot will now stop.", messageBox: true, stopBot: true);

        string[] quests = Bot.Config.Get<string>("quests").Split(',');
        List<int> questIDs = new();
        foreach (string q in quests)
        {
            if (!Int32.TryParse(q.Trim(), out int ID) || questIDs.Contains(ID))
                continue;
            questIDs.Add(ID);
        }

        List<string> drops = Bot.Config.Get<string>("drops").Split(',').ToList();


        GenerateFile();


        if (drops == null || drops.Count() == 0 || drops.All(x => String.IsNullOrEmpty(x)))
            Bot.Drops.Stop();
        else Core.AddDrop(drops.ToArray());

        ClassType classtype = Bot.Config.Get<ClassType>("classtype");
        if (classtype != ClassType.None)
            Core.EquipClass(classtype);

        if (questIDs.Count > 0)
            Core.RegisterQuests(questIDs.ToArray());

        Army.SmartAggroMonStart(map, monNames.ToArray());
        while (!Bot.ShouldExit)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);

        if (questIDs.Count > 0)
            Core.CancelRegisteredQuests();

        string getMonsters()
        {
            List<string> _monsters = new();
            _monData = _monData.OrderByDescending(m => _monData.Count(_m => _m.ID == m.ID)).ToList();

            foreach (var _mon in _monData)
            {
                string data = $"{_monData.Count(m => m.ID == _mon.ID)}x [{_mon.ID}] {_mon.Name}";
                if (!_monsters.Contains(data))
                {
                    _monsters.Add(data);

                    List<string> _cells = new();
                    foreach (string cell in _monData.Where(m => m.ID == _mon.ID).Select(m => m.Cell))
                        if (!_cells.Contains(cell))
                            _cells.Add(cell);

                    _monsters.Add($"     Cell count: {_cells.Count}{(_mon.Race == "None" ? String.Empty : $", Race-Type: {_mon.Race}")}, HP: {_mon.MaxHP}");
                }
            }

            InputDialogViewModel monDiag = new("Monsters in /" + map,
                    "Please tell us what monsters you would wanna aggromon? (Names/IDs)\n\n" +
                    String.Join('\n', _monsters) +
                    "\n\nDont forget to use , as a divider if you wish to use more than one\nmonster.", false);
            if (Ioc.Default.GetRequiredService<IDialogService>().ShowDialog(monDiag) != true)
                Bot.Stop(true);
            return monDiag.DialogTextInput;
        }

        void GenerateFile()
        {
            if (!Bot.Config.Get<bool>("genFile"))
                return;

            InputDialogViewModel diag = new("Name the bot", "What is the name you wish to give the bot. (case-sensitive)", false);
            if (Ioc.Default.GetRequiredService<IDialogService>().ShowDialog(diag) != true)
                return;

            if (!Directory.Exists(Path.Combine(CoreBots.ScriptsPath, "Army", "Generated")))
                Directory.CreateDirectory(Path.Combine(CoreBots.ScriptsPath, "Army", "Generated"));

            string[] template = File.ReadAllLines(Path.Combine(CoreBots.ScriptsPath, "Templates", "CustomAggroMonTemplate.cs"));
            string botName = removeInvalidChar(diag.DialogTextInput);

            int classIndex = FetchIndex("public class CustomAggroMonTemplate");
            template[classIndex] = $"{spaces}public class Generated_{botName}";

            int callingMethodIndex = FetchIndex("CustomAggroMon();");
            template[callingMethodIndex] = $"{spaces}{botName}();";
            int methodVoidIndex = FetchIndex("public void CustomAggroMon()");
            template[methodVoidIndex] = $"{spaces}public void {botName}()";

            if (questIDs.Count > 0)
            {
                int questIndex = FetchIndex("private List<int> questIDs = new() { };");
                template[questIndex] = $"{spaces}private List<int> questIDs = new() {{ {String.Join(", ", questIDs)} }};";
            }
            int monsterIndex = FetchIndex("private List<string> monNames = new() { };");
            template[monsterIndex] = $"{spaces}private List<string> monNames = new() {{ \"{String.Join("\", \"", monNames)}\" }};";

            if (drops.Count > 0)
            {
                int dropsIndex = FetchIndex("private List<string> drops = new() { };");
                template[dropsIndex] = $"{spaces}private List<string> drops = new() {{ \"{String.Join("\", \"", drops)}\" }};";
            }
            int mapIndex = FetchIndex("private string map = \"\";");
            template[mapIndex] = $"{spaces}private string map = \"{map.ToLower().Trim()}\";";
            int classTypeIndex = FetchIndex("private ClassType classtype = ClassType.None;");
            template[classTypeIndex] = $"{spaces}private ClassType classtype = ClassType.{Bot.Config.Get<ClassType>("classtype")};";

            File.WriteAllLines(Path.Combine(CoreBots.SkuaPath, "Scripts", "Army", "Generated", diag.DialogTextInput.Replace(" ", "") + ".cs"), template);
            Core.Logger($"\"{diag.DialogTextInput.Replace(" ", "")}.cs\" has been generated and can be found in Skua_Modules/Scripts/Army/Generated", messageBox: true);

            int FetchIndex(string text)
            {
                int toReturn = Array.FindIndex(template, l => l.Trim() == text);
                if (toReturn == -1)
                    Core.Logger("Failed to find: " + text, stopBot: true);

                spaces = "";
                for (int i = 0; i < template[toReturn].TakeWhile(c => c == ' ').Count(); i++)
                    spaces += " ";

                return toReturn;
            }
        }
    }
    private string spaces = "";

    private string removeInvalidChar(string input)
    {
        string toReturn = "";
        foreach (char c in input)
            if (Char.IsLetter(c))
                toReturn += c;
        return toReturn;
    }
}
