using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Need_Deathrest), nameof(Need_Deathrest.GetTipString))]
    public static class Need_Deathrest_GetTipString_Patch
    {
        public static void Postfix(Need_Deathrest __instance, ref string __result)
        {
            if (__instance.pawn.HasActiveGene(DefsOf.Goji_PeaceRest))
            {
                var pawn = __instance.pawn;
                var text = ("Goji_PeaceRestCap".Translate() + ": " + __instance.CurLevelPercentage.ToStringPercent()).Colorize(ColoredText.TipSectionTitleColor) + "\n";
                if (__instance.Deathresting is false)
                {
                    if (__instance.CurLevelPercentage > 0.1f)
                    {
                        var num = (__instance.CurLevelPercentage - 0.1f) / (1f / 30f);
                        text += "Goji_NextPeaceRestNeed".Translate(pawn.Named("PAWN"), "PeriodDays".Translate(num.ToString("F1")).Named("DURATION")).Resolve().CapitalizeFirst();
                    }
                    else
                    {
                        text += "Goji_PawnShouldPeaceRestNow".Translate(pawn.Named("PAWN")).CapitalizeFirst().Colorize(ColorLibrary.RedReadable);
                    }
                    text += "\n\n";
                }
                __result = text + __instance.def.description;
            }
        }
    }
}
