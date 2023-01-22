/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class GardenMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreDarkon Darkon = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("garden", 1831, findIngredients, buyOnlyThis, buyMode: buyMode);

        #region Dont edit this part
        void findIngredients()
        {
            ItemBase req = Adv.externalItem;
            int quant = Adv.externalQuant;
            int currentQuant = req.Temp ? Bot.TempInv.GetQuantity(req.Name) : Bot.Inventory.GetQuantity(req.Name);
            if (req == null)
            {
                Core.Logger("req is NULL");
                return;
            }

            switch (req.Name)
            {
                default:
                    bool shouldStop = Adv.matsOnly ? !dontStopMissingIng : true;
                    Core.Logger($"The bot hasn't been taught how to get {req.Name}." + (shouldStop ? " Please report the issue." : " Skipping"), messageBox: shouldStop, stopBot: shouldStop);
                    break;
                #endregion

                // Add how to get items here
                case "Darkon's Receipt":
                    Darkon.FarmReceipt(quant);
                    break;

                case "Debris Fragment":
                    Core.EquipClass(ClassType.Farm);
                    Core.KillMonster("garden", "r2", "Left", "*", req.Name, quant, isTemp: false);
                    break;

                case "Darkon's Debris 1952":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.AddDrop("Darkon's Receipt");

                    bool EnoughPeople = false;
                    Core.Join("doomvault", "r5", "Left", true);

                    Core.RegisterQuests(7325);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name))
                    {
                        if (Bot.Map.Name.ToLower() == "doomvault")
                        {
                            while (!Bot.ShouldExit && Bot.Player.Cell != "r5")
                            {
                                Core.Jump("r5", "Left");
                                Bot.Sleep(Core.ActionDelay);
                            }
                            if (Bot.Map.CellPlayers.Count >= 3)
                                EnoughPeople = true;
                            else EnoughPeople = false;
                        }

                        if (!EnoughPeople && Core.IsMember)
                            Core.HuntMonster("ultravoid", "Ultra Kathool", "Ingredients?", 22, false, publicRoom: true, log: false);
                        else Adv.KillUltra("doomvault", "r5", "Left", "Binky", "Ingredients?", 22, false, publicRoom: true, log: false);

                        Bot.Wait.ForPickup("Darkon's Receipt");
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Darkon's Debris 1935.1":
                case "Darkon's Debris 66 Angel Wing":
                case "Darkon's Debris 66 Fallen Wing":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("garden", "Creature 12", req.Name, isTemp: false);
                    break;

                case "Fa's Gamer Fuel":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("garden", "Fa", req.Name, quant, isTemp: false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("52970", "Darkon's Debris 92.1", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 92.1\" ?", false),
        new Option<bool>("52971", "Darkon's Debris 92.2", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 92.2\" ?", false),
        new Option<bool>("52972", "Darkon's Debris 92.3", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 92.3\" ?", false),
        new Option<bool>("52993", "Darkon's Debris 2020", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 2020\" ?", false),
        new Option<bool>("52973", "Darkon's Debris 92 Locks", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 92 Locks\" ?", false),
        new Option<bool>("52974", "Darkon's Debris 92 Hair", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 92 Hair\" ?", false),
        new Option<bool>("53001", "Darkon's Debris 2020 Pet", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 2020 Pet\" ?", false),
        new Option<bool>("54502", "Darkon's Debris 66 Fallen Wings", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 66 Fallen Wings\" ?", false),
        new Option<bool>("54493", "Darkon's Debris 66 Halo Bang", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 66 Halo Bang\" ?", false),
        new Option<bool>("54497", "Darkon's Debris 66 Horns Bang", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 66 Horns Bang\" ?", false),
        new Option<bool>("54495", "Darkon's Debris 66 Halo Horns Bang", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 66 Halo Horns Bang\" ?", false),
        new Option<bool>("54494", "Darkon's Debris 66 Halo Cut", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 66 Halo Cut\" ?", false),
        new Option<bool>("54498", "Darkon's Debris 66 Horns Cut", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 66 Horns Cut\" ?", false),
        new Option<bool>("54496", "Darkon's Debris 66 Halo Horns Cut", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 66 Halo Horns Cut\" ?", false),
        new Option<bool>("54490", "Darkon's Debris 66.1", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 66.1\" ?", false),
        new Option<bool>("54491", "Darkon's Debris 66.2", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 66.2\" ?", false),
        new Option<bool>("54492", "Darkon's Debris 66.3", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 66.3\" ?", false),
        new Option<bool>("54504", "Fa's Attire", "Mode: [select] only\nShould the bot buy \"Fa's Attire\" ?", false),
        new Option<bool>("54507", "Fa's Horn", "Mode: [select] only\nShould the bot buy \"Fa's Horn\" ?", false),
        new Option<bool>("54517", "Darkon's Debris 4.1", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 4.1\" ?", false),
        new Option<bool>("54518", "Darkon's Debris 4.2", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 4.2\" ?", false),
        new Option<bool>("54510", "Darkon's Debris 13", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 13\" ?", false),
        new Option<bool>("54512", "Darkon's Debris 508", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 508\" ?", false),
        new Option<bool>("54513", "Darkon's Debris 508 Dual", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 508 Dual\" ?", false),
        new Option<bool>("54514", "Darkon's Debris 1935 Stick", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 1935 Stick\" ?", false),
        new Option<bool>("54519", "Darkon's Debris 1952 Extra", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 1952 Extra\" ?", false),
        new Option<bool>("54543", "Creature 83 Pet", "Mode: [select] only\nShould the bot buy \"Creature 83 Pet\" ?", false),
        new Option<bool>("54500", "Darkon's Debris 66 Angel Wings", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 66 Angel Wings\" ?", false),
        new Option<bool>("54547", "Summit Snack Combo", "Mode: [select] only\nShould the bot buy \"Summit Snack Combo\" ?", false),
        new Option<bool>("54503", "Darkon's Debris 66 Duality Wings", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 66 Duality Wings\" ?", false),
        new Option<bool>("69757", "Astravian Adept", "Mode: [select] only\nShould the bot buy \"Astravian Adept\" ?", false),
        new Option<bool>("69758", "Astravian Adept Helm", "Mode: [select] only\nShould the bot buy \"Astravian Adept Helm\" ?", false),
        new Option<bool>("69759", "Astravian Adept Plumed Helm", "Mode: [select] only\nShould the bot buy \"Astravian Adept Plumed Helm\" ?", false),
        new Option<bool>("69760", "Astravian Adept Sheaths", "Mode: [select] only\nShould the bot buy \"Astravian Adept Sheaths\" ?", false),
        new Option<bool>("69761", "Astravian Adept Szabla", "Mode: [select] only\nShould the bot buy \"Astravian Adept Szabla\" ?", false),
        new Option<bool>("69762", "Astravian Adept Szablas", "Mode: [select] only\nShould the bot buy \"Astravian Adept Szablas\" ?", false),
        new Option<bool>("69763", "Astravian Shadow Adept", "Mode: [select] only\nShould the bot buy \"Astravian Shadow Adept\" ?", false),
        new Option<bool>("69764", "Shadow Adept Helm", "Mode: [select] only\nShould the bot buy \"Shadow Adept Helm\" ?", false),
        new Option<bool>("69765", "Shadow Adept Plumed Helm", "Mode: [select] only\nShould the bot buy \"Shadow Adept Plumed Helm\" ?", false),
        new Option<bool>("69770", "Astravian Sheathed Szabla", "Mode: [select] only\nShould the bot buy \"Astravian Sheathed Szabla\" ?", false),
        new Option<bool>("69878", "Astravian Sheathed Szablas", "Mode: [select] only\nShould the bot buy \"Astravian Sheathed Szablas\" ?", false),
        new Option<bool>("69838", "Fa's VR Headset", "Mode: [select] only\nShould the bot buy \"Fa's VR Headset\" ?", false),
        new Option<bool>("69839", "Fa's Gauntlet", "Mode: [select] only\nShould the bot buy \"Fa's Gauntlet\" ?", false),
        new Option<bool>("69840", "Fa's Gauntlets", "Mode: [select] only\nShould the bot buy \"Fa's Gauntlets\" ?", false),
    };
}
