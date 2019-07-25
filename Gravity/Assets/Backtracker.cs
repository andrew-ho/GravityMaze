using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backtracker : MonoBehaviour
{
    public GameObject player;

    public GameObject[] grid;
    public Stack<GameObject> stack;
    int wall;
    public GameObject currentCell;
    List<GameObject> visited;

    int wallMask = 1 << 10;
    int floorMask = 1 << 9;
    // Start is called before the first frame update
    void Start()
    {
        currentCell.GetComponent<MeshRenderer>().material.color = Color.yellow;
        
        visited = new List<GameObject>();
        visited.Add(currentCell);
        stack = new Stack<GameObject>();
        while (visited.Count != grid.Length)
        {
            Debug.Log("working");
            List<GameObject> neighbors = CheckNeighbors(currentCell);
            if (neighbors.Count != 0)
            {
                GameObject nextCell = neighbors[Random.Range(0, neighbors.Count)];
                KillWall(nextCell);
                stack.Push(currentCell);
                visited.Add(nextCell);
                currentCell = nextCell;
            }
            else if (stack.Count != 0)
            {
                currentCell = stack.Pop();
            }
        }
        currentCell.GetComponent<MeshRenderer>().material.color = Color.red;
        Instantiate(player, currentCell.transform.position + Vector3.up * .5f, Quaternion.identity);
        Debug.Log("done");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void KillWall(GameObject nextCell)
    {
        Vector3 nextPos = currentCell.transform.position - nextCell.transform.position;
        Debug.Log(nextPos);
        if (nextPos == new Vector3(0,0,1))
        {
            Destroy(currentCell.GetComponent<Wall>().walls[3]);
            Destroy(nextCell.GetComponent<Wall>().walls[2]);
            //Destroy(currentCell.transform.GetChild(3).gameObject);
            //Destroy(nextCell.transform.GetChild(2).gameObject);
        }
        else if (nextPos == new Vector3(0, 0, -1))
        {
            Destroy(currentCell.GetComponent<Wall>().walls[2]);
            Destroy(nextCell.GetComponent<Wall>().walls[3]);
            //Destroy(currentCell.transform.GetChild(2).gameObject);
            //Destroy(nextCell.transform.GetChild(3).gameObject);
        }
        else if (nextPos == new Vector3(1, 0, 0))
        {
            Destroy(currentCell.transform.GetChild(1).gameObject);
            Destroy(nextCell.transform.GetChild(0).gameObject);
        }
        else if (nextPos == new Vector3(-1, 0, 0))
        {
            //Destroy(currentCell.GetComponent<Wall>().walls[0]);
            //Destroy(nextCell.GetComponent<Wall>().walls[1]);
            Destroy(currentCell.transform.GetChild(0).gameObject);
            Destroy(nextCell.transform.GetChild(1).gameObject);
        }
    }
    public List<GameObject> CheckNeighbors(GameObject currentCell)
    {
        RaycastHit hit;
        List<GameObject> neighbors = new List<GameObject>();

        if (Physics.Raycast(currentCell.transform.position, Vector3.right, out hit, 1, ~wallMask))
        {
            GameObject hitCell = hit.transform.gameObject;
            if (!visited.Contains(hitCell))
            {
                neighbors.Add(hitCell);
            }
        }
        if (Physics.Raycast(currentCell.transform.position, -Vector3.right, out hit, 1, ~wallMask))
        {
            GameObject hitCell = hit.transform.gameObject;
            if (!visited.Contains(hitCell))
            {
                neighbors.Add(hitCell);
            }
        }
        if (Physics.Raycast(currentCell.transform.position, Vector3.forward, out hit, 1, ~wallMask))
        {
            GameObject hitCell = hit.transform.gameObject;
            if (!visited.Contains(hitCell))
            {
                neighbors.Add(hitCell);
            }
        }
        if (Physics.Raycast(currentCell.transform.position, -Vector3.forward, out hit, 1, ~wallMask))
        {
            GameObject hitCell = hit.transform.gameObject;
            if (!visited.Contains(hitCell))
            {
                neighbors.Add(hitCell);
            }
        }
        return neighbors;
    }
}
