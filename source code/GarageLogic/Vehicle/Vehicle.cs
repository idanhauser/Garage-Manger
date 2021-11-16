using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {       
        private readonly List<Wheel> r_Wheels;
        private string m_ModelName;
        private string m_LicenseNumber;
        private float m_EnergyPercentage;
        private Engine m_Engine;
        private Dictionary<string, RequiredInformation> m_VehicleRequiredInfo;

        internal Vehicle(int i_NumOfWheels, float i_MaxAirPressure, Dictionary<string, RequiredInformation> i_VehicleRequiredInfo)
        {
            r_Wheels = new List<Wheel>(i_NumOfWheels);
            for (int i = 0; i < r_Wheels.Capacity; i++)
            {
                r_Wheels.Add(new Wheel(i_MaxAirPressure));
            }

            m_VehicleRequiredInfo = new Dictionary<string, RequiredInformation>()
                {
                    { "ModelName", new RequiredInformation("Model name", typeof(string)) }
                };
            m_VehicleRequiredInfo = m_VehicleRequiredInfo.Concat(i_VehicleRequiredInfo).ToDictionary(itemKey => itemKey.Key, itemValue => itemValue.Value);
        }

        public string ModelName
        {
            get { return m_ModelName; }
            set
            {
                if (value.Length > 0)
                {
                    m_ModelName = value;
                }
                else
                {
                    throw new FormatException("Empty string as model name is not valid.");
                }
            }
        }

        public Engine Engine
        {
            get { return m_Engine; }
            set { m_Engine = value; }
        }

        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
            set
            {
                if (value.Length > 0)
                {
                    m_LicenseNumber = value;
                }
                else
                {
                    throw new FormatException("Empty string as license number is not valid.");
                }
            }
        }

        public int NumberOfWheels
        {
            get { return Wheels.Count; }
        }

        public void CalcEnergyPercentage()
        {
            EnergyPercentage = (Engine.CurrentEnergy / Engine.MaxEnergy) * 100;
        }

        public void FillEnergy(float i_EnergyToFill)
        {
            Engine.AddEnergy(i_EnergyToFill);
            CalcEnergyPercentage();
        }

        public float EnergyPercentage
        {
            get { return m_EnergyPercentage; }
            set { m_EnergyPercentage = value; }
        }

        public Dictionary<string, RequiredInformation> VehicleRequiredInfo
        {
            get { return m_VehicleRequiredInfo; }
            set { m_VehicleRequiredInfo = value; }
        }

        public List<Wheel> Wheels
        {
            get { return r_Wheels; }
        }
    }
}