namespace Ex03.GarageLogic
{
    public class GasolineTruck : Truck
    {
        private static readonly Gasoline.eFuelType sr_FuelType = Gasoline.eFuelType.Soler;
        private static readonly float sr_MaxTankCapacity = 120f;

        public GasolineTruck() : base()
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
Number of Wheels: {3}
Wheels manufacturer: {4}
Wheels current air pressure: {5}
Wheels maximum air pressure: {6}
Is Transports Hazardous Materials: {7}
Cargo capacity: {8}
Fuel type: {9}
Energy Percentage: {10:0.00}%
Current fuel in the tank: {11:0.00} liters
Maximum fuel in the tank: {12:0.00} liters",
                    LicenseNumber,
                    typeof(GasolineTruck).Name,
                    ModelName,
                    Wheels.Capacity,
                    Wheels[0].Manufacturer,
                    Wheels[0].CurrentAirPressure,
                    Wheels[0].MaxAirPressure,
                    IsTransmitHazardousMaterials,
                    MaximumCarryWeight,
                    Gasoline.eFuelType.Soler.ToString(),
                    EnergyPercentage,
                    Engine.CurrentEnergy,
                    Engine.MaxEnergy);

            return electricCarDetails;
        }
    }
}