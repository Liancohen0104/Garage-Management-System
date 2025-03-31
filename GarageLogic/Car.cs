using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public class Car : Vehicle
    {
        private eCarColor m_CarColor;
        private eDoorsAmount m_DoorsAmount;

        public Car(Engine i_EngineType, string i_LicensePlateNumber) : base(i_EngineType, i_LicensePlateNumber)
        {
            if (i_EngineType is ElectricEngine)
            {
                i_EngineType.MaxEnergyCapacity = 5.4f;
            }
            else
            {
                i_EngineType.MaxEnergyCapacity = 52.0f;
            }

            InitializeTire();
            initializeUniqueCarQuestions();
        }

        public void InitializeTire()
        {
            Tire currentTire;

            TierList = new List<Tire>(5);
            for (int i = 0; i < 5; ++i)
            {
                currentTire = new Tire();
                TierList.Add(currentTire);
                currentTire.MaxAirPressure = 34.0f;
            }
        }

        public override void SetUniqueFieldsForVehicle(Dictionary<string, string> i_UserAnswers)
        {
            switch (i_UserAnswers["Doors count"])
            {
                case "2":
                    m_DoorsAmount = eDoorsAmount.TwoDoors;
                    break;
                case "3":
                    m_DoorsAmount = eDoorsAmount.ThreeDoors;
                    break;
                case "4":
                    m_DoorsAmount = eDoorsAmount.FourDoors;
                    break;
                case "5":
                    m_DoorsAmount = eDoorsAmount.FiveDoors;
                    break;
            }

            switch (i_UserAnswers["Car color"])
            {
                case "1":
                    m_CarColor = eCarColor.Blue;
                    break;
                case "2":
                    m_CarColor = eCarColor.Black;
                    break;
                case "3":
                    m_CarColor = eCarColor.White;
                    break;
                case "4":
                    m_CarColor = eCarColor.Gray;
                    break;
            }
        }

        private void initializeUniqueCarQuestions()
        {
            StringBuilder carColorBuilder = new StringBuilder();

            carColorBuilder.AppendLine("Please select car color");
            carColorBuilder.AppendLine("1) Blue");
            carColorBuilder.AppendLine("2) Black");
            carColorBuilder.AppendLine("3) White");
            carColorBuilder.AppendLine("4) Gray");
            m_UserInputQuestions.Add("Car color", carColorBuilder.ToString());
            m_UserInputQuestions.Add("Doors count", "Enter your doors count:");
        }

        public override void FieldsValidation(Dictionary<string, string> i_UserAnswers)
        {
            doorsCountValidation(i_UserAnswers);
            carColorValidation(i_UserAnswers);
        }

        private void doorsCountValidation(Dictionary<string, string> i_UserAnswers)
        {
            int doorsCount;

            if (int.TryParse(i_UserAnswers["Doors count"], out doorsCount))
            {
                if (doorsCount < 2 || doorsCount > 5)
                {
                    throw new ValueOutOfRangeException(2, 5, "Doors count");
                }
            }
            else
            {
                throw new FormatException("Wrong format doors count, please try again and enter number between (2-5)");
            }
        }

        private void carColorValidation(Dictionary<string, string> i_UserAnswers)
        {
            int carColor;

            if (int.TryParse(i_UserAnswers["Car color"], out carColor))
            {
                if (carColor < 1 || carColor > 4)
                {
                    throw new ValueOutOfRangeException(1, 4, "Car color");
                }
            }
            else
            {
                throw new FormatException("Wrong format Car color, please try again and enter number between (1-4)");
            }
        }

        public override Dictionary<string, string> GetDetailsFieldsForVehicle()
        {
            Dictionary<string, string> vehicleDetailsDictionary = new Dictionary<string, string>();

            vehicleDetailsDictionary = base.GetDetailsFieldsForVehicle();
            vehicleDetailsDictionary["Car color"] = m_CarColor.ToString();
            vehicleDetailsDictionary["Doors count"] = ((int)m_DoorsAmount).ToString();

            return vehicleDetailsDictionary;
        }
    }
}