using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour {

	public int size_x,size_y;
	public Node[,] grid;

	public int node_size;
	// Use this for initialization
	private void Awake()
	{
		//GenerateGrid();
	}

	void Start () 
	{
		GenerateGrid();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        
	}
	public void GenerateGrid()
	{
		grid = new Node[size_x, size_y];

		for (int i = 0; i < size_x; i++)
		{
			for (int o = 0; o < size_y; o++)
			{
				Vector3 nodePosition = new Vector3(node_size * 0.5f + i * node_size, node_size * 0.5f + o * node_size, 0);
				Vector3 worldNodePosition = transform.position + nodePosition;

				Collider[] colliders = Physics.OverlapSphere(worldNodePosition, node_size * 0.5f);


				bool isTransitable = true;

				for (int k = 0; k < colliders.Length; k++)
				{
					if (colliders[k].tag == "Piece")
					{
						isTransitable = false;
					}
				}
				grid[i, o] = new Node(i, o, node_size, worldNodePosition, isTransitable);
			}

		}
	}
	private void OnDrawGizmosSelected()
	{
		if (grid != null)
		{
			Gizmos.color = Color.green;
			for (int i = 0; i < grid.GetLength(0); i++)
			{
				for (int o = 0; o < grid.GetLength(1); o++)
				{
					Vector3 scale = new Vector3(node_size, node_size, node_size);

					Gizmos.DrawWireCube(grid[i, o]._worldPosition, scale);

					if (grid[i, o].isTransitable == false)
					{
						Gizmos.color = Color.red;
						Gizmos.DrawCube(grid[i, o]._worldPosition, scale);
						Gizmos.color = Color.green;
					}


				}

			}
		}
	
	}
	public Node getNodeContainingPosition(Vector3 worldPosition)
	{
		Vector3 localPosition = worldPosition - transform.position;

		int x = Mathf.FloorToInt(localPosition.x / node_size);
		int y = Mathf.FloorToInt(localPosition.y / node_size);

		if (x < size_x &&
			x >= 0 &&
			y < size_y &&
			y >= 0)
		{
			return grid[x, y];
		}
		else 
		{
			return null;
		}

	}
	public Node GetNode(int x, int y)
	{
		if (x < 0 || y < 0 || x > size_x || y > size_y)
		{
			Debug.LogWarning("Se ha pedido un nodo no valido en la posición :(" + x + " , "+y + ")");
			return null;
		}
		return grid[x, y];
	}
	public List<Node> GetNeighbourNodes(Node myNode,bool eightneightbours)
	{
		List<Node> Neighbours = new List<Node>();
		for (int i = -1; i <= 1; i++)
		{
			for (int j = -1; j <= 1; j++)
			{
				if (!eightneightbours)
				{
					if (Mathf.Abs(i)==Mathf.Abs(j))
					{
						continue;
					}

				}
				else if (i == 0 && j == 0)
				{
					continue;
				}
				Node neightbour = GetNode(myNode._gridPositionX+i, myNode._gridPositionY + j);

				if (neightbour != null)
				{
					Neighbours.Add(neightbour);
				}
                
			}
		}
        
		return Neighbours;
	}

}
public class Node
{
    public float F, H, G;
    public Node parent;
	public int _gridPositionX, _gridPositionY,_nodeSize;
	public Vector3 _worldPosition;
	public bool isTransitable = true;

	public Node()
	{
		
	}

	public Node(int gridPositionX, int gridPositionY,int nSize, Vector3 nPos, bool transitable)
	{
		_gridPositionX = gridPositionX;
		_gridPositionY = gridPositionY;
		_nodeSize = nSize;
		_worldPosition = nPos;
		isTransitable = transitable;
		//Debug.Log(gridPositionX + "," + gridPositionY);
	}
}
