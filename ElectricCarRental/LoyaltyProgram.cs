namespace ElectricCarRental;

// Клас для програми лояльності клієнтів
public class LoyaltyProgram
{
    // Ім'я клієнта
    public string CustomerName { get; set; }

    // Кількість балів
    public int Points { get; set; }

    // Метод для додавання балів
    public void AddPoints(int points)
    {
        Points += points; // Додаємо бали
    }

    // Метод для списання балів
    public void RedeemPoints(int points)
    {
        if (Points >= points)
        {
            Points -= points; // Списуємо бали
        }
    }
}