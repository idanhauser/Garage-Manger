using System.Collections.Generic;
using System;

namespace Ex03.GarageLogic
{
    public class Electric : Engine
    {
        private static readonly Dictionary<string, RequiredInformation> m_RequiredInfo =
            new Dictionary<string, RequiredInformation>() 
                {
                {
                    "CurrentElectricEnergy",
                    new RequiredInformation("The remaining battery time in minutes:", typeof(float))
                }
            };

        public Electric(float i_MaxBatteryTime) : base(i_MaxBatteryTime)
        {
        }

        public float CurrentElectricEnergy
        {
            set
            {
                SetCurrentEnergy(value);
            }
        }

        public override void AddEnergy(float i_EnergyAmountInMinutes)
        {
            float energyAmountInHours = minuteToHour(i_EnergyAmountInMinutes);
            CurrentEnergy += energyAmountInHours;
        }

        private float minuteToHour(float i_EnergyInMinutes)
        {
            return (float)(i_EnergyInMinutes / 60);
        }

        public override Dictionary<string, RequiredInformation> RequiredInfo
        {
            get { return m_RequiredInfo; }
        }

        public override void SetCurrentEnergy(float i_UserInputCurrentEnergy)
        {
            float energyToSetInHours = minuteToHour(i_UserInputCurrentEnergy);
            CurrentEnergy = energyToSetInHours;
        }
    }
}