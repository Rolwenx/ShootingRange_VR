using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class VRFXLoader : MonoBehaviour
{
    public static VRFXLoader instance;

    public GameObject muzzleFlashPrefab;
    public GameObject impactPrefab;

    private AsyncOperationHandle<GameObject> muzzleFlashHandle;
    private AsyncOperationHandle<GameObject> impactHandle;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        LoadFXBasedOnPlatform();
    }

    private void LoadFXBasedOnPlatform()
    {
#if UNITY_ANDROID
        string muzzleKey = "MuzzleFlash_Meta";
        string impactKey = "TargetImpact_Effect_Meta";
#else
        string muzzleKey = "MuzzleFlash_PCVR";
        string impactKey = "TargetImpact_Effect_PCVR";
#endif

        // Load Muzzle Flash
        muzzleFlashHandle = Addressables.LoadAssetAsync<GameObject>(muzzleKey);
        muzzleFlashHandle.Completed += (op) =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
                muzzleFlashPrefab = op.Result;
        };

        // Load Impact FX
        impactHandle = Addressables.LoadAssetAsync<GameObject>(impactKey);
        impactHandle.Completed += (op) =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
                impactPrefab = op.Result;
        };
    }

    public void UnloadFX()
    {
        if (muzzleFlashHandle.IsValid())
            Addressables.Release(muzzleFlashHandle);

        if (impactHandle.IsValid())
            Addressables.Release(impactHandle);
    }
}
