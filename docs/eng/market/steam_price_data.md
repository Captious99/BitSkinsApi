﻿# Market data of items on Steam

In order to obtain data on the market price of an item on Steam, you need to call the function:

```csharp
BitSkinsApi.Market.SteamRawPriceData.GetRawPriceData(BitSkinsApi.Market.AppId.AppName app, string marketHashName);
```

## GetRawPriceData()

### НIs in class:

```csharp
BitSkinsApi.Market.SteamRawPriceData
```

### Function:

```csharp
BitSkinsApi.Market.SteamRawPriceData.GetRawPriceData(BitSkinsApi.Market.AppId.AppName app, string marketHashName);
```

### Function parameters:

* BitSkinsApi.Market.AppId.AppName app - game that owns the requested item.
* string marketHashName - the name of the item for which data is being requested.

### Returns:

```csharp
BitSkinsApi.Market.SteamItemRawPriceData
```

Class properties ```BitSkinsApi.Market.SteamItemRawPriceData```:
* ItemRawPrices - list of records for the sale of an item at a specific time period.
* UpdatedAt? - data update date.

### Possible exceptions
```ArgumentException``` - in case of transfer to the function incorrect data, the message contains detailed information.
\
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
string name = "Operation Phoenix Weapon Case";
BitSkinsApi.Market.SteamItemRawPriceData rawPriceData = BitSkinsApi.Market.SteamRawPriceData.GetRawPriceData(app, name);
foreach (BitSkinsApi.Market.ItemRawPrice rawPrice in rawPriceData.ItemRawPrices)
{
    Console.WriteLine(rawPrice.Time);
    Console.WriteLine(rawPrice.Volume);
    Console.WriteLine(rawPrice.Price);
    Console.WriteLine();
}
```

[<Change sale item](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/market/modify_sale.md) &nbsp;&nbsp;&nbsp;&nbsp; [Items requiring a reset price>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/market/reset_price_items.md)