namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : MotorCycle
    {
        private static readonly float sr_MaxBatteryCapacity = 1.8f;

        public ElectricMotorcycle() : base()
        {
            Engine = new Electric(sr_MaxBatteryCapacity);
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
Current battery time: {10:0.00} hours
Maximum battery time: {11:0.00} hours",
                    LicenseNumber,
                    typeof(ElectricMotorcycle).Name,
                    ModelName,
                    LicenseType.ToString(),
                    NumberOfWheels,
                    Wheels[0].Manufacturer,
                    Wheels[0].CurrentAirPressure,
                    Wheels[0].MaxAirPressure,
                    EnergyPercentage,
                    EngineCapacity,
                    Engine.CurrentEnergy,
                    Engine.MaxEnergy);

            return electricCarDetails;
        }
    }
}