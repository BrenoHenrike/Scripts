/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using System.Diagnostics;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using Skua.Core.Interfaces;
using Skua.Core.Models;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Players;
using Skua.Core.Options;
using Skua.Core.Models.Servers;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

public class CoreArmyLite
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    List<string> cellToAggro = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    #region Army Logging
    public ArmyLogging armyLogging = new();

    public void setLogName(string name)
    {
        armyLogging.setLogName(name);
    }

    public void registerMessage(string message, bool delPrevMsg = true)
    {
        armyLogging.registerMessage(message);
        if (delPrevMsg)
        {
            string? player1 = Bot.Config!.Get<string>("player1");
            if (!string.IsNullOrEmpty(player1) && player1.ToLower() == Core.Username().ToLower())
            {
                Core.Logger("Clearing log");
                armyLogging.ClearLogFile();
            }
        }
    }

    public void ClearLogFile()
    {
        string? player1 = Bot.Config!.Get<string>("player1");
        if (!string.IsNullOrEmpty(player1) && player1.ToLower() == Core.Username().ToLower())
        {
            Core.Logger("Clearing log");
            armyLogging.ClearLogFile();
        }
    }

    public bool isEmpty()
    {
        return armyLogging.isEmpty();
    }

    public bool isAlreadyInLog(string[] playersList)
    {
        return armyLogging.isAlreadyInLog(playersList);
    }

    public bool sendDone(int tryCount = 1)
    {
        int attempts = 0;
        while (attempts < tryCount)
        {
            try
            {
                if (!armyLogging.isAlreadyInLog(Players()))
                {
                    armyLogging.WriteLog(
                        $"{Core.Username().ToLower()}:done:{armyLogging.message}"
                    );
                    return true;
                }
                attempts++;
            }
            catch
            {
                attempts++;
            }
            Core.Sleep();
        }
        return false;
    }

    public bool isDone(int tryCount = 1, string[]? players = null)
    {
        int attempts = 0;
        while (attempts < tryCount)
        {
            try
            {
                string[] playerArray = players ?? Players();
                if (armyLogging.isAlreadyInLog(playerArray))
                    return true;
            }
            catch { }
            attempts++;
            Core.Sleep();
        }
        return false;
    }
    #endregion Army Logging

    public void initArmy()
    {
        Bot.Events.ScriptStopping += Events_ScriptStopping;

        static bool Events_ScriptStopping(Exception? e)
        {
            return true;
        }
    }

    #region Aggro Mon
