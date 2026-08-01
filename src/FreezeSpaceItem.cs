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

    [HarmonyPatch(typeof(ItemExpireSystem), nameof(ItemExpireSystem.Update))]
    public class FreezeSpaceItem
    {
        static bool requires_freezer_upgrade = Plugin.ConfigGeneral.ModData.GetConfigValue<bool>("requires_freezer_upgrade", false);
        static bool apply_freezer_to_everything = Plugin.ConfigGeneral.ModData.GetConfigValue<bool>("apply_freezer_to_everything", true);
        static bool apply_freezer_to_shipcargo = Plugin.ConfigGeneral.ModData.GetConfigValue<bool>("apply_freezer_to_shipcargo", false);
        static bool apply_freezer_to_vest = Plugin.ConfigGeneral.ModData.GetConfigValue<bool>("apply_freezer_to_vest", false);
        static bool apply_freezer_to_backpack = Plugin.ConfigGeneral.ModData.GetConfigValue<bool>("apply_freezer_to_backpack", false);

        
        //passive effect is kept
        public static bool Prefix(MagnumCargo magnumCargo, Mercenaries mercenaries, SpaceTime time)
        {
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

            ItemExpireSystem._itemsCache.Clear();
            foreach (ItemStorage storage in magnumCargo.ShipCargo)
            {
                ProcessFlipExpireItemLogic(storage, time.Time, actually_apply_shipcargo);
            }
            if (!magnumCargo.RecyclingInProgress)
            {
                //recycling is part of cargo
                ProcessFlipExpireItemLogic(magnumCargo.RecyclingStorage, time.Time, actually_apply_shipcargo);
            }
            foreach (Mercenary mercenary in mercenaries.Values)
            {
                /*
                if (actually_apply_vest)
                {
                    //Plugin.Logger.Log("actually_apply_vest:" + actually_apply_vest);
                    ItemExpireSystem.ProcessFreeze(mercenary.CreatureData.Inventory.VestSlot, time.Time);
                }
                else
                {
                    //Plugin.Logger.Log("actually_apply_vest:" + actually_apply_vest);
                    ItemExpireSystem.ProcessExpireItemLogic(mercenary.CreatureData.Inventory.VestSlot, time.Time);
                }
                */

                ProcessFlipExpireItemLogic(mercenary.CreatureData.Inventory.VestStore, time.Time, actually_apply_vest);
                ProcessFlipExpireItemLogic(mercenary.CreatureData.Inventory.BackpackStore, time.Time, actually_apply_backpack);
                //ItemExpireSystem.ProcessExpireItemLogic(mercenary.CreatureData.Inventory.BackpackSlot, time.Time);

                /*
                 foreach (ItemStorage storage2 in mercenary.CreatureData.Inventory.Storages)
                {
                    //ItemExpireSystem.ProcessExpireItemLogic(storage2, time.Time);
                    ProcessFlipExpireItemLogic(storage2, time.Time, actually_apply_backpack);
                }
                 */

                foreach (ItemStorage storage3 in mercenary.CreatureData.Inventory.Slots)
                {
                    //items on slot (weapon/armor/etc slot) will count as backpack
                    ProcessFlipExpireItemLogic(storage3, time.Time, actually_apply_backpack);
                }
            }
            ItemExpireSystem.ProcessFreeze(magnumCargo.FridgeStorage, time.Time);
            foreach (BasePickupItem basePickupItem in ItemExpireSystem._itemsCache)
            {
                //Plugin.Logger.Log("actually_apply_vest:" + basePickupItem);
                ItemStorage storage4 = basePickupItem.Storage;
                ItemExpireRecord record = Data.ItemExpire.GetRecord(basePickupItem.Id, true);
                short stackCount = basePickupItem.StackCount;
                basePickupItem.Storage.Remove(basePickupItem, true);
                if (!string.IsNullOrEmpty(record.ConvertedItemId))
                {
                    BasePickupItem basePickupItem2 = SingletonMonoBehaviour<ItemFactory>.Instance.CreateForInventory(record.ConvertedItemId, false, false);
                    basePickupItem2.StackCount = stackCount;
                    if (storage4.Source == ItemStorageSource.ShipCargo)
                    {
                        if (!storage4.TryPutItem(basePickupItem2, CellPosition.Zero, false, true))
                        {
                            storage4.ExpandHeight(1);
                            storage4.AddItemAndReshuffleOptional(basePickupItem2);
                        }
                    }
                    else
                    {
                        storage4.AddItemAndReshuffleOptional(basePickupItem2);
                    }
                }
            }
            ItemExpireSystem._itemsCache.Clear();
            return false;

        }

        public static void ProcessFlipExpireItemLogic(ItemStorage storage, DateTime time, bool flip) {
            if (flip)
            {
                foreach (BasePickupItem basePickupItem in storage.Items)
                {
                    ExpireComponent expireComponent = basePickupItem.Comp<ExpireComponent>();
                    if (expireComponent != null && expireComponent.IsStarted)
                    {
                        if (!expireComponent.IsFrozen)
                        {
                            expireComponent.IsFrozen = true;
                            expireComponent.LastFreezeTickTime = time;
                        }
                        else
                        {
                            TimeSpan t = time - expireComponent.LastFreezeTickTime;
                            expireComponent.ExpireDate += t;
                            expireComponent.LastFreezeTickTime = time;
                        }
                    }
                }
            }
            else
            {
                foreach (BasePickupItem basePickupItem in storage.Items)
                {
                    ExpireComponent expireComponent = basePickupItem.Comp<ExpireComponent>();
                    if (expireComponent != null && expireComponent.IsStarted)
                    {
                        if (expireComponent.IsFrozen)
                        {
                            expireComponent.IsFrozen = false;
                            expireComponent.LastFreezeTickTime = default(DateTime);
                        }
                        if (expireComponent.ExpireDate < time)
                        {
                            ItemExpireSystem._itemsCache.Add(basePickupItem);
                        }
                    }
                }
            }
        }

    }
}
