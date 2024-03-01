# WeatherCli

WeatherCli is a simple tool that allows you to retrieve weather data using the [OpenWeatherMap API](https://openweathermap.org), displaying the current weather in your terminal.

## Installation

To install WeatherCli, you need to download the [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0). Once installed, run the following command:

```bash
dotnet tool install -g weathercli
```

Obtain a free API key [here](https://openweathermap.org/appid).

## Usage

### Current weather command

```bash
Usage: weather current [options] <CityName>

Arguments:
  CityName      You can call by city name or city name, state code and country code

Options:
  -d|--default  Sets a default city name
  -k|--apikey   Sets an OpenWeatherMap API key
```

Example using the current weather command:

```bash
weather current sofia -k YOUR_API_KEY
```

Example of successful response:
> Sofia\
Mist -2°C\
Feels like -5°C. mist\
Wind 1.0m/s N, 1021hPa, Humidity: 92%\
Visibility: 2.8km

Optionally, you can omit the API key. To do that, you need to set the environment variable.

Windows command:

```bash
setx OPENWEATHERMAP_APIKEY YOUR_API_KEY
```

Linux command:

```bash
export OPENWEATHERMAP_APIKEY=YOUR_API_KEY
```

## Contribution

* If you want to contribute to this project, create a pull request
* If you find any bugs or error, create an issue

## License

Distributed under the MIT License. See [LICENSE](https://github.com/TheDayIsMyEnemy/WeatherCli/blob/main/LICENSE) for more information.
