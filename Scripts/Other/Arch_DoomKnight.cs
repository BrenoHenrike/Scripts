using System;
using RBot;
using System.Collections.Generic;
using RBot.Servers;
using System.Windows.Forms;
using System.Linq;

public class ArchDoomKnight //by Tato
{
	//--------------------------------------------------------------------------------------------------------------\\
			//If QuestID: 567 is not unlocked(and you got the prompt) Read & Follow the following; Arrow symbols ← ↑ → 
	
			//brenos scripts;
			// https://github.com/BrenoHenrike/Rbot-Scripts/releases  
			//  ↑ Hold Cntrl+click & download the latest
			//  ScriptsV(ersion)# that is  out

			//How to Install his scripts (IF U DONT FOLLOW THE INSTRUCTIONS THEY WILL NOT WORK)
			// https://github.com/BrenoHenrike/Rbot-Scripts#faq  <-- Cntrl+click(if on Vscode)
			//  ↑ Hold Cntrl+click & Read
			//  The Faq "how to install"

			//Once setup correctly, goto Rbot-3.6.#> Scritps > LordsofChaos > 5Wolfwing(Darkovia) and run that 
			// restart this bot when complete. \\
	//--------------------------------------------------------------------------------------------------------------\\

	//-----------EDIT BELOW-------------//
	public int MapNumber = 2142069;	
	
	public int ScriptDelay = 1000;
	public readonly int[] SkillOrderVHL = { 4, 2, 1 };
	public readonly int[] SkillOrderLycan = { 4, 3, 2, 1, 2, 1};

    public readonly int[] SkillOrderLR = { 3, 1, 2, 4 };
    public readonly int[] SkillOrderVPL = { 3, 1, 2, 4 };
    public readonly int[] SkillOrderMage = { 3, 1, 2, 4 };
	public readonly int[] SkillOrderHealer = { 2, 1, 3, 4 };
	public static readonly int[] SkillOrderXiangClass1 = { 2, 1, 3, 4 }; // Healer class
	public static readonly int[] SkillOrderXiangClass2 = { 2, 3, 2, 1, 2, 4 }; //DOT class
	private string SoloClass = "Lycan"; //<-----Edit to your preference - SafeEquip(SoloClass);
	private string SoloClass2 = "Void Highlord"; //<-----Edit to your preference - SafeEquip(SoloClass);
	private string FarmClass = "Mage"; // <---- for starter accs?
    private string FarmClass2 = "Vampire Lord"; //<-----Edit to your preference - SafeEquip(FarmClass);
	private string FarmClass3 = "Legion Revenant"; // <----  IF you own it?
	private string XiangClass1 = "Healer";
	private string XiangClass2 = "Dragon of Time";
	public int SaveStateLoops = 8700;
	public int TurnInAttempts = 10;
	public static string[] RequiredItems = { 
		"Escherion's Helm",
		"Legendary Sword of Dragon Control",
		"Hanzamune Dragon Koi Blade",
		"Wolfwing Armor",
		"One Eyed Doll Breaker",
		"Ledgermayne",
		"Tibicenas",
		"Soul of Chaos Armor",
		"Chaos Lionfang Armor",
		"Shorn Chaos King Crown",
		"Xiang Chaos",
		"Drakath's Sword",
		"Chaorrupted Hourglass",
		"Chaotic Power" 
		};
	public static string[] MaxSetEquip = { "Necrotic Sword of Doom", "Dual Necrotic Swords of Doom", "Polly Roger", "Head of the Legion Beast", "Fire Champion's Armor", "Awescended Omni Wings"  };
	public static string[] A_Loyal_FollowerItems = { 
		//A loyal follower QuestLine Items
		//----------
		//reforging;
		"Blinding Light of Destiny Handle",
		//secret order of undead slayers
		"Bonegrinder Medal",
		//essential essences
		"Undead Essence",
		"Spirit Orb",
		//bust some dust
		"Bone Dust",	
		//a loyal follower
		"Celestial Compass",
		"Loyal Spirit Orb"
	 };
	public static string[] Q1items = { 
		"Arch DoomKnight Cape",
		"Undead Energy",
		"Human Souls",
		"Dragon Energy" 
	};
	public static string[] Q2items = { 
		"Arch DoomKnight Cape Sword",		
		"Arch DoomKnight Polearm",
		"Death's Power", 
		"Souls of the Dead" 		
	};
	public static string[] Q3items = { 
		"Arch DoomKnight Sword",
		"Arch DoomKnight's Edge",
		"Escherion's Helm",
		"Legendary Sword of Dragon Control", 
		"Hanzamune Dragon Koi Blade", 
		"Wolfwing Armor",
		"One Eyed Doll Breaker",
		"Ledgermayne",
		"Tibicenas", 
		"Soul of Chaos Armor",
		"Chaos Lionfang Armor",
		"Shorn Chaos King Crown", 
		"Xiang Chaos",
		"Drakath's Sword",
		"Chaorrupted Hourglass",
		"Chaotic Power", 
	};
	public static string[] Q4items = { 
		"Arch DoomKnight",
		"Arch DoomKnight Open Helm",
		"Arch DoomKnight Helm",
		"Ultimate Darkness Gem",
		"Undead Energy",
		"(Necro) Scroll of Dark Arts",
		"Doom Heart",
		"Dread Knight Cleaver",
		"Reaper's Soul",
		"Desolich's Undead Eye" 
	};
	public string[] Q1Rewards = { 
		"Arch DoomKnight Cape" 
		};
	public string[] Q2Rewards = { 
		"Arch DoomKnight Cape Sword",		
		"Arch DoomKnight Polearm" 
		};
	public string[] Q3Rewards = { 
		"Arch DoomKnight Sword",
		"Arch DoomKnight's Edge"
	};
	public string[] Q4Rewards = { 
		"Arch DoomKnight",
		"Arch DoomKnight Open Helm",
		"Arch DoomKnight Helm"
	 };

	public string[] Combined = Q1items.Concat(Q2items).Concat(Q3items).Concat(RequiredItems).Concat(Q4items).Concat(A_Loyal_FollowerItems).Concat(MaxSetEquip).ToArray();
	
	//-----------EDIT ABOVE-------------//



	public int FarmLoop;
	public int SavedState;
	public ScriptInterface bot => ScriptInterface.Instance;
	public void ScriptMain(ScriptInterface bot)
	{
		if (bot.Player.Cell != "Wait") bot.Player.Jump("Wait", "Spawn");

		ConfigureBotOptions();
		ConfigureLiteSettings();

		DeathHandler();
		//VersionCheck("3.6.2"); //..? idk what the next # breo will use wil lbe but meh
		VersionCheck("3.6.3.1"); 

		EquipList(MaxSetEquip);
		GetDropList(Combined);
		UnbankList(Combined);

		//Requirements for this bot; 
		//R7 Ebil
		//alot of inv slots

		while (!bot.ShouldExit())
		{
			UnbankList(Combined);
			if (!bot.Quests.IsUnlocked(567)) 
			{
				MessageBox.Show($"to Run Breno's '5th lord of chaos', please open 'Scripts', then press the 'edit script', Read & Do Line 10-21 (the if you try and run Breno's bots, and it isnt working correctly, MAKE SURE YOU SET IT UP RIGHT EXACTLY AS INSTRUCTED IN THE FAQ.)", "Questline Required!!");
				StopBot($"Do as the Box says");
			}
			else if (bot.Quests.IsUnlocked(567))
			{
					if (bot.Inventory.Contains("Lycan")) {PopTartsAreOk();}
				else if (!bot.Inventory.Contains("Lycan")) {LycanQuestRepPurchaseRankEnhance();}
			}
		}}


