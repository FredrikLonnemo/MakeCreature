using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
    public bool startGame = false;

    public GameObject Head;
    public GameObject Body;
    public GameObject Fleg;
    public GameObject Bleg;
    public GameObject Tail;

    public float spinSpeed = -1f;

    public float verticalBound = 1.5f;
    public float horizontalBound = 3;

    public GameObject legError;

    // Start is called before the first frame update
    void Start()
    {
        startGame = false;
    }

    private void Update()
    {
        if (startGame)
        {

        }
        else
        {
            if (Input.GetButtonDown("Start"))
            {
                if ((  Fleg.transform.position.x > -horizontalBound
                    && Fleg.transform.position.x <  horizontalBound
                    && Fleg.transform.position.y <  verticalBound
                    && Fleg.transform.position.y > -verticalBound)
                    &&
                    (Bleg.transform.position.x > -horizontalBound
                    && Bleg.transform.position.x < horizontalBound
                    && Bleg.transform.position.y < verticalBound
                    && Bleg.transform.position.y > -verticalBound)
                    )
                {
                    startGame = true;
                    Head.GetComponentInChildren<PartMover>().enabled = false;
                    Body.GetComponentInChildren<PartMover>().enabled = false;
                    Fleg.GetComponentInChildren<PartMover>().enabled = false;
                    Bleg.GetComponentInChildren<PartMover>().enabled = false;
                    Tail.GetComponentInChildren<PartMover>().enabled = false;
                    Debug.Log("game started");

                    Camera.main.orthographicSize = 15;

                    gameObject.AddComponent<Rigidbody2D>();

                    Vector3 spin = new Vector3(0, 0, spinSpeed);
                    Fleg.AddComponent<SpinScript>().spin = spin;
                    Bleg.AddComponent<SpinScript>().spin = spin;
                }
                else
                {
                    legError.SetActive(true);
                    StartCoroutine(DisableObjectAfterTime(legError, 3));
                }
            }
        }
    }

    private IEnumerator DisableObjectAfterTime(GameObject disableobject, float seconds)
    {

        yield return new WaitForSeconds(seconds);
        disableobject.SetActive(false);
    }
}
