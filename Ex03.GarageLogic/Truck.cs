using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsRefrigerated;
        private float m_CargoVolume;

        public Truck(string i_LicensePlateNumber) : base(new FuelEngine(eFuelType.Soler), i_LicensePlateNumber)
        {
            EngineType.MaxEnergyCapacity = 125.0f;
            initializeUniqueTruckQuestions();
            InitializeTire();
        }

        public bool IsRefrigerated
        {
            get
            {
                return this.m_IsRefrigerated;
            }
            set
            {
                this.m_IsRefrigerated = value;
            }
        }

        public void InitializeTire()
        {
            TierList = new List<Tire>(14);
            for (int i = 0; i < 14; ++i)
            {
                Tire currentTire = new Tire();
                TierList.Add(currentTire);
                currentTire.MaxAirPressure = 29.0f;
            }
        }

        public override void SetUniqueFieldsForVehicle(Dictionary<string, string> i_UserAnswers)
        {
            switch(i_UserAnswers["Refrigerated"])
            {
                case "1":
                    m_IsRefrigerated = true;
                    break;
                case "2":
                    m_IsRefrigerated = false;
                    break;
            }

            m_CargoVolume = float.Parse(i_UserAnswers["Cargo volume"]);
        }

        // $G$ DSN-003 (-5) Logic is not responsible for the way user interaction is handled. Questions are not Properties.
        private void initializeUniqueTruckQuestions()
        {
            StringBuilder refrigeratedBuilder = new StringBuilder();

            refrigeratedBuilder.AppendLine("Please select if your truck is refrigerated ot not");
            refrigeratedBuilder.AppendLine("1) Yes");
            refrigeratedBuilder.AppendLine("2) No");
            this.m_UserInputQuestions.Add("Refrigerated", refrigeratedBuilder.ToString());
            this.m_UserInputQuestions.Add("Cargo volume", "Enter your truck cargo volume:");
        }

        public override void FieldsValidation(Dictionary<string, string> i_UserAnswers)
        {
            cargoVolumeValidation(i_UserAnswers);
            isRefrigeratedValidation(i_UserAnswers);
        }

        private void cargoVolumeValidation(Dictionary<string, string> i_UserAnswers)
        {
            float cargoVolume;

            if(float.TryParse(i_UserAnswers["Cargo volume"], out cargoVolume))
            {
                if(cargoVolume < 0 || cargoVolume > float.MaxValue)
                {
                    throw new ValueOutOfRangeException(0, float.MaxValue, "Cargo volume");
                }
            }
            else
            {
                throw new FormatException("Wrong format cargo volume, please try again and enter number");
            }
        }

        private void isRefrigeratedValidation(Dictionary<string, string> i_UserAnswers)
        {
            int isRefrigerated;

            if (int.TryParse(i_UserAnswers["Refrigerated"], out isRefrigerated))
            {
                if (isRefrigerated < 1 || isRefrigerated > 2)
                {
                    throw new ValueOutOfRangeException(1, 2, "Refrigerated");
                }
            }
            else
            {
                throw new FormatException("Wrong format is refrigerated, please try again and enter number between (1-2)");
            }
        }

        public override Dictionary<string, string> GetDetailsFieldsForVehicle()
        {
            Dictionary<string, string> vehicleDetailsDictionary = new Dictionary<string, string>();

            vehicleDetailsDictionary = base.GetDetailsFieldsForVehicle();
            vehicleDetailsDictionary["Cargo volume"] = m_CargoVolume.ToString();
            if(IsRefrigerated)
            {
                vehicleDetailsDictionary["Is Refrigerated"] = "Yes";
            }
            else
            {
                vehicleDetailsDictionary["Is Refrigerated"] = "No";
            }

            return vehicleDetailsDictionary;
        }
    }
}