using System.Collections.Generic;
using Tools.Singletons;

public class GameManager : Singletone<GameManager>
{
    private List<EnemyBehaviour> _enemies = new List<EnemyBehaviour>();

    public void AddEnemy(EnemyBehaviour enemy)
    {
        _enemies.Add(enemy);
        UIManager.Instance.EnemiesText.text = $"{_enemies.Count} enemies";
    }
    public void Delete()
    {
        _enemies.Remove(_enemies[0]);
        
        if(UIManager.Instance == null) return;
        
        UIManager.Instance.EnemiesText.text = $"{_enemies.Count} enemies";
        if (_enemies.Count <= 0)
            UIManager.Instance.winPanel.SetActive(true);
    }

    public void PlayerDie()
    {
        UIManager.Instance.losePanel.SetActive(true);
    }

}
