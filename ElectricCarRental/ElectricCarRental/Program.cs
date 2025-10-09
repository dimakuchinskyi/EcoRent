// csharp
// File: 'ElectricCarRental/Program.cs'
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ElectricCarRental
{
    class Program
    {
        // ---- Сховища в пам'яті ----
        static readonly List<Customer> Customers = new();
        static readonly List<ElectricCar> Cars = new();
        static readonly List<Rental> Rentals = new();

        // ---- Вхідна точка програми ----
        static void Main()
        {
            // Налаштування виводу для коректного відображення українських символів
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "EcoRent — Система оренди електромобілів";
            Seed(); // Початкові дані

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=== EcoRent — головне меню ===");
                Console.WriteLine("1 - Клієнти");
                Console.WriteLine("2 - Автопарк");
                Console.WriteLine("3 - Оренди");
                Console.WriteLine("4 - Платежі");
                Console.WriteLine("0 - Вихід");
                Console.Write("Вибір: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ManageCustomers(); break;
                    case "2": ManageCars(); break;
                    case "3": ManageRentals(); break;
                    case "4": ManagePayments(); break;
                    case "0": return;
                    default: Console.WriteLine("Невірний вибір."); break;
                }
            }
        }

        // ---- Керування клієнтами ----
        static void ManageCustomers()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("--- Клієнти ---");
                Console.WriteLine("1 - Додати клієнта");
                Console.WriteLine("2 - Список клієнтів");
                Console.WriteLine("0 - Назад");
                Console.Write("Вибір: ");
                var c = Console.ReadLine();

                if (c == "0") return;

                if (c == "1")
                {
                    // Створення нового клієнта
                    var cust = new Customer
                    {
                        Name = ReadString("Ім'я: "),
                        Phone = ReadString("Телефон: "),
                        Email = ReadString("Email: "),
                        Age = ReadInt("Вік: "),
                        HasLicense = ReadBool("Є посвідчення водія? (y/n): ")
                    };
                    Customers.Add(cust);
                    Console.WriteLine("Клієнта додано.");
                }
                else if (c == "2")
                {
                    // Відображення списку клієнтів
                    if (Customers.Count == 0) { Console.WriteLine("Клієнтів немає."); continue; }
                    for (int i = 0; i < Customers.Count; i++)
                        Console.WriteLine($"{i + 1}. {Customers[i].GetInfo()}");
                }
                else Console.WriteLine("Невірний вибір.");
            }
        }

        // ---- Керування автопарком ----
        static void ManageCars()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("--- Автопарк ---");
                Console.WriteLine("1 - Додати авто");
                Console.WriteLine("2 - Список усіх авто");
                Console.WriteLine("3 - Список доступних авто");
                Console.WriteLine("4 - Перемкнути доступність авто");
                Console.WriteLine("0 - Назад");
                Console.Write("Вибір: ");
                var c = Console.ReadLine();

                if (c == "0") return;

                if (c == "1")
                {
                    // Додавання нового автомобіля
                    var car = new ElectricCar
                    {
                        Model = ReadString("Модель: "),
                        LicensePlate = ReadString("Держномер: "),
                        BatteryCapacity = ReadInt("Ємність батареї (кВт·год): "),
                        PricePerHour = ReadDouble("Ціна за годину: "),
                        PricePerDay = ReadDouble("Ціна за день: "),
                        IsAvailable = true
                    };
                    Cars.Add(car);
                    Console.WriteLine("Автомобіль додано.");
                }
                else if (c == "2")
                {
                    // Всі авто
                    if (Cars.Count == 0) { Console.WriteLine("Авто відсутні."); continue; }
                    for (int i = 0; i < Cars.Count; i++)
                        Console.WriteLine($"{i + 1}. {Cars[i].GetInfo()}");
                }
                else if (c == "3")
                {
                    // Лише доступні авто
                    var a = Cars.Where(x => x.IsAvailable).ToList();
                    if (a.Count == 0) { Console.WriteLine("Немає доступних авто."); continue; }
                    for (int i = 0; i < a.Count; i++)
                        Console.WriteLine($"{i + 1}. {a[i].GetInfo()}");
                }
                else if (c == "4")
                {
                    // Зміна статусу доступності
                    var car = SelectFrom(Cars, "Оберіть авто для зміни статусу");
                    if (car == null) continue;
                    car.ToggleAvailability();
                    Console.WriteLine($"Статус: {(car.IsAvailable ? "Доступний" : "Зайнятий")}");
                }
                else Console.WriteLine("Невірний вибір.");
            }
        }

        // ---- Керування орендами ----
        static void ManageRentals()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("--- Оренди ---");
                Console.WriteLine("1 - Створити оренду");
                Console.WriteLine("2 - Завершити оренду");
                Console.WriteLine("3 - Переглянути оренди");
                Console.WriteLine("0 - Назад");
                Console.Write("Вибір: ");
                var c = Console.ReadLine();

                if (c == "0") return;

                if (c == "1")
                {
                    // Перевірки на наявність клієнтів та авто
                    if (Customers.Count == 0) { Console.WriteLine("Немає клієнтів."); continue; }
                    var available = Cars.Where(x => x.IsAvailable).ToList();
                    if (available.Count == 0) { Console.WriteLine("Немає доступних авто."); continue; }

                    // Вибір клієнта
                    var customer = SelectFrom(Customers, "Оберіть клієнта");
                    if (customer == null) continue;
                    if (!customer.CanRent()) { Console.WriteLine("Клієнт не може орендувати авто."); continue; }

                    // Вибір авто
                    var car = SelectFrom(available, "Оберіть доступне авто");
                    if (car == null) continue;

                    // Вибір тарифу
                    Console.WriteLine("Тариф: 1 - погодинно, 2 - подобово");
                    var tariff = ReadString("Вибір тарифу: ");
                    bool isDaily = tariff == "2";

                    // Кількість одиниць (годин або днів)
                    int units = isDaily
                        ? ReadInt("Кількість днів: ")
                        : ReadInt("Кількість годин: ");

                    // Створення оренди та запуск
                    var rental = new Rental
                    {
                        Car = car,
                        Customer = customer,
                        Hours = units,
                        IsDaily = isDaily
                    };

                    try
                    {
                        rental.StartRental(); // Фіксує час старту зараз, блокує авто, створює платіж
                        Rentals.Add(rental);

                        Console.WriteLine("Оренду створено.");
                        Console.WriteLine(FormatRental(rental));

                        // Оплата одразу (за бажанням)
                        if (rental.Payment != null)
                        {
                            if (ReadBool("Оплатити зараз? (y/n): "))
                            {
                                rental.Payment.ProcessPayment();
                                Console.WriteLine(rental.Payment.GetPaymentDetails());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Помилка: {ex.Message}");
                    }
                }
                else if (c == "2")
                {
                    // Завершення активної оренди
                    var active = Rentals.Where(r => !r.IsCompleted).ToList();
                    if (active.Count == 0) { Console.WriteLine("Активних оренд немає."); continue; }
                    var rsel = SelectFrom(active, "Оберіть оренду для завершення");
                    if (rsel == null) continue;

                    rsel.CompleteRental(); // Розблокує авто, додає пробіг, інкрементує лічильник, завершує платіж якщо Pending
                    Console.WriteLine("Оренду завершено.");
                    Console.WriteLine(FormatRental(rsel));
                    Console.WriteLine($"Пробіг авто: {rsel.Car.Mileage} км");
                    Console.WriteLine($"Оренд клієнта всього: {rsel.Customer.TotalRentals}");
                }
                else if (c == "3")
                {
                    // Перегляд усіх оренд зі статусом
                    if (Rentals.Count == 0) { Console.WriteLine("Оренд немає."); continue; }
                    for (int i = 0; i < Rentals.Count; i++)
                    {
                        var r = Rentals[i];
                        var status = r.IsCompleted ? "Завершена" : (r.IsOverdue() ? "Прострочена" : "Активна");
                        Console.WriteLine($"{i + 1}. {FormatRental(r)} | {status}");
                    }
                }
                else Console.WriteLine("Невірний вибір.");
            }
        }

        // ---- Керування платежами ----
        static void ManagePayments()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("--- Платежі ---");
                Console.WriteLine("1 - Список платежів");
                Console.WriteLine("2 - Оплатити неоплачений");
                Console.WriteLine("3 - Повернення коштів");
                Console.WriteLine("0 - Назад");
                Console.Write("Вибір: ");
                var c = Console.ReadLine();

                if (c == "0") return;

                // Отримуємо всі платежі з оренд
                var payments = Rentals.Where(r => r.Payment != null).Select(r => r.Payment!).ToList();

                if (c == "1")
                {
                    // Відображення всіх платежів
                    if (payments.Count == 0) { Console.WriteLine("Платежів немає."); continue; }
                    for (int i = 0; i < payments.Count; i++)
                        Console.WriteLine($"{i + 1}. {payments[i].GetPaymentDetails()}");
                }
                else if (c == "2")
                {
                    // Оплата платежу зі статусом Pending
                    var pending = payments.Where(p => p.Status == "Pending").ToList();
                    if (pending.Count == 0) { Console.WriteLine("Немає платежів у статусі Pending."); continue; }
                    var psel = SelectFrom(pending, "Оберіть платіж для оплати");
                    if (psel == null) continue;
                    psel.ProcessPayment();
                    Console.WriteLine(psel.GetPaymentDetails());
                }
                else if (c == "3")
                {
                    // Повернення коштів за виконаним платежем
                    var completed = payments.Where(p => p.Status == "Completed").ToList();
                    if (completed.Count == 0) { Console.WriteLine("Немає завершених платежів для повернення."); continue; }
                    var psel = SelectFrom(completed, "Оберіть платіж для повернення коштів");
                    if (psel == null) continue;
                    if (psel.ProcessRefund())
                        Console.WriteLine("Повернення виконано.");
                    else
                        Console.WriteLine("Повернення неможливе.");
                    Console.WriteLine(psel.GetPaymentDetails());
                }
                else Console.WriteLine("Невірний вибір.");
            }
        }

        // ---- Форматування для відображення ----
        static string FormatItem(object o) => o switch
        {
            Customer c => $"{c.Name} | {c.Age} р. | Оренд: {c.TotalRentals}",
            ElectricCar car => $"{car.Model} ({car.LicensePlate}) — {car.BatteryCapacity} кВт·год — {(car.IsAvailable ? "Доступний" : "Зайнятий")}",
            Rental r => FormatRental(r),
            Payment p => p.GetPaymentDetails(),
            _ => o.ToString() ?? ""
        };

        // Людський формат оренди
        static string FormatRental(Rental r)
        {
            // Розрахунок кінця оренди за тарифом
            var till = r.StartTime.AddHours(r.IsDaily ? r.Hours * 24 : r.Hours);
            var pay = r.Payment != null ? r.Payment.Status : "—";
            var tariff = r.IsDaily ? "подобово" : "погодинно";
            return $"{r.Customer.Name} -> {r.Car.Model} | {tariff}: {r.Hours} | " +
                   $"з {r.StartTime:dd.MM.yyyy HH:mm} до {till:dd.MM.yyyy HH:mm} | " +
                   $"Сума: {r.TotalCost:0.##} грн | Платіж: {pay}";
        }

        // ---- Ввід даних з консолі ----
        static T? SelectFrom<T>(IReadOnlyList<T> items, string title)
        {
            // Загальний вибір елемента зі списку
            if (items.Count == 0) { Console.WriteLine("Порожній список."); return default; }
            Console.WriteLine(title + ":");
            for (int i = 0; i < items.Count; i++)
                Console.WriteLine($"{i + 1}. {FormatItem(items[i]!)}");
            int idx = ReadInt("Номер: ") - 1;
            if (idx < 0 || idx >= items.Count) { Console.WriteLine("Невірний номер."); return default; }
            return items[idx];
        }

        static string ReadString(string prompt)
        {
            // Зчитування рядка
            Console.Write(prompt);
            return Console.ReadLine() ?? string.Empty;
        }

        static int ReadInt(string prompt)
        {
            // Зчитування цілого числа (підтримка інваріантної та uk-UA культур)
            while (true)
            {
                Console.Write(prompt);
                var s = (Console.ReadLine() ?? "").Trim();
                if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out int v) ||
                    int.TryParse(s, NumberStyles.Integer, CultureInfo.GetCultureInfo("uk-UA"), out v))
                    return v;
                Console.WriteLine("Введіть ціле число.");
            }
        }

        static double ReadDouble(string prompt)
        {
            // Зчитування числа з крапкою або комою
            while (true)
            {
                Console.Write(prompt);
                var s = (Console.ReadLine() ?? "").Trim().Replace(',', '.');
                if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out double v))
                    return v;
                Console.WriteLine("Введіть число.");
            }
        }

        static bool ReadBool(string prompt)
        {
            // Зчитування булевого значення: y/n (допускаються також т/н)
            while (true)
            {
                Console.Write(prompt);
                var s = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();
                if (s is "y" or "yes" or "т" or "д") return true;
                if (s is "n" or "no" or "н") return false;
                Console.WriteLine("Введіть y або n.");
            }
        }

        static DateTime ReadDateTime(string prompt)
        {
            // Зчитування дати й часу у кількох форматах
            while (true)
            {
                Console.Write(prompt);
                var s = (Console.ReadLine() ?? "").Trim();
                var formats = new[] { "yyyy-MM-dd HH:mm", "dd.MM.yyyy HH:mm", "yyyy-MM-dd", "dd.MM.yyyy" };
                if (DateTime.TryParseExact(s, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                {
                    // Якщо введено лише дату — ставимо 09:00
                    if (s.Length <= 10)
                        dt = dt.AddHours(9);
                    return dt;
                }
                Console.WriteLine("Формат: рррр-мм-дд гг:хх або дд.мм.рррр гг:хх.");
            }
        }

        // ---- Початкові дані для демонстрації ----
        static void Seed()
        {
            // Базові клієнти
            Customers.Add(new Customer { Name = "Іван Іванов", Phone = "+380111111111", Email = "ivan@example.com", Age = 28, HasLicense = true });
            Customers.Add(new Customer { Name = "Олена Петрова", Phone = "+380222222222", Email = "olena@example.com", Age = 32, HasLicense = true });

            // Базовий автопарк
            Cars.Add(new ElectricCar { Model = "Nissan Leaf", LicensePlate = "AA1234BB", BatteryCapacity = 40, PricePerHour = 4.5, PricePerDay = 80, IsAvailable = true });
            Cars.Add(new ElectricCar { Model = "Tesla Model 3", LicensePlate = "BC5678CE", BatteryCapacity = 75, PricePerHour = 6.9, PricePerDay = 120, IsAvailable = true });
        }
    }
}