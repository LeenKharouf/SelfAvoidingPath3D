
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class SelfAvoidingWalk : MonoBehaviour
// {
//     public GameObject cubePrefab; // Assign this in the Unity Editor
//     public int gridSize = 10;     // Set the size of the grid
//     private bool[,,] visited;     // 3D array to keep track of visited cubes
//     private Stack<Vector3> pathStack = new Stack<Vector3>(); // Stack to track the path
//     private List<Color> colors = new List<Color> { Color.red, Color.green, Color.blue, Color.yellow };

//     void Start()
//     {
//         InitializeGrid();
//         StartCoroutine(Walk());
//     }

//     void InitializeGrid()
//     {
//         visited = new bool[gridSize, gridSize, gridSize];
//         Vector3 startPosition = new Vector3(gridSize / 2, gridSize / 2, gridSize / 2);
//         pathStack.Push(startPosition);
//         visited[(int)startPosition.x, (int)startPosition.y, (int)startPosition.z] = true;
//         InstantiateCube(startPosition);
//     }

//     IEnumerator Walk()
//     {
//         while (pathStack.Count > 0)
//         {
//             Vector3 current = pathStack.Peek();
//             List<Vector3> possibleMoves = GetPossibleMoves(current);

//             if (possibleMoves.Count > 0)
//             {
//                 Vector3 chosenMove = possibleMoves[Random.Range(0, possibleMoves.Count)];
//                 pathStack.Push(chosenMove);
//                 visited[(int)chosenMove.x, (int)chosenMove.y, (int)chosenMove.z] = true;
//                 InstantiateCube(chosenMove);
//                 yield return new WaitForSeconds(0.1f); // Adjust time for cube instantiation speed
//             }
//             else
//             {
//                 pathStack.Pop(); // Backtrack
//                 ChangeColorsRandomly();
//                 yield return new WaitForSeconds(0.1f);
//             }
//         }
//     }

//     List<Vector3> GetPossibleMoves(Vector3 current)
//     {
//         List<Vector3> moves = new List<Vector3>();
//         Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

//         foreach (Vector3 dir in directions)
//         {
//             Vector3 newPos = current + dir;
//             if (IsInBounds(newPos) && !visited[(int)newPos.x, (int)newPos.y, (int)newPos.z])
//                 moves.Add(newPos);
//         }
//         return moves;
//     }

//     bool IsInBounds(Vector3 position)
//     {
//         return position.x >= 0 && position.x < gridSize && position.y >= 0 && position.y < gridSize && position.z >= 0 && position.z < gridSize;
//     }

//     void InstantiateCube(Vector3 position)
//     {
//         GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
//         cube.GetComponent<Renderer>().material.color = colors[Random.Range(0, colors.Count)];
//         // cameraFollowScript.target = cube.transform;
//     }



//     void ChangeColorsRandomly()
//     {
//         GameObject[] allCubes = GameObject.FindGameObjectsWithTag("Cube");
//         foreach (GameObject cube in allCubes)
//         {
//             Renderer renderer = cube.GetComponent<Renderer>();
//             renderer.material.color = colors[Random.Range(0, colors.Count)];
//         }
//     }
// }


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class SelfAvoidingWalk : MonoBehaviour
// {
//     public GameObject cubePrefab; // Assign this in the Unity Editor
//     public int gridSize = 10;     // Set the size of the grid
//     private int[,,] visited;     // 3D array to keep track of visited positions by agent IDs
//     private List<Agent> agents = new List<Agent>(); // List of agents
//     private List<Color> colors = new List<Color> { Color.red, Color.green, Color.blue, Color.yellow };
//     private Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

//     void Start()
//     {
//         InitializeGrid();
//         StartCoroutine(Walk());
//     }

//     void InitializeGrid()
//     {
//         visited = new int[gridSize, gridSize, gridSize];
//         // Initialize multiple agents at random starting positions
//         for (int i = 0; i < 4; i++)
//         {
//             Vector3 startPos = new Vector3(Random.Range(0, gridSize), Random.Range(0, gridSize), Random.Range(0, gridSize));
//             agents.Add(new Agent(i+1, startPos, colors[i]));
//             visited[(int)startPos.x, (int)startPos.y, (int)startPos.z] = i + 1;
//             InstantiateCube(startPos, colors[i]);
//         }
//     }

//     IEnumerator Walk()
//     {
//         bool allAgentsStuck = false;
//         while (!allAgentsStuck)
//         {
//             allAgentsStuck = true;
//             foreach (Agent agent in agents)
//             {
//                 if (agent.PathStack.Count > 0)
//                 {
//                     Vector3 current = agent.PathStack.Peek();
//                     List<Vector3> possibleMoves = GetPossibleMoves(current, agent.Id);

//                     if (possibleMoves.Count > 0)
//                     {
//                         allAgentsStuck = false;
//                         Vector3 chosenMove = possibleMoves[Random.Range(0, possibleMoves.Count)];
//                         agent.PathStack.Push(chosenMove);
//                         visited[(int)chosenMove.x, (int)chosenMove.y, (int)chosenMove.z] = agent.Id;
//                         InstantiateCube(chosenMove, agent.Color);
//                         yield return new WaitForSeconds(0.1f);
//                     }
//                     else
//                     {
//                         agent.PathStack.Pop(); // Backtrack
//                     }
//                 }
//             }
//         }
//     }

