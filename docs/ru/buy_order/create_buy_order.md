﻿# Создать заказ на покупку

Заказы на покупку выполняются в течение 30 секунд, если кто-то выставляет на продажу товар по цене заказа или ниже. Средства не удерживаются из-за отложенных заказов на покупку. Заказ на покупку будет автоматически отменен, если на вашем счету недостаточно средств для выполнения заказа на покупку. Для того чтобы создать заказ на покупку предмета в BitSkins, нужно вызвать функцию:

```csharp
BitSkinsApi.BuyOrder.CreatingBuyOrder.CreateBuyOrder(BitSkinsApi.Market.AppId.AppName app, string name, double price, int quantity);
```

## CreateBuyOrder()

### Находится в классе:

```csharp
BitSkinsApi.BuyOrder.CreatingBuyOrder
```

### Функция:

```csharp
BitSkinsApi.BuyOrder.CreatingBuyOrder.CreateBuyOrder(BitSkinsApi.Market.AppId.AppName app, string name, double price, int quantity);
```

### Параметры функции:

* BitSkinsApi.Market.AppId.AppName app - игра, для предмета которой создается заказ на покупку.
* string name - название предмета, который вы хотите приобрести.
* double price - цена, по которой вы хотите приобрести предмет.
* int quantity - количество заказов на покупку, по этой цене для данного товара.

### Возвращает:

```csharp
List<BitSkinsApi.BuyOrder.BuyOrder>
```

Свойства класса ```BitSkinsApi.BuyOrder.BuyOrder```:
* BuyOrderId - id заказа на покупку.
* MarketHashName - название предмета.
* Price? - цена.
* SuggestedPrice? - средняя цена этого предмета в BitSkins.
* State - статус заказа на покупку.
* CreatedAt? - дата создания.
* UpdatedAt? - дата обновления.
* PlaceInQueue? - номер в очереди заказов на покупку.

### Возможные исключения
```ArgumentException``` - в случае передачи в функцию некорректных данных, в сообщение содержится подробная информация.
\
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<BitSkinsApi.BuyOrder.BuyOrder> buyOrders = BitSkinsApi.BuyOrder.CreatingBuyOrder.CreateBuyOrder(app, "CS:GO Weapon Case 2", 0.01, 1);
foreach (BitSkinsApi.BuyOrder.BuyOrder buyOrder in buyOrders)
{
    Console.WriteLine(buyOrder.BuyOrderId);
    Console.WriteLine(buyOrder.MarketHashName);
    Console.WriteLine(buyOrder.PlaceInQueue);
    Console.WriteLine(buyOrder.Price);
    Console.WriteLine();
}
```

[<Подробности торгового предложения](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/trade/trade_details.md) &nbsp;&nbsp;&nbsp;&nbsp; [Ожидаемое место в очереди заказов на покупку>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/buy_order/expected_place_in_queue.md)