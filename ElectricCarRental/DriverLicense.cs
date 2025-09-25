namespace ElectricCarRental;

// Клас для зберігання інформації про водійські права
public class DriverLicense
{
    // Номер водійського посвідчення
    public string LicenseNumber { get; set; }

    // Дата закінчення терміну дії посвідчення
    public DateTime ExpiryDate { get; set; }

    // Метод для перевірки, чи дійсне посвідчення
    public bool IsValid()
    {
        return ExpiryDate > DateTime.Now; // Перевіряємо, чи термін дії не закінчився
    }
}