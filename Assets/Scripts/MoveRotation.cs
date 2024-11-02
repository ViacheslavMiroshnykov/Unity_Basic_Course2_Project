using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class RotateObject : MonoBehaviour
{
    [SerializeField] Vector3 rotationAngle; // Угол вращения, до которого поворачиваем
    [SerializeField] float rotationSpeed; // Скорость вращения
    [SerializeField] [Range(0, 1)] float rotationProgress; // Прогресс вращения

    Quaternion startRotation; // Начальное вращение

    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation; // Сохраняем начальное вращение
    }

    // Update is called once per frame
    void Update()
    {   
        Rotate();
    }

    private void Rotate()
    {
        rotationProgress = Mathf.PingPong(Time.time * rotationSpeed, 1);
        Quaternion offsetRotation = Quaternion.Euler(rotationAngle * rotationProgress); // Вычисляем смещение вращения
        transform.rotation = startRotation * offsetRotation; // Устанавливаем вращение как начальное + смещение
    }
}