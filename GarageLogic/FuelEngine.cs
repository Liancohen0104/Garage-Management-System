using System;

namespace GarageLogic
{
    public class FuelEngine : Engine
    {
        private eFuelType m_FuelType;

        public FuelEngine(eFuelType i_FuelType)
        {
            m_FuelType = i_FuelType;
        }

        public void RefuelEnergy(float i_EnergyAmount, eFuelType i_UserChooseFuelType)
        {
            float energyAfterRefuel = CurrentEnergy + i_EnergyAmount;

            if (i_UserChooseFuelType != m_FuelType)
            {
                throw new ArgumentException("Fuel type not suitable to your vehicle");
            }

            if (energyAfterRefuel > MaxEnergyCapacity || i_EnergyAmount < 0)
            {
                throw new ValueOutOfRangeException(
                    0,
                    (float)Math.Round(MaxEnergyCapacity - CurrentEnergy, 2),
                    "Energy amount");
            }

            CurrentEnergy += i_EnergyAmount;
        }

        public override string ToString()
        {
            return $"Current fuel: {CurrentEnergy}, fuel type: {m_FuelType.ToString()} ";
        }
    }
}