		public void PopTartsAreOk()
		{
			bot.Drops.Add("Undead Energy");
			while (!bot.Player.Loaded) { }
			bot.Log("EbilRep Check");
			if (bot.Player.GetFactionRank("Evil") < 7) EbilRep();
			bot.Log("Undead Energy Check");

			GetDropList(Combined);
			ClassSwapFarming();
			if (bot.Inventory.Contains("Undead Energy")) 
			{
			FormatLog("Undead Energy Check", "You Got It", Tabs: 1);
			LycanQuestRepPurchaseRankEnhance();
			}
			else if (!bot.Inventory.Contains("Undead Energy"))
			{	
				SafeMapJoin("battleunderb", "Enter", "Spawn");
				FormatLog("Undead Energy", "Checking their bones");
				bot.Player.Hunt("*");
				bot.Player.Hunt("*");
				bot.Player.Hunt("*");
				FormatLog("Undead Energy Check", "Part2");
				if (bot.Inventory.Contains("Undead Energy"))
				{
				FormatLog("Undead Energy Check", "You Got It");
				LycanQuestRepPurchaseRankEnhance();
				}
				else if (!bot.Inventory.Contains("Undead Energy")) 
				{
				FormatLog("Undead Energy Check", "You Don't Got It");		
				A_Loyal_Follower(); 
				}
			}
		}
		public void SetCheck()
		{ 
			FormatLog(Title: true, Text: "Arch DoomKnight Set Check Started");
			if ((!bot.Inventory.Contains("Arch DoomKnight Cape" ) && bot.Bank.Contains("Arch DoomKnight Cape" ))) Gathering_Power();
			else if ((!bot.Inventory.Contains("Arch DoomKnight Cape Sword" ) && bot.Bank.Contains("Arch DoomKnight Cape Sword" ))) Deaths_Door();
			else if ((!bot.Inventory.Contains("Arch DoomKnight Polearm" ) && bot.Bank.Contains("Arch DoomKnight Polearm" ))) Deaths_Door();
			else if ((!bot.Inventory.Contains("Arch DoomKnight Sword" ) && bot.Bank.Contains("Arch DoomKnight Sword" ))) Chaotic_Lords();
			else if ((!bot.Inventory.Contains("Arch DoomKnight's Edge" ) && bot.Bank.Contains("Arch DoomKnight's Edge" ))) Chaotic_Lords();
			else if ((!bot.Inventory.Contains( "Arch DoomKnight" ) && bot.Bank.Contains( "Arch DoomKnight" ))) A_Means_To_An_End();
			else if ((!bot.Inventory.Contains("Arch DoomKnight Open Helm" ) && bot.Bank.Contains( "Arch DoomKnight Open Helm" ))) A_Means_To_An_End();
			else if ((!bot.Inventory.Contains("Arch DoomKnight Helm" ) && !bot.Bank.Contains( "Arch DoomKnight Helm" ))) A_Means_To_An_End();
				bot.Options.AutoRelogin = false;
				bot.Options.LagKiller = false;
				bot.Options.AggroMonsters = false;
				MessageBox.Show("if i wrote this right.. you should own the set already :D", "CONGRATULATIONS");
				StopBot($"Set Complete.");
		}

		

	

		public void EbilRep()
		{
					//Evil
					A0:
					FormatLog(Title: true, Text: "Evil Rep + Prerequisets");
					ClassSwapFarming();					
					if (bot.Player.GetFactionRank("Evil") < 4){ goto C;}
					else goto D;
					
					A: // Bone-afide Check -- // Do Rattle Battle
					{
					FormatLog("Prerequisites[Rattle Battle]", "Doing Required Quests Part 2", Tabs: 1);
						ClassSwapFarming();
						ItemFarm(
						"Signed Contracts", 8,
						Temporary: true,
						HuntFor: true,
						QuestID: 365,
						MonsterName: "Rattlebones",
						MapName: "bludrut"
						);
						bot.Quests.EnsureComplete(365);
						goto EvilRep_Farm;
					}

					B: // Rattle Battle -- // Do Youthannized
					{
					FormatLog("Prerequisites[Youthannized]", "Doing Required Quests Part 1", Tabs: 1);
						ClassSwapFarming();
						ItemFarm(
						"Youthanize", 1,
						Temporary: true,
						HuntFor: true,
						QuestID: 364,
						MonsterName: "Slime",
						MapName: "swordhavenbridge"
						);
						bot.Quests.EnsureComplete(364);
						goto A0;
					}
						

					EvilRep_Farm:

					C: if (!bot.Quests.IsUnlocked(367)) {goto A;}
					C2:	FormatLog("Youthanize", "Rep Farm Part 1", Tabs: 2);
					while (bot.Player.GetFactionRank("Evil") < 4) //Youthanize - Do Youthanize
					{ 
						ClassSwapFarming();
						ItemFarm(
						"Youthanize", 1,
						Temporary: true,
						HuntFor: true,
						QuestID: 364,
						MonsterName: "Slime",
						MapName: "swordhavenbridge"
						);
						SafeQuestComplete(364);
					}
					goto D;

					D: if (!bot.Quests.IsUnlocked(365)) {goto B;}
					D2: FormatLog("Bone-afide", "Rep Farm Part 2", Tabs: 2);
					while (bot.Player.GetFactionRank("Evil") < 7) //Bone-afide - Do Bone-afide					
						{
						ClassSwapFarming();
						ItemFarm(
						"Replacement Tibia", 6,
						Temporary: true,
						HuntFor: true,
						QuestID: 367,
						MonsterName: "Skeletal Viking",
						MapName: "graveyard"
						);		

						ItemFarm(
						"Phalanges", 3,
						Temporary: true,
						HuntFor: true,
						QuestID: 367,
						MonsterName: "Skeletal Viking",
						MapName: "graveyard"
						);
						SafeQuestComplete(367);
						}
						PopTartsAreOk();
               
	}

