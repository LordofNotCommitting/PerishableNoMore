using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PerishableNoMore
{
    [HarmonyPatch(typeof(MagnumCargoSystem), nameof(MagnumCargoSystem.Update))]
    public static class GetFridgeData
    {
        
        public static bool fridge_setup = false;

        public static void Postfix(MagnumCargo magnumCargo, MagnumProgression magnumSpaceship, SpaceTime spaceTime)
        {
            fridge_setup = magnumSpaceship.HasStoreFridge;
            //Plugin.Logger.Log("Current fridge status:" + fridge_setup);
        }
    }
}
