namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        private static readonly float sr_MaxBatteryCapacity = 3.2f;

        public ElectricCar() : base()
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
The car color: {3}
Number of doors: {4}
Number of Wheels: {5}
Wheels manufacturer: {6}
Wheels current air pressure: {7}
Wheels maximum air pressure: {8}
Energy Percentage: {9:0.00}%
Current battery time: {10:0.00} hours
Maximum battery time: {11:0.00} hours",
                    LicenseNumber,
                    typeof(ElectricCar).Name,
                    ModelName,
                    CarColor.ToString(),
                    NumberOfDoors.ToString(),
                    NumberOfWheels,
                    Wheels[0].Manufacturer,
                    Wheels[0].CurrentAirPressure,
                    Wheels[0].MaxAirPressure,
                    EnergyPercentage,
                    Engine.CurrentEnergy,
                    Engine.MaxEnergy);

            return electricCarDetails;
        }
    }
}