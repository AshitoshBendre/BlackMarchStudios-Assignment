# CustomEditor-PathFinding

## Overview

**CustomEditor-PathFinding** is a project focused on creating and managing cube arrays within Unity's Editor, leveraging custom editor tools. This project aims to provide a comprehensive learning experience in tool scripting, pathfinding, and the use of tweening libraries.

## Features

- **Cube Array Generation:** Use Unity's Editor to generate arrays of cubes.
- **Scriptable Objects Integration:** Save generated cubes as Scriptable Object assets for persistent data storage.
- **Coordinate Display:** Hover over cubes to display their current coordinates.
- **Obstacle Creation:**
  - Left-click on a cube to mark it as an obstacle.
  - Option to enable a SphereObject as an obstacle via the `CubeGenerator` GameObject settings (default changes cube color to red).
- **Player Movement:** Right-click on any path cube to initiate player movement to that tile (requires saved grid as a Scriptable Object asset referenced in the PathFinding script).
- **Pathfinding Algorithm:** Implements a raw A* Pathfinding algorithm, which requires optimization for actual game use.
- **Tweening:** Utilizes the DOTween library for smooth animations and transitions.

## Project Details

### Cube Generation

This tool allows for the creation of a grid of cubes directly within the Unity Editor. Each cube can be interacted with to set specific attributes, such as marking it as an obstacle.

### Saving and Loading

Generated cubes can be saved as Scriptable Object assets. This ensures that the grid configuration can be easily saved and loaded, facilitating the pathfinding process.

### User Interactions

- **Hover Pointer:** Displays the coordinates of the cube under the pointer.
- **Left-Click:** Marks a cube as an obstacle.
- **Right-Click:** Initiates player movement to the clicked cube (pathfinding in action).

### Pathfinding

The project features a basic implementation of the A* Pathfinding algorithm. While functional, it is a raw implementation and requires optimization for better performance in a game environment.

### Tweening

Animations and transitions are handled using the DOTween library, providing a smooth user experience.

## Future Updates

This project is actively being developed and updated on a weekly basis. Future updates will include optimizations, new features, and improved functionality.

## Images

*Placeholder for images*

> Images showcasing the project in action will be added soon.

## Installation and Usage

1. Clone the repository.
2. Open the project in Unity.
3. Use the custom editor tools to generate and manipulate cube arrays.
4. Save your grid configurations as Scriptable Object assets.
5. Experiment with the pathfinding feature by right-clicking on path cubes.

## Contributing

Contributions are welcome! Feel free to fork the repository and submit pull requests.

## License

This project is licensed under the MIT License. See the LICENSE file for details.
