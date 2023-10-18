namespace DataAccess.Models;

public enum DataType
{
    Skills,
    Aim,
    Occupation,
    WantedLevel
}

public class Data
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public DataType Type { get; set; }
}
