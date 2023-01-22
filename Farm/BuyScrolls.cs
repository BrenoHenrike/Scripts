/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;
using Skua.Core.Models.Quests;

public class BuyScrolls
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public string OptionsStorage = "BuyScrolls";
    public List<IOption> Options = new List<IOption>()
    {
        new Option<bool>("UseMysticParchment", "Use Mystic Parchment", "Use Mystic Parchment instead of gold To Buy Ink", false),
        new Option<Scrolls>("scrollSelect", "Scroll of", "Select the scroll of your choise"),
        new Option<int>("scrollAmount", "How many", "Write -1 to buy up to max. stack", -1),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BuyScroll(Bot.Config.Get<Scrolls>("scrollSelect"), Bot.Config.Get<int>("scrollAmount"));

        Core.SetOptions(false);
    }

    public void BuyScroll(Scrolls scroll, int quant = -1)
    {
        Quest questData = Core.EnsureLoad((int)scroll);
        string _scroll = questData.Rewards.First().Name;
        quant = quant == -1 ? questData.Rewards.First().MaxStack : quant;

        if (Core.CheckInventory(_scroll, quant))
            return;

        string ink = questData.Requirements.First().Name;

        Core.Logger($"Getting you {quant}x {_scroll}");
        Core.AddDrop(_scroll);

        Farm.SpellCraftingREP(5);

        if (!Bot.Config.Get<bool>("UseMysticParchment"))
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(_scroll, quant))
            {
                if (!Core.CheckInventory(ink))
                {
                    if (!Core.CheckInventory("Arcane Quill"))
                    {
                        if (!Core.CheckInventory("Gold Voucher 500k"))
                            Farm.Gold(500000);
                        Core.BuyItem("spellcraft", 693, "Gold Voucher 500k", 2);
                        Core.BuyItem("spellcraft", 693, "Arcane Quill", 10, shopItemID: 8847);
                    }
                    Core.BuyItem("spellcraft", 622, ink, 5);
                }
                Core.EnsureAccept((int)scroll);
                Core.EnsureCompleteMulti((int)scroll, Bot.Inventory.GetQuantity(_scroll) - ((int)Math.Ceiling((float)quant / (float)questData.Rewards.First().Quantity)));
                Bot.Wait.ForPickup(_scroll);
                Bot.Sleep(500);
                Core.Logger($"You now own {Bot.Inventory.GetQuantity(_scroll)} of the requested {quant} {_scroll}'s");
            }
            Core.Logger($"Buying complete, you now own {quant} {_scroll}'s");
        }
        else
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(_scroll, quant))
            {
                if (!Core.CheckInventory(ink))
                {
                    Core.HuntMonster("tercessuinotlim", "Dark makai", "Mystic Parchment", ((quant / 5) / 2) - (Bot.Inventory.GetQuantity("Mystic Parchment")), isTemp: false);
                    Core.BuyItem("spellcraft", 549, ink);
                }
                Core.EnsureAccept((int)scroll);
                Core.EnsureCompleteMulti((int)scroll, Bot.Inventory.GetQuantity(_scroll) - ((int)Math.Ceiling((float)quant / (float)questData.Rewards.First().Quantity)));
                Bot.Wait.ForPickup(_scroll);
                Core.Logger($"You now own {Bot.Inventory.GetQuantity(_scroll)} of the requested {quant} {_scroll}'s");
            }
            Core.Logger($"Buying complete, you now own {quant} {_scroll}'s");
        }
    }
}

public enum Scrolls
{
    Fireball = 2295,
    Shadowburn = 2296,
    PlasmaBolt = 2297,
    DarkEnergy = 2298,
    SsikarisBreath = 2299,
    ShadowBolt = 2300,
    DiamondCage = 2301,
    Exorcise = 2302,
    AcidRain = 2303,
    HeartBeat = 2304,
    Corrosion = 2305,
    CrushingWave = 2306,
    WindStrike = 2307,
    ArcLightning = 2308,
    SpiritRend = 2309,
    DarkArc = 2310,
    Chains = 2311,
    Eclipse = 2312,
    Purge = 2313,
    ScorchedSteel = 2314,
    FireBolt = 2315,
    HolyBolt = 2316,
    BlessedShard = 2317,
    Frostbite = 2318,
    Geyser = 2319,
    FireFlare = 2320,
    FrostFlare = 2321,
    PlagueFlare = 2322,
    ChargedFlare = 2323,
    DoomFlare = 2324,
    HolyFlare = 2325,
    BlindingLight = 2326,
    GuardianBlast = 2327,
    ShadowBlade = 2328,
    FuriousGale = 2329,
    Enrage = 2330,
    Decay = 2331,
    DeathPact = 2332,
    CantorsLament = 2333,
    PulseCompression = 2334,
    Dissonance = 2335,
    SoulCrush = 2336,
    VoidStrike = 2337,
    PsychicWave = 2338,
    ChaosFog = 2339,
    Torment = 2340,
    ShiftBurn = 2341,
    FreezingFlame = 2342,
    FireStorm = 2343,
    Mystify = 2344,
    Wither = 2345,
    Underworld = 2346,
    EtherealSlumber = 2347,
    EtherealCurse = 2348,
    DarkGrip = 2349,
    Weaken = 2350,
    TalonTwisting = 2351,
    Petrify = 2352,
    Cripple = 2353,
}
