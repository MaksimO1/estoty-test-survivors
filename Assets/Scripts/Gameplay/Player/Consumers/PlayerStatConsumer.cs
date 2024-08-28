using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PlayerStatConsumer : MonoBehaviour
{
    private IPlayerStatLogic _iPlayerStatLogic;
    private SignalBus _signalBus;
    private int _playerKillCount = 0;
    private int _nextLevelExperienceCap;
    private int _currentHealth;
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private int _currentExperience;
    [SerializeField]
    private int _experienceNeededPerLevel;
    [Min(1)]
    [SerializeField]
    private int _currentLevel;

    [Inject]
    public void Construct(IPlayerStatLogic iPlayerStatLogic, SignalBus signalBus)
    {
        _iPlayerStatLogic = iPlayerStatLogic;
        _signalBus = signalBus;
    }
    // Start is called before the first frame update
    void Start()
    {
        _nextLevelExperienceCap = _iPlayerStatLogic.CalculateExperienceCap(_currentLevel, _experienceNeededPerLevel);
        _currentHealth = _maxHealth;

        _signalBus.Fire(new PlayerHealthChangeSignal { currentHealth = _currentHealth, maxHealth = _maxHealth, });
        _signalBus.Fire(new PlayerExperienceChangeSignal { currentExperienceAdjusted = _currentExperience - ((_currentLevel - 1) * _experienceNeededPerLevel), currentLevel = _currentLevel, nextLevelExperienceCap = _nextLevelExperienceCap });
        _signalBus.Fire(new PlayerKillsChangeSignal { playerKillCount = _playerKillCount });
    }

    public void AdjustKillCount(int killCountChange)
    {
        _playerKillCount = _iPlayerStatLogic.CalculateNewKillCount(_playerKillCount, killCountChange);
        _signalBus.Fire(new PlayerKillsChangeSignal { playerKillCount = _playerKillCount });
    }

    public void AdjustHealth(int healthChange)
    {
        _currentHealth = _iPlayerStatLogic.CalculateCurrentPlayerHealth(healthChange, _maxHealth, _currentHealth);
        _signalBus.Fire(new PlayerHealthChangeSignal { currentHealth = _currentHealth, maxHealth = _maxHealth, });
        CheckDeath();
    }

    void CheckDeath()
    {
        if (_iPlayerStatLogic.CheckForDeath(_currentHealth))
        {
            Die();
        }
    }

    void Die()
    {
        SceneManager.LoadScene("Boot");
    }

    public void AdjustExperience(int experienceChange)
    {
        _currentExperience = _iPlayerStatLogic.CalculateCurrentExperience(experienceChange, _currentExperience);
        _currentLevel = _iPlayerStatLogic.CalculateLevel(_currentExperience, _experienceNeededPerLevel);
        _nextLevelExperienceCap = _iPlayerStatLogic.CalculateExperienceCap(_currentLevel, _experienceNeededPerLevel);

        _signalBus.Fire(new PlayerExperienceChangeSignal { currentExperienceAdjusted = _currentExperience - ((_currentLevel - 1) * _experienceNeededPerLevel), currentLevel = _currentLevel, nextLevelExperienceCap = _nextLevelExperienceCap });
    }
}
