namespace RPG.UI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using RPG.Scene;
    using TMPro;
    using Unity.Services.Authentication;
    using Unity.Services.CloudSave;
    using Unity.Services.Core;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class AuthenticationAccount : MonoBehaviour
    {
        [Header("Regex")]
        public string UsernameRegexValue = @"^[a-zA-Z0-9_]+$";
        public string PasswordRegexValue = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
        private Regex usernameRegex;
        private Regex passwordRegex;
        [Header("LoginField")]
        [SerializeField] private GameObject LoginUI;
        [SerializeField] private TMP_InputField loginUsername;
        [SerializeField] private TMP_InputField loginPassword;
        [SerializeField] private Toggle rememberLogin;
        [SerializeField] private Button log_loginButton;
        [SerializeField] private Button log_registerButton;
        [SerializeField] private Button forgotPasswordButton;

        [Header("RegisterField")]
        [SerializeField] private GameObject RegisterUI;
        [SerializeField] private TMP_InputField registerUsername;
        [SerializeField] private TMP_InputField registerEmail;
        [SerializeField] private TMP_InputField registerPassword;
        [SerializeField] private TMP_InputField registerPasswordConfirm;
        [SerializeField] private Button reg_registerButton;
        [SerializeField] private Button reg_loginButton;

        [Header("PopupField")]
        [SerializeField] private Image popupImage;
        [SerializeField] private TextMeshProUGUI popupText;
        [Header("Menu")]
        [SerializeField] private Button quitButton;

        private async void Awake()
        {
            try
            {
                usernameRegex = new Regex(UsernameRegexValue);
                passwordRegex = new Regex(PasswordRegexValue);
                CleanPopup();
                await UnityServices.InitializeAsync();
                LoadRememberLogin();
                ButtonListened();
            }
            catch (Exception)
            {
                //Debug.LogException(e);
            }

        }

        private void ButtonListened()
        {
            log_loginButton.onClick.AddListener(() =>
            {
                Login();
            });
            log_registerButton.onClick.AddListener(() =>
            {
                LoginUI.SetActive(false);
                RegisterUI.SetActive(true);
            });
            reg_registerButton.onClick.AddListener(() =>
            {
                Register();
                if (AuthenticationService.Instance.IsSignedIn)
                {
                    RegisterUI.SetActive(false);
                    LoginUI.SetActive(true);
                }
            });
            reg_loginButton.onClick.AddListener(() =>
            {
                RegisterUI.SetActive(false);
                LoginUI.SetActive(true);
            });
            quitButton.onClick.AddListener(() =>
            {
                QuitApplication();
            });
        }

        private void UpdatePopup(Color color, string text)
        {
            popupImage.color = color;
            popupText.text = text;
        }

        private void CleanPopup()
        {
            popupImage.color = Color.clear;
            popupText.text = "";
        }

        IEnumerator SetPopupLife(float value)
        {
            yield return new WaitForSeconds(value);
            CleanPopup();
        }

        private void LoadRememberLogin()
        {
            rememberLogin.isOn = PlayerPrefs.GetInt("RememberLogin", 0) == 1;
            loginUsername.text = PlayerPrefs.GetString("RememberUsername", "");
            loginPassword.text = PlayerPrefs.GetString("RememberPassword", "");
        }

        private void CleanLoginInput()
        {
            if (rememberLogin.isOn == true)
            {
                PlayerPrefs.SetInt("RememberLogin", rememberLogin.isOn ? 1 : 0);
                PlayerPrefs.SetString("RememberUsername", loginUsername.text);
                PlayerPrefs.SetString("RememberPassword", loginPassword.text);
                PlayerPrefs.Save();
            }
            else
            {
                CleanLogin();
            }
        }

        private void CleanLogin()
        {
            loginUsername.text = "";
            loginPassword.text = "";
        }

        private void CleanRegisterInput()
        {
            registerUsername.text = "";
            registerEmail.text = "";
            registerPassword.text = "";
            registerPasswordConfirm.text = "";
        }

        public async void Login()
        {
            string username = loginUsername.text;
            string password = loginPassword.text;

            await LogInWithUsernameAndPassword(username, password);
        }

        public async void Register()
        {
            float delayTime = 1f;
            string username = registerUsername.text;
            string password = registerPassword.text;
            string confirmPassword = registerPasswordConfirm.text;

            if (username == "" || password == "" || confirmPassword == "")
            {
                UpdatePopup(Color.white, "Input must not null");
                StartCoroutine(SetPopupLife(delayTime));
            }
            else if (!usernameRegex.IsMatch(username) || !passwordRegex.IsMatch(password))
            {
                UpdatePopup(Color.white, "A valid Username: alphanumeric, characters and underscores\nA valid Password: at least 8 characters, with uppercase, lowercase, digit, and special character");
                StartCoroutine(SetPopupLife(delayTime));
            }
            else if (password != confirmPassword)
            {
                UpdatePopup(Color.white, "Incorrect confirm password");
                StartCoroutine(SetPopupLife(delayTime));
            }
            else
            {
                await RegisterWithUsernameAndPassword(username, password);
            }
        }

        public void LogOut()
        {
            AuthenticationService.Instance.SignOut();
        }

        public void QuitApplication()
        {
            Application.Quit();
            CleanLoginInput();
        }

        private async Task LogInWithUsernameAndPassword(string username, string password)
        {
            float delayTime = 1f;
            try
            {
                await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
                //Debug.Log("Login is successful.");
                UpdatePopup(Color.white, "Login is successful");
                StartCoroutine(SetPopupLife(delayTime));
                CleanLoginInput();
                MySceneManager.Instance.LoadSceneAfterSecond(MyScene.MenuScene, delayTime);
            }
            catch (AuthenticationException)
            {

            }
            catch (RequestFailedException)
            {
                UpdatePopup(Color.white, "Incorrect username/password");
                StartCoroutine(SetPopupLife(delayTime));
            };
        }

        private async Task RegisterWithUsernameAndPassword(string username, string password)
        {
            float delayTime = 1f;
            try
            {
                await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
                //Debug.Log("Register is successful.");
                UpdatePopup(Color.white, "Register is successful");
                StartCoroutine(SetPopupLife(delayTime));
                CleanRegisterInput();
                AuthenticationService.Instance.SignOut();

            }
            catch (AuthenticationException)
            {
                UpdatePopup(Color.white, "Register is unsuccessful");
                StartCoroutine(SetPopupLife(delayTime));
            }
            catch (RequestFailedException)
            {

            };
        }

    }
}
