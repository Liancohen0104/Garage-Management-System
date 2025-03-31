using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private eLicenceType m_LicenceType;
        private int m_EngineDisplacement;

        public Motorcycle(Engine i_EngineType, string i_LicensePlateNumber) : base(i_EngineType, i_LicensePlateNumber)
        {
            if (i_EngineType is ElectricEngine)
            {
                i_EngineType.MaxEnergyCapacity = 2.9f;
            }
            else
            {
                i_EngineType.MaxEnergyCapacity = 6.2f;
            }

            InitializeTire();
            initializeUniqueMotorcycleQuestions();
        }

        public void InitializeTire()
        {
            TierList = new List<Tire>(2);
            for (int i = 0; i < 2; ++i)
            {
                Tire currentTire = new Tire();
                TierList.Add(currentTire);
                currentTire.MaxAirPressure = 32.0f;
            }
        }

        public override void SetUniqueFieldsForVehicle(Dictionary<string, string> i_UserAnswers)
        {
            m_EngineDisplacement = int.Parse(i_UserAnswers["Engine displacement"]);
            switch (i_UserAnswers["License type"])
            {
                case "1":
                    m_LicenceType = eLicenceType.A1;
                    break;
                case "2":
                    m_LicenceType = eLicenceType.A2;
                    break;
                case "3":
                    m_LicenceType = eLicenceType.B1;
                    break;
                case "4":
                    m_LicenceType = eLicenceType.B2;
                    break;
            }
        }

        private void initializeUniqueMotorcycleQuestions()
        {
            StringBuilder licenseTypeBuilder = new StringBuilder();

            licenseTypeBuilder.AppendLine("Please select an license type");
            licenseTypeBuilder.AppendLine("1) A1");
            licenseTypeBuilder.AppendLine("2) A2");
            licenseTypeBuilder.AppendLine("3) B1");
            licenseTypeBuilder.AppendLine("4) B2");
            m_UserInputQuestions.Add("License type", licenseTypeBuilder.ToString());
            m_UserInputQuestions.Add("Engine displacement", "Enter your engine displacement:");
        }

        public override void FieldsValidation(Dictionary<string, string> i_UserAnswers)
        {
            engineDisplacementValidation(i_UserAnswers);
            licenseTypeValidation(i_UserAnswers);
        }

        private void licenseTypeValidation(Dictionary<string, string> i_UserAnswers)
        {
            int licenseType;

            if (int.TryParse(i_UserAnswers["License type"], out licenseType))
            {
                if (licenseType < 1 || licenseType > 4)
                {
                    throw new ValueOutOfRangeException(1, 4, "License type");
                }
            }
            else
            {
                throw new FormatException("Wrong format License type, please try again and enter number between (1-4)");
            }
        }

        private void engineDisplacementValidation(Dictionary<string, string> i_UserAnswers)
        {
            float engineDisplacement;

            if (float.TryParse(i_UserAnswers["Engine displacement"], out engineDisplacement))
            {
                if (engineDisplacement < 0 || engineDisplacement > float.MaxValue)
                {
                    throw new ValueOutOfRangeException(0, float.MaxValue, "Engine displacement");
                }
            }
            else
            {
                throw new FormatException("Wrong format Engine displacement, please try again and enter number");
            }
        }

        public override Dictionary<string, string> GetDetailsFieldsForVehicle()
        {
            Dictionary<string, string> vehicleDetailsDictionary = new Dictionary<string, string>();

            vehicleDetailsDictionary = base.GetDetailsFieldsForVehicle();
            vehicleDetailsDictionary["License type"] = m_LicenceType.ToString();
            vehicleDetailsDictionary["Engine displacement"] = m_EngineDisplacement.ToString();

            return vehicleDetailsDictionary;
        }
    }
}