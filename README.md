# StardewValleyStatsReloader

Add a configurable passive regeneration of your health and stamina over time, based on percent of your max amount for each stats (default 0.2%).

Configuration available in config.json :

```
{
  "PercentHealthSeconds": 0.2,  // Percent of health regen per 2 seconds
  "PercentStaminaSeconds": 0.2, // Percent of stamina regen per 2 seconds
  "TickRegen": 120              // Number of tick between each regen cycle (120 ~= 2 seconds)
}
```
