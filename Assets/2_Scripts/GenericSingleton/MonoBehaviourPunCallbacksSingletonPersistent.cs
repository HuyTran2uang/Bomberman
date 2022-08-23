using Photon.Pun;

public class MonoBehaviourPunCallbacksSingletonPersistent<T> : MonoBehaviourPunCallbacks where T : MonoBehaviourPunCallbacks
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
