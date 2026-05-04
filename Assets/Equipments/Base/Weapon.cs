using UnityEngine;

public interface IWeapon
{
    void Shoot();
    void Reload();
}

public class Weapon : Item, IWeapon
{
    public int Ammo { get; set; }
    
    private readonly int _initialAmmo;
    
    public Weapon(string name, int initialAmmo) : base(name)
    {
        _initialAmmo = initialAmmo;
        Ammo = _initialAmmo;
    }
    
    public void Shoot()
    {
        if (Ammo <= 0)
        {
            Debug.Log($"{Name} не может стрелять: нет патронов!");
            return;
        }
        
        Ammo--;
        Debug.Log($"{Name} выстрелила! Осталось патронов: {Ammo}");
    }

    public void Reload()
    {
        Ammo = _initialAmmo;
        Debug.Log($"Перезарядка {Name}. Осталось патронов: {Ammo}");
    }
}

