### Опис типів даних у бізнес-моделі

1. **ElectricCar (Електромобіль)**:
   - `Model` (string): Модель автомобіля.
   - `LicensePlate` (string): Державний номер.
   - `BatteryCapacity` (int): Ємність батареї.
   - `PricePerHour` (double): Ціна за годину.
   - `PricePerDay` (double): Ціна за день.
   - `IsAvailable` (bool): Статус доступності.
   - `Mileage` (int): Пробіг.

2. **Customer (Клієнт)**:
   - `Name` (string): Ім'я клієнта.
   - `Phone` (string): Номер телефону.
   - `Email` (string): Електронна пошта.
   - `HasLicense` (bool): Наявність прав.
   - `Age` (int): Вік.
   - `TotalRentals` (int): Кількість оренд.

3. **Rental (Оренда)**:
   - `Car` (ElectricCar): Орендований автомобіль.
   - `Customer` (Customer): Клієнт.
   - `Hours` (int): Тривалість оренди.
   - `IsDaily` (bool): Подобова оренда.
   - `StartTime` (DateTime): Час початку.
   - `IsCompleted` (bool): Статус завершення.
   - `TotalCost` (double): Загальна вартість.
   - `Payment` (Payment): Платіж.

4. **Payment (Платіж)**:
   - `PaymentId` (string): Ідентифікатор платежу.
   - `Amount` (double): Сума.
   - `PaymentDate` (DateTime): Дата.
   - `PaymentMethod` (string): Метод оплати.
   - `Status` (string): Статус.

5. **ChargingStation (Зарядна станція)**:
   - `Location` (string): Розташування.
   - `AvailablePorts` (int): Доступні порти.
   - `TotalPorts` (int): Загальна кількість портів.
   - `CostPerKwh` (double): Вартість заряджання.
   - `CarsCharged` (int): Лічильник авто.

6. **RentalHistory (Історія оренд)**:
   - `Rentals` (List<Rental>): Список оренд.

7. **LoyaltyProgram (Програма лояльності)**:
   - `CustomerName` (string): Ім'я клієнта.
   - `Points` (int): Кількість балів.

8. **Notification (Повідомлення)**:
   - `Message` (string): Текст повідомлення.
   - `IsSent` (bool): Статус відправки.

9. **SupportTicket (Звернення)**:
   - `TicketId` (string): Ідентифікатор звернення.
   - `IssueDescription` (string): Опис проблеми.
   - `IsResolved` (bool): Статус вирішення.

10. **Invoice (Рахунок)**:
   - `InvoiceNumber` (string): Номер рахунку.
   - `Amount` (double): Сума рахунку.
   - `DateIssued` (DateTime): Дата створення.