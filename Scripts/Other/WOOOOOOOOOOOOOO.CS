//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs

//cs_include Scripts/CoreFile(Or folder)/Filename.cs

using RBot;

public class YeeaaahhBabbyyThaaatsWhatImTalkingAbout
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDailys Dailys = new CoreDailys();
    new string[] WOOOOOdrops = 
    {
    //Quest Rewards;        
    "Cr1tikal Meme Poster",
    "2D Moistverse House",
    "GIANT Mushroom Guard (R)",
    "GIANT Mushroom Guard (L)",
    "Major Mushroom Guard (L)",
    "Major Mushroom Guard (R)",
    
    //Drop Rewards;
    "ARTX 3090 Controller", 
    "ARTX Dome",
    "ARTX Shelf",
    "ARTX Twilly Pet",
    "CCCG Set",
    "Cr1tikal Hair",
    "Cr1tikal Hair + Beard",
    "Cr1tikal Hat",
    "Cr1tikal Hat + Beard",
    "Cr1tikal Hero",
    "Major Mushroom Pet",
    "Moist Water Bottle",
    "Treasure Chest" 
    };

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        WOOOOOOOOOOOOOOO();

        Core.SetOptions(false);
    }

    public void WOOOOOOOOOOOOOOO()
    {
        Core.AddDrop(WOOOOOdrops)
        
        Core.EnsureAccept(8497);
        Core.HuntMonster("Mverse", "Major Mushroom", new[] { "Dark Caster Card", "Green-Eyes Red Dragon Card", "Mirror Realm's Force Card", "Drakath The Outlawed One Card" }, 1, true);
        Core.EnsureComplete(8497);
    }
}

"Cr1tikal Meme Poster",
"2D Moistverse House",
"GIANT Mushroom Guard (R)",
"GIANT Mushroom Guard (L)",
"Major Mushroom Guard (L)",
"Major Mushroom Guard (R)",