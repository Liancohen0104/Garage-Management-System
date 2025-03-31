using System;

namespace GarageLogic
{
    public class ElectricEngine : Engine
    {
        public void RechargeEnergy(float i_EnergyAmountInHours)
        {
            float energyAfterRecharge = CurrentEnergy + i_EnergyAmountInHours;

            if (energyAfterRecharge > MaxEnergyCapacity || i_EnergyAmountInHours < 0)
            {
                throw new ValueOutOfRangeException(0, (float)Math.Round((MaxEnergyCapacity - CurrentEnergy) * 60, 2));
            }

            CurrentEnergy += i_EnergyAmountInHours;
        }

        public override string ToString()
        {
            return $"Current electric energy: {CurrentEnergy}";
        }
    }
}