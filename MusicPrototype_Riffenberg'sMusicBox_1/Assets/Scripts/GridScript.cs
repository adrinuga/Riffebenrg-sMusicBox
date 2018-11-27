using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour {

	public int m_Size_x, m_Size_y;
	public Node[,] m_Grid;

	public int m_Node_size;
	// Use this for initialization
	private void Awake()
	{
        GenerateGrid();
    }

	public void GenerateGrid()
	{
		m_Grid = new Node[m_Size_x, m_Size_y];

		for (int i = 0; i < m_Size_x; i++)
		{
			for (int o = 0; o < m_Size_y; o++)
			{
				Vector3 nodePosition = new Vector3(m_Node_size * 0.5f + i * m_Node_size, m_Node_size * 0.5f + o * m_Node_size, 0);
				Vector3 worldNodePosition = transform.position + nodePosition;

				Collider[] colliders = Physics.OverlapSphere(worldNodePosition, m_Node_size * 0.5f);


				bool l_IsTransitable = true;

				for (int k = 0; k < colliders.Length; k++)
				{
					if (colliders[k].tag == "Wall_Puzzle")
					{
						l_IsTransitable = false;
					}
				}
				m_Grid[i, o] = new Node(i, o, m_Node_size, worldNodePosition, l_IsTransitable);
			}

		}
	}
	private void OnDrawGizmosSelected()
	{
		if (m_Grid != null)
		{
			Gizmos.color = Color.green;
			for (int i = 0; i < m_Grid.GetLength(0); i++)
			{
				for (int o = 0; o < m_Grid.GetLength(1); o++)
				{
					Vector3 scale = new Vector3(m_Node_size, m_Node_size, m_Node_size);

					Gizmos.DrawWireCube(m_Grid[i, o].worldPosition, scale);

					if (m_Grid[i, o].isTransitable == false)
					{
						Gizmos.color = Color.red;
						Gizmos.DrawCube(m_Grid[i, o].worldPosition, scale);
						Gizmos.color = Color.green;
					}


				}

			}
		}
	
	}
	public Node GetNodeContainingPosition(Vector3 worldPosition)
	{
		Vector3 localPosition = worldPosition - transform.position;

		int x = Mathf.FloorToInt(localPosition.x / m_Node_size);
		int y = Mathf.FloorToInt(localPosition.y / m_Node_size);

		if (x < m_Size_x &&
			x >= 0 &&
			y < m_Size_y &&
			y >= 0)
		{
			return m_Grid[x, y];
		}
		else 
		{
			return null;
		}

	}
	public Node GetNode(int x, int y)
	{
		if (x < 0 || y < 0 || x >= m_Size_x || y >= m_Size_y)
		{
			Debug.LogWarning("Se ha pedido un nodo no valido en la posición :(" + x + " , "+y + ")");
			return null;
		}
		return m_Grid[x, y];
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
				Node neightbour = GetNode(myNode.gridPositionX+i, myNode.gridPositionY + j);

				if (neightbour != null)
				{
					Neighbours.Add(neightbour);
				}
                
			}
		}
        
		return Neighbours;
	}
    public void ResetVisitedNodes()
    {
        for (int i = 0; i < m_Grid.GetLength(0); i++)
        {
            for (int o = 0; o < m_Grid.GetLength(1); o++)
            {
                m_Grid[i, o].hasBeenVisited = false;
            }
        }
    }
}
public class Node
{
    public Node parent;
	public int gridPositionX, gridPositionY, nodeSize;
	public Vector3 worldPosition;
	public bool isTransitable = true;
    public bool hasBeenVisited = false;

    public Node() { }


	public Node(int gridPositionX, int _gridPositionY,int _nodeSize, Vector3 _nPos, bool _transitable)
	{
		this.gridPositionX = gridPositionX;
		this.gridPositionY = _gridPositionY;
		nodeSize = _nodeSize;
		worldPosition = _nPos;
		isTransitable = _transitable;
		//Debug.Log(gridPositionX + "," + gridPositionY);
	}
}
