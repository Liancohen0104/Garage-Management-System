using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_ModelName;
        protected readonly string r_LicensePlateNumber;
        protected Engine m_EngineType;
        protected List<Tire> m_TierList;
        protected Dictionary<string, string> m_UserInputQuestions;

        protected Vehicle(Engine i_EngineType, string i_LicensePlateNumber)
        {
            this.m_EngineType = i_EngineType;
            this.r_LicensePlateNumber = i_LicensePlateNumber;
            m_UserInputQuestions = new Dictionary<string, string>();
        }

        public Dictionary<string, string> UserInputQuestions
        {
            get
            {
                return this.m_UserInputQuestions;
            }
            set
            {
                this.m_UserInputQuestions = value;
            }
        }

        public string ModelName
        {
            get
            {
                return this.m_ModelName;
            }
            set
            {
                this.m_ModelName = value;
            }
        }

        public string LicensePlateNumber
        {
            get
            {
                return this.r_LicensePlateNumber;
            }
        }

        public Engine EngineType
        {
            get
            {
                return this.m_EngineType;
            }
            set
            {
                this.m_EngineType = value;
            }
        }

        public List<Tire> TierList
        {
            get
            {
                return this.m_TierList;
            }
            set
            {
                this.m_TierList = value;
            }
        }

        public abstract void SetUniqueFieldsForVehicle(Dictionary<string, string> i_UserAnswers);

        public abstract void FieldsValidation(Dictionary<string, string> i_UserAnswers);

        public void SetCurrentTire(Tire i_CurrentTire, string i_CurrentManufacturer, float i_CurrentAirPressure)
        {
            if(i_CurrentAirPressure < 0 || i_CurrentAirPressure > i_CurrentTire.MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, i_CurrentTire.MaxAirPressure);
            }
            else
            {
                i_CurrentTire.CurrentAirPressure = i_CurrentAirPressure;
                i_CurrentTire.ManufacturerName = i_CurrentManufacturer;
            }
        }

        public virtual Dictionary<string, string> GetDetailsFieldsForVehicle()
        {
            Dictionary<string, string> vehicleDetailsDictionary = new Dictionary<string, string>();
            int tireIndex = 1;

            vehicleDetailsDictionary["License plate number"] = LicensePlateNumber;
            vehicleDetailsDictionary["Model Name"] = ModelName;
            foreach(Tire currentTire in m_TierList)
            {
                vehicleDetailsDictionary[$"Tire {tireIndex++}:"] = currentTire.ToString();
                
            }

            vehicleDetailsDictionary["Energy details"] = EngineType.ToString();

            return vehicleDetailsDictionary;
        }
    }
}
