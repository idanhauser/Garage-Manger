using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Gasoline : Engine
    {
        private static readonly Dictionary<string, RequiredInformation> m_RequiredInfo =
            new Dictionary<string, RequiredInformation>()
            {
                {
                    "CurrentEnergy", new RequiredInformation("The current amount of fuel in liters:", typeof(float))
                }
};

        public enum eFuelType
        {
            Octan95 = 1,
            Octan96,
            Octan98,
            Soler,
        }

        private readonly eFuelType r_FuelType;

        public Gasoline(float i_MaxTankCapacity, eFuelType i_FuelType) : base(i_MaxTankCapacity) 
        {
            r_FuelType = i_FuelType;
        }

        public eFuelType FuelType
        {
            get { return r_FuelType; }
        }

        public override void AddEnergy(float i_EnergyAmount)
        {
            SetCurrentEnergy(i_EnergyAmount + CurrentEnergy);
        }

        public override Dictionary<string, RequiredInformation> RequiredInfo
        {
            get { return m_RequiredInfo; }
        }

        public override void SetCurrentEnergy(float i_UserInputCurrentEnergy)
        {
            CurrentEnergy = i_UserInputCurrentEnergy;
        }
    }
}