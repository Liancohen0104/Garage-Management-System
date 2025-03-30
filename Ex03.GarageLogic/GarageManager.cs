using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private Dictionary<string, VehicleInGarage> m_VehiclesInGarage;

        public GarageManager()
        {
            this.m_VehiclesInGarage = new Dictionary<string, VehicleInGarage>();
        }

        public Dictionary<string, VehicleInGarage> VehiclesInGarage
        {
            get
            {
                return this.m_VehiclesInGarage;
            }
            set
            {
                this.m_VehiclesInGarage = value;
            }
        }

        public bool IsVehicleInGarage(string i_LicensePlateNumber)
        {
            return m_VehiclesInGarage.ContainsKey(i_LicensePlateNumber);
        }

        public void UpdateStatusOfVehicle(
            string i_LicensePlateNumber,
            eVehicleStatus i_VehicleStatus = eVehicleStatus.InRepair)
        {
            m_VehiclesInGarage[i_LicensePlateNumber].VehicleStatus = i_VehicleStatus;
        }

        public Vehicle AddVehicle(string i_LicensePlateNumber, string i_VehicleType)
        {
            m_VehiclesInGarage.Add(
                i_LicensePlateNumber,
                new VehicleInGarage(VehiclesFactory.CreateVehicle(i_VehicleType, i_LicensePlateNumber)));

            return m_VehiclesInGarage[i_LicensePlateNumber].CurrentVehicle;
        }

        public void SetPersonalInfo(string i_LicensePlateNumber, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            VehiclesInGarage[i_LicensePlateNumber].SetUserInfoFields(i_OwnerName, i_OwnerPhoneNumber);
        }

        public void LicensePlateNumberValidation(string i_LicensePlateNumber)
        {
            if (i_LicensePlateNumber.Length < 7 || i_LicensePlateNumber.Length > 8)
            {
                throw new ValueOutOfRangeException(7, 8);
            }
        }

        public void FilterVehicleByStatus(eVehicleStatus i_UserChoiceStatus, ref List<string> o_PlateNumberList)
        {
            foreach (KeyValuePair<string,VehicleInGarage> vehicleInGarageItem in VehiclesInGarage)
            {
                if(vehicleInGarageItem.Value.VehicleStatus == i_UserChoiceStatus)
                {
                    o_PlateNumberList.Add(vehicleInGarageItem.Key);
                }
            }
        }

        public void SetAirPressureToMax(string i_LicensePlateNumber)
        {
            foreach(Tire currentTire in m_VehiclesInGarage[i_LicensePlateNumber].CurrentVehicle.TierList)
            {
                currentTire.CurrentAirPressure = currentTire.MaxAirPressure;
            }
        }

        public void IsFuelEngineVehicle(string i_LicensePlateNumber)
        {
            if (!(m_VehiclesInGarage[i_LicensePlateNumber].CurrentVehicle.EngineType is FuelEngine))
            {
                throw new ArgumentException("The vehicle is electric, please enter vehicle with fuel engine");
            }
        }

        public void IsElectricEngineVehicle(string i_LicensePlateNumber)
        {
            if (!(m_VehiclesInGarage[i_LicensePlateNumber].CurrentVehicle.EngineType is ElectricEngine))
            {
                throw new ArgumentException("The vehicle is with fuel engine, please enter vehicle with electric engine");
            }
        }

        public void RefuleVehicle(string i_LicensePlateNumber, float i_EnergyAmount, eFuelType i_UserFuelTypeChoice)
        {
            (VehiclesInGarage[i_LicensePlateNumber].CurrentVehicle.EngineType as FuelEngine)
                .RefuelEnergy(i_EnergyAmount, i_UserFuelTypeChoice);
        }

        public void RechargeVehicle(string i_LicensePlateNumber, float i_EnergyAmount)
        {
            (VehiclesInGarage[i_LicensePlateNumber].CurrentVehicle.EngineType as ElectricEngine)
                .RechargeEnergy(i_EnergyAmount / 60);
        }

        public Dictionary<string,string> GetVehicleDetails(string i_LicensePlateNumber)
        {
            Dictionary<string, string> vehicleDetails =
                m_VehiclesInGarage[i_LicensePlateNumber].CurrentVehicle.GetDetailsFieldsForVehicle();

            vehicleDetails["Owner name"] = m_VehiclesInGarage[i_LicensePlateNumber].OwnerName;
            vehicleDetails["Vehicle status"] = m_VehiclesInGarage[i_LicensePlateNumber].VehicleStatus.ToString();

            return vehicleDetails;
        }
    }
}