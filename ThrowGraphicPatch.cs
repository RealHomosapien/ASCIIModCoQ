using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Console;
using XRL;
using XRL.UI;
using XRL.World;
using XRL.World.Parts;
using HarmonyLib;

namespace ASCIIMod.HarmonyPatches
{
    [HarmonyPatch(typeof(XRL.World.GameObject))]
    public class ThrowGraphicPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("RenderForUI")]
        static void Postfix(GameObject __instance, ref RenderEvent __result, string Context, bool AsIfKnown)
        {
            if (__result == null)
                return;
            //Checks settings
            if (!Options.UseTiles)
            {
                __result.Tile = null;                  // disable tile usage
                __result.RenderString = __result.RenderString ?? "?";
            }
            /*
            if (Context == "Throw" || Context == "Projectile")
            {
                __result.RenderString = "*";
            }
            */
        }
    }
}
