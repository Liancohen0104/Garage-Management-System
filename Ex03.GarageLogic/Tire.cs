namespace Ex03.GarageLogic
{
    public class Tire
    {
        // $G$ DSN-999 (-3) The "maximum air pressure" field should be readonly member of class wheel.
        private string m_ManufacturerName;
        private  float m_CurrentAirPressure;
        private  float m_MaxAirPressure;

        public string ManufacturerName
        {
            get
            {
                return this.m_ManufacturerName;
            }
            set
            {
                this.m_ManufacturerName = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return this.m_CurrentAirPressure;
            }
            set
            {
                this.m_CurrentAirPressure = value;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return this.m_MaxAirPressure;
            }
            set
            {
                this.m_MaxAirPressure = value;
            }
        }

        public override string ToString()
        {
            return $"Manufacturer Name: {ManufacturerName}, AirPressure: {CurrentAirPressure}";
        }
    }
}
