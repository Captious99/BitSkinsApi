﻿# Данные о последних продажах

Для того чтобы получить данные о последних продажах предмета, нужно вызвать функцию:

```csharp
BitSkinsApi.Market.RecentSaleInfo.GetRecentSaleInfo(BitSkinsApi.Market.AppId.AppName app, string marketHashName, int page);
```

## GetRecentSaleInfo()

### Находится в классе:

```csharp
BitSkinsApi.Market.RecentSaleInfo
```

### Функция:

```csharp
BitSkinsApi.Market.RecentSaleInfo.GetRecentSaleInfo(BitSkinsApi.Market.AppId.AppName app, string marketHashName, int page);
```

### Параметры функции:

* BitSkinsApi.Market.AppId.AppName app - игра, которой принадлежит запрашиваемый предмет.
* string marketHashName - название предмета.
* int page - номер страницы (до 5).

### Возвращает:

```csharp
List<BitSkinsApi.Market.ItemRecentSale>
```

Свойства класса ```BitSkinsApi.Market.ItemRecentSale```
* Price - цена продажи предмета.
* WearValue - изношенность предмета.
* SoldAt - дата продажи предмета.

## Пример

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
string name = "AK-47 | Case Hardened";
List<BitSkinsApi.Market.ItemRecentSale> itemRecentSales = BitSkinsApi.Market.RecentSaleInfo.GetRecentSaleInfo(app, name, 1);
foreach (BitSkinsApi.Market.ItemRecentSale recentSale in itemRecentSales)
{
    Console.WriteLine(recentSale.Price);
    Console.WriteLine(recentSale.SoldAt);
    Console.WriteLine();
}
```