
namespace MiniER.OpenSave
{
    public interface ILoad
    {
        string GetExtension();
        bool IsMyExtension(string extension);
        Model.DataSchema Open(string fileName);
    }
}
