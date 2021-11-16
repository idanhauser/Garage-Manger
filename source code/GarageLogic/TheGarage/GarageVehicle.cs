using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class GarageVehicle
    {
        public enum eVehicleStatus
        {
            InRepair = 1,
            Repaired,
            Paid
        }

        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eVehicleStatus m_VehicleStatus;
        private Vehicle m_TheVehicle;
        private Dictionary<string, RequiredInformation> m_GarageVehicleRequiredInfo;

        public GarageVehicle(Vehicle i_Vehicle)
        {
            m_VehicleStatus = eVehicleStatus.InRepair;
            TheVehicle = i_Vehicle;

            m_GarageVehicleRequiredInfo = new Dictionary<string, RequiredInformation>()
            {
                {
                    "OwnerName", new RequiredInformation("Owner name", typeof(string))
                },
                {
                    "OwnerPhone", new RequiredInformation("Owner phone", typeof(string))
                }
            };

            m_GarageVehicleRequiredInfo = m_GarageVehicleRequiredInfo.Concat(i_Vehicle.VehicleRequiredInfo)
                .ToDictionary(itemKey => itemKey.Key, itemValue => itemValue.Value);
        }

        public Vehicle TheVehicle
        {
            get { return m_TheVehicle; }
            set { m_TheVehicle = value; }
        }

        public string OwnerName
        {
            get { return m_OwnerName; }
            set
            {
                if (isValidName(value))
                {
                    m_OwnerName = value;
                }
                else
                {
                    throw new FormatException("Name must contain letters only.");
                }
            }
        }

        public Dictionary<string, RequiredInformation> GarageVehicleRequiredInfo
        {
            get { return m_GarageVehicleRequiredInfo; }
            set { m_GarageVehicleRequiredInfo = value; }
        }

        public eVehicleStatus VehicleStatus
        {
            get { return m_VehicleStatus; }
            set
            {
                if (m_VehicleStatus != value)
                {
                    m_VehicleStatus = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        public string OwnerPhone
        {
            get { return m_OwnerPhoneNumber; }
            set
            {
                if (isValidPhoneNumber(value))
                {
                    m_OwnerPhoneNumber = value;
                }
                else
                {
                    throw new FormatException("Phone number must contain numbers only.");
                }
            }
        }

        public override string ToString()
        {
            string electricCarDetails =
     string.Format(
@"Owner name: {0}
Owner phone number: {1}
Vehicle status: {2}
{3}",
OwnerName,
OwnerPhone,
VehicleStatus,
TheVehicle.ToString());

            return electricCarDetails;
        }

        private bool isValidName(string i_Name)
        {
            bool isNameValid = true;

            if (i_Name.Length > 0)
            {
                foreach (char currentChar in i_Name)
                {
                    if (!char.IsLetter(currentChar))
                    {
                        isNameValid = !isNameValid;
                        break;
                    }
                }
            }
            else
            {
                isNameValid = false;
            }

            return isNameValid;
        }

        private bool isValidPhoneNumber(string i_PhoneNumber)
        {
            bool isPhoneValid = true;

            if (i_PhoneNumber.Length > 0)
            {
                foreach (char currentChar in i_PhoneNumber)
                {
                    if (!char.IsDigit(currentChar))
                    {
                        isPhoneValid = false;
                        break;
                    }
                }
            }
            else
            {
                isPhoneValid = false;
            }

            return isPhoneValid;
        }
    }
}
