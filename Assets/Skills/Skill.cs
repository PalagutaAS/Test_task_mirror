using UnityEngine;

public interface ISkill
{
    string Name { get; }
    void Cast();
}

public class Skill : ISkill
{
    public Skill(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
    
    public void Cast()
    {
        Debug.Log($"Был использован Skill : {Name}");
    }
}


