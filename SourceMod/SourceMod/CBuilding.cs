using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework;
using ColossalFramework.Plugins;
using ColossalFramework.UI;
using ICities;
using UnityEngine;
using ColossalFramework.IO;
using ColossalFramework.Math;

namespace SourceMod
{
    public class CBuilding
    {
        public static void CreateBuilding(out ushort building, ref Randomizer randomizer, uint prefabID, Vector3 position, float angle, int length)
        {
            BuildingInfo prefab = PrefabCollection<BuildingInfo>.GetPrefab(prefabID);
            CreateBuilding(out var building2, ref randomizer, prefab, position, angle, length);
            building = building2;
        }
        public static void CreateBuilding(out ushort building, ref Randomizer randomizer, BuildingInfo info, Vector3 position, float angle, int length)
        {
            int constructionCost = info.GetConstructionCost();
            String o = ("cost: " + constructionCost);
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);
            uint buildIndex = Singleton<SimulationManager>.instance.m_currentBuildIndex; 
            BuildingInfo prefab = PrefabCollection<BuildingInfo>.GetPrefab((uint)1);
            BuildingManager buildingManager = Singleton<BuildingManager>.instance;
            buildingManager.CreateBuilding(out var building2, ref Singleton<SimulationManager>.instance.m_randomizer, info, position, angle, length, buildIndex);
            building = building2;
        }
    }
}
