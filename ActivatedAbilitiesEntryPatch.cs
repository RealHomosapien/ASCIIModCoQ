using ConsoleLib.Console;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using XRL.UI;
using XRL.World.Parts;
using XRL.World;
namespace ASCIIMod.HarmonyPatches
{
    
    

    [HarmonyPatch(typeof(ActivatedAbilityEntry), "GetUITile")]
    public static class Patch_ActivatedAbilityEntry_GetUITile
    {
        [HarmonyPostfix]
        public static void Postfix(ref Renderable __result, ActivatedAbilityEntry __instance)
        {
            if (!Options.UseTiles || Options.ModernUI == false)
            {
                if (__result != null)
                {
                    // Only override RenderString if empty
                    if (string.IsNullOrEmpty(__result.RenderString))
                    {
                        string icon = __instance.Icon ?? "?";
                        __result.RenderString = icon.Length > 0 ? icon : "?";
                    }

                    // Default color
                    if (string.IsNullOrEmpty(__result.ColorString))
                    {
                        __result.ColorString = "&w";
                    }
                }
            }
        }
    }
}
