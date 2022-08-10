namespace MiniER.DB
{
    public interface IDBModul
    {        
        string DBType { get; }
        string ConnectionString { get; }
        Model.DataSchema GetSchema();
    }
}
