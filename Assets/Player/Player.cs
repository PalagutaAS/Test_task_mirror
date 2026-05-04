using UnityEngine;

public interface IPlayer
{
    public int Health { get; }
    void TakeDamage(int amount);
    void Heal(int amount);
    int Lives { get; }
    string Nickname { get; }
    ISkills Skills { get; }
    IEquipment Equipment { get; }
}

public class Player : IPlayer
{
    private readonly int _maxHealth;
    private int currentHealth;
    public int Health
    {
        get => currentHealth;

        private set
        {
            int newHealth = Mathf.Clamp(value, 0, _maxHealth);
            currentHealth = newHealth;
        }
    }
    public int Lives { get; private set; }
    public string Nickname { get; private set; }
    public ISkills Skills { get; }
    public IEquipment Equipment { get; }
    
    public Player(string nickname, int health, int lives, IEquipment equipment, ISkills skills)
    {
        _maxHealth = health;
        Nickname = nickname;
        Lives = lives;
        Health = health;
        Equipment = equipment;
        Skills = skills;
    }
    
    public void TakeDamage(int amount)
    {
        Health -= amount;
        Debug.Log($"{Nickname} получил {amount} урона. Осталось здоровья: {Health}");
        if (Health <= 0) Die();
    }

    public void Heal(int amount)
    {
        Health += amount;
        Debug.Log($"{Nickname} восстановил {amount} здоровья. Текущее здоровье: {Health}");
    }

    private void Die()
    {
        Lives--;
        Debug.Log($"{Nickname} погиб. Осталось жизней: {Lives}");
        if (Lives > 0) 
            Health = _maxHealth;
    }
}
