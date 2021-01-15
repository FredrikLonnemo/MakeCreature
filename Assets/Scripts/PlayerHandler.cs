using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHandler: MonoBehaviour
{
    public bool startGame = false;

    public GameObject Head;
    public GameObject Body;
    public GameObject Fleg;
    public GameObject Bleg;
    public GameObject Tail;

    public CharacterController2D CharController;
    //public CapsuleCollider2D collider;

    private SpinScript[] spinners = new SpinScript[2];

    public float spinSpeed = -1f;

    public float verticalBound = 1.5f;
    public float horizontalBound = 3;
    
    public float moveMult = 3;
    public float gravity = 4;

    public GameObject legError;
    public GameObject winScreen;

    // Start is called before the first frame update
    void Start()
    {
        startGame = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene(0);
        }

        if (startGame)
        {
            bool jump = Input.GetKeyDown(KeyCode.Space) ? true : false;
            CharController.Move(Input.GetAxis("Horizontal") * moveMult, false, jump);

            if (Input.GetAxis("Horizontal") > 0.1f || Input.GetAxis("Horizontal") < -0.1f)
            {
                for (int i = 0; i < spinners.Length; i++)
                {
                    spinners[i].ManualSpin();
                }
            }
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

                    Vector3 spin = new Vector3(0, 0, spinSpeed);

                    spinners[0] = Fleg.AddComponent<SpinScript>();
                    spinners[1] = Bleg.AddComponent<SpinScript>();
                    spinners[0].spin = spin;
                    spinners[1].spin = spin;

                    Head.GetComponentInChildren<PartMover>().enabled = false;
                    Head.GetComponentInChildren<PolygonCollider2D>().enabled = false;

                    Body.GetComponentInChildren<PartMover>().enabled = false;
                    Body.GetComponentInChildren<PolygonCollider2D>().enabled = false;
                    Body.GetComponentInChildren<CapsuleCollider2D>().enabled = true;

                    Fleg.GetComponentInChildren<PartMover>().enabled = false;
                    Fleg.GetComponentInChildren<PolygonCollider2D>().enabled = false;

                    Bleg.GetComponentInChildren<PartMover>().enabled = false;
                    Bleg.GetComponentInChildren<PolygonCollider2D>().enabled = false;

                    Tail.GetComponentInChildren<PartMover>().enabled = false;
                    Tail.GetComponentInChildren<PolygonCollider2D>().enabled = false;
                    Debug.Log("game started");

                    Camera.main.orthographicSize = 15;

                    //gameObject.AddComponent<Rigidbody2D>();

                    Rigidbody2D rigid = gameObject.AddComponent<Rigidbody2D>();
                    rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
                    rigid.gravityScale = gravity;

                    //collider.enabled = true;

                    CharController.enabled = true;

                    CharController.UpdateRigid(rigid);
                }
                else
                {
                    legError.SetActive(true);
                    StartCoroutine(DisableObjectAfterTime(legError, 3));
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Finish"))
        {
            winScreen.SetActive(true);
        }
    }

    private IEnumerator DisableObjectAfterTime(GameObject disableobject, float seconds)
    {

        yield return new WaitForSeconds(seconds);
        disableobject.SetActive(false);
    }
}
