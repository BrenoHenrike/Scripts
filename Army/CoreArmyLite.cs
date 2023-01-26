/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Players;
using Skua.Core.Options;
using Skua.Core.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

public class CoreArmyLite
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    #region Aggro Mon
#nullable enable

    public int AggroMonPacketDelay { get; set; } = 500;

    /// <summary>
    /// Starts the AggroMon. Jumps to the specified map and starts sending the AggroPacket.
    /// </summary>
    public void AggroMonStart(string map)
    {
        if (aggroCTS is not null)
            AggroMonStop();

        Core.Join(map);

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
        aggroCTS?.Cancel();
        if (clear)
            AggroMonClear();
        Bot.Wait.ForTrue(() => aggroCTS == null, 30);
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
        => $"%xt%zm%aggroMon%{Bot.Map.RoomID}%{String.Join('%', MonsterMapIDs)}%";

    public void SmartAggroMonStart(string map, params string[] monsters)
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

        if (drops == null || drops.Count() == 0 || drops.All(x => String.IsNullOrEmpty(x)))
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
        return members == null ? null : members.Concat(new[] { Core.Username().ToLower() }).ToArray();
    }

    public string? getPartyLeader()
        => Bot.Flash.GetGameObject<string>("world.partyOwner");


    public bool isPartyLeader()
        => Core.Username().ToLower() == (getPartyLeader() ?? String.Empty).ToLower();

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
                    Bot.Sleep(Core.ActionDelay);
                    Bot.Map.Jump(Bot.Player.Cell, Bot.Player.Pad);
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

    /// <summary>
    /// Sets a random Room Number to ensure armies join the same room.
    /// </summary>
    public int getRoomNr()
    {
        string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string combinedDigits = "";

        foreach (char c in (CoreBots.SkuaPath ?? Bot.Config!.Get<string>(player1)!).ToUpper())
        {
            if (char.IsDigit(c))
                combinedDigits += c;
            else if (char.IsLetter(c) && Alphabet.Contains(c))
                combinedDigits += Alphabet.IndexOf(c);

            if (combinedDigits.Length >= 36)
                break;
        }

        while (!Bot.ShouldExit && combinedDigits.Length < 4)
        {
            combinedDigits = (int.Parse(combinedDigits) * (DateTime.Now.Hour - DateTimeOffset.UtcNow.Hour) * DateTime.Today.Day).ToString();
        }

        while (!Bot.ShouldExit && combinedDigits.Length >= 6)
        {
            long firstHalf = long.Parse(combinedDigits.Substring(0, (combinedDigits.Length / 2)));
            long secondHalf = long.Parse(combinedDigits.Substring(combinedDigits.Length / 2));
            combinedDigits = (firstHalf + secondHalf).ToString();
            if (combinedDigits.Length <= 4)
                combinedDigits = (long.Parse(combinedDigits) * DateTime.Today.Day).ToString();
        }
        return int.Parse(combinedDigits);
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
        if ((cells == null || cells.Count() == 0))
        {
            List<Monster> monsters = Bot.Monsters.MapMonsters;
            if (monsters == null || monsters.Count() == 0)
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
                Core.Jump(cell);
            cellCount = cellCount == cells.Count() - 1 ? 0 : cellCount + 1;
        }
    }

    public string[] Players()
    {
        List<string> players = new();
        int i = 1;
        while (!Bot.ShouldExit)
        {
            try
            {
                if (Bot.Config == null)
                    break;

                string? player = Bot.Config.Get<string>("player" + i++);
                if (String.IsNullOrEmpty(player))
                    break;

                players.Add(player.ToLower().Trim());
            }
            catch
            {
                break;
            }
        }
        return players.ToArray();
    }

    public void waitForParty(string map, string? item = null)
    {
        string[] players = Players();
        int partySize = players.Count();
        List<string> playersWhoHaveBeenHere = new() { Bot.Player.Username };
        int playerCount = 1;

        int logCount = 0;
        int butlerTimer = 0;
        bool hasWaited = false;

        Core.Join(map);

        while (playerCount < partySize)
        {
            if (Bot.Map.PlayerNames != null)
                foreach (var name in Bot.Map.PlayerNames)
                    if (!playersWhoHaveBeenHere.Contains(name) && players.Select(x => x.ToLower().Trim()).Contains(name.ToLower()))
                        playersWhoHaveBeenHere.Add(name);
            playerCount = playersWhoHaveBeenHere.Count();

            logCount++;
            if (logCount == 15)
            {
                Core.Logger($"Waiting for the party{(item == null ? String.Empty : (" to farm " + item))} [{playerCount}/{partySize}]");
                hasWaited = true;
                logCount = 0;
            }
            Bot.Sleep(1000);

            if (playersWhoHaveBeenHere.Count() == (partySize - 1))
                butlerTimer++;
            if (butlerTimer >= 30)
            {
                b_breakOnMap = Bot.Map.Name;
                string toFollow = players.First(p => !playersWhoHaveBeenHere.Any(n => n.ToLower() == p.ToLower().Trim()));
                Core.Logger($"Missing {toFollow}, initiating Butler.cs");
                Core.Logger("Butler active until in map /" + b_breakOnMap);
                Butler(toFollow, roomNr: getRoomNr());
                Core.Logger($"{toFollow} has joined {b_breakOnMap}. Continueing");
                break;
            }
        }
        if (hasWaited)
            Core.Logger($"Party complete [{partySize}/{partySize}]");
        Bot.Sleep(3500); //To make sure everyone attack at the same time, to avoid deaths
    }

    public bool SellToSync(string item, int quant)
    {
        if (Core.CheckInventory(item, quant))
            return true;
        if (SellToSyncOn)
            Core.SellItem(item, 0, true);
        return false;
    }
    public bool SellToSyncOn = false;
    #endregion
    #region OneClient

    public bool doForAll(bool randomServers = true)
    {
        if (Bot.ShouldExit)
            return false;

        doForAllAccountDetails = doForAllAccountDetails ?? fileSetup();
        if (_doForAllIndex >= doForAllAccountDetails.Length)
            return false;

        Bot.Options.AutoRelogin = false;
        string name = doForAllAccountDetails[_doForAllIndex++];
        string pass = doForAllAccountDetails[_doForAllIndex++];

        if (Core.Username() != name)
        {
            if (Bot.Player.LoggedIn)
            {
                Bot.Servers.Logout();
                Bot.Sleep(Core.ActionDelay);
            }
            Bot.Servers.Login(name, pass);
            Bot.Sleep(3000);
            Bot.Servers.Connect(
                randomServers ?
                Bot.Servers.CachedServers.Where(x => !BlacklistedServers.Contains(x.Name.ToLower())).ToArray()[Bot.Random.Next(0, 8)] :
                Bot.Servers.CachedServers.First(x => x.Name == Bot.Options.ReloginServer));
            Bot.Wait.ForMapLoad("battleon");
            while (!Bot.Player.Loaded) { }
        }
        else Core.Join("battleon-999999");

        Core.ReadCBO();

        return true;

        string[] fileSetup()
        {
            string path = Path.Combine(CoreBots.OptionsPath, "TheFamily.txt");
            if (File.Exists(path))
                return File.ReadAllLines(path);

            Bot.ShowMessageBox("Your login details will be saved locally on your own device. We will not receive them.", "A heads up");

            int i = 1;
            string title = $"Please provide the login details for account #";
            string data = string.Empty;
            Dictionary<string, string> redo = new();

            while (!Bot.ShouldExit)
            {
                bool goRedo = redo.Count() != 0;

                var name = new InputDialogViewModel(title + i, "Account Name", false);
                if (goRedo)
                    name.DialogTextInput = redo.First().Key;
                if (isInvalid(name))
                    break;

                var pass = new InputDialogViewModel(title + i, "Account Password:", false);
                if (goRedo)
                    pass.DialogTextInput = redo.First().Value;
                if (isInvalid(pass))
                    break;

                var res = Bot.ShowMessageBox(
                    "Is this correct?\n\n" +
                    "Name:\t\t" + name.DialogTextInput + "\n" +
                    "Password:\t" + pass.DialogTextInput,
                    "Confirm that these are correct",
                    $"Yes, go to account #{i + 1}", "Yes, I am now done", "No"
                );

                redo = new();
                if (res.Text == "No")
                    redo.Add(name.DialogTextInput, pass.DialogTextInput);
                else
                {
                    data += $"{name.DialogTextInput}\n{pass.DialogTextInput}\n";
                    if (!res.Text.StartsWith("Yes, go"))
                        break;
                    i++;
                }
            }

            if (String.IsNullOrEmpty(data))
                Core.Logger("No input provided, stopping the bot.", messageBox: true, stopBot: true);

            File.WriteAllText(path, data[..^1]);
            Bot.Handlers.RegisterOnce(1, Bot => Bot.ShowMessageBox($"If you ever wish to edit things, the file can be found at:\n{CoreBots.SkuaPath + "/" + path}", "File path"));
            return data[..^1].Split('\n');

            bool isInvalid(InputDialogViewModel input) =>
                Ioc.Default.GetRequiredService<IDialogService>().ShowDialog(input) != true ||
                String.IsNullOrEmpty(input.DialogTextInput) ||
                String.IsNullOrWhiteSpace(input.DialogTextInput);
        }
    }
    private int _doForAllIndex = 0;
    public string[]? doForAllAccountDetails;
    private string[] BlacklistedServers =
    {
        "artix",
        "sir ver",
        "yorumi",
        "gravelyn",
        "galanoth",
        "class test realm"
    };
    #endregion

    public void Butler(string playerName, bool LockedMaps = true, ClassType classType = ClassType.Farm, bool CopyWalk = false, int roomNr = 1, bool rejectDrops = true, string? attackPriority = null)
    {
        // Double checking the playername and assigning it so all functions can read it
        if (playerName == "Insert Name" || String.IsNullOrEmpty(playerName))
            Core.Logger("No name was inserted, stopping the bot.", messageBox: true, stopBot: true);
        playerName = playerName.Trim().ToLower();
        this.b_playerName = playerName;

        // Assigning params to private objects.
        b_doLockedMaps = LockedMaps;
        b_doCopyWalk = CopyWalk;

        if (!String.IsNullOrEmpty(attackPriority))
            _attackPriority.AddRange(attackPriority.Split(',', StringSplitOptions.TrimEntries));

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
            8107    // downbelow
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

        while (!Bot.ShouldExit)
        {
            // Try to go to the followed player
            if (!tryGoto(playerName))
            {
                // Do these things if that fails
                Core.Join("whitemap");
                Core.Logger($"Could not find {playerName}. Check if \"{playerName}\" is in the same server with you.", "tryGoto");
                Core.Logger($"The bot will now hibernate and try to /goto to {playerName} every 60 seconds.", "tryGoto");

                int min = 1;
                while (!Bot.ShouldExit)
                {
                    // Wait 60 seconds
                    for (int t = 0; t < 60; t++)
                    {
                        Bot.Sleep(1000);
                        if (Bot.ShouldExit)
                            break;
                    }

                    // Try again
                    if (tryGoto(playerName))
                    {
                        Core.Logger(playerName + " found!");
                        break;
                    }
                    min++;

                    // Log every 5 minutes
                    if (min % 5 == 0)
                        Core.Logger($"The bot has been hibernating for {min} minutes");
                }
            }
            if (b_breakOnMap != null && b_breakOnMap == Bot.Map.Name)
            {
                b_breakOnMap = null;
                return;
            }

            // Attack any monster that is alive.
            if (!Bot.Combat.StopAttacking && Bot.Monsters.CurrentMonsters.Count(m => m.Alive) > 0)
                PriorityAttack("*");
            Core.Rest();
            Bot.Sleep(Core.ActionDelay);
        }
    }
    private string? b_playerName = null;
    private bool b_doLockedMaps = true;
    private bool b_doCopyWalk = false;
    private List<string> _attackPriority = new();

    private bool tryGoto(string userName)
    {
        // If you're in the same map and same cell, don't do anything
        if (Bot.Map.PlayerExists(userName) && Bot.Map.TryGetPlayer(userName, out PlayerInfo? playerObject) && playerObject != null && playerObject.Cell == Bot.Player.Cell)
            return true;

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
            Bot.Sleep(1000);

            if (LockedZoneWarning)
                break;

            if (Bot.Map.PlayerExists(userName))
            {
                if (Bot.Map.TryGetPlayer(userName, out playerObject) && playerObject != null && playerObject.Cell == Bot.Player.Cell)
                    Bot.Player.SetSpawnPoint();
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
            "ultradarkon"
        };
        string[] MemMaps =
        {
            "shadowlordpast",
            "binky",
            "superlowe"
        };

        int maptry = 1;
        int mapCount = Core.IsMember ? (NonMemMaps.Count() + MemMaps.Count()) : NonMemMaps.Count();

        foreach (string map in NonMemMaps)
        {
            Core.Logger($"[{(maptry.ToString().Length == 1 ? "0" : "")}{maptry++}/{mapCount}] Searching for {b_playerName} in /{map}", "LockedZoneHandler");
            Core.Join(map);

            if (!Bot.Map.PlayerExists(b_playerName!))
                continue;

            tryGoto(b_playerName!);
            Core.Logger($"[{((maptry - 1).ToString().Length == 1 ? "0" : "")}{maptry - 1}/{mapCount}] Found {b_playerName} in /{map}", "LockedZoneHandler");

            switch (map.ToLower())
            {
                case "doomvault":
                    _killTheUltra("r26");
                    break;

                case "doomvaultb":
                    _killTheUltra("r5");
                    break;
            }
            PriorityAttack("*");
            return;
        }

        if (Core.IsMember)
        {
            foreach (string map in MemMaps)
            {
                Core.Logger($"[{(maptry.ToString().Length == 1 ? "0" : "")}{maptry++}/{mapCount}] Searching for {b_playerName} in /{map}", "LockedZoneHandler");
                Core.Join(map);

                if (!Bot.Map.PlayerExists(b_playerName!))
                    continue;

                tryGoto(b_playerName!);
                Core.Logger($"[{((maptry - 1).ToString().Length == 1 ? "0" : "")}{maptry - 1}/{mapCount}] Found {b_playerName} in /{map}", "LockedZoneHandler");

                switch (map.ToLower())
                {
                    case "binky":
                        _killTheUltra("binky");
                        break;
                }
                PriorityAttack("*");
                return;
            }
        }

        insideLockedMaps = true;
        if (tryGoto(b_playerName!))
        {
            insideLockedMaps = false;
            return;
        }
        insideLockedMaps = false;

        Core.Join("whitemap");
        Core.Logger($"Could not find {b_playerName} in any of the maps within the LockedZoneHandler.", "LockedZoneHandler");
        Core.Logger($"The bot will now hibernate and try to /goto to {b_playerName} every 60 seconds", "LockedZoneHandler");

        int min = 1;
        while (!Bot.ShouldExit)
        {
            for (int t = 0; t < 60; t++)
            {
                Bot.Sleep(1000);
                if (Bot.ShouldExit)
                    break;
            }
            if (tryGoto(b_playerName!))
            {
                Core.Logger(b_playerName + " found!");
                return;
            }
            min++;

            if (min % 5 == 0)
                Core.Logger($"The bot is has been hibernating for {min} minutes");
        }
        return;

        void _killTheUltra(string cell)
        {
            if (Bot.Player.Cell == cell && Bot.Monsters.CurrentMonsters.Count(m => m.Alive) > 0)
            {
                Monster? Target = Bot.Monsters.CurrentMonsters.MaxBy(x => x.MaxHP);
                if (Target == null)
                {
                    Core.Logger("No monsters found", "KillUltra");
                    return;
                }
                PriorityAttack(Target.Name);
            }
        }
    }

    private void PriorityAttack(string attNoPrio)
    {
        if (_attackPriority.Count() == 0)
        {
            Bot.Combat.Attack(attNoPrio);
            return;
        }

        foreach (string mon in _attackPriority)
        {
            var _mon = Bot.Monsters.CurrentMonsters.Find(m => m.Name.Trim().ToLower() == mon.ToLower() && m.Alive);
            if (_mon != null)
            {
                Bot.Combat.Attack(_mon);
                return;
            }
        }
        Bot.Combat.Attack(attNoPrio);
    }

    private async void MapNumberParses(string map)
    {
        // Wait untill the full name I.E. "battleon-12345" is set
        if (String.IsNullOrEmpty(Bot.Map.FullName))
        {
            for (int a = 0; a < 10; a++)
            {
                if (!String.IsNullOrEmpty(Bot.Map.FullName))
                    break;
                await Task.Delay(Core.ActionDelay);
                if (a == 9)
                    return;
            }
        }

        if (!Int32.TryParse(Bot.Map.FullName.Split('-').Last(), out int mapNr) || map == b_prevRoom || !Bot.Map.PlayerExists(b_playerName!))
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
        // Removing listeners
        Bot.Events.MapChanged -= MapNumberParses;
        Bot.Events.ExtensionPacketReceived -= LockedZoneListener;
        Bot.Events.ExtensionPacketReceived -= CopyWalkListener;

        // Delete communication files
        if (File.Exists(commFile()))
            File.Delete(commFile());

        return true;
    }

    private string commFile() => Path.Combine(CoreBots.ButlerLogDir, $"{Core.Username().ToLower()}~!{b_playerName}.txt");
    public string? b_breakOnMap = null;
}
