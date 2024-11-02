using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;
    private bool musicStoppedForever = false; // Флаг, чтобы музыка не включалась снова после сцены "Not End"

    private void Awake()
    {
        // Проверяем, существует ли уже экземпляр MusicManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем объект между сценами
            audioSource = GetComponent<AudioSource>(); // Получаем AudioSource
            SceneManager.sceneLoaded += OnSceneLoaded; // Подписываемся на событие загрузки сцены
        }
        else
        {
            Destroy(gameObject); // Удаляем дублирующий объект
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Если флаг установлен, музыка должна быть выключена и не включаться снова
        if (musicStoppedForever) 
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop(); // Останавливаем музыку, если она почему-то играет
            }
            return; // Выходим из метода, чтобы ничего не делать дальше
        }

        // Проверяем, нужно ли остановить музыку на сцене с именем "Not End"
        if (scene.name == "Not End")
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop(); // Останавливаем музыку
                musicStoppedForever = true; // Устанавливаем флаг, чтобы музыка больше не включалась
            }
        }
    }

    private void OnDestroy()
    {
        // Отписываемся от события, когда объект уничтожается
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}