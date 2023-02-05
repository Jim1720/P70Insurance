using A70Insurance;
using A70Insurance.Models;
using A70Insurance.StyleFeature;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

Env.url = builder.Configuration.GetValue<String>("UrlPrefix");

Env.usingStyles = builder.Configuration.GetValue<String>("UseStyles");

builder.Services.AddBlazoredSessionStorage();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

const string yes = "Y";


Env.ePattern = builder.Configuration.GetValue<string>("ePattern");

Env.usingStay = builder.Configuration.GetValue<string>("UseStay") == yes;

Env.usingFocus = builder.Configuration.GetValue<string>("UseFocus") == yes;

Env.usingNav = builder.Configuration.GetValue<string>("UseNav") == yes;

Env.usingActions = builder.Configuration.GetValue<string>("UseActions") == yes; 

builder.Services.AddScoped<IScreenStyleFactory, ScreenStyleFactory>();

builder.Services.AddScoped<IScreenStyleList, ScreenStyleList>();

builder.Services.AddScoped<IScreenStyleManager, ScreenStyleManager>();

builder.Services.AddScoped<IEditDate, EditDate>();

builder.Services.AddScoped<IActionOperations, ActionOperations>();

builder.Services.AddScoped<IFocusedClaim, FocusedClaim>();

builder.Services.AddScoped<IHistorySettings, HistorySettings>();

await builder.Build().RunAsync();
