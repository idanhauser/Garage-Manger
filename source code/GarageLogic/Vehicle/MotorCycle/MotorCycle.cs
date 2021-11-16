using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class MotorCycle : Vehicle
    {
        private const int k_NumOfWheels = 2;
        private const float k_MaxAirPressure = 30;

        public enum eLicenseType
        {
            A = 1,
            B1,
            AA,
            BB
        }

        private static readonly Dictionary<string, RequiredInformation> sr_MotorcycleRequiredInfo =
            new Dictionary<string, RequiredInformation>()
            {
                { "LicenseType", new RequiredInformation("license type", typeof(eLicenseType)) },
                { "EngineCapacity",  new RequiredInformation("Engine capacity", typeof(int)) }
            };

        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;

        public MotorCycle() : base(k_NumOfWheels, k_MaxAirPressure, sr_MotorcycleRequiredInfo)
        {
        }

        public int EngineCapacity
        {
            get { return m_EngineCapacity; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("value must be positive");
                }

                m_EngineCapacity = value; 
            }
        }

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }
    }
}