[SelfAvoidingWalk by leen265](https://leen265.itch.io/selfavoidingwalk)

[GitHub - LeenKharouf/SelfAvoidingPath3D](https://github.com/LeenKharouf/SelfAvoidingPath3D/tree/main)

[Notion Documentation](https://www.notion.so/Self-avoiding-walk-3D-36d5070da5894a68b44d52ac7995f473?pvs=4)

## Description:

The project features a 3D self-avoiding walk simulation, like a ‘maze’, with interactive elements such as explosions that affect the path of the walk, multiple agents that do not collide with each other, and dynamic camera controls for a better view of the simulation, and a reset button.

### Project Summary

---

This project brings to life an interactive 3D world where mazes aren't just static puzzles to solve. Powered by the Unity Game Engine, it takes the concept of a traditional maze and flips it on its head using a self-avoiding walk algorithm. What this means is that every path generated within the labyrinth ensures no overlaps, offering a fresh challenge with every game session.

Not only do you explore these ever-changing mazes, but you also have the power to shape them. Thanks to Unity’s robust physics engine, players can trigger explosions that dynamically alter the maze’s pathways. Imagine pushing through a challenging section of the labyrinth only to blow up an obstructing wall and carve a new route! This element of physical interaction adds a layer of strategy and unpredictability, enhancing the gameplay beyond simple navigation. It also creates cool surroundings that are composed of the exploded cubes that forever float in space. 

I opted for this idea because of its fusion of algorithmic art and interactive gaming. Each maze generation is a unique artistic expression which uses the algorithm to create a visually pleasing and minimalistic piece using the primary colours (+yellow!). I personally really enjoyed this because it almost reminds me of my childhood - almost like putting lego pieces together, and at the end it gives a similar vibe to a huge scrambled Rubik’s cube. 

The project also dives into the realm of generative art by integrating elements like color and motion. Each agent—there are multiple exploring the labyrinth simultaneously—leaves behind a trail in a unique colour, painting the 3D space with a palette that reflects their journey. The movement of these agents mimics organic, autonomous systems, adding a layer of natural simulation to the digital environment.

---

### Inspiration

I originally came across this 2D self-avoiding walk in p5.js and following the concepts helped me understand it better to implement it in 3D.

[Coding Challenge 162: Self-Avoiding Walk](https://www.youtube.com/watch?time_continue=3&v=m6-cm6GZ1iw&embeds_referring_euri=https://www.google.com/&source_ve_path=Mjg2NjY&feature=emb_logo)

Intrigued by the complexity of procedural content generation and traditional maze puzzles, this project addresses key generative art topic areas:

- **Animation and Motion (Physics)**: Applies Unity’s physics to create interactive maze modifications.
- **Randomization and Noise**: Uses a self-avoiding walk algorithm for unpredictable and unique maze generation each time.
- **Interactivity**: Allows users to influence the maze's structure directly, adjusting its layout in real-time.

### Visual Reference

The design was influenced by minimalistic art styles and the abstract patterns seen in architectural blueprints, providing a clean and clear interface that focuses on usability while exploring the maze. As I mentioned in the project summary, I was almost going for a Rubik’s cube look. Looking at the 2D self-avoiding walk reminded me of the Snake Game on an old Nokia phone. I used the 3D version of this game as an inspiration for my project, where multiple paths (or ‘snakes’) are moving instead of one - each a different colour.

![Snake 3D Cube Gameplay.gif](https://prod-files-secure.s3.us-west-2.amazonaws.com/5fd288dc-134f-4488-b417-5444c41b82de/720ec3c4-c1ad-4c36-bb8e-9b494f47967c/Snake_3D_Cube_Gameplay.gif)

![rubiks-cube.jpg](https://prod-files-secure.s3.us-west-2.amazonaws.com/5fd288dc-134f-4488-b417-5444c41b82de/bcdb5e51-6f08-4968-bd5c-567d5a8f4819/rubiks-cube.jpg)

### Process

- **Setup**: Configuring a 3D space and establishing a grid system for the maze.
- **Algorithmic Integration**: Implementing and tweaking the self-avoiding walk algorithm to generate non-overlapping paths dynamically.
- **Interactivity Implementation**: Integrating physics-based interactions where user inputs directly affect the structure of the maze, i.e. explosions.
- **User testing**: Testing it with some friends made me realise I needed a reset button instead of refreshing the page.
- **Challenges**: Adding a reset button and learning how the UI slider and button works. Raycasting to cause the explosion was a new concept too.

### Code References

I initially used the CodingTrain’s tutorial as a base to start with to understand how the algorithm works. I also used the GenerativeUnity website, linked to us by the professor. I used chatGTP to help guide me through implementing the UI interface and implement the code for raycasting the explosion.

### User Testing/Feedback

Initial user testing indicated the need for a clearer reset mechanism. This led to the addition of a reset button to reinitialise the maze without refreshing the entire application.

### Next Steps

- Adding a button to manually place obstacles within the maze.
- Adding a button to remove a single block at a time
- Adding a button to pause and resume the simulation

# Code Snippets

### 1. Setting Up the Environment

- **Creating a Cube Prefab**:
    - 3D cube prefab
    - Each cube represents a step taken by an agent in the maze.
    - Disabling gravity on the cube's Rigidbody to ensure they remain suspended as part of the structured grid.
    
    ```csharp
    Rigidbody rb = cubePrefab.GetComponent<Rigidbody>();
    ```
    

### 2. Defining Movement and Directions

- **Possible Directions**:
    - To dictate potential movements, I've defined an array of **`Vector3`** directions, representing all **possible** adjacent steps an agent can take: upward, downward, leftward, rightward, forward, and backward within a 3D space.
    
    ```csharp
    private Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
    ```
    

### 3. Implementing Pathfinding Logic

- **Checking Possible Moves**:
    - Describe the function that calculates potential moves from a given position, ensuring the move doesn't revisit a position.
    - Calculating potential moves from the agent's current position.
    - A move is *possible* if it's within the **grid's boundaries** and has not been previously **visited**
    - Unique and non-repetitive paths
    
    ```csharp
    List<Vector3> GetPossibleMoves(Vector3 current, int agentId) {
        List<Vector3> moves = new List<Vector3>();
        foreach (Vector3 dir in directions) {
            Vector3 newPos = current + dir;
            if (IsInBounds(newPos) && visited[(int)newPos.x, (int)newPos.y, (int)newPos.z] == 0)
                moves.Add(newPos);
        }
        return moves;
    }
    ```
    

### 4. Managing Multiple Agents

- **Creating and Managing Agents**:
    - Explain the initialization of multiple agents, each with a unique ID and starting position.
    - Each agent is initialised with a unique identifier and a random start position.
    - Collision avoidance: marking grid spaces as  `visited`
    
    ```csharp
    visited[(int)startPos.x, (int)startPos.y, (int)startPos.z] = agent.Id;
    ```
    

### 5. Adding Camera Controls

- **Implementing Camera Movement**:
    - The camera is programmed to be controllable via keyboard (rotate) and trackpad (zoom).
    - Users can rotate the view around a central point or zoom in and out
    
    ```csharp
    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) {
        transform.RotateAround(target, Vector3.up, rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
    }
    ```
    

### 6. Interactive Explosions

- **Triggering Explosions**:
    - **Detecting Mouse Input**:
        - `if (Input.GetMouseButtonDown(0))`: This line checks if the left mouse button (button 0) is pressed.
    - **Creating a Ray from Mouse Position**:
        - `Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);`: Converts the mouse position from screen coordinates into a 3D ray. This ray starts at the camera's position and extends through the mouse's position on the screen into the game world.
    - **Executing the Raycast**:
        - `RaycastHit hit;`: Declares a variable to store information about what the ray hits.
        - `if (Physics.Raycast(ray, out hit))`: Casts the ray into the game scene. If the ray hits a collider, it fills the `hit` variable with information about the hit object, such as the point of contact and the object itself.
    - **Finding Nearby Colliders**:
        - `Collider[] colliders = Physics.OverlapSphere(hit.point, explosionRadius);`: Creates a sphere at the point where the ray hit and finds all colliders within a specified radius (`explosionRadius`). This is useful for determining all objects affected by an explosion at the impact point.
    - **Applying Explosion Force to Affected Objects**:
        - `foreach (Collider hitCollider in colliders)`: Iterates through all colliders returned by `Physics.OverlapSphere`.
        - `Rigidbody rb = hitCollider.GetComponent<Rigidbody>();`: Retrieves the `Rigidbody` component from each collider. The `Rigidbody` component is necessary to apply physics forces.
        - `if (rb != null) { rb.AddExplosionForce(explosionForce, hit.point, explosionRadius); }`: Applies an explosive force to each rigidbody. The force is defined by `explosionForce`, originates from `hit.point`, and affects objects within `explosionRadius`.
    
    ```csharp
    if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Collider[] colliders = Physics.OverlapSphere(hit.point, explosionRadius);
                    foreach (Collider hitCollider in colliders)
                    {
                        Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            rb.AddExplosionForce(explosionForce, hit.point, explosionRadius);
                        }
                    }
                }
            }
    ```
    

### 7. Implementing UI Controls

- **Slider and Button Implementation**:
    - Describe how you implemented a UI slider to control the explosion force and a button to reset the scene.
    - A UI slider allows users to adjust the magnitude of explosions
    - The reset button to clear the maze and regenerate
    
    ```csharp
    forceSlider.onValueChanged.AddListener(value => explosionForce = value);
    ```
