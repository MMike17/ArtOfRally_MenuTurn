using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace MenuTurn
{
    [HarmonyPatch(typeof(CarChooserManager), "Update")]
    static class CarChooserManager_Update_Patch
    {
        static float timer = 0;
        static float currentSpeed = 0;

        static void Prefix(CarChooserManager __instance)
        {
            if (!Main.enabled)
                return;

            if (PadManager.GetPlayer().GetAxis(69) != 0)
            {
                timer = 0;
                currentSpeed = 0;
            }
            else
                timer += Time.deltaTime;

            if (timer < Main.settings.rotationDelay)
                return;

            float rotation = Main.settings.rotationSpeed * (Main.settings.reverseRotation ? -1 : 1);
            currentSpeed = Mathf.MoveTowards(currentSpeed, rotation, Time.deltaTime / 2);

            foreach (CarMenuDisplay carMenuDisplay in __instance.AllCars)
            {
                if (carMenuDisplay.isSelected)
                    Main.SetField(__instance, "currentCarRotation", BindingFlags.Instance, carMenuDisplay.RotateCar(currentSpeed));
            }
        }

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                if (!Main.enabled)
                    yield return instruction;
                else
                    yield return new CodeInstruction(OpCodes.Ret);
            }
        }
    }
}