	public void Gathering_Power() //gathering power 6795
	{
		
		CheckSpace(Q1items);
		GetDropList(Combined);
		ClassSwapFarming();
		bot.Sleep(1500);
		bot.Log("Gathering_Power start");
		bot.Log("Gathering Power Undead Energy");
		ItemFarm("Undead Energy", 1800, false, false, 6795, "Skeleton Warrior", "battleunderb");
		bot.Log("Gathering Power Human Souls");
		ItemFarm("Human Souls", 500, false, true, 6795, "Lightguard Caster|Lightguard Paladin", "noxustower");
		bot.Log("Gathering Power Dragon Energy");
		ItemFarm("Dragon Energy", 600, false, true, 6795, "Water Draconian", "lair");
		bot.Log("Gathering Power turn in");
		SafeQuestComplete(6795);
		Deaths_Door();
	}

	
	public void Deaths_Door() //death's door 6796
	{
		if (!bot.Quests.IsUnlocked(6796)) Gathering_Power();
		CheckSpace(Q2items);
		GetDropList(Combined);	
		ClassSwapSolo();
		bot.Log("Death's Door Death's Power");
		ItemFarm("Death's Power", 1, false, true, 6796, "Death", "shadowattack");
		bot.Log("Death's Door Souls of the Dead");
		ItemFarm("Souls of the Dead", 400, false, true, 6796, "Death", "shadowattack");
		bot.Log("Death's Door turn in");
		SafeQuestComplete(6796);
		Chaotic_Lords();

	}

	
	public void Chaotic_Lords() //Chaotic Lords 6797
	{
		if (!bot.Quests.IsUnlocked(6797)) Deaths_Door();
		UnbankList(Q3items.Concat(RequiredItems).ToArray());
		CheckSpace(Q3items.Concat(RequiredItems).ToArray());
		GetDropList(Q3items.Concat(RequiredItems).ToArray());
		ClassSwapFarming();
		bot.Log("Chaotic Lords Escherion");
		ItemFarm("Escherion's Helm", 1, false, true, 6797, "Staff Of Inversion|Escherion", "escherion");
		bot.Log("Chaotic Lords Vath");
		ItemFarm("Legendary Sword of Dragon Control", 1, false, true, 6797, "Vath", "Stalagbite");
		ClassSwapSolo();
		bot.Log("Chaotic Lords kitsune");
		ItemFarm("Hanzamune Dragon Koi Blade", 1, false, true, 6797, "Kitsune", "kitsune");
		bot.Log("Chaotic Lords wolfwing");
		ItemFarm("Wolfwing Armor", 1, false, true, 6797, "Wolfwing", "wolfwing");
		bot.Log("Chaotic Lords Kimberly");
		ItemFarm("One Eyed Doll Breaker", 1, false, true, 6797, "Kimberly", "palooza");
		bot.Log("Chaotic Lords Ledgermayne");
		ItemFarm("Ledgermayne", 1, false, true, 6797, "Ledgermayne", "Ledgermayne");
		bot.Log("Chaotic Lords Tibicenas");
		ItemFarm("Tibicenas", 1, false, true, 6797, "Tibicenas", "djinn");
		bot.Log("Chaotic Lords Khasaanda");
		ItemFarm("Soul of Chaos Armor", 1, false, true, 6797, "Khasaanda", "dreamnexus");
		bot.Log("Chaotic Lords Lionfang");
		ItemFarm("Chaos Lionfang Armor", 1, false, true, 6797, "Chaos Lord Lionfang", "stormtemple");
		bot.Log("Chaotic Lords Alteon");
		ItemFarm("Shorn Chaos King Crown", 1, false, true, 6797, "Chaos Lord Alteon", "swordhavenfalls");

		ClassSwapSolo();	
		bot.Log("Chaotic Lords Ultra Drakath / this is a 1% drop");
		ItemFarm("Drakath's Sword", 1, false, true, 6797, "Champion of Chaos", "Ultra Drakath"); //1% drop
		bot.Log("Chaotic Lords Iadoa");
		ItemFarm("Chaorrupted Hourglass", 1, false, true, 6797, "Chaos Lord Iadoa", "timespace");

		bot.Log("Chaotic Lords Power");		
		ClassSwapFarming();
		ItemFarm("Chaotic Power", 13, false, true, 6797, "Staff Of Inversion|Escherion", "escherion");
		bot.Log("Chaotic Lords killed'em all boss");
		SafeQuestComplete(6797);
		A_Means_To_An_End();
		

	}

	
	public void A_Means_To_An_End()//a means to an end 6798
	{
		if (!bot.Quests.IsUnlocked(6798)) Chaotic_Lords();
		CheckSpace(Q4items);
		GetDropList(Combined);
		ClassSwapFarming();
		bot.Log("A means to an end Ultimate Darkness Gem");
		ItemFarm("Ultimate Darkness Gem", 50, false, true, 6798, "Skeletal Fire Mage", "shadowfallwar");
		bot.Log("A means to an end Undead Energy");
		ItemFarm("Undead Energy", 2000, false, true, 6795, "Skeleton Warrior", "battleunderb");
		ClassSwapSolo();
		bot.Log("A means to an end (Necro) Scroll of Dark Arts");
		ItemFarm("(Necro) Scroll of Dark Arts", 2, false, true, 6798, "Ultra Vordred", "epicvordred");
		bot.Log("A means to an end Doom Heart");
		ItemFarm("Doom Heart", 1, false, true, 6798, "Ultra Sepulchure", "sepulchurebattle");
		bot.Log("A means to an end Dread Knight Cleaver");
		ItemFarm("Dread Knight Cleaver", 1, false, true, 6798, "Dark Sepulchure", "sepulchure");
		bot.Log("A means to an end Reaper's Soul");
		ItemFarm("Reaper's Soul", 1, false, true, 6798, "Reaper", "thevoid");
		bot.Log("A means to an end Desolich's Undead Eye");
		ItemFarm("Desolich's Undead Eye", 2, false, true, 6798, "Desolich", "Desolich");
		bot.Log("A means to an endturnin");
		SafeQuestComplete(6798);
		bot.Options.LagKiller = false;
		StopBot($"Set Complete.");
	}

