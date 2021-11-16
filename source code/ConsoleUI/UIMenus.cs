using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class UIMenus
    {
        public static void ShowMainMenu()
        {
            string menuOptions =
@"please choose one of the following options (1-8):
        1. Add new vehicle to the garage
        2. Print vehicle's license numbers
        3. Modify a vehicle's status
        4. Inflate to maximum a vehicle's wheels
        5. Refuel a gasoline Engine vehicle
        6. Charge an electric Engine vehicle
        7. Print full details of a vehicle
        8. Quit";

            UI.ClearAndWriteToUser(menuOptions);
        }

        public static void ENumMenu(RequiredInformation i_EnumType)
        {
            int indexToPrint = 1;

            if (!i_EnumType.Type.IsEnum)
            {
                throw new ArgumentException();
            }

            string message = string.Format("Please choose the {0} of the following options ({1}-{2}):", i_EnumType.Information, Enum.GetValues(i_EnumType.Type).GetValue(0).GetHashCode(), Enum.GetValues(i_EnumType.Type).Length);

            UI.ClearAndWriteToUser(message);
            foreach (object eNumOption in i_EnumType.Type.GetEnumValues())
            {
                string option = string.Format("{0}) {1}", indexToPrint, VehicleFactory.GetStringFormat(eNumOption.ToString(), i_EnumType.Type));
                Console.WriteLine(option);
                indexToPrint++;
            }
        }
    }
}
