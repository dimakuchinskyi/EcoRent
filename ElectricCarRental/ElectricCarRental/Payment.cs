// File: 'ElectricCarRental/Payment.cs'
using System;

namespace ElectricCarRental
{
    public class Payment
    {
        // Властивості платежу
        public string PaymentId { get; set; } // Унікальний ідентифікатор платежу
        public Rental Rental { get; set; } // Оренда, пов'язана з платежем
        public double Amount { get; set; } // Сума платежу
        public DateTime PaymentDate { get; set; } // Дата платежу
        public string PaymentMethod { get; set; } // Метод оплати
        public string Status { get; set; } // Статус платежу
        public string TransactionId { get; set; } // Ідентифікатор транзакції

        // Конструктор для ініціалізації платежу
        public Payment()
        {
            PaymentId = "PAY_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            PaymentDate = DateTime.Now;
            Status = "Pending";
            PaymentMethod = PaymentMethod ?? "Card";
        }

        // Обробка платежу
        public bool ProcessPayment()
        {
            if (!IsPaymentValid())
            {
                Status = "Failed";
                return false;
            }
            if (Status == "Completed") return true;

            Status = "Completed";
            TransactionId = "TXN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            PaymentDate = DateTime.Now;

            Console.WriteLine($"Оплата {Amount:0.##} грн пройшла успішно! ID транзакції: {TransactionId}");
            return true;
        }

        // Обробка повернення коштів
        public bool ProcessRefund()
        {
            if (Status != "Completed")
            {
                Console.WriteLine("Повернення неможливе: платіж не завершено.");
                return false;
            }
            Status = "Refunded";
            Console.WriteLine($"Повернення коштів за платіж {PaymentId} виконано.");
            return true;
        }

        // Отримання деталей платежу
        public string GetPaymentDetails()
        {
            var tx = string.IsNullOrWhiteSpace(TransactionId) ? "-" : TransactionId;
            return $"Платіж {PaymentId} | Сума: {Amount:0.##} грн | Статус: {Status} | Метод: {PaymentMethod} | Дата: {PaymentDate:dd.MM.yyyy HH:mm} | Транзакція: {tx}";
        }

        // Перевірка валідності платежу
        public bool IsPaymentValid()
        {
            if (Amount <= 0) return false;
            if (string.IsNullOrWhiteSpace(PaymentMethod)) return false;
            return true;
        }

        // Зміна методу оплати
        public void ChangePaymentMethod(string newMethod)
        {
            PaymentMethod = newMethod;
            Console.WriteLine($"Метод оплати змінено на: {newMethod}");
        }
    }
}