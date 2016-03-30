using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player_NonPhysics2D : MonoBehaviour {

    // Inspector Properties------------------------
    public float Speed = 15.0f; // charactor speed
    public Sprite[] Run; // sprites for running charactor 
    public Sprite[] Jump; // sprites for jumping charactor 

    float jumpVy; // vertical velocity of char
    int animIndex; // animation play index
    bool hasGoal; // checking a charactor has made a goal

	void Start () {

        // initialize
        jumpVy = 0.0f;
        animIndex = 0;
        hasGoal = false;

	}

    // player's collision has overlapped with a collision of another game object.
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Stage_Gate")
        {
            hasGoal = true;
            return;
        }

        // Reload the stage and initialize if a collided object is not the gate.

        //deprecate! 
        //Application.LoadLevel(Application.loadedLevelName);
        SceneManager.LoadScene("SceneA");
    }
	
	// Update is called once per frame
	void Update () {
	
        if (hasGoal)
        {
            return;
        }

        float height = transform.position.y + jumpVy;

        if (height <= 0.0f)
        {
            height = 0.0f;
            jumpVy = 0.0f;

            //if (Input.GetMouseButtonDown("Fire1"))
            if (Input.GetMouseButtonDown(0))
            {
                jumpVy = +1.3f;
                GetComponent<SpriteRenderer>().sprite = Jump[0];
            }
            else
            {
                animIndex++;
                if (animIndex >= Run.Length)
                {
                    animIndex = 0;
                }

                GetComponent<SpriteRenderer>().sprite = Run[animIndex];
            }
        }
        else
        {
            jumpVy -= 0.2f;
            // jumpVy -= 6.0f * Time.deltaTime;
        }

        transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime, height, 0.0f);

        //transform.Translate(Speed * Time.deltaTime, jumpVy, 0.0f);
        //transform.position += new Vector3(Speed * Time.deltaTime, jumpVy, 0.0f);

        GameObject goCam = GameObject.Find("Main Camera");
        goCam.transform.Translate(Speed * Time.deltaTime, 0.0f, 0.0f);
	}

    void OnGUI()
    {
        GUI.TextField(new Rect(10, 10, 300, 60), "[Unity2D Sample 3-1 A] \n마우스 왼쪽 버튼을 누르면 점프!");
        if (GUI.Button(new Rect(10,80,100,20), "Reset"))
        {
            SceneManager.LoadScene("SceneA");
        }
    }
}
