using HarmonyLib;
using UnityEngine;

namespace TheStrangerTheyAre;

[HarmonyPatch(typeof(OWExtensions))]
public class FixTheDamnMod
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(OWExtensions.GetAttachedOWRigidbody), typeof(GameObject), typeof(bool))]
    private static bool GetAttachedOWRigidbody(GameObject obj, bool ignoreThisTransform, out OWRigidbody __result)
    {
        OWRigidbody owrigidbody = null;
        var transform = obj.transform;
        if (ignoreThisTransform)
        {
            transform = obj.transform.parent;
        }
        while (owrigidbody == null)
        {
            owrigidbody = transform.GetComponent<OWRigidbody>();
            if ((transform == obj.transform.root && owrigidbody == null) || owrigidbody != null)
            {
                break;
            }
            transform = transform.parent;
        }
        __result = owrigidbody;
        return false;
    }
}