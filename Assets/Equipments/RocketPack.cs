
using UnityEngine;

public class RocketPack : Item
{
    public int Charges { get; set; }
    public bool _isActive = false;
    public RocketPack(int initialCharges) : base("Rocket Pack")
    {
        Charges = initialCharges;
    }

    public void Activate()
    {
        if (_isActive)
            return;
        
        _isActive = true;
        Debug.Log($"Включили {Name}");
    }    
    
    public void Deactivate()
    {
        if (!_isActive)
            return;
        
        _isActive = false;
        Debug.Log($"Выключили {Name}");
    }

}
