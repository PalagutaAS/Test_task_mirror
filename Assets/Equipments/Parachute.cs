using UnityEngine;

public class Parachute : Item
{
    public Parachute() : base("Parachute") { }

    public void Apply()
    {
        Debug.Log($"Применили {Name}");
    }
}