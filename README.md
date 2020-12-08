# HIBP.NET
[![GitHub license](https://img.shields.io/github/license/VisualBean/HIBP.NET.svg)](https://github.com/VisualBean/HIBP.NET/blob/master/LICENSE) [![Build status](https://ci.appveyor.com/api/projects/status/6hhatdf7gw60thgn?svg=true)](https://ci.appveyor.com/project/alexintime/hibp-net) [![NuGet version](https://badge.fury.io/nu/HIBP.NET.svg)](https://badge.fury.io/nu/HIBP.NET)
![NuGet](https://img.shields.io/nuget/dt/HIBP.NET.svg)


A .Net wrapper for the HIBP API.
The full API is supported;
 * PwnedPasswords
 * Breaches
 * Pastes


Full credits given to Troy Hunt for creating and managing [Have I been pwned?](https://haveibeenpwned.com).

Usage:
===
## PwnedPasswords
```csharp 
async Task MyMethodPlainTextPassword()
{
    var client = new HIBP.PwnedPasswordApi();
    int pwns = await client.IsPasswordPwnedAsync("password1");
    if (pwns > 0)
    {
        Console.WriteLine($"Password has been pwned: {pwns} times");
    }
}

// or

async Task MyMethodPreHashedPassword()
{
    var client = new HIBP.PwnedPasswordApi();
    int pwns = await client.IsPasswordPwnedAsync("password1".ToSha1(), isHash: true);
    if (pwns > 0)
    {
        Console.WriteLine("Password has been pwned");
    }
}

```

# With .Net core dependency injection.
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddHIBP(c =>
    {
        c.ApiKey = new ApiKey("MyKey");
        c.ServiceName = "MyServiceName";
    });
}

class MyClass
{
    private readonly IBreachApi breachApi;
    public MyClass(IBreachApi breachApi)
    {
        this.breachApi = breachApi;
    }
    
    public async Task GetBreaches()
    {
        var breaches = this.breachApi.GetBreachesAsync();
        ... do stuff..
    }
}
```


Changes
===
### Breaking change coming in version 3.0
 * ApiKey has been refactored to be a class of its own. (**BREAKING**)
 * Renamed parameters to better match usage.
 * Added pastes API. 
 * Added middleware for easier injection and setup in netcore projects.

