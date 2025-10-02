namespace ElectricCarRental;

// Клас для створення рахунків
public class Invoice
{
    // Номер рахунку
    public string InvoiceNumber { get; set; }

    // Сума рахунку
    public double Amount { get; set; }

    // Дата створення рахунку
    public DateTime DateIssued { get; set; }

    // Метод для отримання деталей рахунку
    public string GetInvoiceDetails()
    {
        return $"Рахунок #{InvoiceNumber} | Сума: {Amount} грн | Дата: {DateIssued:dd.MM.yyyy}";
    }
}