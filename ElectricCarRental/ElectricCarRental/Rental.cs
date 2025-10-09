using System;

namespace ElectricCarRental
{
    public class Rental
    {
        // Властивості оренди
        public ElectricCar Car { get; set; } // Автомобіль, що орендується
        public Customer Customer { get; set; } // Клієнт, який орендує автомобіль
        public int Hours { get; set; } // Тривалість оренди в годинах
        public bool IsDaily { get; set; } // Чи є оренда подобовою
        public DateTime StartTime { get; set; } // Час початку оренди
        public bool IsCompleted { get; set; } // Статус завершення оренди
        public double TotalCost { get; set; } // Загальна вартість оренди
        public Payment Payment { get; set; } // Платіж, пов'язаний з орендою

        // Початок оренди
        public void StartRental()
        {
            if (!Car.IsAvailable)
                throw new Exception("Автомобіль не доступний");

            StartTime = DateTime.Now; // Фіксація часу початку оренди
            Car.IsAvailable = false; // Зміна статусу автомобіля на "зайнятий"
            TotalCost = Car.CalculateCost(Hours, IsDaily); // Розрахунок вартості оренди

            // Створення платежу
            Payment = new Payment
            {
                Rental = this,
                Amount = TotalCost,
                PaymentMethod = "Card"
            };
        }

        // Завершення оренди
        public void CompleteRental()
        {
            IsCompleted = true; // Встановлення статусу оренди як завершеної
            Car.IsAvailable = true; // Зміна статусу автомобіля на "доступний"
            Car.AddMileage(Hours * 50); // Додавання пробігу до автомобіля
            Customer.IncrementRentals(); // Збільшення кількості оренд клієнта

            // Обробка платежу, якщо він ще не завершений
            if (Payment != null && Payment.Status == "Pending")
            {
                Payment.ProcessPayment();
            }
        }

        // Отримання інформації про оренду
        public string GetRentalInfo()
        {
            string paymentStatus = Payment != null ? Payment.Status : "Не оплачено";
            return $"{Customer.Name} -> {Car.Model} | {Hours} год | {TotalCost} грн | Статус: {paymentStatus}";
        }

        // Перевірка, чи прострочена оренда
        public bool IsOverdue()
        {
            return DateTime.Now > StartTime.AddHours(Hours) && !IsCompleted;
        }
    }
}