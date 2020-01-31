using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Makeey.UI
{
    public class UISystem : MonoBehaviour
    {
        #region Parameters that are set from Unity
        [SerializeField] private UIScreen StartScreen;
        [SerializeField] private UnityEvent onSwitchedScreenEvent;
        [SerializeField] private Image faderImage;

        [Range(0, 5f)]
        [SerializeField] private float faderInDuration = 1f;
        [Range(0, 5f)]
        [SerializeField] private float faderOutDuration = 1f;
        #endregion

        private UIScreen[] screens = new UIScreen[0];
        private UIScreen currentScreen;
        public UIScreen CurrentScreen => currentScreen;
        private UIScreen previousScreen;
        private void Start()
        {
            screens = GetComponentsInChildren<UIScreen>(false);
            SetAllScreenActive();
            if (StartScreen)
            {
                SwitchScreen(StartScreen);
            }
            if (faderImage)
            {
                faderImage.gameObject.SetActive(true);
            }

            FadeIn();
        }

        public void SwitchScreen(UIScreen screen)
        {
            if (!screen)
            {
                return;
            }

            if (currentScreen)
            {
                currentScreen.CloseScreen();
                previousScreen = currentScreen;
            }

            currentScreen = screen;
            currentScreen.ShowScreen();
            onSwitchedScreenEvent?.Invoke();
        }

        private void SetAllScreenActive()
        {
            foreach (var screen in screens)
            {
                screen.gameObject.SetActive(true);
            }
        }

        private void FadeIn()
        {
            if (!faderImage)
            {
                return;
            }

            faderImage.CrossFadeAlpha(0f, faderInDuration, false);
        }

        private void FadeOut()
        {
            if (!faderImage)
            {
                return;
            }

            faderImage.CrossFadeAlpha(1f, faderOutDuration, false);
        }

        public void ExitFromApplication()
        {
            Application.Quit();
        }

        public void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
