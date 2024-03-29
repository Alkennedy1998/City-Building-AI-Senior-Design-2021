﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework;
using ColossalFramework.Plugins;
using ColossalFramework.UI;
using ColossalFramework.Globalization;
using ColossalFramework.Math;
using ICities;
using UnityEngine;
using ColossalFramework.IO;
using System.IO.Pipes;
using System.IO;
using System.Threading;

namespace Tutorial
{

    public class SourceMod : IUserMod
    {

        public string Name
        {
            get { return "Cities: Skylines AI"; }
        }

        public string Description
        {
            get { return "Updated 4/14/21"; }
        }
    }


    public class CustomLoader : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            GameObject go = new GameObject("test obj");
            go.AddComponent<dataReader>();
            base.OnLevelLoaded(mode);
        }
    }

    public class MouseTools : ToolBase
    {
        public static void OutputMousePos()
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 cameraDirection = Vector3.Cross(Camera.main.transform.right, Vector3.up);
            cameraDirection.Normalize();
            Ray m_mouseRay = Camera.main.ScreenPointToRay(mousePosition);
            float m_mouseRayLength = Camera.main.farClipPlane;
            Vector3 m_cameraDirection = cameraDirection;
            ToolBase.RaycastInput input = new ToolBase.RaycastInput(m_mouseRay, m_mouseRayLength);
            if (ToolBase.RayCast(input, out var output))
            {
                Vector3 op = output.m_hitPos;
                String o = ("mousepos " + op);
                DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);
            }
            //int a = 0;
            //int pop = (int)Singleton<DistrictManager>.instance.m_districts.m_buffer[a].m_populationData.m_finalCount;


        }
    }

    public class performance_measures
    {
        public uint population = 0;
        public int happiness = 0;
        public int life_span = 0;
        public int sheltered = 0;
        public int sick_count = 0;

        // infrastructure
        public int electricity_consumption = 0;
        public int water_consumption = 0;
        public int garbage = 0;

        // economy
        public int unemployment = 0;
        public int income_accumulation = 0;

        // society
        public int criminal_amount = 0;
        public int extra_criminals = 0;

        // education
        public int education_1_capacity = 0;
        public int education_1_need = 0;
        public int education_1_rate = 0;
        public int education_2_capacity = 0;
        public int education_2_need = 0;
        public int education_2_rate = 0;
        public int education_3_capacity = 0;
        public int education_3_need = 0;
        public int education_3_rate = 0;

        // environment
        public int water_pollution = 0;
        public int ground_pollution = 0;

        // demand
        public int commercial_demand;
        public int residential_demand;
        public int workplace_demand;
        public int actual_commercial_demand;
        public int actual_residential_demand;
        public int actual_workplace_demand;

        public performance_measures()
        {

        }

        public void get_performance_measures()
        {
            var _districtManager = Singleton<DistrictManager>.instance;
            var _zoneManager = Singleton<ZoneManager>.instance;
            var _economyManager = Singleton<EconomyManager>.instance;

            // health
            population = _districtManager.m_districts.m_buffer[0].m_populationData.m_finalCount;
            happiness = _districtManager.m_districts.m_buffer[0].m_finalHappiness;
            life_span = _districtManager.m_districts.m_buffer[0].GetAverageLifespan();
            sheltered = _districtManager.m_districts.m_buffer[0].GetShelterCitizenNumber();
            sick_count = _districtManager.m_districts.m_buffer[0].GetSickCount();

            // infrastructure
            electricity_consumption = _districtManager.m_districts.m_buffer[0].GetElectricityConsumption();
            water_consumption = _districtManager.m_districts.m_buffer[0].GetWaterConsumption();
            garbage = _districtManager.m_districts.m_buffer[0].GetGarbageAmount();

            // economy
            unemployment = _districtManager.m_districts.m_buffer[0].GetUnemployment();
            income_accumulation = _districtManager.m_districts.m_buffer[0].GetIncomeAccumulation();

            // society
            criminal_amount = _districtManager.m_districts.m_buffer[0].GetCriminalAmount();
            extra_criminals = _districtManager.m_districts.m_buffer[0].GetExtraCriminals();

            // education
            education_1_capacity = _districtManager.m_districts.m_buffer[0].GetEducation1Capacity();
            education_1_need = _districtManager.m_districts.m_buffer[0].GetEducation1Need();
            education_1_rate = _districtManager.m_districts.m_buffer[0].GetEducation1Rate();
            education_2_capacity = _districtManager.m_districts.m_buffer[0].GetEducation2Capacity();
            education_2_need = _districtManager.m_districts.m_buffer[0].GetEducation2Need();
            education_2_rate = _districtManager.m_districts.m_buffer[0].GetEducation2Rate();
            education_3_capacity = _districtManager.m_districts.m_buffer[0].GetEducation3Capacity();
            education_3_need = _districtManager.m_districts.m_buffer[0].GetEducation3Need();
            education_3_rate = _districtManager.m_districts.m_buffer[0].GetEducation3Rate();

            // environment
            water_pollution = _districtManager.m_districts.m_buffer[0].GetWaterPollution();
            ground_pollution = _districtManager.m_districts.m_buffer[0].GetGroundPollution();

            // demand
            commercial_demand = _zoneManager.m_commercialDemand;
            residential_demand = _zoneManager.m_residentialDemand;
            workplace_demand = _zoneManager.m_workplaceDemand;
            actual_commercial_demand = _zoneManager.m_actualCommercialDemand;
            actual_residential_demand = _zoneManager.m_actualResidentialDemand;
            actual_workplace_demand = _zoneManager.m_actualWorkplaceDemand;
        }

        public void print_performance_measures()
        {
            string message1 =
                    "HEALTH" +
                    "\npopulation = " + population.ToString() +
                    "\nhappiness = " + happiness.ToString() +
                    "\nlife span = " + life_span.ToString() +
                    "\nsheltered citizens = " + sheltered.ToString() +
                    "\nsick count = " + sick_count.ToString() +
                    "\nINFRASTRUCTURE" +
                    "\nelectricity consumption = " + electricity_consumption.ToString() +
                    "\nwater consumption = " + water_consumption.ToString() +
                    "\ngarbage amount = " + garbage.ToString() +
                    "\nECONOMY" +
                    "\nunemployment = " + unemployment.ToString() +
                    "\nincome accumulation = " + income_accumulation.ToString() +
                    "\nSOCIETY" +
                    "\ncriminal amount = " + criminal_amount.ToString() +
                    "\nextra criminals = " + extra_criminals.ToString() +
                    "\nEDUCATION" +
                    "\neducation 1 capacity = " + education_1_capacity.ToString() +
                    "\neducation 1 need = " + education_1_need.ToString() +
                    "\neducation 1 rate = " + education_1_rate.ToString() +
                    "\neducation 2 capacity = " + education_2_capacity.ToString() +
                    "\neducation 2 need = " + education_2_need.ToString() +
                    "\neducation 2 rate = " + education_2_rate.ToString() +
                    "\neducation 3 capacity = " + education_3_capacity.ToString() +
                    "\neducation 3 need = " + education_3_need.ToString() +
                    "\neducation 3 rate = " + education_3_rate.ToString() +
                    "\nwater pollution = " + water_pollution.ToString() +
                    "\nground pollution = " + ground_pollution.ToString() + 
                    "\nDEMAND" +
                    "\nresidential demand = " + residential_demand.ToString() +
                    "\ncommercial demand = " + commercial_demand.ToString() +
                    "\nworkplace demand = " + workplace_demand.ToString() +
                    "\nactual_residential_demand = " + actual_residential_demand.ToString() +
                    "\nactual_commercial_demand = " + actual_commercial_demand.ToString() +
                    "\nactual_workplace_demand = " + actual_workplace_demand.ToString();
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, message1);
        }

        public string performance_measures_cs()
        {
            //length = 28
            string s1 =
                    population.ToString() +
                    ", " + happiness.ToString() +
                    ", " + life_span.ToString() +
                    ", " + sheltered.ToString() +
                    ", " + sick_count.ToString() +
                    ", " + electricity_consumption.ToString() +
                    ", " + water_consumption.ToString() +
                    ", " + garbage.ToString() +
                    ", " + unemployment.ToString() +
                    ", " + income_accumulation.ToString() + 
                    ", " + criminal_amount.ToString() +
                    ", " + extra_criminals.ToString() +
                    ", " + education_1_capacity.ToString() +
                    ", " + education_1_need.ToString() +
                    ", " + education_1_rate.ToString() +
                    ", " + education_2_capacity.ToString() +
                    ", " + education_2_need.ToString() +
                    ", " + education_2_rate.ToString() +
                    ", " + education_3_capacity.ToString() +
                    ", " + education_3_need.ToString() +
                    ", " + education_3_rate.ToString() +
                    ", " + water_pollution.ToString() +
                    ", " + ground_pollution.ToString() +
                    ", " + residential_demand.ToString() +
                    ", " + commercial_demand.ToString() +
                    ", " + workplace_demand.ToString() +
                    ", " + actual_residential_demand.ToString() +
                    ", " + actual_commercial_demand.ToString() +
                    ", " + actual_workplace_demand.ToString();

            return s1;
        }
    }

    public class dataReader : MonoBehaviour
    {
        int i = 0;

        private ulong[] m_collidingSegments1 = new ulong[576];
        private ulong[] m_collidingSegments2 = new ulong[576];
        private ulong[] m_collidingBuildings1 = new ulong[768];
        private ulong[] m_collidingBuildings2 = new ulong[768];
        private int m_collidingDepth = 0;

        private long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        private long msperticks = 200;

        private long datatick = 0;

        public float m_brushSize = 200f;

        private bool running = true;

        public String[] prefabNames = {
            "Coal Power Plant",
            "Wind Turbine",
            "Water Drain Pipe",
            "Water Pumping Station",
            "Water Tower",

            "Elementary School",
            "Medical Clinic",
            "Landfill Site",

            "Fire House",

            "High School",
            "Library 01",

            "Cemetery",
            "Child Health Center 01",
            "Eldercare 01",
            "Advanced Wing Turbine",
            "Bus Depot",
            "Police Station",

            "Oil Power Plant",
            "Hospital",
            "Fire Station",
            "Police Headquarters",

            "Combustion Plant",
            "University"
        };

        bool test_building = false;
        bool test_action_parsing = false;
        bool print_delta = false;
        bool print_mouse = false;

        void Start()
        {
            DateTime time = DateTime.Now;
            String o = ("mod start at " + time.ToString());
            String test_change = "test change";
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);
            Thread dataTransferThread = new Thread(sendData);
            dataTransferThread.Start();
        }

        void Update()
        {
            if (running)
            {
                long newmilliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                long delta = newmilliseconds - milliseconds;
                if (delta >= msperticks)
                {
                    if (print_delta)
                    {
                        DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, delta.ToString());
                    }
                    if (print_mouse)
                    {
                        MouseTools.OutputMousePos();
                    }
                    milliseconds = newmilliseconds;
                    datatick += 1;
                }

                if (i == 0)
                {
                    GetMoney(1000000000);
                    Singleton<SimulationManager>.instance.SelectedSimulationSpeed = 3;
                    //testfunction();
                }
                i++;
            }



        }
        void testfunction()
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "in test_function()");
            if (test_building)
            {
                DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "update check");
                Vector3 loc2 = new Vector3(50, 50, 0);
                Vector3 loc3 = new Vector3(-50, -50, 0);
                string building = "cemetery";
                int max = 638;
                //0 = Medical Clinic
                //1 = Hosptial
                //2 = Medical Center
                //3 = Crematory
                //4 = Cemetary
                //5 = Eldercare 01
                //6 = Child Health Center 01
                //7 H3 4x3 Shop03
                //8 H1 1x1 Shop07
                //9 H13 3x4 Shop04

                BuildingManager buildingManager = Singleton<BuildingManager>.instance;
                Vector3 loc1 = new Vector3(0, 0, 0);
                Vector3 loc4 = new Vector3(100, 0, 0);
                Vector3 loc5 = new Vector3(50, 0, 0);


                float angle = 0;
                int length = 0;

                BuildingInfo prefab = PrefabCollection<BuildingInfo>.GetPrefab((uint)1);
                int givemoney = -35000000;
                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Construction, givemoney, prefab.m_class);
                long before = Singleton<EconomyManager>.instance.InternalCashAmount;
                String o = ("money before: " + before);
                DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);
                //CreateBuilding(out var building2, ref Singleton<SimulationManager>.instance.m_randomizer, prefab, loc4, angle, length);
                //CreateBuilding(out var building3, ref Singleton<SimulationManager>.instance.m_randomizer, prefab, loc4, angle, length);
                //CreateBuilding(out var building4, ref Singleton<SimulationManager>.instance.m_randomizer, prefab, loc5, angle, length);
                //GetWaterSources();

                Vector3 v = new Vector3(0, 0, 8);
                Vector3 v2 = new Vector3(1, 0, 24);
                Vector3 v3 = new Vector3(2, 0, 40);
                Vector3 v4 = new Vector3(3, 0, -8);
                Vector3 v5 = new Vector3(4, 0, -24);
                Vector3 v6 = new Vector3(5, 0, -40);
                Vector3 v7 = new Vector3(6, 0, -56);
                Vector3 v8 = new Vector3(7, 0, -72);
                Vector3 v9 = new Vector3(8, 0, -150);

                //Ray m_mouseRay = Camera.main.ScreenPointToRay(mousePosition);


                float distsq = 1000;
                ushort block = 0;
                //Singleton<NetManager>.instance.m_segments.m_buffer[m_closeSegGetClosestZoneBlock(v, ref distsq, ref block);
                int a = Singleton<NetManager>.instance.m_segments.m_buffer.Length;
                //Singleton<NetManager>.instance.GetClosestSegments(v, m_closeSegments, out m_closeSegmentCount);
                //o = ("segment count: " + a);
                //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);

                int newzones = ZoneArea(v, ItemClass.Zone.CommercialLow, 10);
                newzones = ZoneArea(v9, ItemClass.Zone.CommercialLow, 30);
                newzones = ZoneArea(v, ItemClass.Zone.ResidentialLow, 20);

                o = ("painted zones: " + newzones);
                DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);

                //RoadAI rai = Singleton<RoadAI>.instance;
            }
            else if (test_action_parsing)
            {
                DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "conducting parsing test");
                string test_string = "createbuilding|1|0.0,10.0,100.0|3.14|0";
                string test_string2 = "createzone|722.0,3.4,98.0|4|30";
                action_parser ap = new action_parser();
                ap.parse_actions(test_string2);
            }
        }

        public void GetMoney(int money)
        {
            BuildingInfo prefab = PrefabCollection<BuildingInfo>.GetPrefab((uint)1);
            int givemoney = 0 - money;
            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Construction, givemoney, prefab.m_class);
        }

        public bool CreateBuilding(out ushort building, ref Randomizer randomizer, uint prefabID, Vector3 position, float angle, int length)
        {
            String o = ("prefabid: " + prefabID + " pos: " + position + " angle: " + angle + " length: " + length);
            // DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);
            BuildingInfo prefab = PrefabCollection<BuildingInfo>.GetPrefab(prefabID);
            bool f = CreateBuilding(out var building2, ref randomizer, prefab, position, angle, length);
            building = building2;
            return f;
        }

        public bool CreateBuilding(out ushort building, ref Randomizer randomizer, BuildingInfo info, Vector3 position, float angle, int length)
        {
            int constructionCost = info.GetConstructionCost();
            String o = ("cost: " + constructionCost);
            //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);
            if (Singleton<EconomyManager>.instance.PeekResource(EconomyManager.Resource.Construction, constructionCost) != constructionCost)
            {
                o = ("not enough money");
                DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);
                building = 0;
                return false;
            }


            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Construction, constructionCost, info.m_class);
            long after = Singleton<EconomyManager>.instance.InternalCashAmount;
            o = ("money after: " + after);
            //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);

            ClearColliding();
            BeginColliding(out var collidingSegments, out var collidingBuildings);

            ToolBase.ToolErrors toolErrors;
            o = ("mid coliding");
            //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);
            int relocating = 0;
            Vector3 position1 = position;
            float minY = 0;
            float buildingY = position.y;
            bool test = false;
            toolErrors = BuildingTool.CheckSpace(info, info.m_placementMode, relocating, position1, minY, buildingY + info.m_collisionHeight, angle, info.m_cellWidth, info.m_cellLength, test, collidingSegments, collidingBuildings);

            EndColliding();

            o = ("after coliding");
            //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);

            o = ("errors:" + toolErrors.ToString());
            //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);

            Vector3 pos5;
            Vector3 dir;
            bool outbool = BuildingTool.SnapToPath(position, out pos5, out dir, 400f, roadOnly: true);
            if (!outbool)
            {
                o = ("it returned false");
                //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);
            }
            if (outbool)
            {
                o = ("pos5:" + pos5 + "dir: " + dir);
                angle = Mathf.Atan2(0f - dir.x, dir.z);
                Vector3 position2 = pos5 - dir * ((float)info.m_cellLength * 4f);
                position = position2;
                //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);
            }
            if (toolErrors == ToolBase.ToolErrors.None)
            {
                uint buildIndex = Singleton<SimulationManager>.instance.m_currentBuildIndex;
                BuildingInfo prefab = PrefabCollection<BuildingInfo>.GetPrefab((uint)1);
                BuildingManager buildingManager = Singleton<BuildingManager>.instance;
                buildingManager.CreateBuilding(out var building2, ref Singleton<SimulationManager>.instance.m_randomizer, info, position, angle, length, buildIndex);
                building = building2;
                o = ("building building");
                //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);
                return true;
            }
            building = 0;
            return false;
        }

        public void ClearColliding()
        {
            int num = m_collidingSegments1.Length;
            for (int i = 0; i < num; i++)
            {
                m_collidingSegments1[i] = 0uL;
                m_collidingSegments2[i] = 0uL;
            }
            num = m_collidingBuildings1.Length;
            for (int j = 0; j < num; j++)
            {
                m_collidingBuildings1[j] = 0uL;
                m_collidingBuildings2[j] = 0uL;
            }
        }

        public void BeginColliding(out ulong[] collidingSegments, out ulong[] collidingBuildings)
        {
            if (m_collidingDepth++ == 0)
            {
                ResetColliding();
            }
            collidingSegments = m_collidingSegments1;
            collidingBuildings = m_collidingBuildings1;
        }

        public void ResetColliding()
        {
            int num = m_collidingSegments1.Length;
            for (int i = 0; i < num; i++)
            {
                m_collidingSegments1[i] = 0uL;
            }
            num = m_collidingBuildings1.Length;
            for (int j = 0; j < num; j++)
            {
                m_collidingBuildings1[j] = 0uL;
            }
        }

        public void EndColliding()
        {
            if (--m_collidingDepth == 0)
            {
                int num = m_collidingSegments1.Length;
                for (int i = 0; i < num; i++)
                {
                    m_collidingSegments2[i] = m_collidingSegments1[i];
                }
                num = m_collidingBuildings1.Length;
                for (int j = 0; j < num; j++)
                {
                    m_collidingBuildings2[j] = m_collidingBuildings1[j];
                }
            }
        }


        public void GetWaterSources()
        {
            FastList<WaterSource> watersources = Singleton<TerrainManager>.instance.WaterSimulation.m_waterSources;
            int it = 0;
            foreach (WaterSource i in watersources)
            {

                /*
                 * TYPE_NONE 0
                 * TYPE_NATURAL 1
                 * TYPE_FACILITY 2
                 * TYPE_CLEANER 3
                 * 
                 * */
                //if(i.m_type == WaterSource.TYPE_FACILITY)
                {
                    String o = ("watersource " + it + " at inpos: " + i.m_inputPosition + " and outpos: " + i.m_outputPosition + " with inrate/outrate/flow: " + i.m_inputRate + "/" + i.m_outputRate + "/" + i.m_flow + "and target: " + i.m_target);
                    DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);
                }

                WaterManager.Node[] nodeData = Singleton<WaterManager>.instance.m_nodeData;


                it += 1;
            }
        }

        public int ZoneArea(Vector3 inposition, ItemClass.Zone m_zone, int quantity)
        {
            if (quantity < 1) return 0;

            int zones = 0;
            float brushsize = 50f;
            float maxBrushSize = 5000f;
            Vector3 startZone;
            bool flag1 = false;
            while ((flag1 = ApplyClosest(inposition, m_zone, brushsize, out startZone)) == false && brushsize <= maxBrushSize)
            {
                brushsize *= 2;
                //String o = ("brushsize: " + brushsize);
                //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);
            }
            if (flag1 == false)
            {
                //String o = ("returning on flag1 check");
                //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);
                return 0;
            }

            if (startZone.magnitude == 0)
            {
                //String o = ("returning on magnitude check");
                //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);
                return 0;
            }
            zones += 1;
            brushsize = 50f;
            for (int i = 1; i < quantity; i++)
            {
                bool flag2 = false;
                Vector3 pos;

                while ((flag2 = ApplyClosest(inposition, m_zone, brushsize, out pos)) == false && brushsize <= maxBrushSize)
                {
                    brushsize *= 2;
                    //String o = ("brushsize: " + brushsize);
                    //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);
                }
                if (flag2 == true) zones += 1;
                else
                {
                    //String o = ("returning on flag2 check");
                    //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);
                    return zones;
                }
            }

            return zones;

        }

        private bool ApplyClosest(Vector3 inposition, ItemClass.Zone m_zone, float brushsize, out Vector3 appliedPos)
        {
            float num = brushsize * 0.5f;
            Vector3 mousePosition = inposition;
            float num2 = mousePosition.x - num;
            float num3 = mousePosition.z - num;
            float num4 = mousePosition.x + num;
            float num5 = mousePosition.z + num;


            ZoneManager instance = Singleton<ZoneManager>.instance;
            int num6 = Mathf.Max((int)((num2 - 46f) / 64f + 75f), 0);
            int num7 = Mathf.Max((int)((num3 - 46f) / 64f + 75f), 0);
            int num8 = Mathf.Min((int)((num4 + 46f) / 64f + 75f), 149);
            int num9 = Mathf.Min((int)((num5 + 46f) / 64f + 75f), 149);

            //String o = ("1: " + num + " 2: " + num2 + " 3: " + num3 + " 4: " + num4 + " 5: " + num5 + " 6: " + num6 + " 7: " + num7 + " 8: " + num8 + " 9: " + num9);
            //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);

            bool flag1 = false;
            ushort num10min = 0;
            float distmin = 10000000;
            int mini = 0;
            int minj = 0;
            Vector3 minpos = new Vector3(0, 0, 0);
            for (int i = num7; i <= num9; i++)
            {
                for (int j = num6; j <= num8; j++)
                {
                    ushort num10 = instance.m_zoneGrid[i * 150 + j];

                    int num11 = 0;
                    while (num10 != 0)
                    {
                        //o = ("num10" + num10 + "i = " + i + ", j = " + j + ", pos: " + instance.m_blocks.m_buffer[num10].m_position.x + ", " + instance.m_blocks.m_buffer[num10].m_position.z);
                        //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);
                        Vector3 position = instance.m_blocks.m_buffer[num10].m_position;
                        float num12 = Mathf.Max(Mathf.Max(num2 - 46f - position.x, num3 - 46f - position.z), Mathf.Max(position.x - num4 - 46f, position.z - num5 - 46f));
                        if (num12 < 0f)
                        {
                            int icheck;
                            int jcheck;
                            Vector3 minposcheck;
                            float a = CheckBlock(num10, ref instance.m_blocks.m_buffer[num10], mousePosition, num, true, m_zone, out icheck, out jcheck, out minposcheck);
                            //o = ("a = " + a + ", min = " + distmin);
                            //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);
                            if (a < distmin)
                            {
                                //o = ("1 = " + instance.m_blocks.m_buffer[num10].m_zone1 + " 2 = " + instance.m_blocks.m_buffer[num10].m_zone2);
                                //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);

                                flag1 = true;
                                num10min = num10;
                                mini = icheck;
                                minj = jcheck;
                                minpos = minposcheck;
                                distmin = a;
                            }
                        }
                        //o = ("num10: "+num10 + ", nextgrid: " + instance.m_blocks.m_buffer[num10].m_nextGridBlock);
                        //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);

                        num10 = instance.m_blocks.m_buffer[num10].m_nextGridBlock;
                        if (++num11 >= 49152)
                        {
                            CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                            break;
                        }
                    }
                }
            }
            if (flag1 == true)
            {
                //o = ("num10min" + num10min);
                //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);
                float b = 7f;
                ApplyBrushClose(num10min, ref instance.m_blocks.m_buffer[num10min], instance.m_blocks.m_buffer[num10min].m_position, b, true, m_zone, mini, minj);

            }
            appliedPos = minpos;
            return flag1;

        }

        private void ApplyBrushClose(ushort blockIndex, ref ZoneBlock data, Vector3 position, float brushRadius, bool zoning, ItemClass.Zone m_zone, int i, int j)
        {
            //String o = ("pos: " + position.x + ", " + position.z);
            //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);

            bool m_zoning = zoning;
            bool m_dezoning = !zoning;

            bool flag = false;

            if (m_zoning)
            {
                if ((m_zone == ItemClass.Zone.Unzoned || data.GetZone(j, i) == ItemClass.Zone.Unzoned) && data.SetZone(j, i, m_zone))
                {
                    flag = true;
                }
            }
            else if (m_dezoning && data.SetZone(j, i, ItemClass.Zone.Unzoned))
            {
                flag = true;
            }

            //o = ("flag" + flag);
            //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);
            if (flag)
            {
                data.RefreshZoning(blockIndex);
                if (m_zoning)
                {
                    UsedZone(m_zone);
                }
            }

        }



        private float CheckBlock(ushort blockIndex, ref ZoneBlock data, Vector3 position, float brushRadius, bool zoning, ItemClass.Zone m_zone, out int iout, out int jout, out Vector3 minpos)
        {
            bool m_zoning = zoning;
            bool m_dezoning = !zoning;

            Vector3 a = data.m_position - position;
            if (Mathf.Abs(a.x) > 46f + brushRadius || Mathf.Abs(a.z) > 46f + brushRadius)
            {
                iout = 0;
                jout = 0;
                minpos = new Vector3(0, 0, 0);
                return 100000000;
            }
            int num = (int)((data.m_flags & 0xFF00) >> 8);
            Vector3 a2 = new Vector3(Mathf.Cos(data.m_angle), 0f, Mathf.Sin(data.m_angle)) * 8f;
            Vector3 a3 = new Vector3(a2.z, 0f, 0f - a2.x);
            bool flag = false;
            float minnum2 = 100000000;
            int mini = 0;
            int minj = 0;
            Vector3 minv = new Vector3(0, 0, 0);

            for (int i = 0; i < num; i++)
            {
                Vector3 b = ((float)i - 3.5f) * a3;
                for (int j = 0; j < 4; j++)
                {
                    Vector3 b2 = ((float)j - 3.5f) * a2;
                    Vector3 vector = a + b2 + b;
                    float num2 = vector.x * vector.x + vector.z * vector.z;
                    if (!(num2 <= brushRadius * brushRadius))
                    {
                        continue;
                    }
                    if (m_zoning)
                    {
                        //if ((m_zone == ItemClass.Zone.Unzoned || data.GetZone(j, i) == ItemClass.Zone.Unzoned) && CheckBlock(j,i,m_zone,data))
                        if ((m_zone == ItemClass.Zone.Unzoned || data.GetZone(j, i) == ItemClass.Zone.Unzoned) && CheckBlock(j, i, m_zone, data))
                        {
                            if (num2 < minnum2)
                            {
                                mini = i;
                                minj = j;
                                minnum2 = num2;
                                minv = vector + position;
                            }
                            flag = true;
                        }
                    }
                    else if (m_dezoning && data.SetZone(j, i, ItemClass.Zone.Unzoned))
                    {
                        flag = true;
                    }
                }
            }
            if (flag == true)
            {
                iout = mini;
                jout = minj;
                minpos = minv;
                return minnum2;
            }
            iout = 0;
            jout = 0;
            minpos = new Vector3(0, 0, 0);
            return 100000000;
        }

        private bool CheckBlock(int x, int z, ItemClass.Zone zone, ZoneBlock zb)
        {
            if (zone == ItemClass.Zone.Distant)
            {
                zone = ItemClass.Zone.Unzoned;
            }
            int num = (z << 3) | ((x & 1) << 2);
            ulong num2 = (ulong)(~(15L << num));
            if (x < 2)
            {
                ulong num3 = (zb.m_zone1 & num2) | (ulong)((long)zone << num);
                if (num3 != zb.m_zone1)
                {
                    return true;
                }
            }
            else if (x < 4)
            {
                ulong num4 = (zb.m_zone2 & num2) | (ulong)((long)zone << num);
                if (num4 != zb.m_zone2)
                {
                    return true;
                }
            }
            return false;
        }

        private void ApplyBrush(Vector3 inposition, ItemClass.Zone m_zone)
        {

            float num = m_brushSize * 0.5f;
            Vector3 mousePosition = inposition;
            float num2 = mousePosition.x - num;
            float num3 = mousePosition.z - num;
            float num4 = mousePosition.x + num;
            float num5 = mousePosition.z + num;


            ZoneManager instance = Singleton<ZoneManager>.instance;
            int num6 = Mathf.Max((int)((num2 - 46f) / 64f + 75f), 0);
            int num7 = Mathf.Max((int)((num3 - 46f) / 64f + 75f), 0);
            int num8 = Mathf.Min((int)((num4 + 46f) / 64f + 75f), 149);
            int num9 = Mathf.Min((int)((num5 + 46f) / 64f + 75f), 149);

            //String o = ("1: " + num + " 2: " + num2 + " 3: " + num3 + " 4: " + num4 + " 5: " + num5 + " 6: " + num6 + " 7: " + num7 + " 8: " + num8 + " 9: " + num9);
            //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);

            for (int i = num7; i <= num9; i++)
            {
                for (int j = num6; j <= num8; j++)
                {
                    ushort num10 = instance.m_zoneGrid[i * 150 + j];
                    int num11 = 0;
                    while (num10 != 0)
                    {
                        Vector3 position = instance.m_blocks.m_buffer[num10].m_position;
                        float num12 = Mathf.Max(Mathf.Max(num2 - 46f - position.x, num3 - 46f - position.z), Mathf.Max(position.x - num4 - 46f, position.z - num5 - 46f));
                        if (num12 < 0f)
                        {
                            ApplyBrush(num10, ref instance.m_blocks.m_buffer[num10], mousePosition, num, true, m_zone);
                        }
                        num10 = instance.m_blocks.m_buffer[num10].m_nextGridBlock;
                        if (++num11 >= 49152)
                        {
                            CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                            break;
                        }
                    }
                }
            }
        }

        private void ApplyBrush(ushort blockIndex, ref ZoneBlock data, Vector3 position, float brushRadius, bool zoning, ItemClass.Zone m_zone)
        {
            String o = ("pos: " + position.x + ", " + position.z);
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);

            bool m_zoning = zoning;
            bool m_dezoning = !zoning;

            Vector3 a = data.m_position - position;
            if (Mathf.Abs(a.x) > 46f + brushRadius || Mathf.Abs(a.z) > 46f + brushRadius)
            {
                return;
            }
            int num = (int)((data.m_flags & 0xFF00) >> 8);
            Vector3 a2 = new Vector3(Mathf.Cos(data.m_angle), 0f, Mathf.Sin(data.m_angle)) * 8f;
            Vector3 a3 = new Vector3(a2.z, 0f, 0f - a2.x);
            bool flag = false;
            for (int i = 0; i < num; i++)
            {
                Vector3 b = ((float)i - 3.5f) * a3;
                for (int j = 0; j < 4; j++)
                {
                    Vector3 b2 = ((float)j - 3.5f) * a2;
                    Vector3 vector = a + b2 + b;
                    float num2 = vector.x * vector.x + vector.z * vector.z;
                    if (!(num2 <= brushRadius * brushRadius))
                    {
                        continue;
                    }
                    if (m_zoning)
                    {
                        if ((m_zone == ItemClass.Zone.Unzoned || data.GetZone(j, i) == ItemClass.Zone.Unzoned) && data.SetZone(j, i, m_zone))
                        {
                            flag = true;
                        }
                    }
                    else if (m_dezoning && data.SetZone(j, i, ItemClass.Zone.Unzoned))
                    {
                        flag = true;
                    }
                }
            }
            o = ("flag" + flag);
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, o);
            if (flag)
            {
                data.RefreshZoning(blockIndex);
                if (m_zoning)
                {
                    UsedZone(m_zone);
                }
            }
        }

        private void UsedZone(ItemClass.Zone zone)
        {
            if (zone != ItemClass.Zone.None)
            {
                ZoneManager instance = Singleton<ZoneManager>.instance;
                instance.m_zonesNotUsed.Disable();
                instance.m_zoneNotUsed[(int)zone].Disable();
                switch (zone)
                {
                    case ItemClass.Zone.ResidentialLow:
                    case ItemClass.Zone.ResidentialHigh:
                        instance.m_zoneDemandResidential.Deactivate();
                        break;
                    case ItemClass.Zone.CommercialLow:
                    case ItemClass.Zone.CommercialHigh:
                        instance.m_zoneDemandCommercial.Deactivate();
                        break;
                    case ItemClass.Zone.Industrial:
                    case ItemClass.Zone.Office:
                        instance.m_zoneDemandWorkplace.Deactivate();
                        break;
                }
            }
        }

        void sendData()
        {


            long lasttick = datatick;
            NamedPipeClientStream client = new NamedPipeClientStream("NP1");
            try
            {
                client.Connect(10000);
            }
            catch(System.TimeoutException ex)
            {
                client = new NamedPipeClientStream("NP2");
                client.Connect();
            }
            performance_measures pm = new performance_measures();
            action_parser ap = new action_parser();
            bool notexiting = true;
            while (notexiting)
            {
                while (datatick == lasttick) ;
                try
                {
                    
                    pm.get_performance_measures();

                    string o = pm.performance_measures_cs();
                    var buf = Encoding.ASCII.GetBytes(o);
                    client.Write(BitConverter.GetBytes(buf.Length), 0, 4);
                    client.Write(buf, 0, buf.Length);

                    byte[] leng = new byte[4];

                    client.Read(leng, 0, 4);

                    int len = BitConverter.ToInt32(leng,0);

                    byte[] inbuffer = new byte[1024];
                    client.Read(inbuffer, 0, len);
                    String st = System.Text.Encoding.ASCII.GetString(inbuffer);

                    //String op = "read: " + st;
                    //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, op);
                    ap.parse_actions(st);
                    if (st != null && st.Contains("reset"))
                    {
                        //client.Close();
                        //client.Dispose();
                        //notexiting = false;
                        reset();
                    }

                   
                    datatick = lasttick;
                }
                catch (EndOfStreamException)
                {
                    break;
                }
            }
            Console.WriteLine("Client disconnected");
            //client.Close();
            //client.Dispose();
        }

        void reset()
        {
            LoadPanel p = Singleton<LoadPanel>.instance;
            p.QuickLoad();
            
            //running = false;
        }
    }

    class action_parser : dataReader
    {
        public char token_delimiter = '|';
        public char vector_delimiter = ',';
        public bool debug = false;

        public void parse_actions(string input_string) // run functions based on provided input string
        {
            List<string> tokens = split_string_to_tokens(input_string, token_delimiter);
            run_tokens(tokens);
        }

        public List<string> split_string_to_tokens(string input_string, char delimiter)
        {
            List<string> tokenList = new List<string>();
            int token_start_index = 0;
            for (int counter = 0; counter < input_string.Length; counter++)
            {
                if (input_string[counter] == delimiter)
                {
                    if (debug)
                        DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "found token between " + token_start_index.ToString() + " and " + counter.ToString());
                    tokenList.Add(input_string.Substring(token_start_index, counter - token_start_index));
                    if (debug)
                        DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "extracted " + tokenList[tokenList.Count() - 1]);
                    token_start_index = counter + 1;
                }
            }
            if (input_string[input_string.Length - 1] != delimiter)
            {
                if (debug)
                    DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "found token after " + token_start_index.ToString());
                tokenList.Add(input_string.Substring(token_start_index));
                if (debug)
                    DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "extracted " + tokenList[tokenList.Count() - 1]);
            }
            return tokenList;
        }

        Vector3 string_to_vector3(string input_string) // convert string to vector3
        {
            Vector3 new_vector;
            new_vector.x = 0;
            new_vector.y = 0;
            new_vector.z = 0;
            List<string> EltList = new List<string>();
            int elt_start_index = 0;
            for (int counter = 0; counter < input_string.Length; counter++)
            {
                if (input_string[counter] == vector_delimiter)
                {
                    EltList.Add(input_string.Substring(elt_start_index, counter - elt_start_index));
                    elt_start_index = counter + 1;
                }
            }
            if (input_string[input_string.Length - 1] != vector_delimiter)
            {
                EltList.Add(input_string.Substring(elt_start_index));
            }

            new_vector.x = float.Parse(EltList[0]);
            new_vector.y = float.Parse(EltList[1]);
            new_vector.z = float.Parse(EltList[2]);
            if (debug)
            {
                DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "converting string ( " + input_string + " ) to vector ( " + new_vector.x.ToString() + ", " + new_vector.y.ToString() + ", " + new_vector.z.ToString() + " )");
            }
            return new_vector;
        }
        public void run_tokens(List<string> tokens) // run function based on provided token array
        {
            if (debug)
            {
                DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "received " + tokens.Count() + " tokens");
                for (int token_counter = 0; token_counter < tokens.Count(); token_counter++)
                {
                    DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "token: " + tokens[token_counter]);
                }
            }
            switch (tokens[0])
            {
                case "createbuilding":
                    //example: "createbuilding | 12 | 34.5, 6.78, 91.0 | 1.2 | 34"
                    if (debug)
                        DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "createbuilding found with parameters( " + tokens[1] + " | " + tokens[2] + " | " + tokens[3] + " | " + tokens[4] + " )");

                    BuildingManager buildingManager = Singleton<BuildingManager>.instance;

                    if (debug)
                        DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "parsing prefab of " + tokens[1]);
                    uint building_prefab = uint.Parse(tokens[1]);

                    if (debug)
                        DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "parsing position of " + tokens[2]);
                    Vector3 building_position = string_to_vector3(tokens[2]);

                    if (debug)
                        DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "parsing angle of " + tokens[3]);
                    float building_angle = float.Parse(tokens[3]);

                    if (debug)
                        DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "parsing length of " + tokens[4]);
                    int building_length = int.Parse(tokens[4]);

                    CreateBuilding(out var building2, ref Singleton<SimulationManager>.instance.m_randomizer, building_prefab, building_position, building_angle, building_length);
                    break;
                case "createzone":
                    //example: "createzone | 1.2, 3.4, 5.6 | 4 | 30"
                    if (debug)
                    {
                        DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "createzone found with parameters( " + tokens[1] + " | " + tokens[2] + " | " + tokens[3] + " )");
                    }
                    if (debug)
                        DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "parsing position of " + tokens[1]);
                    Vector3 zone_position = string_to_vector3(tokens[1]);

                    if (debug)
                        DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "parsing type of " + tokens[2]);
                    int type = int.Parse(tokens[2]);

                    if (debug)
                        DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "parsing quantity of " + tokens[3]);
                    int quantity = int.Parse(tokens[3]);

                    ZoneArea(zone_position, int_to_zone(type), quantity);
                    break;
                default:
                    if (debug)
                    {
                        DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "invalid first token ( " + tokens[0] + " )");
                    }
                    break;
            }
        }

        public ItemClass.Zone int_to_zone(int inp)
        {
            //unzoned = 0
            //distant = 1
            //res low = 2
            //res high = 3 
            //com low = 4
            //com high = 5
            //industrial = 6
            //office = 7
            switch (inp)
            {
                case 0:
                    return ItemClass.Zone.Unzoned;
                case 1:
                    return ItemClass.Zone.Distant;
                case 2:
                    return ItemClass.Zone.ResidentialLow;
                case 3:
                    return ItemClass.Zone.ResidentialHigh;
                case 4:
                    return ItemClass.Zone.CommercialLow;
                case 5:
                    return ItemClass.Zone.CommercialHigh;
                case 6:
                    return ItemClass.Zone.Industrial;
                case 7:
                    return ItemClass.Zone.Office;
            }
            return ItemClass.Zone.ResidentialLow;
        }

        public void print_tokens_to_console(List<string> tokens)
        {
            string message = "";
            for (int counter = 0; counter < tokens.Count; counter++)
            {
                message = message + token_delimiter + tokens[counter];
            }
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, message);
        }
    }
}