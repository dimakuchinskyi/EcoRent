namespace ElectricCarRental;

// Клас для зберігання контактної інформації
public class ContactInfo
{
    // Номер телефону
    public string Phone { get; set; }

    // Електронна пошта
    public string Email { get; set; }

    // Метод для перевірки валідності контактної інформації
    public bool IsValid()
    {
        return !string.IsNullOrEmpty(Phone) && !string.IsNullOrEmpty(Email);
    }
}