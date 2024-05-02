# SelfAvoidingPath3D
### Project Overview

The project features a 3D self-avoiding walk simulation with interactive elements such as explosions that affect the path of the walk, multiple ‘agents’ that do not collide with each other, and dynamic camera controls for a better view of the simulation. Here’s how each part is implemented:

### 1. Self-Avoiding Walk Implementation

- **3D Grid Setup**: The grid is defined within a 3D space where each position can potentially be visited by an agent.
    
    ```csharp
    private int[,,] visited; // 3D array tracking visited positions.
    
    ```
    
- **Agent Initialization**: Multiple agents are initialized at random starting positions within the grid. Each agent has a unique ID and color.
    
    ```csharp
    for (int i = 0; i < 4; i++) {
        Vector3 startPos = new Vector3(Random.Range(0, gridSize), Random.Range(0, gridSize), Random.Range(0, gridSize));
        agents.Add(new Agent(i + 1, startPos, colors[i]));
    }
    
    ```
    
- **Path Walking**: Agents attempt to move to adjacent unvisited positions. If no moves are possible, they backtrack.
    
    ```csharp
    Vector3 chosenMove = possibleMoves[Random.Range(0, possibleMoves.Count)];
    if (moves.Count > 0) {
        agent.PathStack.Push(chosenMove);
        InstantiateCube(chosenMove, agent.Color, agent);
    }
    
    ```
    

### 2. Cube Management

- **Cube Instantiation**: Cubes are instantiated at each step of an agent to visualize the path.
    
    ```csharp
    GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
    cube.GetComponent<Renderer>().material.color = color;
    
    ```
    
- **Backtracking and Cube Destruction**: When an agent backtracks, the corresponding cube is destroyed.
    
    ```csharp
    if (agent.PathStack.Count > 0) {
        Vector3 backtrackPosition = agent.PathStack.Pop();
        Destroy(agent.PathCubes[backtrackPosition]);
    }
    
    ```
    

### 3. Camera Control

- **Rotation and Zoom**: The camera can be rotated around a central point using keyboard inputs and zoomed in and out using the mouse scroll wheel.
    
    ```csharp
    transform.RotateAround(target, Vector3.up, rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
    float distance = Vector3.Distance(transform.position, target) - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
    distance = Mathf.Clamp(distance, minZoomDistance, maxZoomDistance);
    transform.position = target + (transform.position - target).normalized * distance;
    
    ```
    

### 4. Interactive Explosions

- **Explosion Trigger**: Explosions are triggered by a mouse click, applying a physics force to nearby objects.
    
    ```csharp
    if (Input.GetMouseButtonDown(0)) {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            Collider[] colliders = Physics.OverlapSphere(hit.point, explosionRadius);
            foreach (Collider collider in colliders) {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                rb?.AddExplosionForce(explosionForce, hit.point, explosionRadius);
            }
        }
    }
    
    ```
    

### 5. UI and Interaction

- **Force Adjustment via UI Slider**: The explosion force can be adjusted using a UI slider, which dynamically updates the force applied during explosions.
    
    ```csharp
    forceSlider.onValueChanged.AddListener(value => explosionForce = value);
    
    ```
    

### Summary

This project effectively combines several advanced features of Unity, including 3D graphics, physics interactions, and user input handling. By managing multiple agents within a self-avoiding walk algorithm and incorporating interactive elements like explosions and camera controls, the simulation offers a dynamic and visually engaging experience.
