using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraTarget; // Ссылка на CameraTarget

    private Vector3 offset; // Отступ между камерой и Target

    void Start()
    {
        // Вычисляем начальный отступ
        offset = transform.position - cameraTarget.position;
    }

    void LateUpdate()
    {
        // Фиксируем позицию камеры относительно Target
        transform.position = cameraTarget.position + offset;
    }
}