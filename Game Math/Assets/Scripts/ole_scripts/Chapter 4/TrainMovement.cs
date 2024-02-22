using Unity.VisualScripting;
using UnityEngine;

public class TrainShouldMove : MonoBehaviour
{
    [SerializeField] private Object controllerObject;
    [SerializeField] private int speed;
    private ControllerChapter4 controller;

    private void Start()
    {
        controller = controllerObject.GetComponent<ControllerChapter4>();
    }

    void Update()
    {
        if (transform.localPosition.x < controller.trainBorder + 2 * transform.localScale.x)
        {
            transform.Translate(Vector3.right * (speed * Time.deltaTime));
        }
    }
}