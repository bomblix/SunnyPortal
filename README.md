# SunnyPortalLibrary
.NET Library to access SunnyPortal data.

In this version user can access only to current power of the photovoltaic system.

Simple use:

```
var sunnyPortal = new SunnyPortal( userlogin, userPassword );
sunnyPortal.Connect();
var result = sunnyPortal.GetCurrentPower();
```