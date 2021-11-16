using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Truck : Vehicle
    {
        private const float k_MaxAirPressure = 26;
        private const int k_NumOfWheels = 16;
        private static readonly Dictionary<string, RequiredInformation> sr_TruckRequiredInfo =
            new Dictionary<string, RequiredInformation>()
            {
                { "IsTransmitHazardousMaterials", new RequiredInformation("is the truck transported hazardous materials", typeof(bool)) },
                { "MaxCarryWeight",  new RequiredInformation("Maximum carrying weight", typeof(float)) }
            };

        private float m_MaxCarryWeight;
        private bool m_IsTransmitHazardousMaterials;
        
        public Truck() : base(k_NumOfWheels, k_MaxAirPressure, sr_TruckRequiredInfo)
        {
        }

        public float MaximumCarryWeight
        {
            get { return m_MaxCarryWeight; }
            set { m_MaxCarryWeight = value; }
        }

        public bool IsTransmitHazardousMaterials
        {
            get { return m_IsTransmitHazardousMaterials; }
            set { m_IsTransmitHazardousMaterials = value; }
        }
    }
}