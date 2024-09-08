

namespace App.Repositories;

public class ConnectionStringOption
{
    public const string ConnectionStrings = "ConnectionStrings";
    public string SqlServer { get; set; } = default!;
}
