using System;

namespace Ex03.GarageLogic
{
    public class VehicleInGarage
    {
        private  string m_OwnerName;
        private  string m_OwnerPhoneNumber;
        private  eVehicleStatus m_VehicleStatus;
        private  Vehicle m_CurrentVehicle;

        public VehicleInGarage(Vehicle i_NewVehicle)
        {
            this.m_CurrentVehicle = i_NewVehicle;
        }

        public string OwnerName
        {
            get
            {
                return this.m_OwnerName;
            }
            set
            {
                this.OwnerName = value;
            }
        }

        public eVehicleStatus VehicleStatus
        {
            get
            {
                return this.m_VehicleStatus;
            }
            set
            {
                this.m_VehicleStatus = value;
            }
        }

        public Vehicle CurrentVehicle
        {
            get
            {
                return this.m_CurrentVehicle;
            }
            set
            {
                this.m_CurrentVehicle = value;
            }
        }

        public void OwnerPhoneNumberValidation(string i_OwnerPhoneNumber)
        {
            if(i_OwnerPhoneNumber.Length != 10)
            {
                throw new ValueOutOfRangeException(10, 10);
            }
        }

        public void SetUserInfoFields(string i_OwnerName, string i_OwnerPhoneNumber)
        {
            this.m_OwnerName = i_OwnerName;
            this.m_OwnerPhoneNumber = i_OwnerPhoneNumber;
        }
    }
}