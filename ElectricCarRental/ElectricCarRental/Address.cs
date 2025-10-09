namespace ElectricCarRental;

// Клас для зберігання адреси
public class Address
{
    // Вулиця
    public string Street { get; set; }

    // Місто
    public string City { get; set; }

    // Поштовий індекс
    public string PostalCode { get; set; }

    // Метод для отримання повної адреси
    public string GetFullAddress()
    {
        return $"{Street}, {City}, {PostalCode}";
    }
}