namespace EnemyLogic
{
    public interface IEnemyStateSwitcher
    {
        void SwitchState<T>() where T : StateEnemy;
    }
}