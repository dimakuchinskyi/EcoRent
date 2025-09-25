using System;

namespace ElectricCarRental
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Виведення заголовка програми
            Console.WriteLine("=== СИСТЕМА ОРЕНДИ ЕЛЕКТРОМОБІЛІВ ECO RENT ===\n");

            // Створення клієнта
            Customer customer = new Customer
            {
                Name = "Іван Іванов",
                Phone = "+380123456789",
                Email = "ivan.ivanov@example.com",
                Age = 25,
                HasLicense = true
            };

            // Перевірка, чи клієнт може орендувати авто
            if (!customer.CanRent())
            {
                Console.WriteLine("Клієнт не може орендувати авто (недостатній вік або відсутність прав).");
                return;
            }

            // Створення доступних автомобілів
            ElectricCar tesla = new ElectricCar
            {
                Model = "Tesla Model 3",
                LicensePlate = "AA1234BB",
                BatteryCapacity = 75,
                PricePerHour = 5.50,
                PricePerDay = 89.99,
                IsAvailable = true
            };

            ElectricCar nissan = new ElectricCar
            {
                Model = "Nissan Leaf",
                LicensePlate = "BC5678DE",
                BatteryCapacity = 62,
                PricePerHour = 4.20,
                PricePerDay = 69.99,
                IsAvailable = true
            };

            // Виведення списку доступних автомобілів
            Console.WriteLine("🚗 ДОСТУПНІ АВТОМОБІЛІ:");
            Console.WriteLine($"1. {tesla.GetInfo()}");
            Console.WriteLine($"2. {nissan.GetInfo()}");
            Console.Write("\nОберіть автомобіль (1 або 2): ");
            var choice = Console.ReadLine();

            // Вибір автомобіля на основі введення користувача
            ElectricCar selectedCar = choice == "1" ? tesla : nissan;

            // Перевірка доступності обраного автомобіля
            if (!selectedCar.IsAvailable)
            {
                Console.WriteLine("Обраний автомобіль недоступний для оренди.");
                return;
            }

            // Введення тривалості оренди
            Console.Write("\nВведіть тривалість оренди (години): ");
            int hours = int.Parse(Console.ReadLine());

            // Створення об'єкта оренди
            Rental rental = new Rental
            {
                Car = selectedCar,
                Customer = customer,
                Hours = hours
            };

            try
            {
                // Початок оренди
                rental.StartRental();
                Console.WriteLine($"Оренда розпочата. Вартість: {rental.TotalCost} грн");
            }
            catch (Exception ex)
            {
                // Обробка помилок при початку оренди
                Console.WriteLine($"Помилка: {ex.Message}");
                return;
            }

            // Завершення оренди
            rental.CompleteRental();
            Console.WriteLine($"Оренда завершена. Оновлений пробіг: {selectedCar.Mileage} км");

            // Відправка повідомлення клієнту
            Notification notification = new Notification
            {
                Message = $"Дякуємо за оренду {selectedCar.Model}. Загальна вартість: {rental.TotalCost} грн."
            };
            notification.Send();
            Console.WriteLine(notification.GetStatus());
        }
    }
}