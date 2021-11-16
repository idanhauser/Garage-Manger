using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, GarageVehicle> m_GarageVehicles;

        public Garage()
        {
            GarageVehicles = new Dictionary<string, GarageVehicle>();
        }

        public Dictionary<string, GarageVehicle> GarageVehicles
        {
            get { return m_GarageVehicles; }
            set { m_GarageVehicles = value; }
        }

        public GarageVehicle CreateNewGarageVehicle(string i_LicenseNumber, VehicleFactory.eVehicleType i_ChosenVeihicleType)
        {
            GarageVehicle newGarageVehicle = new GarageVehicle(VehicleFactory.CreateVehicle(i_ChosenVeihicleType));
            newGarageVehicle.TheVehicle.LicenseNumber = i_LicenseNumber;

            return newGarageVehicle;
        }

        public Dictionary<string, GarageVehicle> CreateFilteredCollection(GarageVehicle.eVehicleStatus i_VehicleStatus)
        {
            Dictionary<string, GarageVehicle> filteredCollection = new Dictionary<string, GarageVehicle>();
            foreach (KeyValuePair<string, GarageVehicle> currentVehicle in GarageVehicles)
            {
                if (currentVehicle.Value.VehicleStatus == i_VehicleStatus)
                {
                    filteredCollection.Add(currentVehicle.Key, currentVehicle.Value);
                }
            }

            return filteredCollection;
        }

        public GarageVehicle GetVehicleByLicense(string i_LicenseNumber)
        {
            GarageVehicle garageVehicleToGet;

            if (!GarageVehicles.TryGetValue(i_LicenseNumber, out garageVehicleToGet))
            {
                throw new Exception("License number is not exists.");
            }

            return garageVehicleToGet;
        }

        public bool IsLicenseNumberExists(string i_LicenceNumber)
        {
            return GarageVehicles.ContainsKey(i_LicenceNumber);
        }

        public bool IsValidFuelType(Gasoline.eFuelType i_compareEnergy, GarageVehicle i_vehicleEngery)
        {
            bool isValidFuel;

            switch (i_compareEnergy)
            {
                case Gasoline.eFuelType.Octan95:
                    {
                        isValidFuel = i_vehicleEngery.TheVehicle is GasolineMotorcycle;
                        break;
                    }

                case Gasoline.eFuelType.Octan96:
                    {
                        isValidFuel = i_vehicleEngery.TheVehicle is GasolineCar;
                        break;
                    }

                case Gasoline.eFuelType.Soler:
                    {
                        isValidFuel = i_vehicleEngery.TheVehicle is GasolineTruck;
                        break;
                    }

                default:
                    {
                        isValidFuel = false;
                        break;
                    }
            }

            return isValidFuel;
        }

        public void FillEnergy(string i_energyToFillStr, GarageVehicle i_VehicleToFill)
        {
            float energyToFillFloat;

            if (float.TryParse(i_energyToFillStr, out energyToFillFloat))
            {
                i_VehicleToFill.TheVehicle.FillEnergy(energyToFillFloat);
            }
            else
            {
                throw new FormatException();
            }
        }

        public Type EngineType(GarageVehicle i_VehicleToFill)
        {
            return i_VehicleToFill.TheVehicle.Engine.GetType();
        }

        public bool IsEnergyCapacityFull(GarageVehicle i_VehicleToFill)
        {
            return i_VehicleToFill.TheVehicle.Engine.IsFull();
        }

        public void AddVehicleToGarage(GarageVehicle i_NewGarageVehicle)
        {
            GarageVehicles.Add(i_NewGarageVehicle.TheVehicle.LicenseNumber, i_NewGarageVehicle);
        }

        public bool InflateVehicleWheels(GarageVehicle i_VehicleToInflate)
        {
            bool isFull = true;

            if (i_VehicleToInflate.TheVehicle.Wheels[0].isFullAirPressure())
            {
                isFull = false;
            }
            else
            {
                foreach (Wheel currentWheel in i_VehicleToInflate.TheVehicle.Wheels)
                {
                    currentWheel.WheelAirPressureToMax();
                }
            }

            return isFull;
        }
    }
}
