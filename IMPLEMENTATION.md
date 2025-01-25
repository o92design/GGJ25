# GGJ25 - Implementation Plan

## Project Setup
1. Create a new Unity 2D project.
2. Set up the following folder structure:
   - **Scripts**: All gameplay logic and system handling.
   - **Art**: Sprites and textures.
   - **Audio**: Sound effects, rain ambiance, and voice-over.
   - **Scenes**: Separate scenes for main gameplay and storybook transitions.
3. Install and configure Unity Input System for Xbox 360 controller support.

## Core Mechanics
### Character Movement
- Write a `CharacterController2D` script to handle boy and bear movement.
- Use Rigidbody2D and physics-based movement for smooth interactions.
- Separate movement logic for each character, linking controls to the Xbox 360 controller.

### Combat and Defense
- Implement a `CombatController` script for the boy’s sword attack with animation triggers.
- Implement a `DefenseController` script for the bear’s shield, detecting collisions with enemies or projectiles.

### Controller Remapping
- Add a script for remapping controls and expose remapping options in a game settings UI.

## Art Style Integration
1. Import hand-drawn assets into Unity.
2. Use Shader Graph to create a custom ink-style shader for:
   - Bold ink lines.
   - Selective color splashes (e.g., boy's yellow raincoat).
3. Set up a test scene with placeholders for forest backgrounds and rain effects.

## Enemy System
- Create an `EnemyController` script implementing a finite state machine (FSM):
  - **States**: `Idle`, `Chase`, `Attack`.
  - Logic for detecting the closest target (boy or bear).
  - Basic health and damage system for enemies.
- Test enemy interactions with boy’s attacks and bear’s shield.

## Camera and Environment
1. Use Unity Cinemachine to implement:
   - A dynamic camera that follows both characters.
   - Smooth damping for camera transitions.
   - A tilted top-down perspective for gameplay clarity.
2. Add environmental effects:
   - Particle systems for rain.
   - Interactive mud and puddles with friction adjustments.

## Narrative and Transitions
1. Implement a `StorybookManager` script to handle:
   - Displaying lines of the poem as UI elements.
   - Animating text appearance and fade-in/out transitions.
2. Add audio narration for the mother’s voice calling the boy home.

## Testing and Polish
1. Playtest each mechanic incrementally:
   - Controller mappings.
   - Enemy interactions.
   - Combat and defense mechanics.
2. Refine animations and audio timing to enhance player feedback.

## Packaging
1. Prepare a polished build for Global Game Jam submission.
2. Test the build on multiple hardware setups to ensure controller compatibility.

---

## Development Priority Order
1. **Movement System**: Core gameplay needs smooth, independent character control.
2. **Combat and Defense Mechanics**: Ensure boy’s attack and bear’s defense work seamlessly.
3. **Art Style and Camera**: Solidify the visual aesthetic and create a dynamic camera system.
4. **Enemy Behavior**: Implement and test the single enemy type.
5. **Narrative**: Add storybook transitions and mother’s voice-over.

---

This plan is designed for implementation in Unity, prioritizing core gameplay mechanics and ensuring a cohesive art and narrative style.
