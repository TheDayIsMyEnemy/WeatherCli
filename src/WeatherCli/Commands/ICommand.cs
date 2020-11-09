namespace WeatherCli.Commands
{
    public interface ICommand
    {
        public string Name { get; }

        public void Execute();
    }
}
