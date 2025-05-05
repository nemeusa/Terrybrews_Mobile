using System.Threading.Tasks;
using Unity.Services.RemoteConfig;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class RemoteConfigStart : MonoBehaviour
{
    public static RemoteConfigStart Instance { get; private set; }

    public struct userAttributes { }
    public struct appAttributes { }

    public bool ndGenerator;
    public int BreakChance;
    public int ItemSpawnMin;
    public float ClientSpeed;
    public float Time;
    public int ItemSpawnMax;

    private void Awake()
    {
        Instance = this;
    }

    async Task InitializeRemoteConfigAsync()
    {
        // initialize handlers for unity game services
        await UnityServices.InitializeAsync();

        // remote config requires authentication for managing environment information
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    async Task Start()
    {
        // initialize Unity's authentication and core services, however check for internet connection
        // in order to fail gracefully without throwing exception if connection does not exist
        if (Utilities.CheckForInternetConnection())
        {
            await InitializeRemoteConfigAsync();
        }

        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
        RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
    }

    public void Fetch()
    {
        if (Utilities.CheckForInternetConnection())
            RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
    }

    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        Debug.Log("RemoteConfigService.Instance.appConfig fetched: " + RemoteConfigService.Instance.appConfig.config.ToString());

        ndGenerator = RemoteConfigService.Instance.appConfig.GetBool("ndGenerator");
        BreakChance = RemoteConfigService.Instance.appConfig.GetInt("BreakChance");
        ItemSpawnMin = RemoteConfigService.Instance.appConfig.GetInt("ItemSpawnMin");
        ClientSpeed = RemoteConfigService.Instance.appConfig.GetFloat("ClientSpeed");
        Time = RemoteConfigService.Instance.appConfig.GetFloat("Time");
        ItemSpawnMax = RemoteConfigService.Instance.appConfig.GetInt("ItemSpawnMax");
    }
}