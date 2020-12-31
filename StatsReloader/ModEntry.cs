using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;


namespace StatsReloader
{

    public class ModConfig
    {
        /// <summary>
        /// Percent of health regen per seconds
        /// </summary>
        public float PercentHealthSeconds { get; set; } = 0.2f;

        /// <summary>
        /// Percent of stamina regen per seconds
        /// </summary>
        public float PercentStaminaSeconds { get; set; } = 0.1f;


        /// <summary>
        /// Regen apply each x ticks
        /// </summary>
        public int TickRegen { get; set; } = 120;
    }

    public class ModEntry : Mod
    {

        private ModConfig Config;
        private int lastTickRegen;

        public override void Entry(IModHelper helper)
        {

            this.Config = helper.ReadConfig<ModConfig>();
            helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
        }

        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            if (lastTickRegen + this.Config.TickRegen < Game1.ticks)
            {
                if (Game1.player.Stamina < Game1.player.MaxStamina)
                {
                    float nbStaminaRegen = Percent(Game1.player.MaxStamina, this.Config.PercentStaminaSeconds);
                    if (nbStaminaRegen <= 0)
                    {
                        nbStaminaRegen = 1;
                    }
                    float totalStamina = (Game1.player.Stamina + nbStaminaRegen) > Game1.player.MaxStamina ? Game1.player.MaxStamina : Game1.player.Stamina + nbStaminaRegen;
                    Game1.player.Stamina += nbStaminaRegen;
                }

                if (Game1.player.health < Game1.player.maxHealth)
                {
                    int nbHealthRgen = (int)Percent(Game1.player.maxHealth, this.Config.PercentHealthSeconds);
                    if (nbHealthRgen <= 0)
                    {
                        nbHealthRgen = 1;
                    }
                    
                    int totalHeath = (Game1.player.health + nbHealthRgen) > Game1.player.maxHealth ? Game1.player.maxHealth : Game1.player.health + nbHealthRgen;
                    Game1.player.health = totalHeath;
                }

                lastTickRegen = Game1.ticks;
            }
        }

        private float Percent(float baseNumber, float percent)
        {
            return baseNumber * (percent / 100f);
        }

    }
}
