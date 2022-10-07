//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using CommunityToolkit.Mvvm.DependencyInjection;

public class AggroMonReader
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreArmyLite Army = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "AggroMonReader";
    public List<IOption> Options = new List<IOption>()
    {
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

        AggroMon();

        Core.SetOptions(false);
    }
#nullable enable

    public void AggroMon()
    {
        #region Gathering Data
        // Finding file
        _fileDialog = Ioc.Default.GetRequiredService<IFileDialogService>();
        string _scriptPath = _scriptPath = Path.Combine(AppContext.BaseDirectory, "Scripts");
        string? path = _fileDialog.OpenFile(_scriptPath, "Aggromon File (*.txt)|*.txt");
        if (path == null)
            return;
        string[] file = File.ReadAllLines(path);

        //1) Base file name
        //2) Server name
        //3) Room number
        //4) map name
        //5) < cell >,< pad >,< cell >,< pad >,...
        //6) aggromon packet1; aggromon packet2; ...
        //7) drop 1,drop 2, ...
        //8) quest 1,quest 2, ...
        //9) specific drop 1~quantity1,specific drop 2~quantity2
        //10) y,n, ...

        // Assigning data
        string caller = file[0].Replace(" ", "");
        string map = file[3];
        string[] cellPads = file[4].Split(',');
        string[] packets = file[5].Split(';');
        string[]? drops = getData(6, ',');
        string[]? quests = getData(7, ',');
        string[]? specificDrops = getData(8, ',');

        string[]? getData(int index, char split)
        {
            try
            {
                return file[index].Split(split);
            }
            catch { }
            return null;
        }

        #endregion

        Core.Logger("Map parsed, will join the following map: /" + map.ToLower(), caller);

        #region Cleaning Data
        // Cleaning up data
        List<string> cells = new();
        for (int i = 0; i < cellPads.Length; i = i + 2)
            cells.Add(cellPads[i]);
        if (cells.Count == 1)
            Core.Logger("Cell parsed, will move to the following cell: " + cells.First(), caller);
        else if (cells.Count >= 2)
            Core.Logger("Cells parsed, will devide amongst the following cell: " + String.Join(", ", cells), caller);

        List<int> MMIDs = new();
        foreach (string p in packets)
        {
            string[] d = p.Split('%');
            for (int i = 5; i < d.Length; i++)
            {
                if (Int32.TryParse(d[i], out int id) && !MMIDs.Contains(id))
                    MMIDs.Add(id);
            }
        }
        Core.Logger("Packets parsed, will aggro the following IDs: " + String.Join(", ", MMIDs), caller);

        List<int> questIDs = new();
        if (quests != null)
        {
            foreach (string q in quests)
            {
                if (Int32.TryParse(q, out int id) && !questIDs.Contains(id))
                    questIDs.Add(id);
            }
            if (questIDs.Count == 1)
                Core.Logger("Quest ID parsed, will register the following quest: " + questIDs.First(), caller);
            else if (questIDs.Count >= 2)
                Core.Logger("Quest IDs parsed, will register the following quests: " + String.Join(", ", questIDs), caller);
        }

        List<string> toAddDrop = new();
        if (drops != null)
            toAddDrop.AddRange(drops);
        if (specificDrops != null)
        {
            foreach (string d in specificDrops)
            {
                string item = d.Split('~').First();
                if (!toAddDrop.Contains(item))
                    toAddDrop.Add(item);
            }
        }
        if (toAddDrop.Count == 1)
            Core.Logger("Drop parsed, will pickup the following item: " + toAddDrop.First(), caller);
        else if (toAddDrop.Count >= 2)
            Core.Logger("Drops parsed, will pickup the following items: " + String.Join(", ", toAddDrop), caller);

        #endregion

        // Showtime
        Core.PrivateRoomNumber = Army.getRoomNr();
        Core.PrivateRooms = true;

        Core.AddDrop(toAddDrop.ToArray());

        Army.AggroMonMIDs(MMIDs.ToArray());
        Army.AggroMonStart(map);
        if (cells.Count == 1)
            Core.Jump(cells.First());
        else Army.DivideOnCells(cells.ToArray());

        Core.RegisterQuests(questIDs.ToArray());
        Core.Logger($"AggroMonBot \"{file[0]}\" initiated", caller);

        while (!Bot.ShouldExit)
            Bot.Combat.Attack("*");
        Core.CancelRegisteredQuests();
        Army.AggroMonStop(true);
    }
    private IFileDialogService? _fileDialog;
}
