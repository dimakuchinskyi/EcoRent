namespace ElectricCarRental;

// Клас для статусу бронювання
public class BookingStatus
{
    // Статус бронювання (наприклад, "Активне", "Завершене")
    public string Status { get; set; }

    // Метод для перевірки, чи бронювання активне
    public bool IsActive()
    {
        return Status == "Активне";
    }
}