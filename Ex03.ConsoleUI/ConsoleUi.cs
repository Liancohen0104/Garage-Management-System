using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class ConsoleUi
    {
        public GarageManager m_GarageManager = new GarageManager();

        public void MainLoop()
        {
            eUserActionsInGarage userAction;
            do
            {
                printActionMenu();
                userAction = getUserActionAndValidation();
                makeAction(userAction);
                Console.WriteLine("press any key to clean the console");
                Console.ReadLine();
                Console.Clear();
            }
            while(userAction != eUserActionsInGarage.Exit);
        }

        private void printActionMenu()
        {
            StringBuilder actionMenuBuilder = new StringBuilder();

            actionMenuBuilder.AppendLine("=================================Menu==================================");
            actionMenuBuilder.AppendLine("Please select an action");
            actionMenuBuilder.AppendLine("1) Add a new vehicle to the garage.");
            actionMenuBuilder.AppendLine("2) Show all license plate numbers of vehicles in the garage.");
            actionMenuBuilder.AppendLine("3) Update vehicle status - Change the status of a specific vehicle.");
            actionMenuBuilder.AppendLine("4) Inflate all tires to maximum.");
            actionMenuBuilder.AppendLine("5) Refuel vehicle.");
            actionMenuBuilder.AppendLine("6) Recharge vehicle.");
            actionMenuBuilder.AppendLine("7) View vehicle details.");
            actionMenuBuilder.AppendLine("8) Exit the system.");
            actionMenuBuilder.AppendLine("=======================================================================");
            Console.WriteLine(actionMenuBuilder.ToString());
        }

        private eUserActionsInGarage getUserActionAndValidation()
        {
            eUserActionsInGarage userAction = eUserActionsInGarage.Exit;
            int indexUserAction = 0;
            bool isValidInput = false;

            while (!isValidInput)
            {
                if (int.TryParse(Console.ReadLine(), out indexUserAction))
                {
                    if (indexUserAction >= 1 && indexUserAction <= 8)
                    {
                        isValidInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid index action, please enter a number between (1-8)");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid format, please enter a numeric value.");
                }
            }

            switch(indexUserAction.ToString())
            {
                case "1":
                    userAction = eUserActionsInGarage.AddNewVehicle;
                    break;
                case "2":
                    userAction = eUserActionsInGarage.DisplayVehicle;
                    break;
                case "3":
                    userAction = eUserActionsInGarage.UpdateVehicleStatus;
                    break;
                case "4":
                    userAction = eUserActionsInGarage.InflateTiresToMax;
                    break;
                case "5":
                    userAction = eUserActionsInGarage.RefuelVehicle;
                    break;
                case "6":
                    userAction = eUserActionsInGarage.RechargeVehicle;
                    break;
                case "7":
                    userAction = eUserActionsInGarage.DisplayVehicleDetails;
                    break;
                case "8":
                    userAction = eUserActionsInGarage.Exit;
                    break;
            }

            return userAction;
        }

        private void makeAction(eUserActionsInGarage i_UserActions)
        {
            switch (i_UserActions)
            {
                case eUserActionsInGarage.AddNewVehicle:
                    addNewVehicleToTheGarageOrUpdateStatus();
                    break;
                case eUserActionsInGarage.DisplayVehicle:
                    displayListOfLicensePlate();
                    break;
                case eUserActionsInGarage.UpdateVehicleStatus:
                    updateStatusOfVehicleInTheGarage();
                    break;
                case eUserActionsInGarage.InflateTiresToMax:
                    inflateTiresToMax();
                    break;
                case eUserActionsInGarage.RefuelVehicle:
                    refuelVehicle();
                    break;
                case eUserActionsInGarage.RechargeVehicle:
                    rechargeVehicle();
                    break;
                case eUserActionsInGarage.DisplayVehicleDetails:
                    displayVehicleDetails();
                    break;
                case eUserActionsInGarage.Exit:
                    Console.Clear();
                    break;
            }
        }

        private void updateStatusOfVehicleInTheGarage()
        {
            bool isValidUserInput = false;
            string licensePlateNumber;

            while (!isValidUserInput)
            {
                licensePlateNumber = getLicensePlateNumber();
                if (!m_GarageManager.IsVehicleInGarage(licensePlateNumber))
                {
                    if (checkIfUserWantToBackToMainMenu())
                    {
                        break;
                    }
                }
                else
                {
                    eVehicleStatus newVehicleStatus = getNewVehicleStatusFromUser();
                    m_GarageManager.UpdateStatusOfVehicle(licensePlateNumber, newVehicleStatus);
                    isValidUserInput = true;
                }
            }
        }

        private eVehicleStatus getNewVehicleStatusFromUser()
        {
            StringBuilder vehicleStatusBuilder = new StringBuilder();
            eVehicleStatus newVehicleStatus = eVehicleStatus.InRepair;
            int indexUserVehicleStatus = 0;
            bool isValidInput = false;
            
            vehicleStatusBuilder.AppendLine("Please select a new vehicle status in the garage");
            vehicleStatusBuilder.AppendLine("1) InRepair");
            vehicleStatusBuilder.AppendLine("2) Repaired");
            vehicleStatusBuilder.AppendLine("3) Paid");
            Console.WriteLine(vehicleStatusBuilder.ToString());
            while (!isValidInput)
            {
                Console.WriteLine("Enter vehicle status index (1-3):");
                if (int.TryParse(Console.ReadLine(), out indexUserVehicleStatus))
                {
                    if (indexUserVehicleStatus >= 1 && indexUserVehicleStatus <= 3)
                    {
                        isValidInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid index action, please enter a number between (1-3)");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid format, please enter a numeric value.");
                }
            }

            switch (indexUserVehicleStatus.ToString())
            {
                case "1":
                    newVehicleStatus = eVehicleStatus.InRepair;
                    break;
                case "2":
                    newVehicleStatus = eVehicleStatus.Repaired;
                    break;
                case "3":
                    newVehicleStatus = eVehicleStatus.Paid;
                    break;
            }

            return newVehicleStatus;
        }

        private void addNewVehicleToTheGarageOrUpdateStatus()
        {
            string licensePlateNumber = getLicensePlateNumber();

            if (m_GarageManager.IsVehicleInGarage(licensePlateNumber))
            {
                Console.WriteLine("The vehicle is already in the garage");
                m_GarageManager.UpdateStatusOfVehicle(licensePlateNumber);
            }
            else
            {
                string  newVehicleType = selectYourVehicleType();
                Vehicle newVehicle = m_GarageManager.AddVehicle(licensePlateNumber, newVehicleType);
                Dictionary<string, string> userAnswers = null;
                bool isValidInput = false;

                getAndSetCommonFieldsOfVehicle(newVehicle);
                do
                {
                    try
                    {
                        printUserInputQuestionsAndGetUserInput(out userAnswers, newVehicle);
                        newVehicle.FieldsValidation(userAnswers);
                        isValidInput = true;
                        newVehicle.SetUniqueFieldsForVehicle(userAnswers);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                while(!isValidInput);
                getAndSetUserInfo(licensePlateNumber);
            }
        }

        private void getAndSetUserInfo(string i_LicensePlateNumber)
        {
            string ownerVehicleName;
            string ownerPhoneNumber;

            getUserInfo(i_LicensePlateNumber, out ownerVehicleName, out ownerPhoneNumber);
            m_GarageManager.SetPersonalInfo(i_LicensePlateNumber, ownerVehicleName, ownerPhoneNumber);
        }

        private void printUserInputQuestionsAndGetUserInput(out Dictionary<string, string> o_UserAnswers, Vehicle i_CurrentVehicle)
        {
            string userInput = null;

            o_UserAnswers = new Dictionary<string, string>();
            foreach (KeyValuePair<string,string> question in i_CurrentVehicle.UserInputQuestions)
            {
                bool isValidFormat = false;
                    
                Console.WriteLine(question.Value);
                userInput = Console.ReadLine();
                o_UserAnswers[question.Key] = userInput;
            }
        }

        private void getUserInfo(string i_LicensePlateNumber, out string o_OwnerVehicleName, out string o_OwnerPhoneNumber)
        {
            bool isValidInput = false;
            long ownerVehicleNumber;

            o_OwnerVehicleName = null;
            o_OwnerPhoneNumber = null;
            Console.WriteLine("Enter vehicle's owner name:");
            o_OwnerVehicleName = Console.ReadLine();
            Console.WriteLine("Enter vehicle's owner phone number:");
            o_OwnerPhoneNumber = Console.ReadLine();
            while (!long.TryParse(o_OwnerPhoneNumber, out ownerVehicleNumber))
            {
                Console.WriteLine("Invalid phone number format, please try again");
            }

            try
            {
                m_GarageManager.VehiclesInGarage[i_LicensePlateNumber].OwnerPhoneNumberValidation(o_OwnerPhoneNumber);
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine("Phone number" + ex.Message);
                o_OwnerPhoneNumber = Console.ReadLine();
                while (!long.TryParse(o_OwnerPhoneNumber, out ownerVehicleNumber))
                {
                    Console.WriteLine("Invalid phone number format, please try again");
                }
            }
        }

        private void getAndSetCommonFieldsOfVehicle(Vehicle i_CurrentVehicle)
        {
            getAndSetUserCurrentEnergy(i_CurrentVehicle);
            getAndSetUserModelName(i_CurrentVehicle);
            getAndSetUserManufacturerNameAndCurrentAirPressure(i_CurrentVehicle);
        }

        private string getLicensePlateNumber()
        {
            bool isValidLicensePlateNumber = false;
            long longLicensePlateNumber = 0;

            Console.WriteLine("Enter your license plate number: ");
            while(!isValidLicensePlateNumber)
            {
                if (long.TryParse(Console.ReadLine(), out longLicensePlateNumber))
                {
                    try
                    {
                        m_GarageManager.LicensePlateNumberValidation(longLicensePlateNumber.ToString());
                        isValidLicensePlateNumber = true;
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine("License plate number" + ex.Message);
                    }
                }
            }

            return longLicensePlateNumber.ToString();
        }

        private void getAndSetUserCurrentEnergy(Vehicle i_CurrentVehicle)
        {
            float currentEnergyInput = 0;
            bool  isValidInput = false;

            Console.WriteLine("Enter your vehicle's current energy: ");
            while (!isValidInput)
            {
                if(!float.TryParse(Console.ReadLine(), out currentEnergyInput))
                {
                    Console.WriteLine("Invalid format, please enter a numeric value.");
                }
                else
                {
                    try
                    {
                        i_CurrentVehicle.EngineType.CurrentEnergyValidation(currentEnergyInput);
                        i_CurrentVehicle.EngineType.CurrentEnergy = currentEnergyInput;
                        isValidInput = true;
                    }
                    catch(ValueOutOfRangeException ex)
                    {
                        Console.WriteLine("Current energy" + ex.Message);
                    }
                }
            }
        }

        private void getAndSetUserModelName(Vehicle i_CurrentVehicle)
        {
            string userModelName = null;

            Console.WriteLine("Enter your vehicle's model name");
            userModelName = Console.ReadLine();
            while(string.IsNullOrWhiteSpace(userModelName))
            {
                Console.WriteLine("The input can't be empty, please try again");
                userModelName = Console.ReadLine();
            }

            i_CurrentVehicle.ModelName = userModelName;
        }

        private void getAndSetUserManufacturerNameAndCurrentAirPressure(Vehicle i_CurrentVehicle)
        {
            int userChoose = 0;
            bool isValidIndexOption = false;

            Console.WriteLine("Choose option:");
            Console.WriteLine("1) Enter the same mode for all tires");
            Console.WriteLine("2) Enter mode for each tire separately");
            while(!isValidIndexOption)
            {
                if(int.TryParse(Console.ReadLine() ,out userChoose))
                {
                    if(userChoose >= 1 && userChoose <= 2)
                    {
                        isValidIndexOption = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid index option, please enter valid option index between (1-2)");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid format, please try again and enter a number");
                }
            }

            if(userChoose == 1)
            {
                getAndSetSameModeTire(i_CurrentVehicle);
            }
            else
            {
                getAndSetSeparatelyTire(i_CurrentVehicle);
            }
        }

        private void getAndSetSameModeTire(Vehicle i_CurrentVehicle)
        {
            string userManufacturerName;
            float  userCurrentAirPressure;
            bool   isValidAirPressure = false;

            Console.WriteLine("Enter your tires manufacturer name");
            userManufacturerName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(userManufacturerName))
            {
                Console.WriteLine("Manufacturer name can't be empty, please try again");
                userManufacturerName = Console.ReadLine();
            }

            while(!isValidAirPressure)
            {
                Console.WriteLine("Enter your tire air pressure:");
                while (!float.TryParse(Console.ReadLine(), out userCurrentAirPressure))
                {
                    Console.WriteLine("Invalid air pressure format, please try again");
                }

                try
                {
                    for(int i = 0; i < i_CurrentVehicle.TierList.Count; ++i)
                    {
                        i_CurrentVehicle.SetCurrentTire(i_CurrentVehicle.TierList[i], userManufacturerName, userCurrentAirPressure);
                    }

                    isValidAirPressure = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine("Tire air pressure" + ex.Message);
                }
            }
        }

        private void getAndSetSeparatelyTire(Vehicle i_CurrentVehicle)
        {
            string userManufacturerName;
            float  userCurrentAirPressure;
            int    tireIndex = 0;

            while(tireIndex < i_CurrentVehicle.TierList.Count) 
            {
                Console.WriteLine("Setting tire {0}", tireIndex + 1);
                Console.WriteLine("Enter your tire manufacturer name");
                userManufacturerName = Console.ReadLine();
                while(string.IsNullOrWhiteSpace(userManufacturerName))
                {
                    Console.WriteLine("Manufacturer name can't be empty, please try again");
                    userManufacturerName = Console.ReadLine();
                }

                Console.WriteLine("Enter your tire air pressure:");
                while (!float.TryParse(Console.ReadLine(), out userCurrentAirPressure))
                {
                    Console.WriteLine("Invalid air pressure format, please try again");
                }

                try
                {
                    i_CurrentVehicle.SetCurrentTire(i_CurrentVehicle.TierList[tireIndex], userManufacturerName, userCurrentAirPressure);
                    ++tireIndex;
                }
                catch(ValueOutOfRangeException ex)
                {
                    Console.WriteLine("Tire air pressure" + ex.Message);
                }
            }
        }

        private string selectYourVehicleType()
        {
            bool   isValidIndexVehicleType = false;
            int    vehicleTypeIndex = 1;
            string userVehicleType = null;

            Console.WriteLine("Please select vehicle type");
            foreach(string vehicleTypeItem in VehiclesFactory.sr_VehicleType)
            {
                Console.WriteLine($"{vehicleTypeIndex++}) {vehicleTypeItem}");
            }

            while(!isValidIndexVehicleType)
            {
                if (int.TryParse(Console.ReadLine(), out vehicleTypeIndex))
                {
                    if (vehicleTypeIndex >= 1 && vehicleTypeIndex <= VehiclesFactory.sr_VehicleType.Count)
                    {
                        isValidIndexVehicleType = true;
                        userVehicleType = VehiclesFactory.sr_VehicleType[vehicleTypeIndex - 1];
                    }
                    else
                    {
                        Console.WriteLine("Invalid index option, please enter valid option index");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid format, please try again");
                }
            }

            return userVehicleType;
        }

        private void displayListOfLicensePlate()
        {
            bool isValidIndexOption = false;
            int  userChoose = 0;
            StringBuilder filterOptionBuilder = new StringBuilder();
            List<string>  filteredLicensePlateNumberList = new List<string>();

            filterOptionBuilder.AppendLine("Please select option");
            filterOptionBuilder.AppendLine("1) Show all");
            filterOptionBuilder.AppendLine("2) Show all vehicles In Repair status");
            filterOptionBuilder.AppendLine("3) Show all vehicles Repaired status");
            filterOptionBuilder.AppendLine("4) Show all vehicles Paid status");
            Console.Write(filterOptionBuilder.ToString());
            while (!isValidIndexOption)
            {
                if (int.TryParse(Console.ReadLine(), out userChoose))
                {
                    if (userChoose >= 1 && userChoose <= 4)
                    {
                        isValidIndexOption = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid index option, please enter valid option index");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid format, please try again and enter a number between (1-4)");
                }
            }

            switch(userChoose.ToString())
            {
                case "1":
                    foreach (KeyValuePair< string,VehicleInGarage> vehicleInGarageItem in m_GarageManager.VehiclesInGarage)
                    {
                        Console.WriteLine(vehicleInGarageItem.Key);
                    }
                    break;
                case "2":
                    m_GarageManager.FilterVehicleByStatus(eVehicleStatus.InRepair, ref filteredLicensePlateNumberList);
                    foreach(string licensePlateNumber in filteredLicensePlateNumberList)
                    {
                        Console.WriteLine(licensePlateNumber);
                    }
                    break;
                case "3":
                    m_GarageManager.FilterVehicleByStatus(eVehicleStatus.Repaired, ref filteredLicensePlateNumberList);
                    foreach (string licensePlateNumber in filteredLicensePlateNumberList)
                    {
                        Console.WriteLine(licensePlateNumber);
                    }
                    break;
                case "4":
                    m_GarageManager.FilterVehicleByStatus(eVehicleStatus.Paid, ref filteredLicensePlateNumberList);
                    foreach (string licensePlateNumber in filteredLicensePlateNumberList)
                    {
                        Console.WriteLine(licensePlateNumber);
                    }
                    break;
            }
        }

        private void inflateTiresToMax()
        {
            bool isValidUserInput = false;
            string licensePlateNumber = null;

            while(!isValidUserInput)
            {
                licensePlateNumber = getLicensePlateNumber();
                if(!m_GarageManager.IsVehicleInGarage(licensePlateNumber))
                {
                    if (checkIfUserWantToBackToMainMenu())
                    {
                        break;
                    }
                }
                else
                {
                    isValidUserInput = true;
                    m_GarageManager.SetAirPressureToMax(licensePlateNumber);
                    Console.WriteLine("Inflate success");
                }
            }
        }

        private void refuelVehicle()
        {
            bool isFuelEngine = false;
            bool isValidUserInput = false;
            bool isValidIndexUserInput = false;
            string licensePlateNumber = null;
            int userChoose = 0;
            float energyAmountToRefuel;
            eFuelType userFuelType = eFuelType.Octan95;
            StringBuilder fuelTypeOptionBuilder = new StringBuilder();

            do
            {
                try
                {
                    licensePlateNumber = getLicensePlateNumber();
                    if (!m_GarageManager.IsVehicleInGarage(licensePlateNumber))
                    {
                        if (checkIfUserWantToBackToMainMenu())
                        {
                            isValidUserInput = true;
                            break;
                        }
                    }
                    else
                    {
                        m_GarageManager.IsFuelEngineVehicle(licensePlateNumber);
                        isFuelEngine = true;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (checkIfUserWantToBackToMainMenu())
                    {
                        isValidUserInput = true;
                        break;
                    }
                }
            }
            while(!isFuelEngine);
            fuelTypeOptionBuilder.AppendLine("Please select fuel type");
            fuelTypeOptionBuilder.AppendLine("1) Soler");
            fuelTypeOptionBuilder.AppendLine("2) Octan95");
            fuelTypeOptionBuilder.AppendLine("3) Octan96");
            fuelTypeOptionBuilder.AppendLine("4) Octan98");
            while (!isValidUserInput)
            {
                try
                {
                    Console.Write(fuelTypeOptionBuilder.ToString());
                    while (!isValidIndexUserInput)
                    {
                        if (int.TryParse(Console.ReadLine(), out userChoose))
                        {
                            if (userChoose >= 1 && userChoose <= 4)
                            {
                                isValidIndexUserInput = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid index option, please enter valid option index");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid format, please try again and enter a number between (1-4)");
                        }
                    }

                    switch (userChoose.ToString())
                    {
                        case "1":
                            userFuelType = eFuelType.Soler;
                            break;
                        case "2":
                            userFuelType = eFuelType.Octan95;
                            break;
                        case "3":
                            userFuelType = eFuelType.Octan96;
                            break;
                        case "4":
                            userFuelType = eFuelType.Octan98;
                            break;
                    }

                    Console.WriteLine("Enter energy amount to refuel:");
                    while (!float.TryParse(Console.ReadLine(), out energyAmountToRefuel))
                    {
                        Console.WriteLine("Invalid format energy amount to refuel, please try again and enter a number");
                    }

                    m_GarageManager.RefuleVehicle(licensePlateNumber, energyAmountToRefuel, userFuelType);
                    Console.WriteLine("Refuel success");
                    isValidUserInput = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    isValidIndexUserInput = false;
                }
            }
        }

        private void rechargeVehicle()
        {
            bool isElectricEngine = false;
            bool isValidUserInput = false;
            string licensePlateNumber = null;
            float energyAmountToReCharge = 0;

            do
            {
                try
                {
                    licensePlateNumber = getLicensePlateNumber();
                    if (!m_GarageManager.IsVehicleInGarage(licensePlateNumber))
                    {
                        if (checkIfUserWantToBackToMainMenu())
                        {
                            isValidUserInput = true;
                            break;
                        }
                    }
                    else
                    {
                        m_GarageManager.IsElectricEngineVehicle(licensePlateNumber);
                        isElectricEngine = true;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    if(checkIfUserWantToBackToMainMenu())
                    {
                        isValidUserInput = true;
                        break;
                    }
                }
            }
            while (!isElectricEngine);
            while (!isValidUserInput)
            {
                try
                {
                    Console.WriteLine("Enter energy amount to charge (in minutes):");
                    while (!float.TryParse(Console.ReadLine(), out energyAmountToReCharge))
                    {
                        Console.WriteLine("Invalid format energy amount to charge, please try again and enter a number");
                    }

                    m_GarageManager.RechargeVehicle(licensePlateNumber, energyAmountToReCharge);
                    Console.WriteLine("Recharge success");
                    isValidUserInput = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine("Energy amount" + ex.Message);
                }
            }
        }

        private void displayVehicleDetails()
        {
            bool isValidUserInput = false;
            string licensePlateNumber = null;

            while (!isValidUserInput)
            {
                licensePlateNumber = getLicensePlateNumber();
                if (!m_GarageManager.IsVehicleInGarage(licensePlateNumber))
                {
                    if (checkIfUserWantToBackToMainMenu())
                    {
                        break;
                    }
                }
                else
                {
                    Dictionary<string, string> vehicleDetails = m_GarageManager.GetVehicleDetails(licensePlateNumber);
                    foreach (KeyValuePair<string,string> currentDetail in vehicleDetails)
                    {
                        Console.WriteLine(currentDetail.Key + ": " + currentDetail.Value);
                    }

                    isValidUserInput = true;
                }
            }
        }

        private bool checkIfUserWantToBackToMainMenu()
        {
            int userIndexOption = 0;
            bool isValidIndexChoose = false;
            bool userWantExit = false;

            Console.WriteLine("If you want to return to the main menu, enter 1. To try again, enter 2.");
            while (!isValidIndexChoose)
            {
                if (int.TryParse(Console.ReadLine(), out userIndexOption))
                {
                    if (userIndexOption >= 1 && userIndexOption <= 2)
                    {
                        if (userIndexOption == 1)
                        {
                            userWantExit = true;
                        }
                        isValidIndexChoose = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid index option, please enter valid option index between (1-2)");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid format, please try again and enter number");
                }
            }

            return userWantExit;
        }
    }
}