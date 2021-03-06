# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.2.3] - 2019-08-20
### Added
- New version of method RelistItem() is RelistItemDelayHour(), this method considering that now items cannot be relisted more than once an hour
### Changed
- Make all properties nullable

## [1.2.2] - 2019-06-01
### Added
- Validation of the entered data for all API call methods
  - Get Money Events
  - Money Withdrawal
  - Get Account Inventory
  - Withdraw Item
  - Delist Item
  - Get Buy History
  - Get Sell History
  - Get Item History
  - Get Inventory On Sale
  - Get Specific Items on Sale
  - Modify Sale
  - Buy Item
  - Get Recent Sale Info
  - Re-list Item
  - Get Reset-Price Items
  - Sell Item
  - Get Raw Price Data
  - Get Trade Details
  - Create Bitcoin Deposit
  - Cancel Buy Orders
  - Cancel All BuyOrders
  - Create Buy Order
  - Get Market Buy Orders
  - Get My Buy Orders
  
## [1.2.1] - 2019-05-13
### Changed
- Type of value ```suggestedPrice``` from ```double``` to ```Nullable<double>```
- Type of value ```placeInQueue``` from ```int``` to ```Nullable<int>```
- Type of value ```recentAveragePrice``` from ```double``` to ```Nullable<double>```

## [1.2.0] - 2019-05-05
### Added
- Full coverage BitSkins Buy Orders API:
  - Create Buy Order
  - Get Expected Place In Queue
  - Cancel Buy Orders
  - Cancel All BuyOrders
  - Get My Buy Orders
  - Get Market Buy Orders
  - Summarize Buy Orders
  
## [1.1.0] - 2019-04-21
### Added
- Full coverage BitSkins Crypto Deposits API:
  - Get Bitcoin Deposit Address
  - Get Bitcoin Deposit Rate
  - Create Bitcoin Deposit
  
## [1.0.0] - 2019-04-11
### Added
- Automatic two-factor authentication
- Full coverage BitSkins General API:
  - Get Account Balance
  - Get All Item Prices
  - Get Market Data
  - Get Account Inventory
  - Get Inventory On Sale
  - Get Specific Items on Sale
  - Get Reset-Price Items
  - Get Money Events
  - Money Withdrawal
  - Buy Item
  - Sell Item
  - Modify Sale
  - Delist Item
  - Re-list Item
  - Withdraw Item
  - Bump Item
  - Get Buy History
  - Get Sell History
  - Get Item History
  - Get Trade Details
  - Get Recent Trade Offers
  - Get Recent Sale Info
  - Get Raw Price Data
