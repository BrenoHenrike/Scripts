//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Monsters;
using Skua.Core.Options;

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

    public void AggroMonStart(string map)
    {
        if (aggroCTS is not null)
            AggroMonStop();

        Core.Join(map);

        List<string> _AggroMonCells = this._AggroMonCells;
        List<string> _AggroMonNames = this._AggroMonNames;
        List<int> _AggroMonIDs = this._AggroMonIDs;
        List<int> AggroMonMapIDs = new(); //MMIDs = Monster Map IDs

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
    /// Stops/Pauses the Aggro Mon Task.
    /// </summary>
    public void AggroMonStop(bool clear = false)
    {
        aggroCTS?.Cancel();
        if (clear)
            AggroMonClear();
        Bot.Wait.ForTrue(() => aggroCTS == null, 30);
    }

    public void AggroMonCells(params string[] cells)
        => _AggroMonCells = cells.ToList();
    public void AggroMonNames(params string[] names)
        => _AggroMonNames = names.ToList();
    public void AggroMonIDs(params int[] IDs)
        => _AggroMonIDs = IDs.ToList();
    private List<string> _AggroMonCells = new();
    private List<string> _AggroMonNames = new();
    private List<int> _AggroMonIDs = new();

    public void AggroMonClear()
    {
        _AggroMonCells.Clear();
        _AggroMonNames.Clear();
        _AggroMonIDs.Clear();
    }

    public string AggroMonPacket(params int[] MonsterMapIDs)
        => $"%xt%zm%aggroMon%{Bot.Map.RoomID}%{String.Join('%', MonsterMapIDs)}%";

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
        return members == null ? null : members.Concat(new[] { Bot.Player.Username.ToLower() }).ToArray();
    }

    public string? getPartyLeader()
        => Bot.Flash.GetGameObject<string>("world.partyOwner");


    public bool isPartyLeader()
        => Bot.Player.Username.ToLower() == (getPartyLeader() ?? String.Empty).ToLower();

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

    //WIP
    //public string AvailableCell(params string[] cells)
    //{
    //    List<string> availableCells = cells.ToList() ?? Bot.Map.Cells;

    //    List<string> cellsWithPlayers = (Bot.Map.Players ?? new()).Select(p => p.Cell).ToList();
    //    return availableCells.First(c => !Bot.Map.Players.Any(p => p.Cell == c && Bot.Monsters.MapMonsters.Where(m => m.Cell == c).Count() > 0)) ?? "Enter";
    //}

    /// <summary>
    /// Spreads players around the input cells, if no cells are set - will spread with any cell that has a monster in it.
    /// </summary>
    public void DivideOnCells(params string[] cells)
    {
        // Parsing all the player names from an unspecified amount of player name options
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

        //Deviding the players amongst the cells
        int cellCount = 0;
        string username = Bot.Player.Username.ToLower();
        foreach (string p in players)
        {
            if (username == p)
                Core.Jump(cells[cellCount]);
            cellCount = cellCount == cells.Count() - 1 ? 0 : cellCount + 1;
        }
    }
}
