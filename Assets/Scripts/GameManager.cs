using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("Dependencies")]
    [SerializeField] private DayManager DayManager;

    private bool GameStarted = false;

    #region Singleton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    [ContextMenu("Force Start Game")]
    public void GameStart()
    {
        if (GameStarted) return;

        GameStarted = true;

        DayManager.PreStartDay();
    }

    public void GameEnd()
    {
        GameStarted = false;
    }
}
