using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using XRL.UI;
using XRL;
using XRL.World.Parts;
using UnityEngine;
using System.Diagnostics;
using XRL.World;

namespace ASCIIMod.HarmonyPatches
{
    [HarmonyPatch(typeof(MissileWeapon), "FireEvent")]
    public static class MissileWeapon_FireEvent_Patch
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var code = new List<CodeInstruction>(instructions);
            var useTilesGetter = AccessTools.PropertyGetter(typeof(Options), nameof(Options.UseTiles));

           
            for (int i = 0; i < code.Count - 1; i++)
            {
                // CASE 1: HasTag or HasStringProperty
                if ((code[i].Calls(AccessTools.Method(typeof(XRL.World.GameObject), "HasStringProperty")) ||
                     code[i].Calls(AccessTools.Method(typeof(GameObjectBlueprint), "HasTag", new[] { typeof(string), typeof(bool) }))) &&
                    code[i + 1].opcode == OpCodes.Brfalse_S)//Should trigger on line IL_1174:
                {
                    // Insert: call Options.UseTiles
                    // Then: and with previous bool result
                    code.Insert(i + 1, new CodeInstruction(OpCodes.Call, useTilesGetter));
                    code.Insert(i + 2, new CodeInstruction(OpCodes.And));
                    i += 2;
                }

                // CASE 2: TryGetValue (xTags)
                if (code[i].Calls(AccessTools.Method(typeof(Dictionary<string, Dictionary<string, string>>), "TryGetValue"))) //triggers here IL_11f0:
                {
                    
                    int branchIndex = i + 1;
                    while (branchIndex < code.Count && !code[branchIndex].opcode.ToString().StartsWith("brfalse"))
                        branchIndex++;

                    if (branchIndex < code.Count)
                    {
                        code.Insert(branchIndex, new CodeInstruction(OpCodes.Call, useTilesGetter));
                        code.Insert(branchIndex + 1, new CodeInstruction(OpCodes.And));
                        i = branchIndex + 1;
                    }
                }
            }

            return code;
        }
    }
}
