using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Alert_DeathrestComplete), nameof(Alert_DeathrestComplete.GetExplanation))]
    public static class Alert_DeathrestComplete_GetExplanation_Patch
    {
        public static void Postfix(Alert_DeathrestComplete __instance, ref TaggedString __result)
        {
            if (__instance.targets.All(p => p.HasActiveGene(DefsOf.Goji_PeaceRest)))
            {
                __result = "Goji_AlertPeaceRestCompleteDesc".Translate() + ":\n" + __instance.targets.Select(p => p.NameShortColored.Resolve()).ToLineList("  - ");
            }
        }
    }
}
