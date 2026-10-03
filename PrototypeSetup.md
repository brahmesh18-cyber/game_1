# Prototype Scene Setup

## Player

Create a Capsule named `Player`.

Add:
- Collider
- `PlayerController`
- `RecordingSystem`
- `EchoSystem`
- `EchoInputExample`

Tag it `Player`.

## Echo prefab

Duplicate the player or create a simplified capsule.

Add:
- Collider
- `EchoPlayback`

Tag it `Echo`.

Assign this prefab to `EchoSystem.echoPrefab`.

## Lever

Create a cube/cylinder object and add `Lever`.

## Pressure plate

Create a thin cube with a trigger collider and add `PressurePlate`.

## Enemy

Create a capsule and add:
- Collider
- `Health`
- `BasicEnemyAI`

Set its target to the Player.

## Camera

Use a third-person camera or Unity's Cinemachine package if desired.

## Important

The included `EchoInputExample` uses Unity's legacy `Input` API for a minimal prototype. For a production project, migrate the input bridge to the Unity Input System.
