using System;

namespace WeatherCli.Options
{
    public interface IWritableOptions<out TOptions> where TOptions : class, new()
    {
        TOptions Value { get; }

        void Update(Action<TOptions> applyChanges);
    }
}
