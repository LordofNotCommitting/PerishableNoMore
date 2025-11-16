/*
using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace PerishableNoMore
{

    [HarmonyPatch]
    public static class BasePickupItemCompBlinding
    {

        static bool enable_Mod = Plugin.ConfigGeneral.ModData.GetConfigValue<bool>("Enable_Mod", false);
        static MethodBase TargetMethod()
        {
            // Constructed generic: Comp<ExpireComponent>()
            return typeof(BasePickupItem)
                .GetMethod("Comp", BindingFlags.Public | BindingFlags.Instance)
                .MakeGenericMethod(typeof(ExpireComponent));
        }

        // Prefix must match the constructed type parameter:
        static bool Prefix(BasePickupItem __instance, ref ExpireComponent __result)
        {
            if (!enable_Mod) {
                return true;
            }
            __result = null;   // force null
            return false;      // skip original method
        }
    }

}

*/