	public void A_Loyal_Follower()
		{  
			CheckSpace(A_Loyal_FollowerItems);
			GetDropList(Combined);
			UnbankList(A_Loyal_FollowerItems); 
			bot.Log($"[{DateTime.Now:HH:mm:ss}] A Loyal Follower Quest Line");
			bot.Log($"[{DateTime.Now:HH:mm:ss}] if a quest fails, it means you've done it, as they are one time completes");
			if (!bot.Inventory.Contains("Undead Energy") && bot.Quests.IsUnlocked(2084)); goto A_Loyal_Follower;

		//A_Loyal_FollowerQuestLine

			Reforging_the_Blinding_Light:
			bot.Log($"[{DateTime.Now:HH:mm:ss}] can you 'handle' it...?");
			bot.Quests.Accept(2066);
			if (!bot.Inventory.Contains("Blinding Light of Destiny Handle", 1)) SafePurchase("Blinding Light of Destiny Handle", 1, "doomwood", 276);
			bot.Quests.EnsureComplete(2066, -1, tries: 1);
			bot.Sleep(ScriptDelay);
			if (bot.Inventory.Contains("Blinding Light of Destiny Handle", 1))	SafeSell("Blinding Light of Destiny Handle", 1);								
				bot.Log($"[{DateTime.Now:HH:mm:ss}] Reforging the Blinding Light Done");
				goto Secret_Order_of_Undead_Slayers;

			Secret_Order_of_Undead_Slayers:
			bot.Log($"[{DateTime.Now:HH:mm:ss}] Grinding bones");
			if(!bot.Quests.IsUnlocked(2067)) {goto Reforging_the_Blinding_Light;}
			if (!bot.Inventory.Contains("Bonegrinder Medal", 1))
			{
			bot.Quests.Accept(2067);			
			SafePurchase("Bonegrinder Medal", 1, "doomwood", 276);
			bot.Quests.EnsureComplete(2067, -1, tries: 1);
			}	
			bot.Sleep(ScriptDelay);	
			if (bot.Inventory.Contains("Bonegrinder Medal", 1))	SafeSell("Bonegrinder Medal", 1);
			goto Essential_Essences;

			Essential_Essences:
			bot.Log($"[{DateTime.Now:HH:mm:ss}] are they realy essential?");
			if(!bot.Quests.IsUnlocked(2082)) {goto Secret_Order_of_Undead_Slayers;}
				ItemFarm("Undead Essence", 25, false, false, 2082, "*", "battleunderb");
				bot.Quests.EnsureComplete(2082);
				goto Bust_Some_Dust;	

			Bust_Some_Dust:
			bot.Log($"[{DateTime.Now:HH:mm:ss}] Bone Dust is Required");
			if(!bot.Quests.IsUnlocked(2083)) {goto Essential_Essences;}
				ItemFarm("Bone Dust", 40, false, false, 2083, "*", "battleunderb");
				bot.Quests.EnsureComplete(2083);
				goto A_Loyal_Follower;

			A_Loyal_Follower:
			bot.Log($"[{DateTime.Now:HH:mm:ss}] Spirit orbs are required... i think?");
			if(!bot.Quests.IsUnlocked(2084)) {goto Bust_Some_Dust;}
			while (!bot.Inventory.Contains("Spirit Orb", 100))
			{
				ItemFarm("Undead Essence", 25, false, false, 2082, "*", "battleunderb");
				ItemFarm("Bone Dust", 40, false, false, 2083, "*", "battleunderb");
				SafeQuestComplete(2082);
				SafeQuestComplete(2083);
			}
				ItemFarm("celestial Compass", 1, true, true, 2084, "Ephemerite", "timevoid");
				bot.Quests.EnsureComplete(2084);			
				Relogin();	
		}


	public void Unbank(params string[] Unbank)
		{
		if (bot.Player.Cell != "Wait") bot.Player.Jump("Wait", "Spawn");
		while (bot.Player.State == 2) { }
		bot.Player.LoadBank();
		List<string> Whitelisted = new List<string>() { "Note", "Item", "Resource", "QuestItem", "ServerUse" };
		foreach (var item in Unbank)
			{
				if (bot.Bank.Contains(item)) bot.Bank.ToInventory(item);
			}
		}

		
	public bool CheckInv(params string[] items)
		{
		foreach (string item in items)
		{
			if (bot.Inventory.Contains(item) || bot.Bank.Contains(item))
				continue;
			else
				return false;
		}
		return true;
		}

	public void ChaosLords()
	{
		//idt this is needed for anything?
		StopBot($"setup & run breno's 13 chaos lords; https://github.com/BrenoHenrike/Rbot-Scripts");
	}

	public void LycanQuestRepPurchaseRankEnhance()
	        {
				ClassSwapSolo();
				
                FormatLog(Title: true, Text: "Lycan Class Owner Check");
				if (!bot.Inventory.Contains("Lycan"))
            
                //------------------------------------Unlocking the Rep Quest Start----------------------------------------------\
                    FormatLog(Title: true, Text: "Unlocking the Rep Quest Start");

                    if (!bot.Quests.IsUnlocked(537)) // do Vampire Knights
                    { 
                        ItemFarm("Chaos Vampire Defeated", 10, true, true, 536, "Chaos Vampire Knight", "lycan"); SafeQuestComplete(536);
                    }
                    else if (!bot.Quests.IsUnlocked(536)) // Do No Respect
                    { 
                        ItemFarm("Lycan Defeated", 5, true, true, 535, "Lycan", "lycan"); 
                        ItemFarm("Lycan Knight Defeated", 2, true, true, 535, "Lycan Knight", "lycan"); SafeQuestComplete(535);

                    }
                    else if (!bot.Quests.IsUnlocked(535)) //do A Gift Of Meat
                    { 
                        ItemFarm("Hunks of Meat", 6, true, true, 534, "Dire Wolf", "lycan"); SafeQuestComplete(534);
                    }

                    FormatLog(Title: true, Text: "Unlocking the Rep Quest Done");
                
                //--------------------------------------------Unlocking the Rep Quest Done----------------------------------------------\



            //--------------------------------------------------Lycan Rep Farm Start----------------------------------------------------\
                FormatLog(Title: true, Text: "Lycan Rep Farm");
				ClassSwapSolo();
                while (bot.Player.GetFactionRank("Lycan") < 10)
                {				
                    ItemFarm("Sanguine Mask", 1, true, true, 537, "Sanguine", "lycan"); SafeQuestComplete(537);
                }
            //--------------------------------------------------Lycan Rep Farm Done----------------------------------------------------\

            
            //--------------------------------------------------Lycan Purchase Start----------------------------------------------------\
            
                FormatLog(Title: true, Text: "Lycan Purchase Start");
                if (bot.Player.Gold > 50000){ SafePurchase("Lycan", 1, "lycan", 161); } 
                FormatLog(Title: true, Text: "Making some money"); 
                if (bot.Player.Gold < 50000)
                {
                SafeEquip(SoloClass);
                ItemFarm("Were Egg", 1, true, true, 236, "Big Bad Boar", "greenguardwest");
                ExitCombat();									
                SafeQuestComplete(236);
                bot.Sleep(ScriptDelay);								
                SafeSell("Berserker Bunny", 0);
                }
                FormatLog(Title: true, Text: "Lycan Purchase Done");
            //--------------------------------------------------Lycan Purchase Done----------------------------------------------------\

            
            //-----------------------------------------------------------------Lycan Ranking & Purchase Start-----------------------------------------------------------\

                FormatLog(Title: true, Text: "Lycan Ranking & Purchase Start");
                if (!bot.Inventory.Contains("Lycan")){
                {
                    SafePurchase("Lycan", 1, "lycan", 161);
                    bot.Sleep(1500);
                }
                    SafeEquip("Lycan");
				}

				if (bot.Inventory.Contains("Lycan"))
				{
					
					if (bot.Player.IsMember)
					{
						SafeMapJoin("nightmare");
						{
							while (bot.Player.Rank < 10)
							{				
								bot.Player.Hunt("Nothing");							
							}                    
						} 
					}

					else if (!bot.Player.IsMember)
					{
						if (bot.Player.Level > 34 && bot.Player.Level < 51)
						{
							SafeMapJoin("icestormarena");	
							while (bot.Player.Rank < 10)
							{				
								bot.Player.Hunt("Snow Leopard Skin Walker");							
							}	
						}

						else if (bot.Player.Level > 50 && bot.Player.Level < 75)
						{
						bot.Options.AggroMonsters = true;	
							{
								SafeMapJoin("icestormarena");	
								while (bot.Player.Rank < 10)
								{		
									bot.Player.Hunt("Icy Wind");
								}
							}
						}

						else if (bot.Player.Level > 74)
						{
						bot.Options.AggroMonsters = true;	
							{
								SafeMapJoin("icestormarena");	
								while (bot.Player.Rank < 10)
								{		
									bot.Player.Hunt("Frost Spirit");
								}
							}
						} 
					}
				}          
            FormatLog(Title: true, Text: "Lycan Ranking & Purchase Done");
            //-----------------------------------------------------------------Lycan Ranking & Purchase Done-----------------------------------------------------------\


            //-------------------------------------------------------------Lycan Enhance Start---------------------------------------------------------------\
           		if (bot.Inventory.Contains("Lycan")){
                FormatLog(Title: true, Text: "Lycan Enhance Start");
                bot.Options.AggroMonsters = false;
                bot.SendPacket($"%xt%zm%enhanceItemShop%7384%26440%66447%763%");
                FormatLog(Title: true, Text: "Lycan Enhance Done");
				SetCheck();
				   }
			}
            //-------------------------------------------------------------Lycan Enhance Done---------------------------------------------------------------\
			



