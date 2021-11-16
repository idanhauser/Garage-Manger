using System.Reflection;
using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageManger
    {
        public enum eMenuOptions
        {
            AddVehicle = 1,
            PrintLicenseNumbers,
            ModifyVehicleStatus,
            InflateVehicleWheels,
            RefuelGasolineVehicle,
            ChargeElectricVehicle,
            DisplayVehicleDetails,
            QuitProgram
        }

        private Garage m_TheGarage = new Garage();

        public Garage Garage
        {
            get { return m_TheGarage; }
            set { m_TheGarage = value; }
        }

        public void Run()
        {
            bool quitRequested = false;

            while (!quitRequested)
            {
                UIMenus.ShowMainMenu();
                try
                {
                    eMenuOptions userInput = (eMenuOptions)UI.readEnum(typeof(eMenuOptions));
                    chooseCase(userInput, ref quitRequested);
                }
                catch (FormatException)
                {
                    UI.SystemMessage("The value is not in the right format.");
                }
                catch (ValueOutOfRangeException exception)
                {
                    UI.SystemMessage(exception.Message);
                }
                catch (ArgumentException)
                {
                    UI.SystemMessage("Illegal argument");
                }
                catch (Exception)
                {
                    UI.SystemMessage("Illegal input: terminating program.");
                    return;
                }
            }
        }

        private void chooseCase(eMenuOptions i_MenuOption, ref bool io_IsExit)
        {
            switch (i_MenuOption)
            {
                case eMenuOptions.AddVehicle:
                    {
                        addVehicleToGarage();
                        break;
                    }

                case eMenuOptions.PrintLicenseNumbers:
                    {
                        showLicenseNumber();
                        break;
                    }

                case eMenuOptions.ModifyVehicleStatus:
                    {
                        modifyVehicleStatus();
                        break;
                    }

                case eMenuOptions.InflateVehicleWheels:
                    {
                        inflateWheelsToMax();
                        break;
                    }

                case eMenuOptions.RefuelGasolineVehicle:
                    {
                        fillVehicleEnergy(typeof(Gasoline));
                        break;
                    }

                case eMenuOptions.ChargeElectricVehicle:
                    {
                        fillVehicleEnergy(typeof(Electric));
                        break;
                    }

                case eMenuOptions.DisplayVehicleDetails:
                    {
                        showVehicleDetails();
                        break;
                    }

                case eMenuOptions.QuitProgram:
                    {
                        exitProgram();
                        io_IsExit = true;
                        return;
                    }

                default:
                    {
                        throw new ValueOutOfRangeException(1, (float) Enum.GetValues(i_MenuOption.GetType()).Length);
                    }
            }
        }

        private void addVehicleToGarage()
        {
            try
            {
                string vehicleOptions =
                string.Format("The vehicle type:", Enum.GetValues(typeof(VehicleFactory.eVehicleType)).Length);
                RequiredInformation requiredData = new RequiredInformation(vehicleOptions, typeof(VehicleFactory.eVehicleType));
                VehicleFactory.eVehicleType vehicleType = (VehicleFactory.eVehicleType)UI.WriteEnumAndRead(requiredData);
                string licenseNumber = UI.WriteRequestAndRead("The garage vehicle license number:");

                if (licenseNumber.Length <= 0)
                {
                    throw new FormatException("Entering empty string is illegal.");
                }

                if (Garage.IsLicenseNumberExists(licenseNumber))
                {
                    UI.SystemMessage("License number is already exists");
                }
                else
                {
                    createVehicle(vehicleType, licenseNumber, out GarageVehicle newGarageVehicle);
                    Garage.AddVehicleToGarage(newGarageVehicle);
                    UI.SystemMessage("The new vehicle was added successfully!");
                }
            }
            catch (FormatException)
            {
                UI.SystemMessage("The value is not in the right format.");
                addVehicleToGarage();
            }
            catch (ValueOutOfRangeException exception)
            {
                UI.SystemMessage(exception.Message);
                addVehicleToGarage();
            }
        }

        private void createVehicle(VehicleFactory.eVehicleType i_VehicleType, string i_LicenseNumber, out GarageVehicle i_GarageVehicle)
        {
            i_GarageVehicle = Garage.CreateNewGarageVehicle(i_LicenseNumber, i_VehicleType);
            setClassRequiredMembers(i_GarageVehicle);
            i_GarageVehicle.TheVehicle.CalcEnergyPercentage();
        }

        private void setClassRequiredMembers(GarageVehicle i_GarageVehicleToSet)
        {
            setByPropery(i_GarageVehicleToSet, i_GarageVehicleToSet.GarageVehicleRequiredInfo);
            setByPropery(i_GarageVehicleToSet.TheVehicle, i_GarageVehicleToSet.TheVehicle.VehicleRequiredInfo);
            cloneForAllWheels(i_GarageVehicleToSet);
            setByPropery(i_GarageVehicleToSet.TheVehicle.Engine, i_GarageVehicleToSet.TheVehicle.Engine.RequiredInfo);
        }

        private void cloneForAllWheels(GarageVehicle i_GarageVehicleToSet)
        {
            Wheel tempWheel = new Wheel(i_GarageVehicleToSet.TheVehicle.Wheels[0].MaxAirPressure);

            setByPropery(tempWheel, i_GarageVehicleToSet.TheVehicle.Wheels[0].WheelRequiredInfo);
            foreach (Wheel currentWheel in i_GarageVehicleToSet.TheVehicle.Wheels)
            {
                currentWheel.WheelRequiredInfo = tempWheel.WheelRequiredInfo;
                currentWheel.CurrentAirPressure = tempWheel.CurrentAirPressure;
                currentWheel.Manufacturer = tempWheel.Manufacturer;
            }
        }

        private void setByPropery(object i_ObjectToSet, Dictionary<string, RequiredInformation> i_TheInfo)
        {
            try
            {
                RequiredInformation currentRequiredData;
                PropertyInfo[] propertyInfoVehicle = i_ObjectToSet.GetType().GetProperties();

                foreach (PropertyInfo currentProperty in propertyInfoVehicle)
                {
                    if (i_TheInfo.TryGetValue(currentProperty.Name, out currentRequiredData))
                    {
                        object requiredInput = UI.readRequiredObject(currentRequiredData);
                        if (currentProperty.CanWrite)
                        {
                            currentProperty.SetValue(i_ObjectToSet, requiredInput);
                        }
                    }
                }
            }
            catch (ValueOutOfRangeException exception)
            {
                UI.SystemMessage(exception.Message);
                setByPropery(i_ObjectToSet, i_TheInfo);
            }
            catch (ArgumentException exception)
            {
                UI.SystemMessage(exception.Message);
                setByPropery(i_ObjectToSet, i_TheInfo);
            }
            catch (Exception exception)
            {
                UI.SystemMessage("Illegal input: " + exception.InnerException.Message);
                setByPropery(i_ObjectToSet, i_TheInfo);
            }
        }

        private void showLicenseNumber()
        {
            try
            {
                UI.ClearAndWriteToUser("Would you like to filter license number by status?");
                bool boolInput = UI.readBool();
                Dictionary<string, GarageVehicle> vehiclesToDisplay = Garage.GarageVehicles;

                if (boolInput == true)
                {
                    GarageVehicle.eVehicleStatus status = (GarageVehicle.eVehicleStatus)UI.WriteEnumAndRead(new RequiredInformation("status", typeof(GarageVehicle.eVehicleStatus)));
                    vehiclesToDisplay = Garage.CreateFilteredCollection(status);
                }

                printVehicleCollection(vehiclesToDisplay);
            }
            catch (FormatException exception)
            {
                UI.SystemMessage(exception.Message);
                showLicenseNumber();
            }
        }

        private void inflateWheelsToMax()
        {
            try
            {
                GarageVehicle currentVehicle = UI.getVehicleFromLicense(Garage);

                if (Garage.InflateVehicleWheels(currentVehicle))
                {
                    UI.SystemMessage("The wheels were successfully inflated to maximum.");
                }
                else
                {
                    UI.SystemMessage("The wheels were already maximum inflated.");
                }
            }
            catch (Exception)
            {
                UI.SystemMessage("The license number you entered is not found!");
            }
        }

        private void modifyVehicleStatus()
        {
            GarageVehicle vehicleToModify = null;
            RequiredInformation requiredInfo;

            try
            {
                vehicleToModify = UI.getVehicleFromLicense(Garage);
                requiredInfo = new RequiredInformation("Vehicle new status:", typeof(GarageVehicle.eVehicleStatus));

                GarageVehicle.eVehicleStatus newStatus = (GarageVehicle.eVehicleStatus)UI.WriteEnumAndRead(requiredInfo);
                vehicleToModify.VehicleStatus = newStatus;
                UI.SystemMessage(string.Format("The vehicle status modified to {0}.", newStatus.ToString()));
            }
            catch (ArgumentException)
            {
                UI.SystemMessage(string.Format("Vehicle status is already {0}", vehicleToModify.VehicleStatus.ToString()));
            }
            catch (Exception)
            {
                UI.SystemMessage("The license number you entered is not found!");
            }
        }

        private void attemptToFillVehicleEnergy(Type i_EngineType)
        {
            try
            {
                string energyToFillStr = null;
                GarageVehicle vehicle = UI.getVehicleFromLicense(Garage);

                if (i_EngineType != Garage.EngineType(vehicle))
                {
                    throw new ArgumentException();
                }

                if (Garage.IsEnergyCapacityFull(vehicle))
                {
                    UI.SystemMessage("This vehicle was already had a full tank.");
                    return;
                }

                if (i_EngineType == typeof(Gasoline))
                {
                    RequiredInformation currentRequiredData = new RequiredInformation("The fuel type to refuel:", typeof(Gasoline.eFuelType));
                    Gasoline.eFuelType userInputEnergy = (Gasoline.eFuelType)UI.WriteEnumAndRead(currentRequiredData);

                    if (!Garage.IsValidFuelType(userInputEnergy, vehicle))
                    {
                        throw new ArgumentException();
                    }

                    energyToFillStr = UI.WriteRequestAndRead("The number of liters of fuel that you want to fill:");
                }
                else
                {
                    energyToFillStr = UI.WriteRequestAndRead("For how many minutes you want to charge:");
                }

                Garage.FillEnergy(energyToFillStr, vehicle);
                UI.SystemMessage("The energy was filled successfully!");
            }
            catch (FormatException)
            {
                UI.SystemMessage("The value is not in the right format.");
            }
            catch (ValueOutOfRangeException exception)
            {
                UI.SystemMessage(exception.Message);
                attemptToFillVehicleEnergy(i_EngineType);
            }
            catch (ArgumentException)
            {
                UI.SystemMessage("The vehicle you entered doesn't fit this fuel type!");
            }
            catch (Exception)
            {
                UI.SystemMessage("The value is not valid.");
            }
        }

        private void fillVehicleEnergy(Type i_EnergySourceType)
        {
            try
            {
                attemptToFillVehicleEnergy(i_EnergySourceType);
            }
            catch (ArgumentException)
            {
                UI.SystemMessage(
                    string.Format("The vehicle you entered doesn't fit {0} type!", i_EnergySourceType.Name));
            }
            catch (FormatException)
            {
                UI.SystemMessage("The value is not in the right format.");
            }
        }

        private void showVehicleDetails()
        {
            try
            {
                GarageVehicle chosenGarageVehicle = UI.getVehicleFromLicense(Garage);
                UI.ClearAndWriteToUser(chosenGarageVehicle.ToString());
                UI.EnterToContinue();
            }
            catch (Exception)
            {
                UI.SystemMessage("The license number you entered is not found!");
            }
        }

        private void printVehicleCollection(Dictionary<string, GarageVehicle> i_GarageVehicleCollection)
        {
            if (i_GarageVehicleCollection.Count == 0)
            {
                UI.SystemMessage("There are no vehicles");
            }
            else
            {
                Console.Clear();
                int index = 1;
                foreach (KeyValuePair<string, GarageVehicle> currentGarageVehicle in i_GarageVehicleCollection)
                {
                    Console.WriteLine(string.Format("{0}) {1}", index, currentGarageVehicle.Key));
                    index++;
                }

                UI.EnterToContinue();
            }
        }

        private void exitProgram()
        {
            string quitMessage = @"GoodBye!";
            UI.SystemMessage(quitMessage);
        }
    }
}