//     List<Vector3> GetPossibleMoves(Vector3 current, int agentId)
//     {
//         List<Vector3> moves = new List<Vector3>();
//         foreach (Vector3 dir in directions)
//         {
//             Vector3 newPos = current + dir;
//             if (IsInBounds(newPos) && visited[(int)newPos.x, (int)newPos.y, (int)newPos.z] == 0)
//                 moves.Add(newPos);
//         }
//         return moves;
//     }

//     bool IsInBounds(Vector3 position)
//     {
//         return position.x >= 0 && position.x < gridSize && position.y >= 0 && position.y < gridSize && position.z >= 0 && position.z < gridSize;
//     }

//     void InstantiateCube(Vector3 position, Color color)
//     {
//         GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
//         cube.GetComponent<Renderer>().material.color = color;
//     }
// }

// public class Agent
// {
//     public Stack<Vector3> PathStack = new Stack<Vector3>();
//     public int Id;
//     public Color Color;

//     public Agent(int id, Vector3 startPosition, Color color)
//     {
//         Id = id;
//         PathStack.Push(startPosition);
//         Color = color;
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfAvoidingWalk : MonoBehaviour
{
    public GameObject cubePrefab; // Assign this in the Unity Editor
    public int gridSize = 10;     // Set the size of the grid
    private int[,,] visited;      // 3D array to keep track of visited positions by agent IDs
    private List<Agent> agents = new List<Agent>(); // List of agents
    private List<Color> colors = new List<Color> { Color.red, Color.green, Color.blue, Color.yellow };
    private Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

    void Start()
    {
        InitializeGrid();
        StartCoroutine(Walk());
    }

    void InitializeGrid()
    {
        visited = new int[gridSize, gridSize, gridSize];
        // Initialize multiple agents at random starting positions
        for (int i = 0; i < 4; i++)
        {
            Vector3 startPos = new Vector3(Random.Range(0, gridSize), Random.Range(0, gridSize), Random.Range(0, gridSize));
            Agent newAgent = new Agent(i + 1, startPos, colors[i]);
            agents.Add(newAgent);
            visited[(int)startPos.x, (int)startPos.y, (int)startPos.z] = i + 1;
            InstantiateCube(startPos, colors[i], newAgent);
        }
    }

    IEnumerator Walk()
    {
        bool allAgentsStuck = false;
        while (!allAgentsStuck)
        {
            allAgentsStuck = true;
            foreach (Agent agent in agents)
            {
                if (agent.PathStack.Count > 0)
                {
                    Vector3 current = agent.PathStack.Peek();
                    List<Vector3> possibleMoves = GetPossibleMoves(current, agent.Id);

                    if (possibleMoves.Count > 0)
                    {
                        allAgentsStuck = false;
                        Vector3 chosenMove = possibleMoves[Random.Range(0, possibleMoves.Count)];
                        agent.PathStack.Push(chosenMove);
                        visited[(int)chosenMove.x, (int)chosenMove.y, (int)chosenMove.z] = agent.Id;
                        InstantiateCube(chosenMove, agent.Color, agent);
                        yield return new WaitForSeconds(0.1f);  // Controls the speed of cube generation
                    }
                    else
                    {
                        if (agent.PathStack.Count > 0)
                        {
                            Vector3 backtrackPosition = agent.PathStack.Pop();
                            GameObject cubeToDestroy;
                            if (agent.PathCubes.TryGetValue(backtrackPosition, out cubeToDestroy))
                            {
                                Destroy(cubeToDestroy);
                                agent.PathCubes.Remove(backtrackPosition);
                                yield return new WaitForSeconds(0.1f);  // Controls the speed of cube removal
                            }
                        }
                    }
                }
            }
        }
    }

    List<Vector3> GetPossibleMoves(Vector3 current, int agentId)
    {
        List<Vector3> moves = new List<Vector3>();
        foreach (Vector3 dir in directions)
        {
            Vector3 newPos = current + dir;
            if (IsInBounds(newPos) && visited[(int)newPos.x, (int)newPos.y, (int)newPos.z] == 0)
                moves.Add(newPos);
        }
        return moves;
    }

    bool IsInBounds(Vector3 position)
    {
        return position.x >= 0 && position.x < gridSize && position.y >= 0 && position.y < gridSize && position.z >= 0 && position.z < gridSize;
    }

    void InstantiateCube(Vector3 position, Color color, Agent agent)
    {
        GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
        cube.GetComponent<Renderer>().material.color = color;
        agent.PathCubes[position] = cube;  // Store the cube for potential removal during backtracking
    }

    public void ResetAndRespawnCubes()
    {
        foreach (Agent agent in agents)
        {
            foreach (GameObject cube in agent.PathCubes.Values)
            {
                Destroy(cube);
            }
        }

        Start();
    }
}

public class Agent
{
    public Stack<Vector3> PathStack = new Stack<Vector3>();
    public Dictionary<Vector3, GameObject> PathCubes = new Dictionary<Vector3, GameObject>();
    public int Id;
    public Color Color;

    public Agent(int id, Vector3 startPosition, Color color)
    {
        Id = id;
        PathStack.Push(startPosition);
        Color = color;
    }
}
