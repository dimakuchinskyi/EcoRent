namespace ElectricCarRental
{
    public class ChargingStation
    {
        // Властивості зарядної станції
        public string Location { get; set; } // Розташування станції
        public int AvailablePorts { get; set; } // Кількість доступних портів
        public int TotalPorts { get; set; } // Загальна кількість портів
        public double CostPerKwh { get; set; } // Вартість заряджання за кВт·год
        public int CarsCharged { get; set; } // Лічильник обслужених авто

        // Перевірка доступності портів для заряджання
        public bool CanCharge()
        {
            return AvailablePorts > 0;
        }

        // Розрахунок вартості заряджання
        public double CalculateChargingCost(double kwh)
        {
            return kwh * CostPerKwh;
        }

        // Початок заряджання автомобіля
        public void StartCharging(ElectricCar car)
        {
            if (AvailablePorts > 0)
            {
                AvailablePorts--; // Зменшення кількості доступних портів
                CarsCharged++; // Збільшення лічильника обслужених авто
                Console.WriteLine($"Заряджання {car.Model} розпочато на станції {Location}");
            }
        }

        // Завершення заряджання автомобіля
        public void StopCharging()
        {
            if (AvailablePorts < TotalPorts)
            {
                AvailablePorts++; // Збільшення кількості доступних портів
                Console.WriteLine("Заряджання завершено");
            }
        }

        // Отримання статусу зарядної станції
        public string GetStatus()
        {
            return $"{Location}: {AvailablePorts}/{TotalPorts} портів вільно | Обслужено: {CarsCharged} авто";
        }
    }
}