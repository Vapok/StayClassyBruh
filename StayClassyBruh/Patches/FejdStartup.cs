﻿using HarmonyLib;

namespace StayClassyBruh.Patches;

public class FejdStartupPatches
{

    [HarmonyPatch(typeof(FejdStartup), nameof(FejdStartup.Awake))]
    [HarmonyAfter("org.bepinex.helpers.LocalizationManager")]
    [HarmonyBefore("org.bepinex.helpers.ItemManager")]
    public static class FejdStartupAwakePatch
    {
        static void Prefix()
        {
            StayClassyBruh.Waiter.ValheimIsAwake(true);
        }
    }

}