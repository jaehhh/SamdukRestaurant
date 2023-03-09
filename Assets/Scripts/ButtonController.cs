using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{
    public class ExtendedEvent : UnityEvent { }
    public ExtendedEvent extendedEvent;

    [SerializeField] private MenuCameraController menuCameraController;
    [HideInInspector] public AudioSource audioSource;

    private void Awake()
    {
        extendedEvent = new ExtendedEvent();
        audioSource = GetComponent<AudioSource>();
    }

    public void SceneChange(string sceneName)
    {
        audioSource.Play();
        StartCoroutine(WaitForStart(sceneName));
    }

    private IEnumerator WaitForStart(string sceneName)
    {
        Time.timeScale = 1;

        yield return new WaitForSeconds(0.2f);

        SceneManager.LoadScene(sceneName);
    }

    public void Popup(GameObject target)
    {
        audioSource.Play();

        target.SetActive(true);
    }

    public void PopupStopTime(GameObject target)
    {
        Time.timeScale = 0;

        audioSource.Play();

        target.SetActive(true);
    }

    public void Close(GameObject target)
    {
        audioSource.Play();

        target.SetActive(false);

        extendedEvent.Invoke();
        extendedEvent.RemoveAllListeners();
    }

    public void MoveMenuCamera(int value)
    {
        menuCameraController.MoveWithMouse(value);
    }

    public void MultiPurposeButton(UnityAction action)
    {
        extendedEvent.AddListener(action);
        extendedEvent.Invoke();
        extendedEvent.RemoveAllListeners();
    }

    public void Resume(GameObject target)
    {
        Time.timeScale = 1;

        audioSource.Play();

        target.SetActive(false);
    }
}
