using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace PerishableNoMore
{
    /*

    [HarmonyPatch(typeof(MissionSystem), nameof(MissionSystem.ProcessFinishedDungeonData))]
    public class FreezeItem
    {
        static bool requires_freezer_upgrade = Plugin.ConfigGeneral.ModData.GetConfigValue<bool>("requires_freezer_upgrade", false);
        static bool apply_freezer_during_mission = Plugin.ConfigGeneral.ModData.GetConfigValue<bool>("apply_freezer_during_mission", true);
        static bool apply_freezer_to_everything = Plugin.ConfigGeneral.ModData.GetConfigValue<bool>("apply_freezer_to_everything", true);
        static bool apply_freezer_to_shipcargo = Plugin.ConfigGeneral.ModData.GetConfigValue<bool>("apply_freezer_to_shipcargo", false);
        static bool apply_freezer_to_vest = Plugin.ConfigGeneral.ModData.GetConfigValue<bool>("apply_freezer_to_vest", false);
        static bool apply_freezer_to_backpack = Plugin.ConfigGeneral.ModData.GetConfigValue<bool>("apply_freezer_to_backpack", false);

        
        //passive effect is kept
        public static void Postfix(MissionFactory missionFactory, Missions missions, Stations stations, Mercenaries mercenaries, MagnumCargo magnumCargo, MagnumProgression magnumSpaceship, MagnumProjects magnumProjects, StoryTriggers storyTriggers, SpaceTime spaceTime, PopulationDebugData populationDebugData, TravelMetadata travelMetadata, RaidMetadata raidMetadata, Statistics statistics, Factions factions, ItemsPrices itemsPrices, News news, FactionCachedData factionCachedData, DungeonFinishedData finishedData, Difficulty difficulty, PerkFactory perkFactory, SpaceObjects spaceObjects, OrbitRing[] rings, Spaceship spaceship)
        {
            if (apply_freezer_during_mission) {
                bool actually_apply_fridgecheck = true;
                bool actually_apply_everything = true;
                bool actually_apply_shipcargo = true;
                bool actually_apply_vest = true;
                bool actually_apply_backpack = true;

                actually_apply_fridgecheck = requires_freezer_upgrade ? GetFridgeData.fridge_setup : true;
                actually_apply_everything = actually_apply_fridgecheck ? apply_freezer_to_everything : false;
                actually_apply_shipcargo = actually_apply_fridgecheck ? (apply_freezer_to_everything ? true : apply_freezer_to_shipcargo) : false;
                actually_apply_vest = actually_apply_fridgecheck ? (apply_freezer_to_everything ? true : apply_freezer_to_vest) : false;
                actually_apply_backpack = actually_apply_fridgecheck ? (apply_freezer_to_everything ? true : apply_freezer_to_backpack) : false;
                //Plugin.Logger.Log("actually_apply_fridgecheck:" + actually_apply_fridgecheck);
                //Plugin.Logger.Log("fridgecheck:" + (GetFridgeData.fridge_setup));
                //Plugin.Logger.Log("actually_apply_everything:" + actually_apply_everything);
                //Plugin.Logger.Log("actually_apply_shipcargo:" + actually_apply_shipcargo);
                //Plugin.Logger.Log("actually_apply_vest:" + actually_apply_vest);
                //Plugin.Logger.Log("actually_apply_backpack:" + actually_apply_backpack);

                foreach (Mercenary mercenary in mercenaries.Values)
                {
                    FreezeSpaceItem.ProcessFlipExpireItemLogic(mercenary.CreatureData.Inventory.VestStore, spaceTime.Time, actually_apply_vest);
                    FreezeSpaceItem.ProcessFlipExpireItemLogic(mercenary.CreatureData.Inventory.BackpackStore, spaceTime.Time, actually_apply_backpack);
                    foreach (ItemStorage storage3 in mercenary.CreatureData.Inventory.Slots)
                    {
                        //items on slot (weapon/armor/etc slot) will count as backpack
                        FreezeSpaceItem.ProcessFlipExpireItemLogic(storage3, spaceTime.Time, actually_apply_backpack);
                    }
                }
            }
        }


    }
    */
}
