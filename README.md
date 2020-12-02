# WeatherCli

A simple tool that allows you to get weather data using the [OpenWeatherMap](https://openweathermap.org) API.

Shows the current Weather in your terminal.

## Installation

Download [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1).
Once installed, run this command:

```
dotnet tool install -g weathercli
```

Get a free API key [here](https://openweathermap.org/appid).

## Usage

### Current weather command

```
Usage: weather current [options] <CityName>

Arguments:
  CityName      You can call by city name or city name, state code and country code

Options:
  -d|--default  Sets a default city name
  -k|--apikey   Sets an OpenWeatherMap API key
```

**Example using the current weather command:**

```
weather current sofia -k YOUR_API_KEY
```

**Example of successful response:** 
> Sofia\
Mist -2°C\
Feels like -5°C. mist\
Wind 1.0m/s N, 1021hPa, Humidity: 92%\
Visibility: 2.8km

Optionally, you can omit the API key.
In order to do that, you have to set the environment variable.

How to do it in Windows:


For the current process:
```
set OPENWEATHERMAP_APIKEY=YOUR_API_KEY
```
For the user:
```
setx OPENWEATHERMAP_APIKEY YOUR_API_KEY
```

## Contribution

* If you want to contribute to this project, create a pull request
* If you find any bugs or error, create an issue

## License

Distributed under the MIT License. See [LICENSE](https://github.com/TheDayIsMyEnemy/WeatherCli/blob/main/LICENSE) for more information.
