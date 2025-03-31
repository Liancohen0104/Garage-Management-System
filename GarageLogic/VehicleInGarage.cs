using System;

namespace GarageLogic
{
    public class VehicleInGarage
    {
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eVehicleStatus m_VehicleStatus;
        private Vehicle m_CurrentVehicle;

        public VehicleInGarage(Vehicle i_NewVehicle)
        {
            m_CurrentVehicle = i_NewVehicle;
        }

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }
            set
            {
                OwnerName = value;
            }
        }

        public eVehicleStatus VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }
            set
            {
                m_VehicleStatus = value;
            }
        }

        public Vehicle CurrentVehicle
        {
            get
            {
                return m_CurrentVehicle;
            }
            set
            {
                m_CurrentVehicle = value;
            }
        }

        public void OwnerPhoneNumberValidation(string i_OwnerPhoneNumber)
        {
            if (i_OwnerPhoneNumber.Length != 10)
            {
                throw new ValueOutOfRangeException(10, 10);
            }
        }

        public void SetUserInfoFields(string i_OwnerName, string i_OwnerPhoneNumber)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
        }
    }
}