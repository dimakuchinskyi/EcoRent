using System;

namespace ElectricCarRental
{
    public class ElectricCar
    {
        // Властивості електромобіля
        public string Model { get; set; } // Модель автомобіля
        public string LicensePlate { get; set; } // Державний номер
        public int BatteryCapacity { get; set; } // Ємність батареї в кВт·год
        public double PricePerHour { get; set; } // Ціна за годину оренди
        public double PricePerDay { get; set; } // Ціна за день оренди
        public bool IsAvailable { get; set; } // Статус доступності автомобіля
        public int Mileage { get; set; } // Пробіг автомобіля

        // Отримання інформації про автомобіль
        public string GetInfo()
        {
            return $"{Model} ({LicensePlate}) - {BatteryCapacity}kWh - {(IsAvailable ? "Доступний" : "Зайнятий")}";
        }

        // Розрахунок вартості оренди
        public double CalculateCost(int hours, bool isDaily = false)
        {
            return isDaily ? hours * PricePerDay : hours * PricePerHour;
        }

        // Додавання пробігу до автомобіля
        public void AddMileage(int km)
        {
            Mileage += km;
        }

        // Перемикання статусу доступності автомобіля
        public void ToggleAvailability()
        {
            IsAvailable = !IsAvailable;
        }
    }
}