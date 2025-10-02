using System;
using System.Collections.Generic;

namespace ElectricCarRental
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== СИСТЕМА ОРЕНДИ ЕЛЕКТРОМОБІЛІВ ECO RENT ===");
            Console.ResetColor();
            Console.WriteLine();

            // Ініціалізація даних
            var customer = InitializeCustomer();
            // Перевірка доступу: 18+ і наявність прав
            if (!customer.CanRent())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Доступ заборонено: потрібні 18+ років і дійсні водійські права.");
                Console.ResetColor();
                return;
            }
            var cars = InitializeCars();
            var chargingStation = InitializeChargingStation();
            var loyaltyProgram = InitializeLoyaltyProgram(customer);
            var rentalHistory = new List<Rental>();

            while (true)
            {
                ShowMainMenu();
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAvailableCars(cars);
                        break;
                    case "2":
                        RentCar(cars, customer, loyaltyProgram, rentalHistory);
                        break;
                    case "3":
                        ShowChargingStation(chargingStation, cars);
                        break;
                    case "4":
                        ShowCustomerProfile(customer, loyaltyProgram);
                        break;
                    case "5":
                        ShowRentalHistory(rentalHistory);
                        break;
                    case "6":
                        ShowFeedbackSystem();
                        break;
                    case "7":
                        ShowInvoiceSystem();
                        break;
                    case "8":
                        ShowDriverLicenseInfo();
                        break;
                    case "9":
                        ShowAddressSystem();
                        break;
                    case "10":
                        ToggleCarAvailability(cars);
                        break;
                    case "0":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Дякуємо за використання EcoRent! До побачення!");
                        Console.ResetColor();
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Невірний вибір! Спробуйте ще раз.");
                        Console.ResetColor();
                        break;
                }

                Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void ShowMainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== ГОЛОВНЕ МЕНЮ ===");
            Console.ResetColor();
            Console.WriteLine("1. 🚗 Переглянути доступні автомобілі");
            Console.WriteLine("2. 🏁 Орендувати автомобіль");
            Console.WriteLine("3. ⚡ Зарядні станції");
            Console.WriteLine("4. 👤 Мій профіль");
            Console.WriteLine("5. 📋 Історія оренд");
            Console.WriteLine("6. 💬 Система відгуків");
            Console.WriteLine("7. 🧾 Рахунки");
            Console.WriteLine("8. 🆔 Водійські права");
            Console.WriteLine("9. 📍 Адреси");
            Console.WriteLine("10. 🔁 Перемкнути доступність авто");
            Console.WriteLine("0. ❌ Вийти");
            Console.Write("\nОберіть опцію: ");
        }

        static Customer InitializeCustomer()
        {
            Console.Write("Введіть ваше ім'я: ");
            var name = Console.ReadLine() ?? "Анонімний Клієнт";
            Console.Write("Введіть телефон: ");
            var phone = Console.ReadLine() ?? "+380000000000";
            Console.Write("Введіть email: ");
            var email = Console.ReadLine() ?? "client@example.com";
            Console.Write("Введіть вік: ");
            int.TryParse(Console.ReadLine(), out int age);
            Console.Write("Чи є у вас водійські права? (y/n): ");
            var hasLicense = (Console.ReadLine() ?? "").Trim().ToLower() == "y";

            return new Customer
            {
                Name = name,
                Phone = phone,
                Email = email,
                Age = age,
                HasLicense = hasLicense
            };
        }

        static List<ElectricCar> InitializeCars()
        {
            return new List<ElectricCar>
            {
                new ElectricCar
                {
                    Model = "Tesla Model 3",
                    LicensePlate = "AA1234BB",
                    BatteryCapacity = 75,
                    PricePerHour = 5.50,
                    PricePerDay = 89.99,
                    IsAvailable = true
                },
                new ElectricCar
                {
                    Model = "Nissan Leaf",
                    LicensePlate = "BC5678DE",
                    BatteryCapacity = 62,
                    PricePerHour = 4.20,
                    PricePerDay = 69.99,
                    IsAvailable = true
                },
                new ElectricCar
                {
                    Model = "BMW i3",
                    LicensePlate = "CC9999XX",
                    BatteryCapacity = 42,
                    PricePerHour = 3.80,
                    PricePerDay = 59.99,
                    IsAvailable = false
                }
            };
        }

        static ChargingStation InitializeChargingStation()
        {
            return new ChargingStation
            {
                Location = "Kyiv Center",
                AvailablePorts = 3,
                TotalPorts = 5,
                CostPerKwh = 12.5,
                CarsCharged = 15
            };
        }

        static LoyaltyProgram InitializeLoyaltyProgram(Customer customer)
        {
            return new LoyaltyProgram
            {
                CustomerName = customer.Name,
                Points = 0
            };
        }

        static void ShowAvailableCars(List<ElectricCar> cars)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== ДОСТУПНІ АВТОМОБІЛІ ===");
            Console.ResetColor();

            for (int i = 0; i < cars.Count; i++)
            {
                var car = cars[i];
                Console.WriteLine($"{i + 1}. {car.GetInfo()}");
                Console.WriteLine($"   Ціна: {car.PricePerHour} грн/год | {car.PricePerDay} грн/день");
                Console.WriteLine($"   Пробіг: {car.Mileage} км");
                Console.WriteLine();
            }
        }

        static void RentCar(List<ElectricCar> cars, Customer customer, LoyaltyProgram loyaltyProgram, List<Rental> rentalHistory)
        {
            if (!customer.CanRent())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Ви не можете орендувати авто! Перевірте вік та водійські права.");
                Console.ResetColor();
                return;
            }

            ShowAvailableCars(cars);
            Console.Write("Оберіть автомобіль (номер): ");
            if (!int.TryParse(Console.ReadLine(), out int carIndex) || carIndex < 1 || carIndex > cars.Count)
            {
                Console.WriteLine("Невірний вибір!");
                return;
            }

            var selectedCar = cars[carIndex - 1];
            if (!selectedCar.IsAvailable)
            {
                Console.WriteLine("Цей автомобіль недоступний!");
                return;
            }

            Console.Write("Введіть тривалість оренди (години): ");
            if (!int.TryParse(Console.ReadLine(), out int hours) || hours <= 0)
            {
                Console.WriteLine("Невірна кількість годин!");
                return;
            }

            Console.Write("Подобова оренда? (y/n): ");
            bool isDaily = (Console.ReadLine() ?? "").Trim().ToLower() == "y";

            var rental = new Rental
            {
                Car = selectedCar,
                Customer = customer,
                Hours = hours,
                IsDaily = isDaily
            };

            try
            {
                // Розрахунок для показу (використання CalculateCost)
                double previewCost = selectedCar.CalculateCost(hours, isDaily);
                Console.WriteLine($"ℹ️ Попередній розрахунок вартості: {previewCost} грн");

                rental.StartRental();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✅ Оренда розпочата! Вартість: {rental.TotalCost} грн");
                Console.ResetColor();

                // Додаємо бонуси
                int bonusPoints = (int)(rental.TotalCost / 10);
                loyaltyProgram.AddPoints(bonusPoints);
                Console.WriteLine($"🎁 Отримано {bonusPoints} бонусних балів!");

                // Зміна методу оплати (демонстрація ChangePaymentMethod)
                if (rental.Payment != null)
                {
                    rental.Payment.ChangePaymentMethod("Card");
                    Console.WriteLine($"💳 {rental.Payment.GetPaymentDetails()}");
                }

                rentalHistory.Add(rental);

                // Завершуємо оренду (додає пробіг через Car.AddMileage всередині CompleteRental)
                rental.CompleteRental();
                Console.WriteLine($"🏁 Оренда завершена! Пробіг авто: {selectedCar.Mileage} км");

                // Відправляємо повідомлення
                var notification = new Notification
                {
                    Message = $"Дякуємо за оренду {selectedCar.Model}! Вартість: {rental.TotalCost} грн."
                };
                notification.Send();
                Console.WriteLine($"📱 {notification.GetStatus()}");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ Помилка: {ex.Message}");
                Console.ResetColor();
            }
        }

        static void ShowChargingStation(ChargingStation station, List<ElectricCar> cars)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("=== ЗАРЯДНА СТАНЦІЯ ===");
            Console.ResetColor();

            Console.WriteLine($"📍 {station.GetStatus()}");
            Console.WriteLine($"💰 Вартість: {station.CostPerKwh} грн за кВт·год");

            if (station.CanCharge())
            {
                Console.WriteLine("\nДоступні автомобілі для зарядки:");
                for (int i = 0; i < cars.Count; i++)
                {
                    if (cars[i].IsAvailable)
                    {
                        Console.WriteLine($"{i + 1}. {cars[i].Model} ({cars[i].LicensePlate})");
                    }
                }

                Console.Write("Оберіть авто для зарядки (номер): ");
                if (int.TryParse(Console.ReadLine(), out int carIndex) && carIndex > 0 && carIndex <= cars.Count)
                {
                    var car = cars[carIndex - 1];
                    station.StartCharging(car);

                    Console.Write("Введіть кількість кВт·год для зарядки: ");
                    if (double.TryParse(Console.ReadLine(), out double kwh))
                    {
                        double cost = station.CalculateChargingCost(kwh);
                        Console.WriteLine($"💡 Вартість зарядки: {cost} грн");
                    }

                    station.StopCharging();
                }
            }
            else
            {
                Console.WriteLine("❌ Немає вільних портів для зарядки");
            }
        }

        static void ShowCustomerProfile(Customer customer, LoyaltyProgram loyaltyProgram)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("=== МІЙ ПРОФІЛЬ ===");
            Console.ResetColor();

            Console.WriteLine($"👤 {customer.GetInfo()}");
            Console.WriteLine($"📞 Телефон: {customer.Phone}");
            Console.WriteLine($"📧 Email: {customer.Email}");
            Console.WriteLine($"🆔 Права водія: {(customer.HasLicense ? "✅ Так" : "❌ Ні")}");
            Console.WriteLine($"⭐ Бали лояльності: {loyaltyProgram.Points}");

            // Показуємо контактну інформацію + валідацію
            var contactInfo = new ContactInfo
            {
                Phone = customer.Phone,
                Email = customer.Email
            };
            Console.WriteLine($"📋 Контакти валідні: {(contactInfo.IsValid() ? "✅ Так" : "❌ Ні")}");

            // Демонстрація списання балів
            Console.Write("Списати 10 балів лояльності? (y/n): ");
            if ((Console.ReadLine() ?? "").Trim().ToLower() == "y")
            {
                loyaltyProgram.RedeemPoints(10);
                Console.WriteLine($"🔄 Залишок балів: {loyaltyProgram.Points}");
            }
        }

        static void ShowRentalHistory(List<Rental> rentalHistory)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("=== ІСТОРІЯ ОРЕНД ===");
            Console.ResetColor();

            if (rentalHistory.Count == 0)
            {
                Console.WriteLine("У вас ще немає оренд");
                return;
            }

            for (int i = 0; i < rentalHistory.Count; i++)
            {
                var rental = rentalHistory[i];
                Console.WriteLine($"{i + 1}. {rental.GetRentalInfo()}");
                Console.WriteLine($"   Прострочена: {(rental.IsOverdue() ? "❌ Так" : "✅ Ні")}");
                Console.WriteLine();
            }
        }

        static void ShowFeedbackSystem()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== СИСТЕМА ВІДГУКІВ ===");
            Console.ResetColor();

            Console.Write("Введіть ваш відгук: ");
            var message = Console.ReadLine() ?? "Без коментарів";

            var feedback = new Feedback
            {
                FeedbackId = "FDB_" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                Message = message,
                CreatedAt = DateTime.Now
            };

            Console.WriteLine($"✅ Відгук збережено!");
            Console.WriteLine($"🆔 ID: {feedback.FeedbackId}");
            Console.WriteLine($"💬 Повідомлення: {feedback.Message}");
            Console.WriteLine($"📅 Дата: {feedback.CreatedAt:dd.MM.yyyy HH:mm}");
        }

        static void ShowInvoiceSystem()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("=== СИСТЕМА РАХУНКІВ ===");
            Console.ResetColor();

            Console.Write("Введіть суму рахунку: ");
            if (double.TryParse(Console.ReadLine(), out double amount))
            {
                var invoice = new Invoice
                {
                    InvoiceNumber = "INV-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Amount = amount,
                    DateIssued = DateTime.Now
                };

                Console.WriteLine($"🧾 {invoice.GetInvoiceDetails()}");
            }
            else
            {
                Console.WriteLine("Невірна сума!");
            }
        }

        static void ShowDriverLicenseInfo()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("=== ВОДІЙСЬКІ ПРАВА ===");
            Console.ResetColor();

            Console.Write("Введіть номер посвідчення: ");
            var licenseNumber = Console.ReadLine() ?? "AB123456";

            Console.Write("Введіть рік закінчення терміну: ");
            int.TryParse(Console.ReadLine(), out int expiryYear);
            if (expiryYear <= 0) expiryYear = DateTime.Now.Year + 2;

            var license = new DriverLicense
            {
                LicenseNumber = licenseNumber,
                ExpiryDate = new DateTime(expiryYear, 12, 31)
            };

            Console.WriteLine($"🆔 Номер: {license.LicenseNumber}");
            Console.WriteLine($"📅 Термін дії: {license.ExpiryDate:dd.MM.yyyy}");
            Console.WriteLine($"✅ Дійсність: {(license.IsValid() ? "✅ Дійсні" : "❌ Прострочені")}");
        }

        static void ShowAddressSystem()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("=== СИСТЕМА АДРЕС ===");
            Console.ResetColor();

            Console.Write("Введіть вулицю: ");
            var street = Console.ReadLine() ?? "Хрещатик, 1";

            Console.Write("Введіть місто: ");
            var city = Console.ReadLine() ?? "Київ";

            Console.Write("Введіть поштовий індекс: ");
            var postalCode = Console.ReadLine() ?? "01001";

            var address = new Address
            {
                Street = street,
                City = city,
                PostalCode = postalCode
            };

            Console.WriteLine($"📍 Повна адреса: {address.GetFullAddress()}");

            // BookingStatus + CarDetails демонстрація
            var bookingStatus = new BookingStatus { Status = "Активне" };
            Console.WriteLine($"Статус бронювання: {bookingStatus.Status} (активне: {bookingStatus.IsActive()})");

            var details = new CarDetails { TypeName = "Седан", FeatureName = "Клімат-контроль" };
            Console.WriteLine($"Деталі авто: {details.TypeName} | {details.FeatureName}");
        }

        static void ToggleCarAvailability(List<ElectricCar> cars)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("=== ПЕРЕМИКАННЯ ДОСТУПНОСТІ АВТО ===");
            Console.ResetColor();

            ShowAvailableCars(cars);
            Console.Write("Оберіть авто для перемикання статусу (номер): ");
            if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > cars.Count)
            {
                Console.WriteLine("Невірний вибір!");
                return;
            }

            var car = cars[idx - 1];
            bool before = car.IsAvailable;
            car.ToggleAvailability();
            Console.WriteLine($"Статус змінено: {before} -> {car.IsAvailable}");
        }
    }
}