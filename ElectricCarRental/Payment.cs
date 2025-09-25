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
        }

        // Обробка платежу
        public bool ProcessPayment()
        {
            if (Amount <= 0)
            {
                Status = "Failed";
                return false;
            }

            Status = "Completed";
            TransactionId = "TXN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            PaymentDate = DateTime.Now;

            Console.WriteLine($"Оплата {Amount} грн пройшла успішно! ID: {TransactionId}");
            return true;
        }

        // Обробка повернення коштів
        public bool ProcessRefund()
        {
            if (Status != "Completed")
            {
                Console.WriteLine("Не можна повернути кошти - оплата не була успішною");
                return false;
            }

            Status = "Refunded";
            Console.WriteLine($"Кошти у розмірі {Amount} грн повернено клієнту");
            return true;
        }

        // Отримання деталей платежу
        public string GetPaymentDetails()
        {
            return $"Оплата #{PaymentId} | {Amount} грн | {PaymentMethod} | {Status} | {PaymentDate:dd.MM.yyyy HH:mm}";
        }

        // Перевірка валідності платежу
        public bool IsPaymentValid()
        {
            return Status == "Completed" && Amount > 0;
        }

        // Зміна методу оплати
        public void ChangePaymentMethod(string newMethod)
        {
            PaymentMethod = newMethod;
            Console.WriteLine($"Метод оплати змінено на: {newMethod}");
        }
    }
}