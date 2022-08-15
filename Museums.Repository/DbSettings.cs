namespace Museums.Repository;
public class DbSettings
{
    public string ConnectionString { get; set; }

    public string DatabaseName { get; set; }

    public string MuseumCollection { get; set; }

    public string CrontabCollection { get; set; }

    public string LogCollection { get; set; }
}
