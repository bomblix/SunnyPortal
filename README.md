#SunnyPortal
WPF application and .NET Library to access SunnyPortal data.

#SunnyPortal.Core 
Library to get data from your photovoltaic system:

Features:
- get current power
- get historical data from a day

Simple use of this library:

```
var sunnyPortal = new SunnyPortal();

// login to SunnyPortal
sunnyPortal.Connect(userlogin, userPassword);

// get current power
var result = sunnyPortal.GetCurrentPower();

// get data for yesterday
var yesterday = sunnyPortal.GetHistoricalData(DateTime.Now.AddDays(-1));
```

#SunnyPortal.Client
Simple Wpf client - created using MVVM pattern (GalaSoft.MVVMLight)

Currently client display only current system production.

#What I want to create in close future:

Client:
- [ ] - Display daily, monthly etc. data
- [ ] - Application settings

Core:
- [ ] - Method to get monthly and yearly data
