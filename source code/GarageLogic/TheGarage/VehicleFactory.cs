using System;
using System.Text;
using static Ex03.GarageLogic.MotorCycle;

namespace Ex03.GarageLogic
{
    public static class VehicleFactory
    {
        public enum eVehicleType
        {
            GasolineCar = 1,
            ElectricCar,
            GasolineMotorcycle,
            ElectricMotorcycle,
            Truck
        }

        public static Vehicle CreateVehicle(eVehicleType i_UserDecision)
        {
            Vehicle newVehicle;
            switch (i_UserDecision)
            {
                case eVehicleType.ElectricMotorcycle:
                    {
                        newVehicle = new ElectricMotorcycle();
                        break;
                    }

                case eVehicleType.GasolineMotorcycle:
                    {
                        newVehicle = new GasolineMotorcycle();
                        break;
                    }

                case eVehicleType.GasolineCar:
                    {
                        newVehicle = new GasolineCar();
                        break;
                    }

                case eVehicleType.ElectricCar:
                    {
                        newVehicle = new ElectricCar();
                        break;
                    }

                case eVehicleType.Truck:
                    {
                        newVehicle = new GasolineTruck();
                        break;
                    }

                default:
                    {
                        throw new ValueOutOfRangeException(1f, (float)Enum.GetValues(i_UserDecision.GetType()).Length);
                    }
            }

            return newVehicle;
        }

        public static string GetStringFormat(string i_VehicleTypeStr, Type i_EnumType)
        {
            string printFormat = null;
            StringBuilder builder = new StringBuilder();

            if (i_EnumType != typeof(eLicenseType))
            {
                foreach (char ch in i_VehicleTypeStr)
                {
                    if (char.IsUpper(ch) && builder.Length > 0)
                    {
                        builder.Append(' ');
                    }

                    builder.Append(ch);
                }

                printFormat = builder.ToString();
            }
            else
            {
                printFormat = i_VehicleTypeStr;
            }

            return printFormat;
        }
    }
}