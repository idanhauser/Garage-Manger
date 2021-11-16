namespace Ex03.GarageLogic
{
    public class GasolineCar : Car
    {
        private static readonly Gasoline.eFuelType sr_FuelType = Gasoline.eFuelType.Octan95;
        private static readonly float sr_MaxTankCapacity = 45f;

        public GasolineCar() : base()
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
The car color: {3}
Number of doors: {4}
Number of Wheels: {5}
Wheels manufacturer: {6}
Wheels current air pressure: {7}
Wheels maximum air pressure: {8}
Energy Percentage: {9:0.00}%
Fuel type: {10}
Current fuel in the tank: {11:0.00} liters
Maximum fuel in the tank: {12:0.00} liters",
                    LicenseNumber,
                    typeof(GasolineCar).Name,
                    ModelName,
                    CarColor.ToString(),
                    NumberOfDoors.ToString(),
                    NumberOfWheels,
                    Wheels[0].Manufacturer,
                    Wheels[0].CurrentAirPressure,
                    Wheels[0].MaxAirPressure,
                    EnergyPercentage,
                    Gasoline.eFuelType.Octan96,
                    Engine.CurrentEnergy,
                    Engine.MaxEnergy);

            return electricCarDetails;
        }
    }
}