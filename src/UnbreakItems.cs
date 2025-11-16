
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
    public class UnbreakItems
    {
        static bool Unbreak_Now = Plugin.ConfigGeneral.ModData.GetConfigValue<bool>("Unbreak_Now", false);

        //passive effect is kept
        public static void Postfix(ItemStorage storage, DateTime gameDate)
        {
            if (Unbreak_Now) {
                foreach (BasePickupItem basePickupItem in storage.Items)
                {

                    ExpireComponent expireComponent = basePickupItem.Comp<ExpireComponent>();
                    ItemExpireRecord record = Data.ItemExpire.GetRecord(basePickupItem.Id, true);
                    if (expireComponent != null && record != null)
                    {
                        expireComponent.ExpireDate = gameDate.AddHours((double)record.ExpiresAfterHours);
                    }
                    else if (expireComponent != null)
                    {
                        expireComponent.ExpireDate = gameDate.AddHours((double)10000.0);
                    }
                }
            }
            return;
        }

    }
}
