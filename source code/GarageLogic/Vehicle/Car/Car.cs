using System.Collections.Generic;
using System.Reflection;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        private const int k_NumberOfWheels = 4;
        private const float k_MaxAirPressure = 32;

        public enum eNumOfDoors
        {
            Two = 1,
            Three,
            Four,
            Five
        }

        public enum eCarColor
        {
            Red = 1,
            Silver,
            White,
            Black
        }

        private eCarColor m_CarColor;
        private eNumOfDoors m_NumOfDoors;

        private static readonly Dictionary<string, RequiredInformation> sr_CarRequiredInfo =
            new Dictionary<string, RequiredInformation>()
            {
                { "CarColor", new RequiredInformation("car color", typeof(eCarColor)) },
                { "NumberOfDoors",  new RequiredInformation("number of doors", typeof(eNumOfDoors)) }
            };

        public Car() : base(k_NumberOfWheels, k_MaxAirPressure, sr_CarRequiredInfo)
        {
        }

        public eCarColor CarColor
        {
            get { return m_CarColor; }
            set { m_CarColor = value; }
        }

        public eNumOfDoors NumberOfDoors
        {
            get { return m_NumOfDoors; }
            set { m_NumOfDoors = value; }
        }
    }
}