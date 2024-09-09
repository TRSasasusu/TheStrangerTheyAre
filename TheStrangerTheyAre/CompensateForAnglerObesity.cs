using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using OWML.Common;

namespace TheStrangerTheyAre;

[HarmonyPatch]
public class CompensateForAnglerObesity
{
    private const float ANGLER_OBESITY_CONSTANT = 600;

    [HarmonyPrefix]
    [HarmonyPatch(typeof(NoiseSensor), nameof(NoiseSensor.LateUpdate))]
    public static bool InsulatingVolume_LateUpdate_Patch(NoiseSensor __instance)
    {
        // variables
        float anglerScale = 6;
        NoiseMaker noiseMaker = null;
        float num = float.PositiveInfinity;

        TheStrangerTheyAre.WriteLine("ANGLER OBESITY PATCH RUNNING "); // debug message
        if (__instance.gameObject.name != "Scale")
        {
            return true;
        }

        List<NoiseMaker> activeNoiseMakers = NoiseMaker.GetActiveNoiseMakers();

        for (int i = 0; i < activeNoiseMakers.Count; i++)
        {
            float noiseRadius = activeNoiseMakers[i].GetNoiseRadius();
            if (noiseRadius <= 0f)
            {
                continue;
                
            }
            TheStrangerTheyAre.WriteLine("Noise Radius: " + noiseRadius, MessageType.Success); // debug message
            TheStrangerTheyAre.WriteLine("Magnitude: " + (__instance.transform.position - activeNoiseMakers[i].GetNoiseOrigin()).magnitude, MessageType.Success); // debug message
            float sqrMagnitude = (__instance.transform.position - activeNoiseMakers[i].GetNoiseOrigin()).sqrMagnitude;
            sqrMagnitude -= Mathf.Pow(ANGLER_OBESITY_CONSTANT * anglerScale, 2);
            if (!(sqrMagnitude > noiseRadius * noiseRadius))
            {
                var eventDelegate1 = (MulticastDelegate)typeof(NoiseSensor).GetField("OnAudibleNoise", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(__instance);
                if (eventDelegate1 != null)
                {
                    foreach (var handler in eventDelegate1.GetInvocationList())
                    {
                        handler.Method.Invoke(handler.Target, [activeNoiseMakers[i]]);
                    }
                }
                if (sqrMagnitude < num)
                {
                    noiseMaker = activeNoiseMakers[i];
                    num = sqrMagnitude;
                }
            }
        }
        if (noiseMaker != null)
        {
            var eventDelegate2 = (MulticastDelegate)typeof(NoiseSensor).GetField("OnClosestAudibleNoise", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(__instance);
            if (eventDelegate2 != null)
            {
                foreach (var handler in eventDelegate2.GetInvocationList())
                {
                    handler.Method.Invoke(handler.Target, [noiseMaker]);
                }
            }
        }
        return false; // return true runs original
    }
}