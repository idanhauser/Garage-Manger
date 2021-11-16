using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class Wheel
    {   
        private readonly float r_MaxAirPressure;
        private string m_Manufacturer;
        private float m_CurrentAirPressure;
        private Dictionary<string, RequiredInformation> m_RequiredInfo;

        public Wheel(float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;

            m_RequiredInfo = new Dictionary<string, RequiredInformation>()
            {
                {
                    "Manufacturer", new RequiredInformation("Wheel's manufacturer", typeof(string))
                },
                {
                    "CurrentAirPressure",
                    new RequiredInformation("The current air pressure of the wheels:", typeof(float))
                }
            };
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set
            {
                if (isValidAirPressure(value))
                {
                    m_CurrentAirPressure = value;
                }
                else
                {
                    throw new ArgumentException(string.Format("Air Pressure must be lower then {0}", r_MaxAirPressure));
                }
            }
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }

        public string Manufacturer
        {
            get { return m_Manufacturer; }
            set
            {
                if (value.Length > 0)
                {
                    m_Manufacturer = value;
                }
                else
                {
                    throw new FormatException("Empty string as manufacturer is not valid.");
                }
            }
        }

        public void WheelAirPressureToMax()
        {
            m_CurrentAirPressure = r_MaxAirPressure;
        }

        private bool isValidAirPressure(float i_CurrentAirPressure)
        {
            bool isValid = false;
            if (i_CurrentAirPressure <= MaxAirPressure && i_CurrentAirPressure >= 0)
            {
                isValid = true;
            }
            return isValid;
        }

        public bool isFullAirPressure()
        {
            return m_CurrentAirPressure == r_MaxAirPressure;
        }

        public Dictionary<string, RequiredInformation> WheelRequiredInfo
        {
            get { return m_RequiredInfo; }
            set { m_RequiredInfo = value; }
        }
    }
}