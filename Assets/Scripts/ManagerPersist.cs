using UnityEngine;

public class ManagerPersist : MonoBehaviour
{
    private static ManagerPersist Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
			
        DontDestroyOnLoad(gameObject);
    }
}
