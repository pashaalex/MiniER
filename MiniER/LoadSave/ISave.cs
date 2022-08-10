
namespace MiniER.OpenSave
{
    public interface ISave
    {
        string GetExtension();
        bool IsMyExtension(string extension);
        void Save(Model.DataSchema schema, string fileName);
    }
}
