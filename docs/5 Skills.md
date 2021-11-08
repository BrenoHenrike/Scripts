## Skills
It is recommended you load skills from an XML skills file created from the Skills form. If you would like to manually manage skills in your script, you can do this through `ScriptInterface#Skills`. Manual management is overridden by the UI by default.

### Properties
The following properties can be modified to change the behaviour of the skill timer thread:

| Property | Type | Description |
|---|:---:|---|
| `SkillTimer` | *int* | The time period in ms at which the timer cycles through skills and tries to use them. The default is 100ms. |
| `SkillTimeout` | *int* | The timeout in **multiples** of SkillTimer milliseconds it will wait before skipping the current unavailable skill when using SkillMode.Wait (which comes with [Patterns](/docs/5%20Skills#patterns) or your own [SkillProvider](/docs/5%20Skills#skill-provider)). The default is -1 (no timeout). |
| `TimerRunning` | *bool* | Whether the skill timer thread is currently running. |

### Methods
The following methods can be used to programatically manage skills. The method's return types are ommitted as they are all void.

| Method Definition | Description |
|---|---|
| `StartTimer()` | Starts the skill timer thread. This must be called for the bot to use skills whether you are using the skills form or manually managing skills (see example below). If the thread is already running, this method does nothing but replace the currently loaded skills with the UI override skills if they are set. |
| `StopTimer()` | Kills the skill timer thread if it is running. This will stop the bot from using any skills. |
| `StartPattern(string def)` | Loads the pattern definition file and restarts the skill timer thread. |
| `StartSkills(string xml)` | Loads the skill definition txt file and restarts the skill timer thread. |
| `Add(int index, float useThresh)` | Adds the skill with the given index (1-4) to the timer. This skill will only be used when the player's health is below `useThresh`, where `useThresh` is a fraction of the player's health over the player's maximum health. A `useThresh` of `1f` indicates that the skill can always be used. |
| `Add(int index, UseRule rule)` | Adds the skill with the given index (1-4) to ther timer to only be used when the given `UseRule` allows. See below about use rules. |
| `Remove(int index)` | Removex the skill with the given index from the timer. This skill will no longer be used unless it is added again. |
| `Clear()` | Clears all skills from the timer. |
| `LoadSkills(string xml)` | Loads a skill definition XML file which was created by the UI. `xml` is the path to the definition file which can be absolute or relative to the running directory of the bot. |
| `LoadPattern(string def)` | Loads a pattern definition file (can be any type of text file). `def` is the path to the definition file which can be absolute or relative to the running directory of the bot. |
| `StartSkills(string xml)` | Loads a skill definition XML file and then restarts the skill timer. |

### Skill Use Rules
If you would like finer grained control over when skills are used, you can apply a `UseRule` to a skill at the given index. There are 3 rules which can be used in the UI:

1. `HealthUseRule` - Specifies a minimum and maximum health at which the skill can be used.
2. `ManaUseRule` - Specifies a minimum and maximum mana at which the skill can be used.
3. `CombinedUseRule` - Combines any number of other rules with the specified logical operator.

These are very simply understood using the UI skills editor.

You can also create your own use rules using a script. To do this, create a class that extends `UseRule` and override the `UseRule#ShouldUse` method. For example:

```csharp
using RBot;
using System;

public class Script
{
	public void ScriptMain(ScriptInterface bot)
	{
        bot.Skills.Add(1, new ExampleUseRule());
        // ...add other skills
        bot.Skills.StartTimer();

        // ...other bot code
	}

    public class ExampleUseRule : UseRule
    {
        public int Interval { get; set; } = 2000;

        private int lastUse = 0;

        public override bool ShouldUse(ScriptInterface bot)
        {
            if(Environment.TickCount - lastUse > Interval)
            {
                lastUse = Environment.TickCount;
                return true;
            }
            return false;
        }
    }
}
```

This allows the skill at index `1` to only be used every 2 seconds. In this example, `Interval` is configurable to change this 2 seconds to any other time.

### Patterns

Alongside with Skill XMLs, you can also define ordered skill patterns using skill pattern definition files. They are very simple and contain 3 commands:

- `repeat <n>`: Repeats a sublist of commands `n` times.
- `pattern <pattern>`: Executes the skill indexes in `pattern` in the given order.
- `end`: Ends the current repeat sublist. Note that nested repeats are supported.

For example, the skill pattern definition file for Immortal Chronomancer might look like this:

```csharp
//Repeat the sublist 7 times
repeat 7
    //Use the skills 1 then 4
	pattern 14
//End of first repeat
end
//Use the skills 2 then 3
pattern 23
//Repeat the sublist 5 times
repeat 5
    //Use the skills 4 then 1
	pattern 41
//End of second repeat
end
//Use the skills 4, 2 and 3
pattern 423
```

You can create it in any text editor and then save it like `fileName.txt` or `fileName.def`. To use a skill pattern definition, simply call `ScriptSkills#StartPattern`:

```csharp
bot.Skills.StartPattern("Skills/ic.def");
```

This will override any other options set up with the skill timer (including manually added skills and loaded xml definitions). To clear the loaded skill pattern, call `ScriptSkills#ClearPattern`:

```csharp
bot.Skills.ClearPattern();
```

**Note:** The execution state of skill patterns are reseted when the player has no target (i.e. the skill timer starts again from the beginning of the pattern definition file when the player loses their target, and targets another monster).

### Skill Provider

//TODO

---------
<center>
<a href="/Rbot-Scripts/4 Monsters" title="4. Monsters">◄ Previous</a> — <a href="/Rbot-Scripts/" title="Back to Index">Index</a> — <a href="/Rbot-Scripts/6 Shops" title="6. Shops">Next ►</a></center>