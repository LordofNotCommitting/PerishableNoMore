/*
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

    [HarmonyPatch(typeof(PickupItem), nameof(PickupItem.Add))]
    [HarmonyPatch(new Type[] { typeof(BasePickupItemRecord), typeof(bool) })]
    public class AddItemEditTwo
    {
        static bool enable_Mod = Plugin.ConfigGeneral.ModData.GetConfigValue<bool>("Enable_Mod", false);

        //passive effect is kept
        public static bool Prefix(BasePickupItemRecord record, bool isPrimary)
        {
            if (!enable_Mod) {
                return true;
            }

            if (typeof(BasePickupItemRecord) == typeof(ExpireComponent)) {
                return false;
            }

            return true;
        }

    }
}
*/