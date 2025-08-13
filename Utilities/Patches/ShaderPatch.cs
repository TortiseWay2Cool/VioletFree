using HarmonyLib;
using UnityEngine;

namespace VioletFree.Utilities.Patches
{
    [HarmonyPatch(typeof(GameObject), "CreatePrimitive", MethodType.Normal)]
    public class ShaderPatch
    {
        public static void Postfix(GameObject __result)
        {
            if (__result.GetComponent<Renderer>() != null)
            {
                __result.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                __result.GetComponent<Renderer>().material.color = Color.black;
            }
        }
    }
}
