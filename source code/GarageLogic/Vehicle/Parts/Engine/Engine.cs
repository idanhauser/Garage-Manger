using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        private readonly float r_MaxEnergy;
        private float m_CurrentEnergy = 0;

        public Engine(float i_MaxEnergy)
        {
            r_MaxEnergy = i_MaxEnergy;
        }

        public float MaxEnergy
        {
            get { return r_MaxEnergy; }
        }

        public float CurrentEnergy
        {
            get { return m_CurrentEnergy; }
            set
            {
                if (value <= MaxEnergy && value >= 0)
                {
                    m_CurrentEnergy = value;
                }
                else
                {
                    if (this is Electric)
                    {
                        throw new ValueOutOfRangeException(0, (MaxEnergy - m_CurrentEnergy) * 60);
                    }
                    else if (this is Gasoline)
                    {
                        throw new ValueOutOfRangeException(0, MaxEnergy - m_CurrentEnergy);
                    }
                }
            }
        }

        public abstract void AddEnergy(float i_EnergyToAdd);

        public abstract void SetCurrentEnergy(float i_UserInputCurrentEnergy);

        public bool IsFull()
        {
            return CurrentEnergy == MaxEnergy;
        }

        public abstract Dictionary<string, RequiredInformation> RequiredInfo
        {
            get;
        }
    }
}