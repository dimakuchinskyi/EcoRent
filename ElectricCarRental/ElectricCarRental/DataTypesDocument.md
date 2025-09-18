### Опис типів даних у бізнес-моделі

1. **ElectricCar (Електромобіль)**
    - `Model` (string): Модель автомобіля.
    - `LicensePlate` (string): Державний номер.
    - `BatteryCapacity` (int): Ємність батареї в кВт·год.
    - `PricePerHour` (double): Ціна за годину оренди.
    - `PricePerDay` (double): Ціна за день оренди.
    - `IsAvailable` (bool): Статус доступності автомобіля.
    - `Mileage` (int): Пробіг автомобіля.

2. **Customer (Клієнт)**
    - `Name` (string): Ім'я клієнта.
    - `Phone` (string): Номер телефону.
    - `Email` (string): Електронна пошта.
    - `HasLicense` (bool): Наявність водійських прав.
    - `Age` (int): Вік клієнта.
    - `TotalRentals` (int): Загальна кількість оренд.

3. **Rental (Оренда)**
    - `Car` (ElectricCar): Автомобіль, що орендується.
    - `Customer` (Customer): Клієнт, який орендує автомобіль.
    - `Hours` (int): Тривалість оренди в годинах.
    - `IsDaily` (bool): Чи є оренда подобовою.
    - `StartTime` (DateTime): Час початку оренди.
    - `IsCompleted` (bool): Статус завершення оренди.
    - `TotalCost` (double): Загальна вартість оренди.
    - `Payment` (Payment): Платіж, пов'язаний з орендою.

4. **ChargingStation (Зарядна станція)**
    - `Location` (string): Розташування станції.
    - `AvailablePorts` (int): Кількість доступних портів.
    - `TotalPorts` (int): Загальна кількість портів.
    - `CostPerKwh` (double): Вартість заряджання за кВт·год.
    - `CarsCharged` (int): Лічильник обслужених авто.

5. **Payment (Платіж)**
    - `PaymentId` (string): Унікальний ідентифікатор платежу.
    - `Rental` (Rental): Оренда, пов'язана з платежем.
    - `Amount` (double): Сума платежу.
    - `PaymentDate` (DateTime): Дата платежу.
    - `PaymentMethod` (string): Метод оплати.
    - `Status` (string): Статус платежу.
    - `TransactionId` (string): Ідентифікатор транзакції.

---