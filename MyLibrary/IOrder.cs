namespace MyLibrary
{
    public interface IOrder
    {
        bool Insert(string id, string name);
        bool Update(string id, string name);
        bool Delete(string id);
    }
}