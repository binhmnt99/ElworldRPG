namespace RPG.Scene
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class MySceneManager : MonoBehaviour
    {
        public static MySceneManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void LoadScene(MyScene scene)
        {
            SceneManager.LoadScene(scene.ToString());
        }

        public void LoadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void LoadSceneAfterSecond(MyScene scene, float value)
        {
            StartCoroutine(LoadScene(scene, value));
        }

        IEnumerator LoadScene(MyScene scene, float value)
        {
            yield return new WaitForSeconds(value);
            SceneManager.LoadScene(scene.ToString());
        }
    }

    public enum MyScene
    {
        AuthenticationScene,
        MenuScene
    }
}