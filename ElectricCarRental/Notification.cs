namespace ElectricCarRental;

// Клас для управління повідомленнями
public class Notification
{
    // Властивість для зберігання тексту повідомлення
    public string Message { get; set; }

    // Властивість для зберігання статусу відправки
    public bool IsSent { get; private set; }

    // Метод для відправки повідомлення
    public void Send()
    {
        // Логіка відправки повідомлення
        IsSent = true; // Встановлюємо статус як "відправлено"
    }

    // Метод для отримання статусу повідомлення
    public string GetStatus()
    {
        return IsSent ? "Повідомлення відправлено" : "Повідомлення не відправлено";
    }
}