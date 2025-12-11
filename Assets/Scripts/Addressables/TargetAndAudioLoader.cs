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

        // ---- Load Target Prefab ----
        Addressables.LoadAssetAsync<GameObject>(targetKey).Completed += (op) =>
        {
            targetPrefab = op.Result;
            Debug.Log("Loaded target prefab: " + targetPrefab.name);
        };

        // ---- Load Shoot Sound ----
        Addressables.LoadAssetAsync<AudioClip>(audioShootKey).Completed += (op) =>
        {
            shootSFX = op.Result;
        };

        // ---- Load Impact Sound ----
        Addressables.LoadAssetAsync<AudioClip>(audioImpactKey).Completed += (op) =>
        {
            impactSFX = op.Result;
        };

        isReady = true;
    }
}
