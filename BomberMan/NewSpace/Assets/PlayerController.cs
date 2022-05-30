using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    private float thrust = 30.0f;
    private Rigidbody2D rb2D;

    public static bool bombAvailable = true;
    public GameObject bomb;
    public GameObject blast;

    public Tilemap tilemap;

    public Tile breakableWall;
    public Tile unbreakableWall;

    public GameObject canvas;
    public UnityEngine.UI.Text msg; 


    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb2D.AddForce(transform.up * thrust);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb2D.AddForce(-transform.right * thrust);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb2D.AddForce(-transform.up * thrust);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb2D.AddForce(transform.right * thrust);
        }

        if (Input.GetKeyDown(KeyCode.Space) && bombAvailable)
        {
            bombAvailable = false;
            Vector3Int cell = tilemap.WorldToCell(transform.position);
            Vector3 cellCenter = tilemap.GetCellCenterWorld(cell);
            GameObject lBomb= Instantiate(bomb, cellCenter, Quaternion.identity) as GameObject;
            Physics2D.IgnoreCollision(lBomb.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            rb2D.velocity = Vector2.zero;
            rb2D.angularVelocity = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void BombBlast(Vector2 worldPos)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos);

        ExplodeCell(originCell);
        ExplodeCell(originCell + new Vector3Int(1, 0, 0));
        ExplodeCell(originCell + new Vector3Int(0, 1, 0));
        ExplodeCell(originCell + new Vector3Int(-1, 0, 0));
        ExplodeCell(originCell + new Vector3Int(0, -1, 0));
        ExplodeCell(originCell + new Vector3Int(1, 1, 0));
        ExplodeCell(originCell + new Vector3Int(1, -1, 0));
        ExplodeCell(originCell + new Vector3Int(-1, 1, 0));
        ExplodeCell(originCell + new Vector3Int(-1, -1, 0));
    }

    bool ExplodeCell(Vector3Int cell)
    {
        Tile tile = tilemap.GetTile<Tile>(cell);

        if (tile == unbreakableWall)
        {
            return false;
        }

        if (tile == breakableWall)
        {
            tilemap.SetTile(cell, null);
        }

        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        Instantiate(blast, pos, Quaternion.identity);

        return true;
    }

    int enemyCount = 0;

    public void Counter()
    {
        enemyCount++;
        if(enemyCount >= 5)
        {
            canvas.SetActive(true);
            msg.text = "You Win !";
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Enemy") || col.gameObject.name.Contains("explosion"))
        {
            canvas.SetActive(true);
            msg.text = "You Lose !";
            Time.timeScale = 0;
        }

    }

    public void RestartTheGame()
    {
        bombAvailable = true;
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
