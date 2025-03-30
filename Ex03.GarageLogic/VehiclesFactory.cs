using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class VehiclesFactory
    {
        public static readonly List<string> sr_VehicleType =
            new List<string> {"Fuel Motorcycle", "Electric Motorcycle", "Fuel Car", "Electric Car", "Truck" };

    public static Vehicle CreateVehicle(string i_VehicleType, string i_LicensePlateNumber)
        {
            Vehicle newVehicle = null;

            switch (i_VehicleType)
            {
                case "Fuel Motorcycle":
                    newVehicle = createFuelMotorcycle(i_LicensePlateNumber);
                    break;
                case "Electric Motorcycle":
                    newVehicle = createElectricMotorcycle(i_LicensePlateNumber);
                    break;
                case "Fuel Car":
                    newVehicle = createFuelCar(i_LicensePlateNumber);
                    break;
                case "Electric Car":
                    newVehicle = createElectricCar(i_LicensePlateNumber);
                    break;
                case "Truck":
                    newVehicle = createTruck(i_LicensePlateNumber);
                    break;
            }

            return newVehicle;
        }

        private static Truck createTruck(string i_LicensePlateNumber)
        {
            return new Truck(i_LicensePlateNumber);
        }

        private static Car createElectricCar(string i_LicensePlateNumber)
        {
            return new Car(new ElectricEngine(), i_LicensePlateNumber);
        }

        private static Car createFuelCar(string i_LicensePlateNumber)
        {
            return new Car(new FuelEngine(eFuelType.Octan95), i_LicensePlateNumber);
        }

        private static Motorcycle createElectricMotorcycle(string i_LicensePlateNumber)
        {
            return new Motorcycle(new ElectricEngine(), i_LicensePlateNumber);
        }

        private static Motorcycle createFuelMotorcycle(string i_LicensePlateNumber)
        {
            return new Motorcycle(new FuelEngine(eFuelType.Octan98), i_LicensePlateNumber);
        }
    }
}