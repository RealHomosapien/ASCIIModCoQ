using HarmonyLib;
using XRL.World;
using XRL.World.Parts;

namespace ASCIIMod
{
    [HarmonyPatch(typeof(Physics), nameof(Physics.ProcessTakeDamage))]
    public static class ProcessTakeDamageFloatTextPatch
    {
        public struct State
        {
            public GameObject Defender;
            public Cell DefenderCell;
        }

        [HarmonyPrefix]
        public static void Prefix(Physics __instance, out State __state)
        {
            __state = new State();

            var defender = __instance?.ParentObject;
            if (defender == null)
                return;

            __state.Defender = defender;
            __state.DefenderCell = defender.GetCurrentCell();
        }

        [HarmonyPostfix]
        public static void Postfix(Event E, bool __result, State __state)
        {
            if (!__result)
                return;

            if (__state.DefenderCell == null)
                return;

            if (__state.Defender == null)
                return;

            if (ASCIIMod.CustomOPtions.ASCIIOptions.ASCIIDmgFloatText)
                return;

            var damage = E.GetParameter("Damage") as Damage;
            if (damage == null)
                return;

            if (damage.Amount <= 0)
                return;

            __state.DefenderCell.ParticleText(
                "-" + damage.Amount.ToString(),
                'r',
                false,
                1.5f,
                24f
            );
        }
    }
}