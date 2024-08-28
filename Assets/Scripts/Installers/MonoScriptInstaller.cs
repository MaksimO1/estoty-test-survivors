using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Zenject;

public class MonoScriptInstaller : MonoInstaller<MonoScriptInstaller>
{
    [SerializeField] private GameObject bulletPrefab;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        //Signals
        Container.DeclareSignal<PlayerHealthChangeSignal>();
        Container.DeclareSignal<PlayerExperienceChangeSignal>();
        Container.DeclareSignal<PlayerKillsChangeSignal>();
        Container.DeclareSignal<PlayerAmmoChangeSignal>();

        Container.DeclareSignal<PlayerAmmoItemConsumptionSignal>();
        Container.DeclareSignal<PlayerHealingItemConsumptionSignal>();
        Container.DeclareSignal<PlayerExperienceItemConsumptionSignal>();

        Container.DeclareSignal<PlayerSpawnBulletSignal>();

        Container.DeclareSignal<EnemyCollisionSignal>();
        Container.DeclareSignal<EnemyDeathSignal>();
        Container.DeclareSignal<EnemySpawnItemSignal>();


        //Factory
        Container.Bind<BulletSpawner>().AsSingle();
        Container.Bind<EnemySpawner>().AsSingle();
        Container.Bind<ItemSpawner>().AsSingle();

        Container.Bind<FactoryConsumer>().FromComponentInHierarchy().AsSingle();

        //Player
        Container.Bind<IPlayerMovement>().To<PlayerMovement>().AsSingle();

        Container.Bind<IPlayerStatLogic>().To<PlayerStatLogic>().AsSingle();
        Container.Bind<PlayerStatConsumer>().FromComponentInHierarchy().AsSingle();

        Container.Bind<IPlayerAttacks>().To<PlayerAttacks>().AsSingle();
        Container.Bind<PlayerAttackConsumer>().FromComponentInHierarchy().AsSingle();

        //Items
        Container.Bind<IItemLogic>().To<ItemLogic>().AsSingle();

        //Enemies
        Container.Bind<IEnemyMovement>().To<EnemyMovement>().AsSingle();
        Container.Bind<IEnemyAttacks>().To<EnemyAttacks>().AsSingle();
        Container.Bind<IEnemyStatLogic>().To<EnemyStatLogic>().AsSingle();

        //UI
        Container.Bind<IUILogic>().To<UILogic>().AsSingle();
        Container.Bind<UILogicConsumer>().FromComponentInHierarchy().AsSingle();

        //Bulets
        Container.Bind<IBullet>().To<Bullet>().AsSingle();

        Container.BindFactory<UnityEngine.Vector2, UnityEngine.Quaternion, BulletConsumer, BulletConsumer.Factory>().FromComponentInNewPrefab(bulletPrefab);

        //Signal Bindings
        Container.BindSignal<PlayerHealthChangeSignal>().ToMethod<UILogicConsumer>
        ((consumer, signal) => consumer.UpdateHealthUI(signal.currentHealth, signal.maxHealth)).FromResolve();
        Container.BindSignal<PlayerExperienceChangeSignal>().ToMethod<UILogicConsumer>
        ((consumer, signal) => consumer.UpdateExperienceUI(signal.currentExperienceAdjusted, signal.nextLevelExperienceCap, signal.currentLevel)).FromResolve();
        Container.BindSignal<PlayerKillsChangeSignal>().ToMethod<UILogicConsumer>
        ((consumer, signal) => consumer.UpdateKillCountUI(signal.playerKillCount)).FromResolve();
        Container.BindSignal<PlayerAmmoChangeSignal>().ToMethod<UILogicConsumer>
        ((consumer, signal) => consumer.UpdateAmmoUI(signal.currentAmmo, signal.maxAmmo, signal.stashedAmmo)).FromResolve();

        Container.BindSignal<PlayerSpawnBulletSignal>().ToMethod<FactoryConsumer>
        ((consumer, signal) => consumer.SpawnBullet(signal.rotation, signal.spawnLocation)).FromResolve();

        Container.BindSignal<EnemySpawnItemSignal>().ToMethod<FactoryConsumer>
        ((consumer, signal) => consumer.SpawnItem(signal.spawnLocation, signal.itemTypeEnum)).FromResolve();
        Container.BindSignal<EnemyCollisionSignal>().ToMethod<PlayerStatConsumer>
        ((consumer, signal) => consumer.AdjustHealth(-signal.collisionDamage)).FromResolve();
        Container.BindSignal<EnemyDeathSignal>().ToMethod<PlayerStatConsumer>
        ((consumer, signal) => consumer.AdjustKillCount(1)).FromResolve();

        Container.BindSignal<PlayerAmmoItemConsumptionSignal>().ToMethod<PlayerAttackConsumer>
        ((consumer, signal) => consumer.GainAmmo(signal.ammoChange)).FromResolve();
        Container.BindSignal<PlayerHealingItemConsumptionSignal>().ToMethod<PlayerStatConsumer>
        ((consumer, signal) => consumer.AdjustHealth(signal.healthChange)).FromResolve();
        Container.BindSignal<PlayerExperienceItemConsumptionSignal>().ToMethod<PlayerStatConsumer>
        ((consumer, signal) => consumer.AdjustExperience(signal.experienceChange)).FromResolve();
    }
}