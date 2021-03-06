﻿# BitSkins price database

In order to get the full price database used by BitSkins, you need to call the function:

```csharp
BitSkinsApi.Market.PriceDatabase.GetAllItemPrices(BitSkinsApi.Market.AppId.AppName app);
```

## GetAllItemPrices()

### Is in class:

```csharp
BitSkinsApi.Market.PriceDatabase
```

### Function:

```csharp
BitSkinsApi.Market.PriceDatabase.GetAllItemPrices(BitSkinsApi.Market.AppId.AppName app);
```

### Function parameters:

* BitSkinsApi.Market.AppId.AppName app - game for which price database is requested.

### Returns:

```csharp
List<BitSkinsApi.Market.ItemPrice>
```

Class properties ```BitSkinsApi.Market.ItemPrice```
* MarketHashName - item name.
* Price? - item price.
* PricingMode - pricing mode.
* CreatedAt? - item creation date.
* IconUrl - item image link.
* InstantSalePrice? - price of instant sale item.

### Possible exceptions
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<BitSkinsApi.Market.ItemPrice> itemPrices = BitSkinsApi.Market.PriceDatabase.GetAllItemPrices(app);
foreach (BitSkinsApi.Market.ItemPrice item in itemPrices)
{
    Console.WriteLine(item.MarketHashName);
    Console.WriteLine(item.Price);
    Console.WriteLine();
}
```

[<BitSkins market data](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/market/market_data.md) &nbsp;&nbsp;&nbsp;&nbsp; [All items on sale at BitSkins>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/market/inventory_on_sale.md)