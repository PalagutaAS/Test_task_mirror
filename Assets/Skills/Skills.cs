using System.Collections.Generic;
using UnityEngine;

public interface ISkills
{
    void AddSkill(ISkill skill);
    void CastSkill(int index);
    IReadOnlyList<ISkill> SkillsList { get; }
}

public class Skills : ISkills
{
    private readonly List<ISkill> _skills = new List<ISkill>();
    
    public IReadOnlyList<ISkill> SkillsList { get; }
    
    public void AddSkill(ISkill skill)
    {
        _skills.Add(skill);
    }

    public void CastSkill(int index)
    {
        if (_skills.Count <= index)
        {
            Debug.Log($"Пустая ячейка {index+1} для Skill.");
            return;
        }
        
        _skills[index].Cast();
    }


}


