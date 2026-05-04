using UnityEngine;
using VContainer;

public class GameLoop : MonoBehaviour
{
    private IPlayer _player;
    
    [Inject]
    private void Constructor(IPlayerProvider playerProvider)
    {
        //игрок уже существует в этот момент, подтягиваем зависимость и сохраняем ссылку
        _player = playerProvider.Player;
    }
    
    void Start()
    {
        // создание и наполнение экипировки игрока
        _player.Equipment.AddItem(new Weapon("Винтовка", 50));
        _player.Equipment.AddItem(new Parachute());
        _player.Equipment.AddItem(new RocketPack(3));
        
        _player.Skills.AddSkill(new Skill("First Skill"));
        _player.Skills.AddSkill(new Skill("Second Skill"));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _player.Equipment.GetItem<Weapon>().Reload();
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            _player.Equipment.GetItem<Weapon>().Shoot();
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            _player.Equipment.GetItem<Parachute>().Apply();
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            _player.Equipment.GetItem<RocketPack>().Activate();
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            _player.Equipment.GetItem<RocketPack>().Deactivate();
        }
        
        if (Input.GetKeyDown(KeyCode.H))
        {
            _player.Heal(10);
        }        
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            _player.TakeDamage(12);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            var newWeapon = new Weapon("Gun", 10);
            _player.Equipment.AddItem(newWeapon);
        }        
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _player.Skills.CastSkill(0);
        }  
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _player.Skills.CastSkill(1);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _player.Skills.CastSkill(2);
        }
    }
}
