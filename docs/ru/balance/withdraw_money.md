﻿# Вывод денег с баланса

Для того чтобы вывести деньги с баланса BitSkins аккаунта, нужно вызвать функцию:

```csharp
BitSkinsApi.Balance.MoneyWithdraw.WithdrawMoney(double amount, WithdrawalMoneyMethod withdrawalMethod);
```

## MoneyWithdrawal()

### Находится в классе:

```csharp
BitSkinsApi.Balance.MoneyWithdraw
```

### Функция:

```csharp
BitSkinsApi.Balance.MoneyWithdraw.WithdrawMoney(double amount, WithdrawalMoneyMethod withdrawalMethod);
```

### Параметры функции:

* double amount - сумма в долларах США для вывода. Не должна быть меньше доступного для вывода баланса и быть больше 5,00 долларов США.
* WithdrawalMoneyMethod withdrawalMethod - способ вывода денег. Может быть:
  - PayPal
  - Skrill
  - Bitcoin
  - Ethereum
  - Litecoin
  - BankWire

### Возвращает:

```csharp
bool success
```

success - успешно ли выполнена операция.

### Возможные исключения
```ArgumentException``` - в случае передачи в функцию некорректных данных, в сообщение содержится подробная информация.
\
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

```csharp
BitSkinsApi.Balance.AccountBalance balance = BitSkinsApi.Balance.CurrentBalance.GetAccountBalance();
if (balance.WithdrawableBalance > 5)
{
    bool success = BitSkinsApi.Balance.MoneyWithdraw.WithdrawMoney(5, BitSkinsApi.Balance.MoneyWithdraw.WithdrawalMoneyMethod.PayPal);
    Console.WriteLine(success ? "Success" : "Fail");
}
```

[<События с балансом аккаунта](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/balance/money_events.md) &nbsp;&nbsp;&nbsp;&nbsp; [Инвентарь аккаунта>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/inventory/account_inventory.md)