using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] pauseMenuObjectsForActivate;
    [SerializeField] private GameObject[] pauseMenuObjectsForDeactivate;
    private bool _paused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_paused) Pause();
            else if (_paused) Play();
        }
    }

    public void Pause()
    {
        _paused = true;
        Time.timeScale = 0;
        SetActivePauseObjects(_paused);
    }

    public void Play()
    {
        _paused = false;
        Time.timeScale = 1;
        SetActivePauseObjects(_paused);
        
        foreach (GameObject obj in pauseMenuObjectsForDeactivate)
        {
            obj.SetActive(_paused);
        }
    }

    private void SetActivePauseObjects(bool paused)
    {
        foreach (GameObject obj in pauseMenuObjectsForActivate)
        {
            obj.SetActive(paused);
        }
    }
}
