namespace RPG.UI
{
    using System;
    using RPG.Scene;
    using TMPro;
    using Unity.Services.Authentication;
    using Unity.Services.Core;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class AuthenticationPlayerInfo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerInfoText;

        private async void Awake()
        {
            try
            {
                await UnityServices.InitializeAsync();
                //playerInfoText.text = "Welcome back, " + AuthenticationService.Instance.PlayerId + "!";
                //UpdateUI();
                if (AuthenticationService.Instance.IsSignedIn)
                {
                    //Debug.Log("Signin");
                    //Debug.Log($"PlayedID: {AuthenticationService.Instance.PlayerId}");
                    PlayerInfo playerInfo = await AuthenticationService.Instance.GetPlayerInfoAsync();
                    playerInfoText.text = "Welcome back, " + playerInfo.Username + "!";
                }
            }
            catch (AuthenticationException e)
            {
                Debug.LogException(e);
            }
            catch (RequestFailedException e)
            {
                Debug.LogException(e);
            };

        }

        private void UpdateUI()
        {
            playerInfoText.text = "Welcome back, " + AuthenticationService.Instance.PlayerName + "!";
        }

        public void LogOut()
        {
            AuthenticationService.Instance.SignOut();
            MySceneManager.Instance.LoadScene(MyScene.AuthenticationScene);
        }
        
    }
}