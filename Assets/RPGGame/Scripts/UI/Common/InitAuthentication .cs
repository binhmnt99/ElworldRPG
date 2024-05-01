namespace RPG.Init
{
    using UnityEngine;
    using Unity.Services.Core;
    using Unity.Services.Authentication;
    using UnityEngine.SceneManagement;

    public class InitAuthentication : MonoBehaviour
    {
        private async void Start()
        {
            await UnityServices.InitializeAsync();
            SignIn();
        }

        private async void SignIn()
        {
            if (UnityServices.State == ServicesInitializationState.Initialized)
            {
                AuthenticationService.Instance.SignedIn += OnSignedIn;

                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                if (AuthenticationService.Instance.IsSignedIn)
                {
                    string username = PlayerPrefs.GetString("Username");
                    if (string.IsNullOrEmpty(username))
                    {
                        username = "Player";
                        PlayerPrefs.SetString("Username", username);
                    }
                    await SceneManager.LoadSceneAsync("MainMenuScene");
                }
            }

        }

        private void OnSignedIn()
        {
            Debug.Log($"Player id: {AuthenticationService.Instance.PlayerId}");
            Debug.Log($"Token: {AuthenticationService.Instance.AccessToken}");
        }
    }
}