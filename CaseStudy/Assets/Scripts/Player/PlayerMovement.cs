using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float rotationSpeed=5f;
    private Vector2 firstTouchPosition;
    private Vector3 firstTransformRotation;

    void Start()
    {
        firstTransformRotation=transform.localEulerAngles;
    }

    void Update()
    {
       if(GameManager.Instance.CurrentGameState==GameState.InGame)
       {

            transform.Translate(Vector3.forward*movementSpeed*Time.deltaTime,Space.Self);

            if(Input.touchCount>0)
            {
                firstTransformRotation=transform.localEulerAngles;
                Touch touch= Input.GetTouch(0);
                if(touch.phase==TouchPhase.Began)
                {
                    firstTouchPosition=touch.deltaPosition;
                    Debug.Log(firstTouchPosition);
                }
                if(touch.phase==TouchPhase.Moved)
                {
                    Vector2 currentTouchPosition=touch.deltaPosition;
                    float delta=currentTouchPosition.x-firstTouchPosition.x;
                    Quaternion targetRotation=Quaternion.Euler(0f,delta+firstTransformRotation.y,0f);
                    transform.localRotation=Quaternion.Lerp(transform.localRotation,targetRotation,rotationSpeed*Time.deltaTime);

                }
            }
       }
    }

}
