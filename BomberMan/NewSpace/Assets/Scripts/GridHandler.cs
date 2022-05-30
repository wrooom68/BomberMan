using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GridHandler : MonoBehaviour
{
    public Tilemap tilemap;
    public List<Vector3Int> _tileBasesPos = new List<Vector3Int>();

    public List<Vector3Int> _randomItemPos = new List<Vector3Int>();

    public Tile _base;

    public Tile _item;

    public Tile _red;

    public Tile _yellow;

    public Tile _green;

    public int gridHeight;
    public int gridWidth;

    public int _itemNumber;

    public int _itemDistance;

    public int _life;

    public Transform _selected;

    public LayerMask _playArea;


    // Start is called before the first frame update
    void Start()
    {
        GetAllPos();
        _selected.position = Vector3.zero;
    }

    void GetAllPos()
    {
        for(int i =0; i < gridWidth; i++)
        {
            for(int j=0;j < gridHeight; j++)
            {
                Vector3Int localPos = new Vector3Int(i, j, 0);
                tilemap.SetTile(localPos, _base);
                _tileBasesPos.Add(localPos);
            }
        }

        for(int i = 0; i < _itemNumber; i++)
        {
            int _randomNumber = Random.Range(0, _tileBasesPos.Count);
            _randomItemPos.Add(_tileBasesPos[_randomNumber]);
            _tileBasesPos.Remove(_tileBasesPos[_randomNumber]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(_life > 0)
            {
                CheckSelectedPos(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_life > 0)
            {
                CheckSelectedPos(_selected.position);
            }
        }
      
    }


    void CheckSelectedPos(Vector3 pos)
    {
        if (pos.x > (gridWidth - 1) || pos.x < 0 || pos.y > (gridHeight - 1) || pos.y < 0)
            return;

        Vector3Int cell = tilemap.WorldToCell(new Vector3(pos.x,pos.y,0));

        Debug.Log(cell);

        if (_randomItemPos.Exists(x => x == cell))
        {
            Debug.Log("true");
            tilemap.SetTile(cell, _red);
            _randomItemPos.Remove(cell);
            _life--;
        }
        else if(_tileBasesPos.Exists(x => x == cell))
        {
            if (CheckIsNear(cell))
            {
                tilemap.SetTile(cell, _yellow);
                _tileBasesPos.Remove(cell);

            }
            else
            {
                tilemap.SetTile(cell, _green);
                _tileBasesPos.Remove(cell);
            }

            _life--;
        }
    }

    bool CheckIsNear(Vector3Int cell)
    {
        for(int i=0; i < _randomItemPos.Count; i++)
        {
            if (Vector3Int.Distance(cell, _randomItemPos[i]) <= _itemDistance)
            {
                return true;
            }
        }

        return false;
    }
}