#nullable enable

    public int AggroMonPacketDelay { get; set; } = 500;

    /// <summary>
    /// Starts the AggroMon. Jumps to the specified map and starts sending the AggroPacket.
    /// </summary>
    public void AggroIfAnyPlayers()
    {
        if (aggroCTS is not null)
            AggroMonStop();

        string[] players = Players();
        int partySize = players.Length;
        List<int> AggroMonMapIDs = this._AggroMonMIDs;
        foreach (string player in players)
        {
            if (player.ToLower() != Core.Username().ToLower())
            {
                Bot.Map.TryGetPlayer(player, out PlayerInfo? playerObject);
                if (playerObject != null)
                {
                    AddMapIDs(GetMapIDs(Bot.Monsters.GetMonstersByCell(playerObject.Cell)));
                }
            }
        }

        aggroCTS = new();
        Task.Run(async () =>
        {
            while (!Bot.ShouldExit && !aggroCTS.IsCancellationRequested)
            {
                try
                {
                    if (AggroMonMapIDs.Count > 0)
                        Bot.Send.Packet(AggroMonPacket(AggroMonMapIDs.ToArray()));
                    await Task.Delay(AggroMonPacketDelay);
                }
                catch { }
            }
            aggroCTS = null;
        });
        List<int> GetMapIDs(List<Monster> monsterData) => monsterData.Select(m => m.MapID).ToList();
        void AddMapIDs(List<int> MMIDs)
        {
            foreach (int ID in MMIDs)
                if (!AggroMonMapIDs.Contains(ID))
                    AggroMonMapIDs.Add(ID);
        }
    }

    public void AggroMonStart(string? map = null, string? _cell = null, string? _pad = null)
    {
        if (aggroCTS is not null)
            AggroMonStop();

        Bot.Config!.Get<string>(player1);
        Bot.Config.Get<string>(player2);
        Bot.Config.Get<string>(player3);
        Bot.Config.Get<string>(player4);
        Bot.Config.Get<string>(player5);
        Bot.Config.Get<string>(player6);
        Bot.Config.Get<string>(player7);
        Bot.Config.Get<string>(player8);
        Bot.Config.Get<string>(player9);
        Bot.Config.Get<string>(player10);

        int partySize = Players().Length;
        Core.Logger($"Part Size: {Players().Length}");

        if (map != null)
        {
            Core.Join(map);
            waitForPartyCell(_cell, _pad, partySize);
        }

        List<string> _AggroMonCells = this._AggroMonCells;
        List<string> _AggroMonNames = this._AggroMonNames;
        List<int> _AggroMonIDs = this._AggroMonIDs;
        List<int> AggroMonMapIDs = this._AggroMonMIDs; //MMIDs = Monster Map IDs

        foreach (string cell in _AggroMonCells)
            AddMapIDs(GetMapIDs(Bot.Monsters.GetMonstersByCell(cell)));
        foreach (string name in _AggroMonNames)
            AddMapIDs(GetMapIDs(Bot.Monsters.MapMonsters.Where(m => m.Name == name).ToList()));
        foreach (int ID in _AggroMonIDs)
            AddMapIDs(GetMapIDs(Bot.Monsters.MapMonsters.Where(m => m.ID == ID || m.MapID == ID).ToList()));

        aggroCTS = new();
        Task.Run(async () =>
        {
            while (!Bot.ShouldExit && !aggroCTS.IsCancellationRequested)
            {
                try
                {
                    Bot.Send.Packet(AggroMonPacket(AggroMonMapIDs.ToArray()));
                    await Task.Delay(AggroMonPacketDelay);
                }
                catch { }
            }
            aggroCTS = null;
        });

        List<int> GetMapIDs(List<Monster> monsterData)
            => monsterData.Select(m => m.MapID).ToList();
        void AddMapIDs(List<int> MMIDs)
        {
            foreach (int ID in MMIDs)
                if (!AggroMonMapIDs.Contains(ID))
                    AggroMonMapIDs.Add(ID);
        }
    }
    private CancellationTokenSource? aggroCTS = null;

    /// <summary>
    /// Stops/Pauses the Aggro Mon Task. Clear will clear the stored settings like AggroMonClear so you can set a new one.
    /// </summary>
    public void AggroMonStop(bool clear = false)
    {
    Retry:
        Bot.Options.AttackWithoutTarget = false;
        aggroCTS?.Cancel();
        if (clear)
            AggroMonClear();
        Bot.Wait.ForTrue(() => aggroCTS == null, 30);
        Core.Jump(Bot.Player.Cell, Bot.Player.Pad);
        if (aggroCTS != null)
            goto Retry;
    }

    /// <summary>
    /// Set the AggroMon using Cells. Aggros everything in the Cell.
    /// </summary>
    public void AggroMonCells(params string[] cells)
        => _AggroMonCells = cells.ToList();
    /// <summary>
    /// Set the AggroMon using Monster Names. Aggros everything with the specified name.
    /// </summary>
    public void AggroMonNames(params string[] names)
        => _AggroMonNames = names.ToList();
    /// <summary>
    /// Set the AggroMon using Monster IDs. Aggros everything using the specified ID.
    /// </summary>    
    public void AggroMonIDs(params int[] monsterIDs)
        => _AggroMonIDs = monsterIDs.ToList();
    /// <summary>
    /// Set the AggroMon using Monster Map IDs. Aggros everything using the specified Map ID.
    /// </summary>
    public void AggroMonMIDs(params int[] monsterMapIDs)
        => _AggroMonMIDs = monsterMapIDs.ToList();
    private List<string> _AggroMonCells = new();
    private List<string> _AggroMonNames = new();
    private List<int> _AggroMonIDs = new();
    private List<int> _AggroMonMIDs = new();

    /// <summary>
    /// Clears the stored Monster Cells/Names/IDs so you can set another AggroMon.
    /// </summary>
    public void AggroMonClear()
    {
        _AggroMonCells.Clear();
        _AggroMonNames.Clear();
        _AggroMonIDs.Clear();
        _AggroMonMIDs.Clear();
        _SmartAggroMonCells.Clear();
    }

    /// <summary>
    /// 
    /// </summary>
    public string AggroMonPacket(params int[] MonsterMapIDs)
        => $"%xt%zm%aggroMon%{Bot.Map.RoomID}%{string.Join('%', MonsterMapIDs)}%";

    public void SmartAggroMonStart(string map, params string?[] monsters)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = getRoomNr();
        Core.Join(map);

        //Devining variables
        var _monsters = Bot.Monsters.MapMonsters.Where(m => monsters.Contains(m.Name)).ToList();
        var cellComparison = new Dictionary<string, int>();

        //Prioritizing monsters of which fewer excist
        foreach (Monster m in _monsters)
            if (!cellComparison.ContainsKey(m.Cell))
                cellComparison.Add(m.Cell, _monsters.Count(t => t.Name == m.Name));
        var SortedDict = cellComparison.OrderBy(kvp => kvp.Value).ToDictionary(pair => pair.Key, pair => pair.Value).Keys.ToArray();
        cellComparison = null;

        //Special option on DivideOnCells, which will have it store all cells that it divides people to
        _getCellsForSmartAggroMon = true;
        DivideOnCells(SortedDict);
        _getCellsForSmartAggroMon = false;

        AggroMonCells(Core.ButlerOnMe() ? new[] { Bot.Player.Cell } : _SmartAggroMonCells.ToArray());
        AggroMonStart(map);
    }
    private bool _getCellsForSmartAggroMon = false;
    private List<string> _SmartAggroMonCells = new();

    public void RunGeneratedAggroMon(string map, List<string> monNames, List<int> questIDs, ClassType classtype, List<string>? drops = null)
    {
        if (classtype != ClassType.None)
            Core.EquipClass(classtype);

        if (questIDs.Count > 0)
            Core.RegisterQuests(questIDs.ToArray());

        if (drops == null || drops.Count == 0 || drops.All(x => string.IsNullOrEmpty(x)))
            Bot.Drops.Stop();
        else Core.AddDrop(drops.ToArray());

        SmartAggroMonStart(map, monNames.ToArray());



        while (!Bot.ShouldExit)
            Bot.Combat.Attack("*");
        AggroMonStop(true);

        if (questIDs.Count > 0)
            Core.CancelRegisteredQuests();
    }

    #region Script Options

    public Option<string> player1 = new("player1", "Account #1", "Name of one of your accounts.", "");
    public Option<string> player2 = new("player2", "Account #2", "Name of one of your accounts.", "");
    public Option<string> player3 = new("player3", "Account #3", "Name of one of your accounts.", "");
    public Option<string> player4 = new("player4", "Account #4", "Name of one of your accounts.", "");
    public Option<string> player5 = new("player5", "Account #5", "Name of one of your accounts.", "");
    public Option<string> player6 = new("player6", "Account #6", "Name of one of your accounts.", "");
    public Option<string> player7 = new("player7", "Account #7", "Name of one of your accounts.", "");
    public Option<string> player8 = new("player8", "Account #8", "Name of one of your accounts.", "");
    public Option<string> player9 = new("player9", "Account #9", "Name of one of your accounts.", "");
    public Option<string> player10 = new("player10", "Account #10", "Name of one of your accounts.", "");

    public Option<int> packetDelay = new(
        "PacketDelay", "Delay for Packet Spam", "Sets the delay for the Packet Spam\n" +
        "Increase if spamming too much - Decrease if missing kills\n" +
        "Recommended setting: 500 or 1000)", 500
    );

    #endregion

    #endregion
    #region Party Management

    public void PartyInvite(string Name)
        => Bot.Send.Packet($"%xt%zm%gp%1%pi%{Name}%");

    private void PartyAccept(int partyID)
        => Bot.Send.Packet($"%xt%zm%gp%1%pa%{partyID}%");

    public void PartyKick(string Name)
        => Bot.Send.Packet($"%xt%zm%gp%1%pk%{Name}%");

    public void PartyLeave()
        => Bot.Send.Packet($"%xt%zm%gp%1%pl%");

    public void PartySummon(string Name)
        => Bot.Send.Packet($"%xt%zm%gp%1%ps%{Name}%");

    public void PartySummonAccept()
        => Bot.Send.Packet("%xt%zm%gp%1%psa%");

    public void PartyPromote(string Name)
        => Bot.Send.Packet($"%xt%zm%gp%1%pp%{Name}%");

    public void PartyOn()
        => Bot.Send.Packet("%xt%zm%cmd%1%partyon%");

    public string[]? PartyMemberArray()
    {
        string[]? members = Bot.Flash.GetGameObject<string[]>("world.partyMembers");
        return members?.Concat(new[] { Core.Username().ToLower() }).ToArray();
    }

    public string? getPartyLeader()
        => Bot.Flash.GetGameObject<string>("world.partyOwner");


    public bool isPartyLeader()
        => Core.Username().ToLower() == (getPartyLeader() ?? string.Empty).ToLower();

    private int getPartyID()
        => Bot.Flash.GetGameObject<int>("world.partyID");

    public void PartyManagement(dynamic packet)
    {
        string type = packet["params"].type;
        dynamic data = packet["params"].dataObj;
        if (type == "json")
        {
            string cmd = data.cmd;
            switch (cmd)
            {
                //When being invited for a party, accept
                case "pi":
                    //string sender = data.owner;
                    int partyID = data.pid;
                    //if (sender.ToLower() == PartyLeader)
                    //{
                    PartyAccept(partyID);
                    Core.Logger($"Joined the party");
                    Core.Sleep();
                    Bot.Map.Jump(Bot.Player.Cell, Bot.Player.Pad, false);
                    //}
                    break;
                //When being summoned by someone, accept
                case "ps":
                    PartySummonAccept();
                    Core.Logger($"Accepted the summon");
                    break;
                //When someone leaves the party (stopped their bot), stop the bot
                case "pr":
                    string prUNM = data.unm;
                    if (!Bot.ShouldExit && stopping)
                    {
                        stopping = true;
                        Core.Logger($"A member has left the party, stopping the bot");
                        Bot.Stop(true);
                    }
                    break;
            }
        }
    }
    private bool stopping = false;

    #endregion
    #region Utility

    private bool monsterAvail(string[] monsterList)
    {
        for (int i = 0; i < monsterList.Length; i++)
        {
            if (IsMonsterAlive(monsterList[i]))
            {
                return true;
            }
        }
        return false;
    }

    public void doPriorityAttack(string[] monsterList)
    {
        for (int i = 0; i < monsterList.Length; i++)
        {
            if (IsMonsterAlive(monsterList[i]))
            {
                if (Int32.TryParse(monsterList[i], out int x))
                {
                    Bot.Combat.Attack(x);
                    return;
                }
            }
        }
    }

    public bool IsMonsterAlive(string monster)
    {
        try
        {
            string? jsonData = Bot.Flash.Call("availableMonsters");
            if (string.IsNullOrEmpty(jsonData))
            {
                Core.Logger("Failed to retrieve available monsters data.");
                return false;
            }
            JArray monsters = JArray.Parse(jsonData);

            if (monsters.Count == 0)
            {
                return false;
            }

            if (monster == "*")
            {
                foreach (var mon in monsters)
                {
                    var intState = mon["intState"]?.ToString();
                    if (string.IsNullOrEmpty(intState) || intState == "1" || intState == "2")
                    {
                        return true;
                    }
                }
                return false;
            }

            bool isByMapID = monster.StartsWith("id-");
            string identifier = isByMapID ? monster.Substring(3) : monster;

            foreach (var mon in monsters)
            {
                bool match = isByMapID
                    ? mon["MonMapID"]?.ToString() == identifier
                    : mon["strMonName"]?.ToString() == identifier;

                if (match)
                {
                    var intState = mon["intState"]?.ToString();
                    if (string.IsNullOrEmpty(intState) || intState == "1" || intState == "2")
                    {
                        return true;
                    }
                    else if (intState == "0")
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        catch { return true; }

    }

    public void StartFarm(string item, int quant)
    {
        bool needSendDone = true;
        int countCheck = 0;
        while (!Bot.ShouldExit)
        {
            if (Core.CheckInventory(item, quant) && needSendDone)
            {
                if (sendDone())
                    needSendDone = false;
            }
            if (!needSendDone && isDone() && countCheck == 10)
            {
                break;
            }
            countCheck++;
            if (countCheck > 10)
                countCheck = 0;

            // killing monster
            if (IsMonsterAlive("*"))
            {
                Bot.Combat.Attack("*");
            }

            Bot.Sleep(100);
        }
    }

    public void StartFarmRep(string faction, int rank)
    {
        bool needSendDone = true;
        int countCheck = 0;
        while (!Bot.ShouldExit)
        {
            if (Farm.FactionRank(faction) >= rank && needSendDone)
            {
                if (sendDone())
                    needSendDone = false;
            }
            if (!needSendDone && isDone() && countCheck == 10)
            {
                break;
            }
            countCheck++;
            if (countCheck > 10)
                countCheck = 0;

            if (IsMonsterAlive("*"))
            {
                Bot.Combat.Attack("*");
            }

            Bot.Sleep(100);
        }
    }

    public void StartFarmGold(int quant)
    {
        bool needSendDone = true;
        int countCheck = 0;
        while (!Bot.ShouldExit)
        {
            if (Bot.Player.Gold >= quant && needSendDone)
            {
                if (sendDone())
                    needSendDone = false;
            }
            if (!needSendDone && isDone() && countCheck == 10)
            {
                break;
            }
            countCheck++;
            if (countCheck > 10)
                countCheck = 0;

            if (IsMonsterAlive("*"))
            {
                Bot.Combat.Attack("*");
            }

            Bot.Sleep(100);
        }
    }

    /// <summary>
    /// Generates a unique 5-digit room number based on the machine name,
    /// username, and the current date and time (with a 5-hour offset). The 
    /// output is deterministic for a given machine and user, ensuring no 
    /// leading zeros in the returned integer.
    /// </summary>
    /// <returns>A unique 5-digit integer room number that does not start 
    /// with a zero.</returns>
    public int getRoomNr()
    {
        // Combine machine name, username, and fixed date for uniqueness
        string uniqueIdentifier = $"{Environment.MachineName}_{Environment.UserName}_{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}";

        // Hash the unique identifier
        using SHA256 sha256 = SHA256.Create();
        byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(uniqueIdentifier));

        // Generate combinedDigits from the hash while incorporating randomness
        StringBuilder combinedDigits = new();
        for (int i = 0; i < hash.Length; i++)
        {
            // Mix in the index to add variability
            int digit = (hash[i] + i) % 10; // Ensures it's always a digit (0-9)
            combinedDigits.Append(digit);
        }

        // Ensure we have at least 5 digits
        if (combinedDigits.Length < 5)
        {
            combinedDigits.Append('0', 5 - combinedDigits.Length); // Pad with zeros if needed
        }

        // Take the first 5 characters
        string roomNumberStr = combinedDigits.ToString().Substring(0, 5);

        // Ensure the first digit isn't '0'
        if (roomNumberStr[0] == '0')
        {
            roomNumberStr = string.Concat("1", roomNumberStr.AsSpan(1)); // Replace with '1'
        }

        // Return the integer
        return int.Parse(roomNumberStr);
    }

    /// <summary>
    /// Spreads players around the input cells, if no cells are set - will spread to any cell that has a monster in it. 
    /// If player count is more than cell count, will add players to the cells listed in order. Example: c1: P1 + P4, c2: P2, c3: P3
    /// </summary>
    public void DivideOnCells(params string[] cells)
    {
        // Parsing all the player names from an unspecified amount of player name options
        string[] _players = Players();

        // If no paramaters are given, select all cells that have monsters in them
        if (cells == null || cells.Length == 0)
        {
            List<Monster> monsters = Bot.Monsters.MapMonsters;
            if (monsters == null || monsters.Count == 0)
                return;

            List<string> _cells = new();
            foreach (string cell in monsters.Select(m => m.Cell))
                if (!_cells.Contains(cell))
                    _cells.Add(cell);
            cells = _cells.OrderBy(x => x).ToArray();
        }

        //Dividing the players amongst the cells
        int cellCount = 0;
        string username = Core.Username().ToLower();
        foreach (string p in _players)
        {
            string cell = cells[cellCount];
            if (_getCellsForSmartAggroMon && !_SmartAggroMonCells.Contains(cell))
                _SmartAggroMonCells.Add(cell);

            if (username == p)
                Core.Jump(cell, "Left");
            cellCount = cellCount == cells.Length - 1 ? 0 : cellCount + 1;
        }
    }

    public void DivideOnCellsPriority(string[] cells, string priorityCell, bool setAggro = false, bool log = false, bool equipClass = false)
    {
        // Parsing all the player names from an unspecified amount of player name options
        string[] _players = Players();
        if (log) Core.Logger($"divide on cells: {string.Join(",", cells)}. priority cell: {priorityCell}");

        if (setAggro)
        {
            cellToAggro.Clear();
            int playerCount = _players.Length;
            int _cellCount = cells.Length;
            int aggroCell = playerCount > _cellCount ? _cellCount : playerCount;
            for (int i = 0; i < aggroCell; i++)
            {
                cellToAggro.Add(cells[i]);
            }
            AggroMonCells(cellToAggro.ToArray());
        }

        if (string.IsNullOrEmpty(priorityCell))
        {
            int cellCount = 0;
            string username = Core.Username().ToLower();
            foreach (string p in _players)
            {
                string cell = cells[cellCount];
                if (username == p)
                {
                    Core.Logger($"i am on cell: {cell}");
                    Core.Jump(cell, "Left");
                }
                cellCount = cellCount == cells.Length - 1 ? 0 : cellCount + 1;
            }
        }
        else
        {
            int playersCount = _players.Length;
            string username = Core.Username().ToLower(); // Username of the player running this method
            for (int i = 0; i < playersCount; i++) // Iterate through each player
            {
                string p = _players[i];
                string cell = (cells.Length > 0 && i < cells.Length) ? cells[i] : priorityCell; // Get the cell for the current player

                if (username == p)
                {
                    Core.Logger($"i am on cell: {cell}");
                    if (equipClass)
                    {
                        if (cell.Equals(priorityCell))
                        {
                            Core.EquipClass(ClassType.Solo);
                        }
                        else
                        {
                            Core.EquipClass(ClassType.Farm);
                        }
                    }
                    Core.Jump(cell, "Left");
                }
            }
        }
    }

    public void waitForSignal(string message, string[]? players = null, bool delPrevMsg = false)
    {
        registerMessage(message, delPrevMsg);
        while (!Bot.ShouldExit)
        {
            sendDone(10);
            if (isAlreadyInLog(new string[] { Core.Username().ToLower() }))
                break;
            Bot.Sleep(500);
        }
        while (!Bot.ShouldExit)
        {
            if (players == null ? isDone(10) : isDone(10, players)) break;
            Bot.Sleep(500);
        }
    }

    /// <summary>
    /// Waits for the party members to join the specified cell in the game.
    /// If no cell is specified, it checks the current cell for the required 
    /// player count. The method logs the final list of players and monitors 
    /// the player status until the expected number of players is present or 
    /// a bugged lobby condition is detected.
    /// </summary>
    /// <param name="cell">The cell to jump to, if specified. If null, the 
    /// current cell is used.</param>
    /// <param name="pad">The direction to pad when jumping to the cell; 
    /// defaults to "Left".</param>
    /// <param name="playerCount">The expected number of players in the 
    /// party; defaults to the current player count.</param>
    public void waitForPartyCell(string? cell = null, string? pad = null, int? playerCount = null)
    {
        if (cell != null)
        {
            Bot.Map.Jump(cell, pad ?? "Left", false); // Jump to specified cell if provided
        }

        Core.Logger($"Final list of players: {string.Join(", ", Players())}");

        // Wait for party players to be ready
        while (!Bot.ShouldExit &&
       cell != null &&
       Bot.Map.CellPlayers != null &&
       Bot.Map.PlayerNames != null && // Add null check for PlayerNames
       Bot.Map.PlayerNames.Count != Players().Length)
        {
            // Sleep briefly before checking again
            Core.Sleep();

            // Check if we are waiting in a specific cell and if there are players present
            if (cell != null && Bot.Map.PlayerNames != null && Bot.Map.PlayerNames.Count() > 0)
            {
                List<string> missingPlayers = Players().Except(Bot.Map.PlayerNames).ToList();

                // Check for bugged lobby condition - Fail safe
                if (missingPlayers.Count == 1 && missingPlayers[0] == Core.Username())
                {
                    Core.Logger("Bugged lobby: we are the only one missing?");
                    break;
                }

                // Log player status if there are missing players
                if (missingPlayers.Count > 0)
                {
                    Bot.Log($"[Players Ready: {Bot.Map.PlayerNames.Count()}/{Players().Length}] Missing: {string.Join(", ", missingPlayers)}");
                }
            }
        }
    }

    public string[] Players()
    {
        List<string> players = new();
        int index = 1;

        while (!Bot.ShouldExit && index <= 20) // Stop when index exceeds 20
        {
            if (Bot.Config == null)
            {
                Core.Logger("Configuration is null.");
                break;
            }

            // Retrieve player names from the configuration
            string? playerName = Bot.Config.Get<string>($"player{index}");

            // If the player name is null or empty, continue to the next index
            if (string.IsNullOrEmpty(playerName))
            {
                index++; // Skip to the next player index
                continue; // Continue to the next loop iteration
            }

            // Add the player's name to the list and log it
            players.Add(playerName.ToLower().Trim());

            index++; // Increment the index after processing
        }

        return players.ToArray();
    }


    public void waitForParty(string map, string? item = null, int playerMax = -1)
    {
        Bot.Events.PlayerAFK += PlayerAFK;
        string[] players = Players();
        int partySize = players.Length;
        List<string> playersWhoHaveBeenHere = new() { Core.Username() };
        int playerCount = 1;

        int logCount = 0;
        int butlerTimer = 0;
        bool hasWaited = false;

        Core.Join(map);
        int dynamicPartySize = playerMax == -1 ? partySize : playerMax;

        while (playerCount < dynamicPartySize)
        {
            if (Bot.Map.PlayerNames != null)
                foreach (var name in Bot.Map.PlayerNames)
                    if (!playersWhoHaveBeenHere.Contains(name) && players.Select(x => x.ToLower().Trim()).Contains(name.ToLower()))
                        playersWhoHaveBeenHere.Add(name);
            playerCount = playersWhoHaveBeenHere.Count;

            logCount++;
            if (logCount == 15)
            {
                Core.Logger($"Waiting for the party{(item == null ? string.Empty : (" to farm " + item))} [{playerCount}/{dynamicPartySize}]");
                hasWaited = true;
                logCount = 0;
            }
            Core.Sleep(1000);

            if (playersWhoHaveBeenHere.Count == (dynamicPartySize - 1))
                butlerTimer++;
            if (butlerTimer >= 30)
            {
                b_breakOnMap = Bot.Map.Name;
                string toFollow = players.First(p => !playersWhoHaveBeenHere.Any(n => n.ToLower() == p.ToLower().Trim()));
                Core.Logger($"Missing {toFollow}, initiating Butler.cs");
                Core.Logger("Butler active until in map /" + b_breakOnMap);
                Butler(toFollow, roomNr: getRoomNr());
                Core.Logger($"{toFollow} has joined {b_breakOnMap}. continuing");
                Bot.Events.PlayerAFK -= PlayerAFK;
                break;
            }
        }
        if (hasWaited)
            Core.Logger($"Party complete [{partySize}/{partySize}]");
        Core.Sleep(3500); //To make sure everyone attack at the same time, to avoid deaths

        void PlayerAFK()
        {
            Core.Logger("Anti-AFK engaged");
            Core.Sleep(1500);
            Bot.Send.Packet("%xt%zm%afk%1%false%");
        }
    }

    public bool SellToSync(string? item, int quant)
    {
        if (Core.CheckInventory(item, quant) || item == null)
            return true;
        if (SellToSyncOn)
            Core.SellItem(item, all: true);
        return false;
    }
    public bool SellToSyncOn = false;
    #endregion
    #region OneClient

    public bool doForAll(bool randomServers = false)
    {
        if (Bot.ShouldExit || _doForAllIndex >= (doForAllAccountDetails ??= readManager()).Length)
            return false;

        Bot.Options.AutoRelogin = false;

        string name = doForAllAccountDetails[_doForAllIndex].Item1;
        string pass = doForAllAccountDetails[_doForAllIndex++].Item2;

        Server[] ServerList = Bot.Servers.CachedServers
            .Where(x => !BlacklistedServers.Contains(x.Name.ToLower()) && (Core.IsMember || !x.Upgrade) && (x.Online))
            .ToArray();

        if (Core.Username() != name)
        {
            if (Bot.Player.LoggedIn)
            {
                Bot.Servers.Logout();
                while (Bot.Player.LoggedIn)
                    Core.Sleep();
            }
            Bot.Servers.Login(name, pass);
            Core.Sleep(3000);

            Bot.Servers.Connect(
            randomServers ?
            Bot.Servers.ServerList.Where(x => !BlacklistedServers.Contains(x.Name.ToLower()) && !x.Upgrade && x.Online).ToArray()[Bot.Random.Next(1, 5)] :
            Bot.Servers.CachedServers.First(x => x.Name == Bot.Options.ReloginServer));
        }

        Bot.Wait.ForMapLoad("battleon");
        while (!Bot.ShouldExit && !Bot.Player.Loaded) { Bot.Wait.ForTrue(() => Bot.Player.Loaded, 20); }

        Bot.Send.Packet($"%xt%zm%house%1%{Core.Username()}%");
        Bot.Wait.ForMapLoad("house");

        Core.ReadCBO();
        Core.IsMember = Bot.Player.IsMember;

        return true;

        (string, string)[] readManager()
        {
            string dirPath = Path.Combine(
                                Environment.GetFolderPath(
                                    Environment.SpecialFolder.LocalApplicationData),
                                "Skua.Manager");
            if (!Directory.Exists(dirPath))
            {
                Core.Logger($"There were no (sub-)folders named {Bot.Version} found in AppData/Local/Skua.Manager. Please set up your accounts in the Account tab in the Skua.Manager.exe", "AccountManager", true, true);
                return Array.Empty<(string, string)>();
            }

            string[]? dirs = Directory.GetDirectories(dirPath, Bot.Version.ToString(), SearchOption.AllDirectories);

            // These two IFs are here cuz of the 1.2.3 VS 1.2.2.1 issue
            if (dirs == null || dirs.Length == 0)
            {
                dirs = Directory.GetDirectories(
                    Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                        "Skua.Manager"
                    ),
                    "1.2.3.0",
                    SearchOption.AllDirectories
                );
            }
            if (dirs == null || dirs.Length == 0)
            {
                dirs = Directory.GetDirectories(
                    Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                        "Skua.Manager"
                    ),
                    "1.2.2.1",
                    SearchOption.AllDirectories
                );
            }

            if (dirs == null || dirs.Length == 0)
            {
                Core.Logger($"There were no (sub-)folders named {Bot.Version} found in AppData/Local/Skua.Manager. Please set up your accounts in the Account tab in the Skua.Manager.exe", "AccountManager", true, true);
                return Array.Empty<(string, string)>();
            }
            if (dirs.Length > 1)
            {
                Core.Logger($"Two or more (sub-)folders named {Bot.Version} were found in AppData/Local/Skua.Manager. Clean up AppData/Local/Skua and add your accounts to the account manager.", "AccountManager", true, true);
                return Array.Empty<(string, string)>();
            }

            var xml = new XmlDocument();
            xml.Load(Path.Combine(dirs[0], "user.config"));

            dynamic[] dyn;
            try
            {
                dyn = JsonConvert.DeserializeObject<dynamic[]>(
                    JsonConvert.DeserializeObject<dynamic>(
                        JsonConvert.SerializeXmlNode(xml)
                    )!
                    .configuration
                    .userSettings
                    ["Skua.Manager.Properties.Settings"]
                    .setting
                    .ToString()
                );
            }
            catch
            {
                Core.Logger($"Failed to parse account information from AppData/Local/Skua.Manager. Clean up AppData/Local/Skua and add your accounts to the account manager.", "AccountManager", true, true);
                return Array.Empty<(string, string)>(); ;
            }
            List<(string, string)> toReturn = new();
            foreach (var d in dyn)
            {
                if (d["@name"] == "ManagedAccounts" && d["value"].ArrayOfString["string"] != null)
                {
                    string[] accs = JsonConvert.DeserializeObject<string[]>(d["value"].ArrayOfString["string"].ToString());
                    foreach (string acc in accs)
                    {
                        string[] info = acc.Split("{=}");
                        toReturn.Add((info[1], info[2]));
                    }
                    break;
                }
            }
            if (toReturn.Count == 0)
            {
                Core.Logger($"No accounts were found in the Skua.Manager's Account Manager. Please set up your accounts in the Skua.Manager and start up the bot again.", "AccountManager", true);
                Process.Start(Path.Combine(AppContext.BaseDirectory, "Skua.Manager.exe"));
                Bot.Stop(true);
                return Array.Empty<(string, string)>();
            }
            return toReturn.ToArray();
        }
    }
    private int _doForAllIndex = 0;
    public (string, string)[]? doForAllAccountDetails;
    private readonly string[] BlacklistedServers =
    {
        "artix",
        "sir ver",
        "yorumi",
        "gravelyn",
        "galanoth",
        "class test realm"
    };
    #endregion
    #region Butler
    public void Butler(string playerName, bool LockedMaps = true, string? LockedMapsList = null, ClassType classType = ClassType.Farm, bool CopyWalk = false, int roomNr = 1, bool rejectDrops = true, string? attackPriority = null, int hibernateTimer = 0)
    {
        #region no need to read
        // Double checking the playername and assigning it so all functions can read it
        if (playerName == "Insert Name" || string.IsNullOrEmpty(playerName))
            Core.Logger("No name was inserted, stopping the bot.", messageBox: true, stopBot: true);
        playerName = playerName.Trim().ToLower();
        b_playerName = playerName;

        // Assigning params to private objects.
        b_doLockedMaps = LockedMaps;
        b_doCopyWalk = CopyWalk;
        b_hibernationTimer = hibernateTimer;
        b_shouldHibernate = b_hibernationTimer > 0;

        if (!string.IsNullOrEmpty(attackPriority))
            _attackPriority.AddRange(attackPriority.Split(',', StringSplitOptions.TrimEntries));


        if (!string.IsNullOrEmpty(LockedMapsList))
            _LockedMapsList.AddRange(LockedMapsList.Split(',', StringSplitOptions.TrimEntries));

        // Creating directory and file to communicate with the followed player.
        if (!Directory.Exists(CoreBots.ButlerLogDir))
            Directory.CreateDirectory(CoreBots.ButlerLogDir);
        File.Create(commFile());

        // Setting room number
        if (roomNr != 999999 && roomNr >= 1000)
        {
            Core.PrivateRooms = true;
            Core.PrivateRoomNumber = roomNr;
        }

        // Bypasses
        int[] bypasses = {
            598,    // lycan
            3004,   // doomvaultb
            3008,   // doomvault
            3484,   // towerofdoom
            3799,   // shadowattack
            4616,   // mummies
            8107,    // downbelow
            9126    // manacradle
        };
        Bot.Quests.Load(bypasses);
        foreach (int questId in bypasses)
            Bot.Quests.UpdateQuest(questId);
        Core.SetAchievement(18); // doomvaultb

        // Enabling listeners
        Bot.Events.MapChanged += MapNumberParses;
        Bot.Events.ScriptStopping += ScriptStopping;
        if (CopyWalk)
            Bot.Events.ExtensionPacketReceived += CopyWalkListener;

        // Equipping class
        Core.EquipClass(classType);

        // Toggling drops
        if (!rejectDrops)
            Bot.Drops.Stop();

        #endregion no need to read

        while (!Bot.ShouldExit)
        {
            // Try to /goto the player
            if (!tryGoto(playerName))
            {
                // Handle combat disengagement if we are in combat
                while (!Bot.ShouldExit && (Bot.Player.HasTarget || Bot.Player.InCombat))
                {
                    Bot.Options.AttackWithoutTarget = false;
                    Bot.Combat.CancelTarget();
                    Core.Sleep();
                    Core.JumpWait();
                }

                // Join fallback map if /goto fails
                string stopLocation = Core.CustomStopLocation?.Trim().ToLower() ?? string.Empty;
                Core.Join(!string.IsNullOrWhiteSpace(stopLocation) ? stopLocation : "whitemap");

                // Log failure and prepare for hibernation
                Core.Logger($"Could not find {playerName}. Ensure they are on the same server.", "tryGoto");
                if (b_shouldHibernate)
                    Core.Logger($"Bot will hibernate and retry every {hibernateTimer} seconds.", "tryGoto");

                int elapsedMinutes = 0;

                // Enter hibernation retry loop
                while (!Bot.ShouldExit)
                {
                    if (b_shouldHibernate)
                    {
                        // Sleep for the hibernate period
                        for (int t = 0; t < hibernateTimer; t++)
                        {
                            Core.Sleep(1000);
                            if (Bot.ShouldExit)
                                break;
                        }
                    }

                    // Retry /goto after hibernation
                    if (tryGoto(playerName))
                    {
                        Core.Logger($"{playerName} found!");
                        break;
                    }

                    // Increment elapsed minutes and log every 5 minutes
                    elapsedMinutes += hibernateTimer;
                    if (elapsedMinutes >= 300)
                    {
                        Core.Logger($"Bot has been hibernating for {elapsedMinutes / 60} minutes.");
                        elapsedMinutes = 0;
                    }
                }
            }

            // Check for break on specific map
            if (b_breakOnMap != null && b_breakOnMap == Bot.Map.Name)
            {
                b_breakOnMap = null;
                break;
            }
            #region Combat Area
            // Check if there are monsters in the same cell
            // Loop until the bot is instructed to exit
            while (!Bot.ShouldExit && tryGoto(playerName))
            {
                if (tryGoto(playerName))
                {
                    // Check for priority attacks or available monsters
                    if (!string.IsNullOrEmpty(attackPriority))
                    {
                        if (!Bot.Combat.StopAttacking)
                            PriorityAttack();
                    }
                    else
                    {
                        if (!Bot.Combat.StopAttacking)
                            Bot.Combat.Attack("*"); // Attack any monster if no priority exists}
                        Core.Sleep(); // Pause to avoid busy waiting
                    }
                }
                else
                {
                    Bot.Combat.StopAttacking = true;
                    Core.JumpWait();
                    Core.Logger("Player Moved Cells/maps");
                    break;
                }

            }
            #endregion Combat Area
        }
        ButlerStop();
    }

    private string? b_playerName = null;
    private bool b_doLockedMaps = true;
    private bool b_doCopyWalk = false;
    private int b_hibernationTimer = 0;
    private bool b_shouldHibernate = true;
    private List<string> _attackPriority = new();
    private List<string> _LockedMapsList = new();

    private bool tryGoto(string userName)
    {
        // If you're in the same map and same cell, don't do anything
        if (Bot.Map.PlayerExists(userName) && Bot.Map.TryGetPlayer(userName, out PlayerInfo? playerObject) && playerObject != null && playerObject.Cell != Bot.Player.Cell)
        {
            Bot.Player.Goto(userName);
            return true;
        }

        if (b_doLockedMaps)
            Bot.Events.ExtensionPacketReceived += LockedZoneListener;

        // Try 3 times
        for (int i = 0; i < 3; i++)
        {
            // If the followed player is not in the map, go to a save space
            if (!Bot.Map.PlayerExists(userName))
                Core.JumpWait();

            Core.ToggleAggro(false);

            Bot.Player.Goto(userName);
            Core.Sleep();

            if (LockedZoneWarning)
                break;

            if (Bot.Map.PlayerExists(userName))
            {
                if (Bot.Map.TryGetPlayer(userName, out playerObject) && playerObject != null)
                {
                    if (playerObject.Cell != Bot.Player.Cell)
                        Bot.Player.Goto(userName);
                    Bot.Player.SetSpawnPoint();
                }
                Core.ToggleAggro(true);
                return true;
            }
        }

        if (b_doLockedMaps && LockedZoneWarning && !insideLockedMaps)
        {
            LockedZoneWarning = false;
            LockedMaps();
            Core.ToggleAggro(true);
            Bot.Events.ExtensionPacketReceived -= LockedZoneListener;
            return true;
        }

        LockedZoneWarning = false;
        Bot.Events.ExtensionPacketReceived -= LockedZoneListener;
        return false;
    }
    private bool LockedZoneWarning = false;
    private bool insideLockedMaps = false;

    private void LockedZoneListener(dynamic packet)
    {
        string type = packet["params"].type;
        dynamic data = packet["params"].dataObj;

        if (type is not null and "str")
        {
            string cmd = data[0];
            switch (cmd)
            {
                case "warning":
                    string LockerZonePacket = Convert.ToString(packet);
                    if (LockerZonePacket.Contains("a Locked zone."))
                        LockedZoneWarning = true;
                    break;
            }
        }
    }

    private void LockedMaps()
    {
        // If the followed player is leaving behind a location in the file
        if (File.Exists(Path.Combine(CoreBots.ButlerLogDir, b_playerName + ".txt")))
        {
            // Fetch the first line in the file (should only have 1 thing)
            string? targetMap = File.ReadAllLines(Path.Combine(CoreBots.ButlerLogDir, b_playerName + ".txt")).FirstOrDefault();

            // If it was not empty
            if (targetMap != null)
            {
                Core.Join(targetMap);
                if (Bot.Map.PlayerExists(b_playerName!))
                    return;
            }
        }

        string[] NonMemMaps =
        {
            "tercessuinotlim",
            "doomvaultb",
            "doomvault",
            "shadowrealmpast",
            "battlegrounda",
            "battlegroundb",
            "battlegroundc",
            "battlegroundd",
            "battlegrounde",
            "battlegroundf",
            "doomwood",
            "shadowrealm",
            "confrontation",
            "darkoviaforest",
            "ledgermayne",
            "hollowdeep",
            "hyperium",
            "willowcreek",
            "voidflibbi",
            "voidnightbane",
            "championdrakath",
            "ultraezrajal",
            "ultrawarden",
            "ultraengineer",
            "ultradage",
            "ultratyndarius",
            "ultranulgath",
            "ultradrago",
            "ultradarkon",
            "ultraspeaker"
        };
        string[] MemMaps =
        {
            "shadowlordpast",
            "binky",
            "superlowe"
        };

        string[] EventMaps =
        {
            "yoshino"
        };

        string[] VerusMaps =
        {
            "seavoice"
        };

        var levelLockedMaps = new[]
        {
            new { Map = "icestormunder", LevelRequired = 75 },
            new { Map = "icewing", LevelRequired = 75 },
            new { Map = "battlegrounde", LevelRequired = 61 },
            new { Map = "voidxyfrag", LevelRequired = 80 },
            new { Map = "voidnerfkitten", LevelRequired = 80 }
        };


        int maptry = 1;
        int mapCount = _LockedMapsList.Count == 0 ? (Core.IsMember ? NonMemMaps.Length + MemMaps.Length : NonMemMaps.Length) : (_LockedMapsList.Count);

        if (_LockedMapsList.Count == 0)
        {
            foreach (string map in VerusMaps)
            {
                Core.Logger($"[{(maptry.ToString().Length == 1 ? "0" : "")}{maptry++}/{mapCount}] Searching for {b_playerName} in /{map}", "LockedZoneHandler");
                Core.Join(map);

                if (!Bot.Map.PlayerExists(b_playerName!))
                    continue;
                else
                {
                    Core.Logger($"{b_playerName!} Found!");
                    return;
                }
            }

            foreach (string map in EventMaps)
            {
                if (!Core.isSeasonalMapActive(map))
                    continue;

                Core.Logger($"[{(maptry.ToString().Length == 1 ? "0" : "")}{maptry++}/{mapCount}] Searching for {b_playerName} in /{map}", "LockedZoneHandler");
                Core.Join(map);

                if (!Bot.Map.PlayerExists(b_playerName!))
                    continue;
                else
                {
                    Core.Logger($"{b_playerName!} Found!");
                    return;
                }
            }

            foreach (var mapInfo in levelLockedMaps)
            {
                if (Bot.Player.Level < mapInfo.LevelRequired)
                {
                    Core.Logger($"Not a high enough level.\n" +
                    $"required: {mapInfo.LevelRequired}, your's: {Bot.Player.Level}");
                    continue;
                }

                Core.Logger($"[{(maptry.ToString().Length == 1 ? "0" : "")}{maptry++}/{mapCount}] Searching for {b_playerName} in /{mapInfo.Map}", "LockedZoneHandler");
                Core.Join(mapInfo.Map);

                if (!Bot.Map.PlayerExists(b_playerName!))
                    continue;
                else
                {
                    Core.Logger($"{b_playerName!} Found!");
                    return;
                }
            }

            foreach (string map in NonMemMaps)
            {
                Core.Logger($"[{(maptry.ToString().Length == 1 ? "0" : "")}{maptry++}/{mapCount}] Searching for {b_playerName} in /{map}", "LockedZoneHandler");
                Core.Join(map);

                if (!Bot.Map.PlayerExists(b_playerName!))
                    continue;
                else
                {
                    Core.Logger($"{b_playerName!} Found!");
                    return;
                }
            }

            if (Core.IsMember)
            {
                foreach (string map in MemMaps)
                {
                    Core.Logger($"[{(maptry.ToString().Length == 1 ? "0" : "")}{maptry++}/{mapCount}] Searching for {b_playerName} in /{map}", "LockedZoneHandler");
                    Core.Join(map);

                    if (!Bot.Map.PlayerExists(b_playerName!))
                        continue;
                    else
                    {
                        Core.Logger($"{b_playerName!} Found!");
                        return;
                    }
                }
            }
        }
        else
        {
            foreach (string map in _LockedMapsList)
            {
                Core.Logger($"[{(maptry.ToString().Length == 1 ? "0" : "")}{maptry++}/{mapCount}] Searching for {b_playerName} in /{map}", "LockedZoneHandler");
                if (Bot.Map.Name != map)
                    Core.Join(map);

                if (!Bot.Map.PlayerExists(b_playerName!))
                    continue;

                Core.Logger($"{b_playerName!} Found!");
                return;
            }

        }
        Core.Join("whitemap");
        Core.Logger($"Could not find {b_playerName} in any of the maps within the LockedZoneHandler.", "LockedZoneHandler");
        if (b_shouldHibernate)
            Core.Logger($"The bot will now hibernate and try to /goto to {b_playerName} every {b_hibernationTimer} seconds", "LockedZoneHandler");

        int min = 1;
        while (!Bot.ShouldExit)
        {
            if (b_shouldHibernate)
            {
                for (int t = 0; t < b_hibernationTimer; t++)
                {
                    Core.Sleep(1000);
                    if (Bot.ShouldExit)
                        break;
                }
            }
            if (tryGoto(b_playerName!))
            {
                Core.Logger(b_playerName + " found!");
                return;
            }
            min += b_hibernationTimer;

            if (min >= 300)
            {
                Core.Logger($"The bot is has been hibernating for {min / 300} minutes");
                min = 0;
            }
        }
        return;

        // If useful, will refactor so that we can use this again with this method

        // void _killTheUltra(string cell)
        // {
        //     if (Bot.Player.Cell == cell && Bot.Monsters.CurrentMonsters.Count(m => Core.IsMonsterAlive(m)) > 0)
        //     {
        //         Monster? Target = Bot.Monsters.CurrentMonsters.MaxBy(x => x.MaxHP);
        //         if (Target == null)
        //         {
        //             Core.Logger("No monsters found", "KillUltra");
        //             return;
        //         }
        //         PriorityAttack(Target.Name);
        //     }
        // }
    }

    public void PriorityAttack()
    {
        // If there's an attack priority list, search for a priority monster to attack
        if (_attackPriority?.Count > 0)
        {
            foreach (string mon in _attackPriority)
            {
                string formattedMonName = mon.FormatForCompare();

                // Find the first matching priority monster in the same cell
                Monster? priorityMonster = Bot.Monsters.CurrentMonsters
                    .FirstOrDefault(m => m.Name?.FormatForCompare() == formattedMonName && m.Cell == Bot.Player.Cell);

                if (priorityMonster != null)
                {
                    Bot.Combat.Attack(priorityMonster); // Attack the priority monster
                    Core.Sleep(); // Pause after attacking
                    return; // Exit if a priority monster was found and attacked
                }
            }
        }

        // If no priority monster was found, attack any available monster
        Bot.Combat.Attack("*");
    }

    private async void MapNumberParses(string map)
    {
        // Wait untill the full name I.E. "battleon-12345" is set
        if (string.IsNullOrEmpty(Bot.Map.FullName))
        {
            for (int a = 0; a < 10; a++)
            {
                if (!string.IsNullOrEmpty(Bot.Map.FullName))
                    break;
                await Task.Delay(Core.ActionDelay);
                if (a == 9)
                    return;
            }
        }

        if (!int.TryParse(Bot.Map.FullName.Split('-').Last(), out int mapNr) || map == b_prevRoom || !Bot.Map.PlayerExists(b_playerName!))
            return;

        // If the number is the same number as on the previous map
        if (b_allocRoomNr == mapNr)
        {
            // If the set private room number wasn't correct
            if (Core.PrivateRoomNumber != mapNr)
            {
                Core.Logger("Static room number detected. PrivateRoomNumber is now " + mapNr);
                Core.PrivateRoomNumber = mapNr;
            }
            Core.PrivateRooms = mapNr >= 1000;
            Bot.Events.MapChanged -= MapNumberParses;
            return;
        }

        b_prevRoom = map;
        b_allocRoomNr = mapNr;
    }
    private int b_allocRoomNr = 0;
    private string? b_prevRoom = null;

    private void CopyWalkListener(dynamic packet)
    {
        string type = packet["params"].type;
        dynamic data = packet["params"].dataObj;
        if (type is not null and "str")
        {
            string cmd = data[0];
            switch (cmd)
            {
                //movement in the same cell || From server: %xt%uotls%-1%{playerName}%sp:8,tx:181,ty:358,strFrame:Bigger%
                //movement to another cell || From server: %xt%uotls%-1%{playerName}%mvts:-1,px:500,py:375,strPad:Left,bResting:false,mvtd:0,tx:0,ty:0,strFrame:Bigger%
                case "uotls":
                    string WalkPacket = Convert.ToString(packet);
                    if (!WalkPacket.Contains(b_playerName!))
                        break;

                    foreach (string str in WalkPacket.Split(','))
                    {
                        string spl = "";
                        if (str.Contains(':'))
                            spl = str.Split(':')[1];

                        switch (str.Split(':')[0])
                        {
                            // Setting X cordinate
                            case "tx":
                                moveX = int.Parse(spl);
                                break;

                            // Setting Y cordinate
                            case "ty":
                                moveY = int.Parse(spl);
                                break;

                            // Setting speed
                            case "sp":
                                moveSpeed = int.Parse(spl);
                                break;
                        }
                    }

                    if (moveX != 0 || moveY != 0)
                        Bot.Flash.Call("walkTo", moveX, moveY, moveSpeed);
                    break;
            }
        }
    }
    private int moveX = 0;
    private int moveY = 0;
    private int moveSpeed = 0;

    private bool ScriptStopping(Exception? e)
    {
        ButlerStop();
        return true;
    }

    private void ButlerStop()
    {
        // Removing listeners
        Bot.Events.MapChanged -= MapNumberParses;
        Bot.Events.ExtensionPacketReceived -= LockedZoneListener;
        Bot.Events.ExtensionPacketReceived -= CopyWalkListener;

        // Delete communication files
        if (File.Exists(commFile()))
            File.Delete(commFile());
    }

    private string commFile() => Path.Combine(CoreBots.ButlerLogDir, $"{Core.Username().ToLower()}~!{b_playerName}.txt");
    public string? b_breakOnMap = null;
    #endregion
}

