using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMover : MonoBehaviour
{
    public float speed = 5.0f;
    public Transform movePoint;

    public LayerMask _playArea;

    public Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);

        //Vector3Int cell = tilemap.WorldToCell(movePoint.position);


        //transform.position = cell;

        Debug.Log(tilemap.size);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) == 1)
            {
                if (Input.GetAxis("Horizontal") > 0)
                {
                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(1 + tilemap.cellGap.x, 0, 0), 0.2f, _playArea))
                    {
                        movePoint.position += new Vector3(1 + tilemap.cellGap.x, 0, 0);
                    }
                }
                else
                {
                    if (Physics2D.OverlapCircle(movePoint.position - new Vector3(1 + tilemap.cellGap.x, 0, 0), 0.2f, _playArea))
                    {
                        movePoint.position -= new Vector3(1 + tilemap.cellGap.x, 0, 0);
                    }
                }

            }
            else if (Mathf.Abs(Input.GetAxis("Vertical")) == 1)
            {
                if (Mathf.Abs(Input.GetAxis("Vertical")) == 1)
                {
                    if (Input.GetAxis("Vertical") > 0)
                    {
                        if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0, 1 + tilemap.cellGap.y, 0), 0.2f, _playArea))
                        {
                            movePoint.position += new Vector3(0, 1 + tilemap.cellGap.y, 0);
                        }
                    }
                    else
                    {
                        if (Physics2D.OverlapCircle(movePoint.position - new Vector3(0, 1 + tilemap.cellGap.y, 0), 0.2f, _playArea))
                        {
                            movePoint.position -= new Vector3(0, 1 + tilemap.cellGap.x, 0);
                        }
                    }
                }
            }

        }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
    }
}
