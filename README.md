# HIBP.NET
[![GitHub license](https://img.shields.io/github/license/VisualBean/HIBP.NET.svg)](https://github.com/VisualBean/HIBP.NET/blob/master/LICENSE) ![.NET Core](https://github.com/VisualBean/HIBP.NET/workflows/.NET%20Core/badge.svg) [![NuGet version](https://badge.fury.io/nu/HIBP.NET.svg)](https://badge.fury.io/nu/HIBP.NET)
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
async Task MyMethodPlainTextPasswordAsync()
{
    var client = new HIBP.PwnedPasswordClient("MyAwesomeService");
    int pwns = await client.IsPasswordPwnedAsync("password1");
    if (pwns > 0)
    {
        Console.WriteLine($"Password has been pwned: {pwns} times");
    }
}

// or

async Task MyMethodPreHashedPasswordAsync()
{
    var client = new HIBP.PwnedPasswordClient("MyAwesomeService");
    int pwns = await client.IsPasswordPwnedAsync("password1".ToSHA1(), isHash: true);
    if (pwns > 0)
    {
        Console.WriteLine("Password has been pwned");
    }
}

```

## With .Net core dependency injection.
### Adding the clients
```csharp
// for all clients
public void ConfigureServices(IServiceCollection services)
{
    services.AddHIBP(c =>
    {
        c.ApiKey = new ApiKey("MyKey");
        c.ServiceName = "MyAwesomeService";
    });
}
```
or you can add individual clients
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddBreachClient(c =>
    {
        c.ApiKey = new ApiKey("MyKey");
        c.ServiceName = "MyAwesomeService";
    });
    
    services.AddPwnedPasswordsClient("MyAwesomeService");
    
    services.AddPastesClient(new ApiKey("MyKey"), "MyAwesomeService");
 }
```

Inject the client.
```csharp
class MyClass
{
    private readonly IBreachClient breachClient;
    public MyClass(IBreachClient breachClient)
    {
        this.breachClient = breachClient;
    }
    
    public async Task GetBreachesAsync()
    {
        var breaches = this.breachClient.GetBreachesAsync();
        // do stuff..
    }
}
```


Changes
===
### Breaking changes are coming in version 3.0
 * ApiKey has been refactored to be a class of its own. (**BREAKING**)
 * Renamed API clients from `{name}Api` to `{name}Client` (**BREAKING**)
 * Renamed parameters to better match usage.
 * Added pastes client. 
 * Added extension for easier injection and setup in netcore projects.
 * Expose `ToSHA1()`. for easy hashing when using the PwnedPasswords API.
 * Update to .Net core 3.1