	/*------------------------------------------------------------------------------------------------------------
													 Invokable Functions
	------------------------------------------------------------------------------------------------------------*/

	/*
		*	These functions are used to perform a major action in AQW.
		*	All of them require at least one of the Auxiliary Functions listed below to be present in your script.
		*	Some of the functions require you to pre-declare certain integers under "public class Script"
		*	ItemFarm, MultiQuestFarm and HuntItemFarm will require some Background Functions to be present as well.
		*	All of this information can be found inside the functions. Make sure to read.
		*	ItemFarm("ItemName", ItemQuantity, Temporary, HuntFor, QuestID, "MonsterName", "MapName", "CellName", "PadName");
		*	MultiQuestFarm("MapName", "CellName", "PadName", QuestList[], "MonsterName");
		*	SafeEquip("ItemName");
		*	SafePurchase("ItemName", ItemQuantityNeeded, "MapName", "MapNumber", ShopID)
		*	SafeSell("ItemName", ItemQuantityNeeded)
		*	SafeQuestComplete(QuestID, ItemID)
		*	StopBot("Text", "MapName", "MapNumber", "CellName", "PadName", "Caption")
	*/

	/// <summary>
	/// Farms you the specified quantity of the specified item with the specified quest accepted from specified monsters in the specified location. Saves States every ~5 minutes.
	/// </summary>
	public void ItemFarm(string ItemName, int ItemQuantity, bool Temporary = false, bool HuntFor = false, int QuestID = 0, string MonsterName = "*", string MapName = "Map", string CellName = "Enter", string PadName = "Spawn")
	{
	/*
		*   Must have the following functions in your script:
		*   SafeMapJoin
		*   SmartSaveState
		*   SkillList
		*   ExitCombat
		*   GetDropList OR ItemWhitelist
		*
		*   Must have the following commands under public class Script:
		*   int FarmLoop = 0;
		*   int SavedState = 0;
	*/

	startFarmLoop:
		if (FarmLoop > 0) goto maintainFarmLoop;
		SavedState++;
		FormatLog("Farm", $"Started Farming Loop {SavedState}");
		goto maintainFarmLoop;

	breakFarmLoop:
		SmartSaveState();
		FormatLog("Farm", $"Completed Farming Loop {SavedState}");
		FarmLoop = 0;
		goto startFarmLoop;

	maintainFarmLoop:
		if (Temporary)
		{
			if (HuntFor)
			{
				while (!bot.Inventory.ContainsTempItem(ItemName, ItemQuantity))
				{
					FarmLoop++;
					if (bot.Map.Name != MapName.ToLower()) SafeMapJoin(MapName.ToLower());
					if (QuestID > 0) bot.Quests.EnsureAccept(QuestID);
					bot.Options.AggroMonsters = false;
					AttackType("h", MonsterName);
					if (FarmLoop > SaveStateLoops) goto breakFarmLoop;
				}
			}
			else
			{
				while (!bot.Inventory.ContainsTempItem(ItemName, ItemQuantity))
				{
					FarmLoop++;
					if (bot.Map.Name != MapName.ToLower()) SafeMapJoin(MapName.ToLower(), CellName, PadName);
					if (bot.Player.Cell != CellName) bot.Player.Jump(CellName, PadName);
					if (QuestID > 0) bot.Quests.EnsureAccept(QuestID);
					bot.Options.AggroMonsters = false;
					AttackType("a", MonsterName);
					if (FarmLoop > SaveStateLoops) goto breakFarmLoop;
				}
			}
		}
		else
		{
			if (HuntFor)
			{
				while (!bot.Inventory.Contains(ItemName, ItemQuantity))
				{
					FarmLoop++;
					if (bot.Map.Name != MapName.ToLower()) SafeMapJoin(MapName.ToLower());
					if (QuestID > 0) bot.Quests.EnsureAccept(QuestID);
					bot.Options.AggroMonsters = false;
					AttackType("h", MonsterName);
					if (FarmLoop > SaveStateLoops) goto breakFarmLoop;
				}
			}
			else
			{
				while (!bot.Inventory.Contains(ItemName, ItemQuantity))
				{
					FarmLoop++;
					if (bot.Map.Name != MapName.ToLower()) SafeMapJoin(MapName.ToLower(), CellName, PadName);
					if (bot.Player.Cell != CellName) bot.Player.Jump(CellName, PadName);
					if (QuestID > 0) bot.Quests.EnsureAccept(QuestID);
					bot.Options.AggroMonsters = false;
					AttackType("a", MonsterName);
					if (FarmLoop > SaveStateLoops) goto breakFarmLoop;
				}
			}
		}
	}

	/// <summary>
	/// Farms all the quests in a given string, must all be farmable in the same room and cell.
	/// </summary>
	public void MultiQuestFarm(string MapName, string CellName, string PadName, int[] QuestList, string MonsterName = "*")
	{
	/*
		*   Must have the following functions in your script:
		*   SafeMapJoin
		*   SmartSaveState
		*   SkillList
		*   ExitCombat
		*   GetDropList OR ItemWhitelist
		*
		*   Must have the following commands under public class Script:
		*   int FarmLoop = 0;
		*   int SavedState = 0;
	*/

	startFarmLoop:
		if (FarmLoop > 0) goto maintainFarmLoop;
		SavedState++;
		FormatLog("Farm", $"Started Farming Loop {SavedState}");
		goto maintainFarmLoop;

	breakFarmLoop:
		SmartSaveState();
		FormatLog("Farm", $"Completed Farming Loop {SavedState}");
		FarmLoop = 0;
		goto startFarmLoop;

	maintainFarmLoop:
		FarmLoop++;
		if (bot.Map.Name != MapName.ToLower()) SafeMapJoin(MapName.ToLower(), CellName, PadName);
		if (bot.Player.Cell != CellName) bot.Player.Jump(CellName, PadName);
		foreach (var Quest in QuestList)
		{
			if (!bot.Quests.IsInProgress(Quest)) bot.Quests.EnsureAccept(Quest);
			if (bot.Quests.CanComplete(Quest)) SafeQuestComplete(Quest);
		}
		bot.Options.AggroMonsters = false;
		AttackType("a", MonsterName);
		if (FarmLoop > SaveStateLoops) goto breakFarmLoop;
	}

