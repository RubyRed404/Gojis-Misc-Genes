using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Gene_Deathrest), nameof(Gene_Deathrest.GetGizmos))]
    public static class Gene_Deathrest_GetGizmos_Patch
    {
        public static IEnumerable<Gizmo> Postfix(IEnumerable<Gizmo> __result, Gene_Deathrest __instance)
        {
            var swap = __instance.pawn.HasActiveGene(DefsOf.Goji_PeaceRest);
            foreach (var gizmo in __result)
            {
                if (swap && gizmo is Command_Action action && action.icon == Gene_Deathrest.WakeCommandTex.Texture)
                {
                    var pawn = __instance.pawn;
                    var text = "Goji_WakePeaceRestDesc".Translate(pawn.Named("PAWN"), __instance.deathrestTicks.ToStringTicksToPeriod().Named("DURATION")).Resolve() + "\n\n";
                    text = __instance.DeathrestPercent >= 1f
                        ? text + "Goji_WakePeaceRestExtraSafe".Translate(pawn.Named("PAWN")).Resolve()
                        : text + "Goji_WakePeaceRestExtraExhaustion".Translate(pawn.Named("PAWN"), __instance.MinDeathrestTicks.ToStringTicksToPeriod().Named("TOTAL")).Resolve();
                    action.defaultDesc = text;
                    var wake = action.action;
                    action.action = () =>
                    {
                        if (__instance.DeathrestPercent < 1f)
                        {
                            var warning = "Goji_WarningWakingInterruptsPeaceRest".Translate(__instance.pawn.Named("PAWN"), __instance.MinDeathrestTicks.ToStringTicksToPeriod().Named("MINDURATION"), __instance.deathrestTicks.ToStringTicksToPeriod().Named("CURDURATION")).Resolve();
                            Find.WindowStack.Add(Dialog_MessageBox.CreateConfirmation(warning, wake, destructive: true));
                        }
                        else
                        {
                            wake();
                        }
                    };
                }
                else if (swap && gizmo is Command_Toggle toggle && toggle.icon == Gene_Deathrest.AutoWakeCommandTex.Texture)
                {
                    toggle.defaultDesc = "Goji_AutoWakePeaceRestDesc".Translate(__instance.pawn.Named("PAWN")).Resolve();
                }
                yield return gizmo;
            }
        }
    }
}
