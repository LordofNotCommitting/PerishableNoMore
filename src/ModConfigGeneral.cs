using MGSC;
using ModConfigMenu.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerishableNoMore
{
    // Token: 0x02000006 RID: 6
    public class ModConfigGeneral
    {
        // Token: 0x0600001D RID: 29 RVA: 0x00002840 File Offset: 0x00000A40
        public ModConfigGeneral(string ModName, string ConfigPath)
        {
            this.ModName = ModName;
            this.ModData = new ModConfigData(ConfigPath);
            this.ModData.AddConfigHeader("STRING:General Settings", "general");
            this.ModData.AddConfigValue("general", "requires_freezer_upgrade", false, "STRING:Only works with Fridge researched.", "STRING:Turn this off to make the mod work regardless of Magnum upgrade status.");
            //this.ModData.AddConfigValue("general", "apply_freezer_during_mission", true, "STRING:Apply Freezing during mission", "STRING:Expiration timer will not tick down for time spent on mission with this option on.");
            this.ModData.AddConfigValue("general", "apply_freezer_to_everything", true, "STRING:Apply Freezer effect to everything", "STRING:This put freezer effect on everything. <color=#f51b1b>Overrides any setup below.</color>");
            this.ModData.AddConfigValue("general", "apply_freezer_to_shipcargo", false, "STRING:Apply Freezer effect to ship cargo", "STRING:This will apply freezer effect, on ship cargo only.");
            this.ModData.AddConfigValue("general", "apply_freezer_to_vest", false, "STRING:Apply Freezer effect to vest", "STRING:This will apply freezer effect, on ship cargo only.");
            this.ModData.AddConfigValue("general", "apply_freezer_to_backpack", false, "STRING:Apply Freezer effect to backpack", "STRING:This will apply freezer effect, on backpack only.");
            this.ModData.AddConfigValue("general", "about2", "STRING:<color=#f51b1b>The game must be restarted after setting then saving this config to take effect.</color>\n");
            this.ModData.RegisterModConfigData(ModName);
        }

        // Token: 0x04000011 RID: 17
        private string ModName;

        // Token: 0x04000012 RID: 18
        public ModConfigData ModData;

    }
}