	/// <summary>
	/// Equips an item.
	/// </summary>
	public void SafeEquip(string ItemName)
	{
		//Must have the following functions in your script:
		//ExitCombat

		while (bot.Inventory.Contains(ItemName) && !bot.Inventory.IsEquipped(ItemName))
		{
			ExitCombat();
			bot.Player.EquipItem(ItemName);
		}
	}
	

	/// <summary>
	/// Sets attack type to Attack(Attack/A) or Hunt(Hunt/H)
	/// </summary>
	/// <param name="AttackType">Attack/A or Hunt/H</param>
	/// <param name="MonsterName">Name of the monster</param>
	public void AttackType(string AttackType, string MonsterName)
	{
		string attack_ = AttackType.ToLower();

		if (attack_ == "a" || attack_ == "attack")
		{
			bot.Player.Attack(MonsterName);
		}
		else if (attack_ == "h" || attack_ == "hunt")
		{
			bot.Player.Hunt(MonsterName);
		}
	}

	/// <summary>
	/// Purchases the specified quantity of the specified item from the specified shop in the specified map.
	/// </summary>
	public void SafePurchase(string ItemName, int ItemQuantityNeeded, string MapName, int ShopID)
	{
		//Must have the following functions in your script:
		//SafeMapJoin
		//ExitCombat

		while (!bot.Inventory.Contains(ItemName, ItemQuantityNeeded))
		{
			if (bot.Map.Name != MapName.ToLower()) SafeMapJoin(MapName.ToLower(), "Wait", "Spawn");
			ExitCombat();
			bot.Log($"[{DateTime.Now:HH:mm:ss}] Purchasing \t [{ItemName}]");
			bot.Shops.Load(ShopID);
			bot.Log($"[{DateTime.Now:HH:mm:ss}] Shop \t \t Loaded Shop {ShopID}.");
			bot.Shops.BuyItem(ItemName);
			bot.Log($"[{DateTime.Now:HH:mm:ss}] Shop \t \t Purchased {ItemName} from Shop {ShopID}.");
		}
	}

	/// <summary>
	/// Sells the specified item until you have the specified quantity.
	/// </summary>
	public void SafeSell(string ItemName, int ItemQuantityNeeded)
	{
		//Must have the following functions in your script:
		//ExitCombat

		int sellingPoint = ItemQuantityNeeded + 1;
		while (bot.Inventory.Contains(ItemName, sellingPoint))
		{
			ExitCombat();
			bot.Shops.SellItem(ItemName);
		}
	}

	/// <summary>
	/// Attempts to complete the quest with the set amount of {TurnInAttempts}. If it fails to complete, logs out. If it successfully completes, re-accepts the quest and checks if it can be completed again.
	/// </summary>
	public void SafeQuestComplete(int QuestID, int ItemID = -1) // Ty Exelot for this edition
	{
		//Must have the following functions in your script:
		//ExitCombat

		string Cell = bot.Player.Cell;
		string Pad = bot.Player.Pad;
		int i = 0;
		while (bot.Quests.CanComplete(QuestID)) {
			if (bot.Player.Cell != "Wait" || bot.Player.InCombat)
				ExitCombat();
			bot.Quests.EnsureComplete(QuestID);
			i++;
			if (i > TurnInAttempts) {
				FormatLog("Quest", $"Turning in Quest {QuestID} failed. Logging out");
				bot.Player.Logout();
			}
		}
		FormatLog("Quest", $"Turning in Quest {QuestID} successful.");
		while (!bot.Quests.IsInProgress(QuestID)) bot.Quests.EnsureAccept(QuestID);
		bot.Player.Jump(Cell, Pad);
	}

	/// <summary>
	/// Stops the bot at yulgar if no parameters are set, or your specified map if the parameters are set.
	/// </summary>
	public void StopBot(string Text = "Bot stopped successfully.", string MapName = "yulgar", string CellName = "Enter", string PadName = "Spawn", string Caption = "Stopped", string MessageType = "event")
	{
		//Must have the following functions in your script:
		//SafeMapJoin
		//ExitCombat
		bot.Player.Jump("Enter", "Spawn");
		if (bot.Map.Name != MapName.ToLower()) SafeMapJoin(MapName.ToLower(), CellName, PadName);
		if (bot.Player.Cell != CellName) bot.Player.Jump(CellName, PadName);
		bot.Drops.RejectElse = false;
		bot.Options.LagKiller = false;
		bot.Options.AggroMonsters = false;
		FormatLog(Title: true, Text: "Script Stopped");
		Console.WriteLine(Text);
		SendMSGPacket(Text, Caption, MessageType);
		ScriptManager.StopScript();
	}

	/*------------------------------------------------------------------------------------------------------------
													Auxiliary Functions
	------------------------------------------------------------------------------------------------------------*/

	/*
		*   These functions are used to perform small actions in AQW.
		*   They are usually called upon by the Invokable Functions, but can be used separately as well.
		*   Make sure to have them loaded if your Invokable Function states that they are required.
		*   ExitCombat()
		*   SmartSaveState()
		*   SafeMapJoin("MapName", "CellName", "PadName")
		*	FormatLog("Topic", "Text", Tabs, Title, Followup)
	*/

	/// <summary>
	/// Exits Combat by jumping cells.
	/// </summary>
	public void ExitCombat()
	{
		bot.Options.AggroMonsters = false;
		bot.Player.Jump("Wait", "Spawn");
		while (bot.Player.State == 2) { }
	}

	/// <summary>
	/// Creates a quick Save State by messaging yourself.
	/// </summary>
	public void SmartSaveState()
	{
		bot.SendPacket("%xt%zm%whisper%1%creating save state%" + bot.Player.Username + "%");
		FormatLog("Saving", "Successfully Saved State");
	}

	/// <summary>
	/// Joins the specified map.
	/// </summary>
	public void SafeMapJoin(string MapName, string CellName = "Enter", string PadName = "Spawn")
	{
		//Must have the following functions in your script:
		//ExitCombat
		string mapname = MapName.ToLower();
		while (bot.Map.Name != mapname)
		{
			ExitCombat();
			if (mapname == "tercessuinotlim") bot.Player.Jump("m22", "Center");
			bot.Player.Join($"{mapname}-{MapNumber}", CellName, PadName);
			bot.Wait.ForMapLoad(mapname);
			bot.Sleep(500);
		}
		if (bot.Player.Cell != CellName) bot.Player.Jump(CellName, PadName);
		FormatLog("Joined", $"[{mapname}-{MapNumber}, {CellName}, {PadName}]");
	}
	
