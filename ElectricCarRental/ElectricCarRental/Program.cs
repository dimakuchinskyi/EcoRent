using System;

namespace ElectricCarRental
{
    class Program
    {
        static void Main()
        {
            // Встановлення кодування для коректного відображення символів
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Виведення заголовку програми
            Console.WriteLine("=== СИСТЕМА ОРЕНДИ ЕЛЕКТРОМОБІЛІВ ECO RENT ===\n");

            // Запуск демонстраційної системи
            DemoSystem();
        }

        static void DemoSystem()
        {
            // Створення об'єкта електромобіля Tesla
            var tesla = new ElectricCar
            {
                Model = "Tesla Model 3",
                LicensePlate = "AA1234BB",
                BatteryCapacity = 75,
                PricePerHour = 5.50,
                PricePerDay = 89.99,
                IsAvailable = true
            };

            // Створення об'єкта електромобіля Nissan
            var nissan = new ElectricCar
            {
                Model = "Nissan Leaf",
                LicensePlate = "BC5678DE",
                BatteryCapacity = 62,
                PricePerHour = 4.20,
                PricePerDay = 69.99,
                IsAvailable = true
            };

            // Створення клієнта 1
            var customer1 = new Customer
            {
                Name = "Іван Петренко",
                Phone = "+380501234567",
                Email = "ivan@example.com",
                HasLicense = true,
                Age = 25
            };

            // Створення клієнта 2
            var customer2 = new Customer
            {
                Name = "Марія Коваль",
                Phone = "+380671234567",
                Email = "maria@example.com",
                HasLicense = true,
                Age = 30
            };

            // Створення зарядної станції
            var chargingStation = new ChargingStation
            {
                Location = "Центр міста",
                AvailablePorts = 3,
                TotalPorts = 5,
                CostPerKwh = 0.35
            };

            // Виведення інформації про доступні автомобілі
            Console.WriteLine("🚗 ДОСТУПНІ АВТОМОБІЛІ:");
            Console.WriteLine($"- {tesla.GetInfo()}");
            Console.WriteLine($"- {nissan.GetInfo()}");

            // Виведення інформації про клієнтів
            Console.WriteLine($"\n👥 КЛІЄНТИ:");
            Console.WriteLine($"- {customer1.GetInfo()}");
            Console.WriteLine($"- {customer2.GetInfo()}");

            // Виведення статусу зарядної станції
            Console.WriteLine($"\n⚡ ЗАРЯДНА СТАНЦІЯ: {chargingStation.GetStatus()}");

            // Оренда автомобіля клієнтом 1
            Console.WriteLine($"\n📋 ОРЕНДА 1:");
            if (customer1.CanRent()) // Перевірка, чи клієнт може орендувати авто
            {
                var rental1 = new Rental
                {
                    Car = tesla,
                    Customer = customer1,
                    Hours = 3,
                    IsDaily = false
                };

                // Початок оренди
                rental1.StartRental();
                Console.WriteLine($"Створено: {rental1.GetRentalInfo()}");

                // Перевірка доступності портів для заряджання
                if (chargingStation.CanCharge())
                {
                    // Початок заряджання автомобіля
                    chargingStation.StartCharging(tesla);
                }

                // Завершення оренди
                rental1.CompleteRental();
                Console.WriteLine($"Завершено: {rental1.GetRentalInfo()}");
            }

            // Оренда автомобіля клієнтом 2
            Console.WriteLine($"\n📋 ОРЕНДА 2:");
            if (customer2.CanRent()) // Перевірка, чи клієнт може орендувати авто
            {
                var rental2 = new Rental
                {
                    Car = nissan,
                    Customer = customer2,
                    Hours = 2,
                    IsDaily = true
                };

                // Початок оренди
                rental2.StartRental();

                // Зміна методу оплати
                rental2.Payment.ChangePaymentMethod("GooglePay");
                Console.WriteLine($"Створено: {rental2.GetRentalInfo()}");

                // Завершення оренди
                rental2.CompleteRental();
                Console.WriteLine($"Завершено: {rental2.GetRentalInfo()}");

                // Повернення коштів за оренду
                Console.WriteLine($"\n💸 ПОВЕРНЕННЯ КОШТІВ:");
                rental2.Payment.ProcessRefund();
            }

            // Виведення підсумкової інформації
            Console.WriteLine($"\n📊 ПІДСУМКИ:");
            Console.WriteLine($"Клієнт 1 оренд: {customer1.TotalRentals}");
            Console.WriteLine($"Клієнт 2 оренд: {customer2.TotalRentals}");
            Console.WriteLine($"Пробіг Tesla: {tesla.Mileage} км");
            Console.WriteLine($"Пробіг Nissan: {nissan.Mileage} км");
            Console.WriteLine(chargingStation.GetStatus());
        }
    }
}