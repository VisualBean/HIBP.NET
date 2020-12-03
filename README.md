# HIBP.NET
[![GitHub license](https://img.shields.io/github/license/VisualBean/HIBP.NET.svg)](https://github.com/VisualBean/HIBP.NET/blob/master/LICENSE) [![Build status](https://ci.appveyor.com/api/projects/status/6hhatdf7gw60thgn?svg=true)](https://ci.appveyor.com/project/alexintime/hibp-net) [![NuGet version](https://badge.fury.io/nu/HIBP.NET.svg)](https://badge.fury.io/nu/HIBP.NET)
![NuGet](https://img.shields.io/nuget/dt/HIBP.NET.svg)


A simple .NET Core wrapper for the HIBP (Have I been pwned?) Api

Full credits given to Troy Hunt for creating and managing [Have I been pwned?](https://haveibeenpwned.com).

Usage:
===

### Example:
```csharp
using (var api = new HIBP.BreachApi("My-Api-Key", "MyTotallyAwesomeService"))
{
    var result = await api.GetBreachesAsync();
    foreach(var breach in result)
    {
        Console.WriteLine(breach.ToString());
    }
}

class MyClass
{
    private readonly IBreachApi breachApi;
    public MyClass(IBreachApi breachApi)
    {
        this.breachApi = breachApi;
    }
    
    public async Task MyMethod()
    {
        var breaches = this.breachApi.GetBreachesAsync();
        foreach(var breach in breaches)
        {
            Console.WriteLine(breach.ToString());
        }
    }
}
```


Changes
===
### Breaking change coming in version 3.0
ApiKey has been refactored to be a class of its own.
Version 3 is mostly minor refactorings and addition of cancellationtokens on particular API calls instead of from the Base.