	/// <summary>
	/// Logs following a specific format. No more than 3 tabs allowed.
	/// </summary>
	public void FormatLog(string Topic = "FormatLog", string Text = "Missing Input", int Tabs = 2, bool Title = false, bool Followup = false)
	{
		if (Title)
			bot.Log($"[{DateTime.Now:HH:mm:ss}] -----{Text}-----");
		else 
		{
			Tabs = Tabs > 3 ? 3 : Tabs;
			string TabPlace = "";
			for (int i = 0; i < Tabs; i++) 
				TabPlace += "\t";
			if (Followup) 
				bot.Log($"[{DateTime.Now:HH:mm:ss}] ↑ {TabPlace}{Text}");
			else 
				bot.Log($"[{DateTime.Now:HH:mm:ss}] {Topic} {TabPlace}{Text}");
		}
	}

	/*------------------------------------------------------------------------------------------------------------
													Background Functions
	------------------------------------------------------------------------------------------------------------*/

	/*
		*   These functions help you to either configure certain settings or run event handlers in the background.
		*   It is highly recommended to have all these functions present in your script as they are very useful.
		*   Some Invokable Functions may call or require the assistance of some Background Functions as well.
		*   These functions are to be run at the very beginning of the bot under public class Script.
		*   ConfigureBotOptions("PlayerName", "GuildName", LagKiller, SafeTimings, RestPackets, AutoRelogin, PrivateRooms, InfiniteRange, SkipCutscenes, ExitCombatBeforeQuest)
		*   ConfigureLiteSettings(UntargetSelf, UntargetDead, CustomDrops, ReacceptQuest, SmoothBackground, Debugger)
		*   SkillList(int[])
		*   GetDropList(string[])
		*   ItemWhiteList(string[])
		*   EquipList(string[])
		*   UnbankList(string[])
		*   CheckSpace(string[])
		*   SendMSGPacket("Message", "Name", "MessageType")
	*/

	/// <summary>
	/// Change the player's name and guild for your bots specifications.
	/// Recommended Default Bot Configurations.
	/// </summary>
	public void ConfigureBotOptions(string PlayerName = "Tato", string GuildName = "Booba", bool aggroMonsters = false, bool LagKiller = true, bool SafeTimings = true, bool RestPackets = true, bool AutoRelogin = true, bool PrivateRooms = true, bool InfiniteRange = true, bool SkipCutscenes = true, bool ExitCombatBeforeQuest = true, bool HideMonster=true)
	{
		SendMSGPacket("Drink Water Pls?", "Tato", "moderator");
		SendMSGPacket("Drink Water Pls?", "Tato", "warning");
		SendMSGPacket("Drink Water Pls?", "Tato", "server");
		SendMSGPacket("Drink Water Pls?", "Tato", "event");
		SendMSGPacket("Drink Water Pls?", "Tato", "guild");
		SendMSGPacket("Drink Water Pls?", "Tato", "zone");
		SendMSGPacket("Drink Water Pls?", "Tato", "whisper");
		bot.Options.CustomName = PlayerName;
		bot.Options.CustomGuild = GuildName;
		bot.Options.LagKiller = LagKiller;
		bot.Options.SafeTimings = SafeTimings;
		bot.Options.RestPackets = RestPackets;
		bot.Options.AutoRelogin = AutoRelogin;
		bot.Options.PrivateRooms = PrivateRooms;
		bot.Options.InfiniteRange = InfiniteRange;
		bot.Options.SkipCutscenes = SkipCutscenes;
		bot.Options.ExitCombatBeforeQuest = ExitCombatBeforeQuest;
		// bot.Events.PlayerDeath += PD => ScriptManager.RestartScript();
		// bot.Events.PlayerAFK += PA => ScriptManager.RestartScript();
		HideMonsters(HideMonster);
	}

	/// <summary>
	/// Hides the monsters for performance
	/// </summary>
	/// <param name="Value"> true -> hides monsters. false -> reveals them </param>
	public void HideMonsters(bool Value) {
	  switch(Value) {
	     case true:
	        if (!bot.GetGameObject<bool>("ui.monsterIcon.redX.visible")) {
	           bot.CallGameFunction("world.toggleMonsters");
	        }
	        return;
	     case false:
	        if (bot.GetGameObject<bool>("ui.monsterIcon.redX.visible")) {
	           bot.CallGameFunction("world.toggleMonsters");
	        }
	        return;
	  }
	}

	/// <summary>
	/// Gets AQLite Functions
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="optionName"></param>
	/// <returns></returns>
	public T GetLite<T>(string optionName)
	{
		return bot.GetGameObject<T>($"litePreference.data.{optionName}");
	}

	/// <summary>
	/// Sets AQLite Functions
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="optionName"></param>
	/// <param name="value"></param>
	public void SetLite<T>(string optionName, T value)
	{
		bot.SetGameObject($"litePreference.data.{optionName}", value);
	}

	/// <summary>
	/// Allows you to turn on and off AQLite functions.
	/// Recommended Default Bot Configurations.
	/// </summary>
	public void ConfigureLiteSettings(bool UntargetSelf = true, bool UntargetDead = true, bool CustomDrops = true, bool ReacceptQuest = false, bool SmoothBackground = true, bool Debugger = false)
	{
		SetLite("bUntargetSelf", UntargetSelf);
		SetLite("bUntargetDead", UntargetDead);
		SetLite("bCustomDrops", CustomDrops);
		SetLite("bReaccept", ReacceptQuest);
		SetLite("bSmoothBG", SmoothBackground);
		SetLite("bDebugger", Debugger);
	}

	/// <summary>
	/// Spams Skills when in combat. You can get in combat by going to a cell with monsters in it with bot.Options.AggroMonsters enabled or using an attack command against one.
	/// </summary>
	
	string currentSkillSet = "" ;
	public void SkillList(string skillSetName, params int[] Skillset)
	{
		bot.Handlers.RemoveAll(handlers => handlers.Name == currentSkillSet);
		bot.RegisterHandler(1, b => {
			if (bot.Player.InCombat)
			{
				foreach (var Skill in Skillset)
				{
					if (bot.Player.CanUseSkill(Skill))
    				bot.Player.UseSkill(Skill);
				}
			}
		}, skillSetName);
		currentSkillSet = skillSetName;
	}

	/// <summary>
	/// Checks if items in an array have dropped every second and picks them up if so. GetDropList is recommended.
	/// </summary>
	public void GetDropList(params string[] GetDropList)
	{
		bot.RegisterHandler(4, b => {
			foreach (string Item in GetDropList)
			{
				if (bot.Player.DropExists(Item)) bot.Player.Pickup(Item);
			}
			bot.Player.RejectExcept(GetDropList);
		});
	}

	/// <summary>
	/// Pick up items in an array when they dropped. May fail to pick up items that drop immediately after the same item is picked up. GetDropList is preferable instead.
	/// </summary>
	public void ItemWhiteList(params string[] WhiteList)
	{
		foreach (var Item in WhiteList)
		{
			bot.Drops.Add(Item);
		}
		bot.Drops.RejectElse = true;
		bot.Drops.Start();
	}

	/// <summary>
	/// Equips all items in an array.
	/// </summary>
	/// <param name="EquipList"></param>
	public void EquipList(params string[] MaxSetEquip)
	{
		foreach (var Item in MaxSetEquip)
		{
			if (bot.Inventory.Contains(Item))
			{
				SafeEquip(Item);
			}
		}
	}

