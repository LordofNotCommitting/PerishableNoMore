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

    [HarmonyPatch(typeof(ItemExpireSystem), nameof(ItemExpireSystem.ProcessExpireItemLogic))]
    public class FreezeItem
    {
        static bool enable_Mod = Plugin.ConfigGeneral.ModData.GetConfigValue<bool>("Enable_Mod", false);

        //passive effect is kept
        public static bool Prefix(ItemStorage storage, DateTime gameDate)
        {
            if (!enable_Mod) {
                return true;
            }

            foreach (BasePickupItem basePickupItem in storage.Items)
            {
                ExpireComponent expireComponent = basePickupItem.Comp<ExpireComponent>();
                if (expireComponent != null && expireComponent.IsStarted && expireComponent.ExpireDate < gameDate)
                {

                    expireComponent.IsStarted = false;
                }
            }
            return false;
        }

    }
}
