/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Options; //< ----Must be here

public class OptionsExample
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public string OptionsStorage = "OptionsExample"; //<--rename this
    public bool DontPreconfigure = true; //<- Leave this alone.
    public List<IOption> Options = new()
    {
        CoreBots.Instance.SkipOptions,
        //when "Skip Options" is checked it wont show the options window next time you run the script.
        new Option<string>("StringName", "Display Name(beside fill-in)", "Description goes here", "Leave this Empty to be filled in, or set a Default to be overridden"),
        //"StringName" is the part used in the script.
        //"Displayname" is what will appear beside the fillin box in options
        //the last part should be left blank to be fill in by the player (if you've used any army script recently, its basicly like where you'd put the playername in.
        
        new Option<bool>("BoolNameGoesHere", "Display Name",  "Yes(True) / No(False)", false),
        //"BoolNameGoesHere" is the part used in the script
        //Yes(True) / No(False) > obvious
        //leave false by default or true.. doesnt matter its upto the user
//
        new Option<ExampleName>("ExampleName", "What it will Say beside the Selection", "This is The Description", ExampleName.Item_1),
        //the 1st "ExampleName" is the Enum Name Below Copied Exactly.
        //the 2nd "ExampleName" is the part we'll be using in the script. it can be named anything but keep it *very* short.
        //ExampleName.thing1 is the Default option for when starting the script, until changed by the user.
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Example();

        Core.SetOptions(false);
    }

    public void Example()
    {
        //Examples on how to use the options:

        //Sting if what was filled in matches the {== "this"}:
        if (Bot.Config?.Get<string>("StringName") == "Insert what ever here its \"supposed to be\".")
        {
            //Do a thing
        }

        //Bool if true
        if (Bot.Config!.Get<bool>("BoolNameGoesHere"))
        {
            //Do a thing if true
        }

        //Enum
        //the (int) is the ItemID below but the bot knows it so no need to fill it in on your part
        if (!Core.CheckInventory((int)Bot.Config.Get<ExampleName>("ExampleName")))
        {
            //if player doesnt have thing in Enum
        }
    }


    private enum ExampleName
    {
        //_ are used as spaces
        //Itemids are used as ' arent allowed.

        Item_1 = 0001,
        Item_2 = 0002,
        Item_3 = 0003
    };
}



/*
using Skua.Core.Options; <---- this goes at the top under the includes

    public string OptionsStorage = "FarmerJoePet";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        CoreBots.Instance.SkipOptions,
        new Option<bool>("BoolName", "Get a Pre-Made Outfit, Curtious of the Community", "We are farmers, bum ba dum bum bum bum bum", false),
        new Option<string>("Itemname", "Equip outfit at the end?", "Yay or Nay", false),
        new Option<PetChoice>("PetChoice", "Choose Your Pet", "Extra stuff to choose, if you have any suggestions -form in disc, and put it under request. or dm Tato(the retarded one on disc)", PetChoice.None),
    };

    explaination of the 
*/