public class ArmyLogging
{
    private static readonly object lockObject = new();
    private string logFilePath = string.Empty;
    public string message = string.Empty;

    // public ArmyLogging(string fileName = "ArmyLog.txt")
    // {
    //     logFilePath = Path.Combine(ClientFileSources.SkuaOptionsDIR, fileName);
    //     ClearLogFile();
    // }
    public void setLogName(string fileName)
    {
        logFilePath = Path.Combine(ClientFileSources.SkuaOptionsDIR, fileName + ".log");
        //ClearLogFile();
    }

    public void registerMessage(string msg)
    {
        message = msg;
    }

    public bool isEmpty()
    {
        if (new FileInfo(logFilePath).Length == 0)
        {
            return true;
        }
        using FileStream stream = File.OpenRead(logFilePath);
        return stream.Length == 0;
    }

    public bool isAlreadyInLog(string[] playersList)
    {
        try
        {
            List<string> lines = ReadLog();
            string joinedString = string.Join(Environment.NewLine, lines);
            string[] players = playersList;
            foreach (string pl in players)
            {
                if (!joinedString.Contains($"{pl.ToLower()}:done:{message}"))
                {
                    return false;
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void WriteLog(string logMessage)
    {
        lock (lockObject)
        {
            using StreamWriter w = File.AppendText(logFilePath);
            w.WriteLine($"{logMessage}");
        }
    }

    public List<string> ReadLog()
    {
        List<string> lines = new();
        lock (lockObject)
        {
            using StreamReader r = File.OpenText(logFilePath);
            string? line;
            while ((line = r.ReadLine()) != null)
            {
                lines.Add(line);
            }
        }
        return lines;
    }

    public void ClearLogFile()
    {
        lock (lockObject)
        {
            using FileStream fs = File.Open(logFilePath, FileMode.Create, FileAccess.Write);
            // File is truncated and cleared
        }
    }
}

// Top-level static class for DateTime extension methods
public static class DateTimeExtensions
{
    public static long ToUnixTimeSeconds(this DateTime dateTime)
    {
        return (long)(dateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
    }
}