	/// <summary>
	/// Unbanks all items in an array after banking every other AC-tagged Misc item in the inventory.
	/// </summary>
	/// <param name="UnbankList"></param>
	public void UnbankList(params string[] UnbankList)
	{
		if (bot.Player.Cell != "Wait") bot.Player.Jump("Wait", "Spawn");
		while (bot.Player.State == 2) { }
		bot.Player.LoadBank();
		List<string> Whitelisted = new List<string>() { "Note", "Item", "Resource", "QuestItem", "ServerUse" };
		foreach (var item in bot.Inventory.Items)
		{
			if (!Whitelisted.Contains(item.Category.ToString())) continue;
			if (item.Name != "Treasure Potion" && item.Coins && !Array.Exists(UnbankList, x => x == item.Name)) bot.Inventory.ToBank(item.Name);
		}
		foreach (var item in UnbankList)
		{
			if (bot.Bank.Contains(item)) bot.Bank.ToInventory(item);
		}
	}

	/// <summary>
	/// Checks the amount of space you need from an array's length/set amount.
	/// </summary>
	/// <param name="ItemList"></param>
	public void CheckSpace(params string[] ItemList)
	{
		int MaxSpace = bot.GetGameObject<int>("world.myAvatar.objData.iBagSlots");
		int FilledSpace = bot.GetGameObject<int>("world.myAvatar.items.length");
		int EmptySpace = MaxSpace - FilledSpace;
		int SpaceNeeded = 0;

		foreach (var Item in ItemList)
		{
			if (!bot.Inventory.Contains(Item)) SpaceNeeded++;
		}

		if (EmptySpace < SpaceNeeded)
		{
			MessageBox.Show("If this box is showing, you need to make space in your inventory", "close Dialong box and read the chat, for inv space required.");
			StopBot($"Need {SpaceNeeded} empty inventory slots, please make room for the quest.", bot.Map.Name, bot.Player.Cell, bot.Player.Pad, "Error", "moderator");
		}
	}

	/// <summary>
	/// Sends a message packet to client in chat.
	/// </summary>
	/// <param name="Message"></param>
	/// <param name="Name"></param>
	/// <param name="MessageType">moderator, warning, server, event, guild, zone, whisper</param>
	public void SendMSGPacket(string Message = " ", string Name = "SERVER", string MessageType = "zone")
	{
		// bot.SendClientPacket($"%xt%{MessageType}%-1%{Name}: {Message}%");
		bot.SendClientPacket($"%xt%chatm%0%{MessageType}~{Message}%{Name}%");
	}


	public void DeathHandler() {
      bot.RegisterHandler(2, b => {
         if (bot.Player.State==0) {
            bot.Player.SetSpawnPoint();
            ExitCombat();
            bot.Sleep(12000);
         }
      });
	}

		public void VersionCheck(string version)
    {
        if (!Forms.Main.Text.StartsWith($"RBot {version}"))
        {
            MessageBox.Show($"This bot is likely glitch out if you dont have RBot {version} or above. You have been warned", "WARNING");
        }
    }

	public void Relogin()
	{
		bot.Options.AutoRelogin = false;
		bot.Sleep(10000);
		bot.Player.Logout();
		bot.Sleep(10000);
    	bot.Player.Login(bot.Player.Username, bot.Player.Password);
    	RBot.Servers.Server server = bot.Options.AutoReloginAny ? RBot.Servers.ServerList.Servers.Find(x => x.IP != RBot.Servers.ServerList.LastServerIP) : bot.Options.LoginServer ?? RBot.Servers.ServerList.Servers[0];
    	bot.Player.Connect(server);
    	while (!bot.Player.LoggedIn) { bot.Sleep(2500); }
		bot.Sleep(10000);
		bot.Options.AutoRelogin = true;
		bot.Sleep(17000);
		PopTartsAreOk();
	}

	public void ClassSwapSolo()
	{
		FormatLog(Title: true, Text: "ClassSwapSolo");
		if (bot.Inventory.Contains(SoloClass2))
		{ 
		SkillList(SoloClass2, SkillOrderVHL);
		SafeEquip(SoloClass2); //Void Highlord
		FormatLog("ClassSwapSolo", "Void Highlord");
		}
		else if (bot.Inventory.Contains(SoloClass))
		{
		SkillList(SoloClass, SkillOrderLycan);
		SafeEquip(SoloClass); // Lycan
		FormatLog("ClassSwapSolo", "Lycan");
		}
	}

		public void ClassSwapFarming()
	{
		FormatLog(Title: true, Text: "ClassSwapFarming");
		if (bot.Inventory.Contains(FarmClass3))
		{		
		SkillList(FarmClass3, SkillOrderLR);
		SafeEquip(FarmClass3); //Legion Revenant
		FormatLog("ClassSwapFarming", "Legion Reventant");
		}
		else if (bot.Inventory.Contains(FarmClass2))
		{ 
		SkillList(FarmClass2, SkillOrderVPL); //Vampire Lord
		SafeEquip(FarmClass2);
		FormatLog("ClassSwapFarming", "Vampire Lord");
		}
		else if (bot.Inventory.Contains(FarmClass))
		{ 
		SkillList(FarmClass, SkillOrderMage); //Mage
		SafeEquip(FarmClass);
		FormatLog("ClassSwapFarming", "Mage");
		}
	}

	public void CLassSwapXiang()
	{
		FormatLog(Title: true, Text: "CLassSwapXiang");

		//--------------------------------Xiang Healer / DOT fast kill-----------------------------\\
		bot.Log("Chaotic Lords Xiang");
		if(bot.Inventory.Contains(XiangClass2, 1) || bot.Bank.Contains(XiangClass2, 1)) 
		{ if (bot.Bank.Contains(XiangClass2)) bot.Bank.ToInventory(XiangClass2); SafeEquip(XiangClass2);
			{
			FormatLog("CLassSwapXiang", "Dragon of Time");
			SkillList(XiangClass2, SkillOrderXiangClass2);//DoT class
			}}
		 
		else if(bot.Inventory.Contains(XiangClass1, 1) || bot.Bank.Contains(XiangClass1, 1)) 
		{ if (bot.Bank.Contains(XiangClass1)) bot.Bank.ToInventory(XiangClass1); SafeEquip(XiangClass1);
			{
			FormatLog("CLassSwapXiang", "Healer");
			SkillList(XiangClass1, SkillOrderXiangClass1);//Healer class
			}}
		//--------------------------------Xiang Healer / DOT fast kill-----------------
	}

		public void SafeQuestCompleteDaily(int QuestID, int ItemID = -1)
	{
		//Must have the following functions in your script:
		//ExitCombat

		ExitCombat();
		bot.Quests.EnsureAccept(QuestID);
		bot.Quests.EnsureComplete(QuestID, ItemID, tries: 10);
		if (bot.Quests.IsInProgress(QuestID))
		{
			bot.Log("Failed to turn in Quest {QuestID}. Logging out.");
			bot.Player.Logout();
		}
		bot.Log("Turned In Quest {QuestID} successfully.");
	}



}