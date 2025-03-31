namespace GarageLogic
{
    public abstract class Engine
    {
        private float m_CurrentEnergy;
        private float m_MaxEnergyCapacity;

        public float CurrentEnergy
        {
            get
            {
                return m_CurrentEnergy;
            }
            set
            {
                m_CurrentEnergy = value;
            }
        }

        public float MaxEnergyCapacity
        {
            get
            {
                return m_MaxEnergyCapacity;
            }
            set
            {
                m_MaxEnergyCapacity = value;
            }
        }

        public void CurrentEnergyValidation(float i_CurrentEnergy)
        {
            if (i_CurrentEnergy < 0 || i_CurrentEnergy > m_MaxEnergyCapacity)
            {
                throw new ValueOutOfRangeException(0, m_MaxEnergyCapacity);
            }
        }

        public abstract override string ToString();
    }
}