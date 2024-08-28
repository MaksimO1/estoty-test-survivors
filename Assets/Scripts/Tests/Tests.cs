using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Zenject;

public abstract class ZenjectUnitTestFixture
{
    DiContainer _container;

    protected DiContainer Container
    {
        get
        {
            return _container;
        }
    }

    [SetUp]
    public virtual void Setup()
    {
        _container = new DiContainer();
    }
}

[TestFixture]
public class EnemyTests : ZenjectUnitTestFixture
{
    [SetUp]
    public void CommonInstall()
    {
        Container.Bind<EnemyStatLogic>().AsSingle();
        Container.Inject(this);
    }

    [Inject]
    EnemyStatLogic _enemyStatLogic;

    [Test]
    public void TestCalculateNewHealth()
    {
        Assert.That(_enemyStatLogic.CalculateNewHealth(10, -2) == 8);
    }
}

[TestFixture]
public class PlayerTests : ZenjectUnitTestFixture
{
    [SetUp]
    public void CommonInstall()
    {
        Container.Bind<PlayerStatLogic>().AsSingle();
        Container.Bind<PlayerAttacks>().AsSingle();
        Container.Inject(this);
    }

    [Inject]
    PlayerStatLogic _playerStatLogic;
    [Inject]
    PlayerAttacks _playerAttacks;

    [Test]
    public void TestCalculateLevel()
    {
        Assert.That(_playerStatLogic.CalculateLevel(4000, 1000) == 5);
    }
    [Test]
    public void TestCalculateReloadedAmmo()
    {
        Assert.That(_playerAttacks.CalculateReloadedAmmo(10, 6, 8) == 2);
    }

}

[TestFixture]
public class ItemTests : ZenjectUnitTestFixture
{
    [SetUp]
    public void CommonInstall()
    {
        Container.Bind<ItemLogic>().AsSingle();
        Container.Inject(this);
    }

    [Inject]
    ItemLogic _itemLogic;

    [Test]
    public void TestInAttractionRange()
    {
        Assert.That(_itemLogic.InAttractionRange(new Vector2(0, 0), new Vector2(1, 1), 5.0f) == true);
    }
}
