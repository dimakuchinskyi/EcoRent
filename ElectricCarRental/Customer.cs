namespace ElectricCarRental
{
    public class Customer
    {
        // Властивості клієнта
        public string Name { get; set; } // Ім'я клієнта
        public string Phone { get; set; } // Номер телефону
        public string Email { get; set; } // Електронна пошта
        public bool HasLicense { get; set; } // Наявність водійських прав
        public int Age { get; set; } // Вік клієнта
        public int TotalRentals { get; set; } // Загальна кількість оренд

        // Отримання інформації про клієнта
        public string GetInfo()
        {
            return $"{Name} | {Age} років | Ореньд: {TotalRentals}";
        }

        // Перевірка, чи клієнт може орендувати автомобіль
        public bool CanRent()
        {
            return Age >= 18 && HasLicense;
        }

        // Збільшення кількості оренд клієнта
        public void IncrementRentals()
        {
            TotalRentals++;
        }
    }
}