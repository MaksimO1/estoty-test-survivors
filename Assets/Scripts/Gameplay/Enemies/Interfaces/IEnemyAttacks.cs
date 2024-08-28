using Zenject;

public interface IEnemyAttacks
{
    public void ConductPlayerAttack(SignalBus bus, int damage);
}
