using System.Collections.Generic;
using System.Linq;

public interface IEquipment
{
    void AddItem(Item item);
    T GetItem<T>() where T : Item;
}

public class Equipment : IEquipment
{
    private readonly List<Item> _items = new List<Item>();
    public IReadOnlyList<Item> Items => _items;

    public void AddItem(Item item)
    {        
        // Если добавляется оружие, то оно заменяется
        if (item is Weapon)
        {
            _items.RemoveAll(i => i is Weapon);
        }
        _items.Add(item);
    }

    public T GetItem<T>() where T : Item => _items.OfType<T>().FirstOrDefault();
}