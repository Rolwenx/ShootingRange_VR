using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TargetAndAudioLoader : MonoBehaviour
{
    public static TargetAndAudioLoader instance;

    [Header("Loaded Assets")]
    public GameObject targetPrefab;
    public AudioClip shootSFX;
    public AudioClip impactSFX;

    public bool isReady = false;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadAssets();
    }

    private void LoadAssets()
    {
        // 👉 Platform-specific Addressable keys (YOUR BLOCK HERE)
#if UNITY_ANDROID
        string targetKey = "Target_Quest";
        string audioShootKey = "Shoot_Quest";
        string audioImpactKey = "Impact_Quest";
#else
        string targetKey = "Target_PCVR";
        string audioShootKey = "Shoot_PCVR";
        string audioImpactKey = "Impact_PCVR";
#endif

        // loading target prefab
        Addressables.LoadAssetAsync<GameObject>(targetKey).Completed += (op) =>
        {
            targetPrefab = op.Result;
            isReady = true; // on signale que la cible est prête
        };


        // loading shoot sound 
        Addressables.LoadAssetAsync<AudioClip>(audioShootKey).Completed += (op) =>
        {
            shootSFX = op.Result;
        };

        // loading impact sound 
        Addressables.LoadAssetAsync<AudioClip>(audioImpactKey).Completed += (op) =>
        {
            impactSFX = op.Result;
        };

    }
}
