using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject objectPoolerTarget;

    public static GameManager Instance;

    public GameObject ObjectPoolerTarget { get => objectPoolerTarget; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
