using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace MenuTurn
{
    [HarmonyPatch(typeof(CarChooserManager), "Update")]
    static class CarChooserManager_Update_Patch
    {
        static void Prefix(CarChooserManager __instance)
        {
            if (!Main.enabled)
                return;

            float rotation = Main.settings.rotationSpeed * (Main.settings.reverseRotation ? -1 : 1);

            foreach (CarMenuDisplay carMenuDisplay in __instance.AllCars)
            {
                if (carMenuDisplay.isSelected)
                    Main.SetField(__instance, "currentCarRotation", BindingFlags.Instance, carMenuDisplay.RotateCar(rotation));
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
