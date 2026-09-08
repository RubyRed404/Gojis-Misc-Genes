using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(FloatMenuOptionProvider_Deathrest), "GetSingleOptionFor")]
    public static class FloatMenuOptionProvider_Deathrest_GetSingleOptionFor_Patch
    {
        public static void Postfix(FloatMenuContext context, ref FloatMenuOption __result)
        {
            if (context.FirstSelectedPawn.HasActiveGene(DefsOf.Goji_PeaceRest) && __result?.Label == "StartDeathrest".Translate())
            {
                __result.Label = "Goji_StartPeaceRest".Translate();
            }
        }
    }
}
