using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace NoMonsters
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class NoMonsters : BaseUnityPlugin
    {
        public static bool loaded;

        private const string modGUID = "AngelMadeline.NoMonsters";

        private const string modName = "No Monsters";

        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static NoMonsters Instance;

        public static ManualLogSource mls;

        private void Awake()
        {
            mls = BepInEx.Logging.Logger.CreateLogSource(modName);
            mls.LogInfo("Loaded No Monsters.");
            harmony.PatchAll(typeof(NoMonsters));
        }

        [HarmonyPatch(typeof(RoundManager), "LoadNewLevel")]
        [HarmonyPrefix]
        private static bool NoMonstersSpawn(ref SelectableLevel newLevel)
        {
            foreach (SpawnableEnemyWithRarity Enemy in newLevel.Enemies)
            {
                Enemy.rarity = 0;
            }
            foreach (SpawnableEnemyWithRarity Enemy in newLevel.OutsideEnemies)
            {
                Enemy.rarity = 0;
            }
            mls.LogDebug($"{modName}: Removed All Enemies");
            return true;
        }
    }
}