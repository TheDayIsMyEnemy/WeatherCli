# WeatherCli

Simple tool that allows you to get weather data from OpenWeatherMap API.

It shows the current Weather in your terminal.

## Installation
Download .NET Core 3.1 SDK.
Once installed, run this command:

```
dotnet tool install -g weathercli
```
Get a free API key at [https://openweathermap.org/appid](https://openweathermap.org/appid)

## Usage

```
Usage: weather current [options] <CityName>

Arguments:
  CityName      You can call by city name or city name, state code and country code

Options:
  -d|--default  Sets a default city name
  -k|--apikey   Sets an OpenWeatherMap API key
  -?|-h|--help  Show help information
```

Example using current weather command:

```
weather current sofia -k YOUR_API_KEY
```
Example of successfull response: 
> Sofia
-5.29°C -0.56/-11°C
Feels like -8.83°C. FOG, 
Wind 1m/s ESE, 1023hPa, Humidity: 85%
Visibility: 0.25km

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
