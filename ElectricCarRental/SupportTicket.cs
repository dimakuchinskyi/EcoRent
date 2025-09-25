namespace ElectricCarRental;

// Клас для обробки звернень клієнтів
public class SupportTicket
{
    // Унікальний ідентифікатор звернення
    public string TicketId { get; set; }

    // Опис проблеми
    public string IssueDescription { get; set; }

    // Статус вирішення проблеми
    public bool IsResolved { get; private set; }

    // Метод для позначення звернення як вирішеного
    public void Resolve()
    {
        IsResolved = true; // Встановлюємо статус як "вирішено"
    }

    // Метод для отримання статусу звернення
    public string GetStatus()
    {
        return IsResolved ? "Звернення вирішено" : "Звернення не вирішено";
    }
}