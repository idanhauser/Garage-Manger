namespace Ex03.GarageLogic
{
    public class GasolineMotorcycle : MotorCycle
    {
        private static readonly Gasoline.eFuelType sr_FuelType = Gasoline.eFuelType.Octan98;
        private static readonly float sr_MaxTankCapacity = 6f;

        public GasolineMotorcycle() : base()
        {
            Engine = new Gasoline(sr_MaxTankCapacity, sr_FuelType);
        }

        public override string ToString()
        {
            string electricCarDetails =
                string.Format(
@"License number: {0}
Vehicle type: {1}
ModelName: {2}
The license type: {3}
Number of Wheels: {4}
Wheels manufacturer: {5}
Wheels current air pressure: {6}
Wheels maximum air pressure: {7}
Energy Percentage: {8:0.00}%
Engine capacity: {9}
Fuel type: {10}
Current fuel in the tank: {11:0.00} liters
Maximum fuel in the tank: {12:0.00} liters",
                    LicenseNumber,
                    typeof(GasolineMotorcycle).Name,
                    ModelName,
                    LicenseType.ToString(),
                    NumberOfWheels,
                    Wheels[0].Manufacturer,
                    Wheels[0].CurrentAirPressure,
                    Wheels[0].MaxAirPressure,
                    EnergyPercentage,
                    EngineCapacity,
                    Gasoline.eFuelType.Octan95,
                    Engine.CurrentEnergy,
                    Engine.MaxEnergy);

            return electricCarDetails;
        }
    }
}