namespace ElectricCarRental;

// Клас для зберігання історії оренд клієнта
public class RentalHistory
{
    // Список оренд клієнта
    public List<Rental> Rentals { get; set; } = new();

    // Метод для додавання оренди до історії
    public void AddRental(Rental rental)
    {
        Rentals.Add(rental); // Додаємо оренду до списку
    }

    // Метод для отримання всіх оренд у вигляді рядка
    public string GetHistory()
    {
        return string.Join("\n", Rentals.Select(r => r.GetRentalInfo())); // Форматуємо список оренд
    }
}