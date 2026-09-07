using HarmonyLib;
using Verse;
using System.Reflection;
using System;
using RimWorld;

namespace GojisMiscGenes
{
    public static class VFECore_Abilities_GetRangeForPawn_Patch
    {
        public static MethodBase TargetMethod()
        {
            if (!ModLister.HasActiveModWithName("OskarPotocki.VanillaFactionsExpanded.Core"))
            {
                return null;
            }
            return AccessTools.Method(typeof(VFECore.Abilities.Ability), nameof(VFECore.Abilities.Ability.GetRangeForPawn));
        }

        public static void Postfix(object __instance, ref float __result)
        {
            if (__instance == null) return;
            var ability = __instance as VFECore.Abilities.Ability;
            var pawn = ability.pawn;
            bool hasPsycastExtension = false;
            foreach (var ext in ability.def.modExtensions)
            {
                if (ext?.GetType()?.Name == "AbilityExtension_Psycast")
                {
                    hasPsycastExtension = true;
                    break;
                }
            }

            if (hasPsycastExtension && pawn.HasActiveGene(DefsOf.Goji_Clairvoyance))
            {
                var psySensitivity = pawn.GetStatValue(StatDefOf.PsychicSensitivity);
                var bonusRange = (int)Math.Floor(psySensitivity - 1f);

                if (bonusRange > 0 && __result > 0)
                {
                    __result += bonusRange;
                }
            }
        }
    }
}
