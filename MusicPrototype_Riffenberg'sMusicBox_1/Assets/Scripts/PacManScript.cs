using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManScript : MonoBehaviour {

    public AudioSource chomp, chompFruit;
    public Node currentNode;
	public Vector3 finalPosition,spawnPosition;
	public GridScript GameGrid;
	public float speed;
    public Vector2 direction;


	// Use this for initialization
	void Start () 
	{
        spawnPosition = transform.position;
        currentNode = GameGrid.getNodeContainingPosition(transform.position);
        //transform.position = new Vector3(currentNode._gridPositionX + (currentNode._nodeSize / 2), currentNode._gridPositionY + (currentNode._nodeSize / 2), 0);
        transform.position = currentNode._worldPosition;
        finalPosition = currentNode._worldPosition;


    }

    // Update is called once per frame
    void Update () 
	{
        //if (GameManagerScript.gameState == "Playing")
        //{
            currentNode = GameGrid.getNodeContainingPosition(transform.position);
            //Debug.Log(currentNode._gridPositionX + "  " + currentNode._gridPositionY);

            if(!chomp.isPlaying){ chomp.Play(); }


            if (transform.position == currentNode._worldPosition)
            {
                if (Input.GetAxisRaw("Horizontal") != 0) { direction.x = Input.GetAxisRaw("Horizontal"); }

                if (direction.x > 0)
                {
                    Node nextNode = GameGrid.GetNode(currentNode._gridPositionX + 1, currentNode._gridPositionY);

                    if (nextNode.isTransitable)
                    {
                        finalPosition = nextNode._worldPosition;
                        direction.y = 0;
                    }
                    else
                    {
                        direction.x = 0;
                    }
                }
                if (direction.x < 0)
                {
                    Node nextNode = GameGrid.GetNode(currentNode._gridPositionX - 1, currentNode._gridPositionY);
                    if (nextNode.isTransitable)
                    {
                        finalPosition = nextNode._worldPosition;
                        direction.y = 0;
                    }
                    else
                    {
                        direction.x = 0;
                    }
                }


                if (Input.GetAxisRaw("Vertical") != 0) { direction.y = Input.GetAxisRaw("Vertical"); }

                if (direction.y > 0)
                {
                    Node nextNode = GameGrid.GetNode(currentNode._gridPositionX, currentNode._gridPositionY + 1);
                    if (nextNode.isTransitable)
                    {
                        finalPosition = nextNode._worldPosition;
                        direction.x = 0;
                    }
                    else
                    {
                        direction.y = 0;
                    }
                }



                if (direction.y < 0)
                {
                    Node nextNode = GameGrid.GetNode(currentNode._gridPositionX, currentNode._gridPositionY - 1);
                    if (nextNode.isTransitable)
                    {
                        finalPosition = nextNode._worldPosition;
                        direction.x = 0;
                    }
                    else
                    {
                        direction.y = 0;
                    }
                }
            }
            //Debug.Log(direction.x + " " + direction.y);
            transform.position = Vector3.MoveTowards(transform.position, finalPosition, speed * Time.deltaTime);

        //}

    }
    public void ResetPosition()
    {
        currentNode = GameGrid.getNodeContainingPosition(spawnPosition);
        transform.position = currentNode._worldPosition;
        finalPosition = currentNode._worldPosition;
    }
    void OnTriggerEnter(Collider col)
    {

    }

}
