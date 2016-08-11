# SunnyPortalLibrary
.NET Library to access SunnyPortal data.

In this version user can access only to current power of the photovoltaic system.

Simple use:

```
var sunnyPortal = new SunnyPortal( userlogin, userPassword );

// login to SunnyPortal
sunnyPortal.Connect();

// get current power
var result = sunnyPortal.GetCurrentPower();

// get data for yesterday
var yesterday = sunnyPortal.GetHistoricalData(DateTime.Now.AddDays(-1));
```