using System;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UI
    {
        public static void SystemMessage(string i_Message)
        {
            ClearAndWriteToUser(i_Message);
            EnterToContinue();
        }

        public static string WriteRequestAndRead(string i_MessageToPrint)
        {
            string message = string.Format("Please enter {0}", i_MessageToPrint);
            ClearAndWriteToUser(message);
            return Console.ReadLine();
        }

        public static object WriteEnumAndRead(RequiredInformation i_Data)
        {
            try
            {
                UIMenus.ENumMenu(i_Data);
                return readEnum(i_Data.Type);
            }
            catch (ValueOutOfRangeException exception)
            {
                SystemMessage(exception.Message);
                return WriteEnumAndRead(i_Data);
            }
            catch (ArgumentException)
            {
                SystemMessage("Argument Issue");
                return WriteEnumAndRead(i_Data);
            }
            catch (FormatException)
            {
                SystemMessage("The value is not in the right format.");
                return WriteEnumAndRead(i_Data);
            }
        }

        public static void ClearAndWriteToUser(string i_MessageToPrint)
        {
            Console.Clear();
            Console.WriteLine(i_MessageToPrint);
        }

        public static void EnterToContinue()
        {
            Console.WriteLine(Environment.NewLine + "Enter any key continue.");
            Console.ReadLine();
        }

        internal static GarageVehicle getVehicleFromLicense(Garage i_Garage)
        {
            GarageVehicle vehicle = null;
            string licenceNumber = WriteRequestAndRead("The vehicle license number:");

            try
            {
                vehicle = i_Garage.GetVehicleByLicense(licenceNumber);
            }
            catch (Exception errMsg)
            {
                SystemMessage(errMsg.Message);
                getVehicleFromLicense(i_Garage);
            }

            return vehicle;
        }

        internal static object readRequiredObject(RequiredInformation i_RequiredInformation)
        {
            object userEnteredInput;
            ClearAndWriteToUser(i_RequiredInformation.Information);

            if (i_RequiredInformation.Type == typeof(bool))
            {
                userEnteredInput = readBool();
            }
            else if (i_RequiredInformation.Type == typeof(int))
            {
                userEnteredInput = readInt();
            }
            else if (i_RequiredInformation.Type == typeof(float))
            {
                userEnteredInput = readFloat();
            }
            else if (i_RequiredInformation.Type == typeof(string))
            {
                userEnteredInput = Console.ReadLine();
            }
            else if (i_RequiredInformation.Type.IsEnum)
            {
                UIMenus.ENumMenu(i_RequiredInformation);
                userEnteredInput = readEnum(i_RequiredInformation.Type);
            }
            else
            {
                throw new ArgumentException();
            }

            return userEnteredInput;
        }

        internal static bool readBool()
        {
            const string k_No = "N";
            const string k_Yes = "Y";

            bool boolInput;
            Console.WriteLine("Enter 'Y' for yes or 'N' for no");
            string input = Console.ReadLine();

            if (input.Equals(k_Yes))
            {
                boolInput = true;
            }
            else if (input.Equals(k_No))
            {
                boolInput = false;
            }
            else
            {
                throw new FormatException("Invalid input, you should enter 'Y' or 'N' only!");
            }

            return boolInput;
        }

        private static object readInt()
        {
            int intValue;
            string intValueStr = Console.ReadLine();

            if (!int.TryParse(intValueStr, out intValue))
            {
                throw new FormatException("The number you entered is illegal");
            }

            return intValue;
        }

        private static object readFloat()
        {
            float floatValue;
            string floatValueStr = Console.ReadLine();

            if (!float.TryParse(floatValueStr, out floatValue))
            {
                throw new FormatException("The value you entered is illegal");
            }

            return floatValue;
        }

        public static void CheckStringInput(string i_StringToCheck)
        {
            if (i_StringToCheck.Length <= 0)
            {
                throw new FormatException("Entering empty string is illegal.");
            }
        }

        public static object readEnum(Type i_EnumType)
        {
            string inputEnumStr = Console.ReadLine();
            Array range;

            if (inputEnumStr.Length <= 0)
            {
                throw new FormatException("Entering empty string is illegal.");
            }

            if (int.TryParse(inputEnumStr, out int intInput))
            {
                if (!Enum.IsDefined(i_EnumType, intInput))
                {
                    range = Enum.GetValues(i_EnumType);
                    int minValue = (int)range.GetValue(0);
                    int maxValue = (int)range.GetValue(range.Length - minValue);

                    throw new ValueOutOfRangeException(minValue, maxValue);
                }
            }
            else
            {
                throw new FormatException("The value you entered is illegal");
            }

            return Enum.Parse(i_EnumType, inputEnumStr);
        }
    }
}