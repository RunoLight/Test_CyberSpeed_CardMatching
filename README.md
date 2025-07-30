# Card Matching Unity Game

A lightweight 2D card-matching mini-game built with Unity and structured using Domain-Driven Design (DDD) principles.

Was created as Test Task for CyberSpeed.

## 🎮 Gameplay

Flip cards to find matching pairs. Smooth animations and sound effects provide responsive feedback. The game is ideal as a fast-paced memory trainer or a prototype for casual mobile games.

## 🛠️ How to Run

1. Open the project in **Unity 6.0 or newer**.
2. Load the scene: `Assets/Scenes/MainScene.unity`.
3. Enter Play Mode to test the game.

## 🧠 Architecture

This project follows a layered architecture separated to asseemblies using DDD principles:

- **Domain**: Core game logic, card state, and rules.
- **Application**: Orchestrates game use cases (e.g. game start, win condition).
- **Infrastructure**: Input, audio, Unity-related services.
- **Presentation**: Views, animations, and MonoBehaviours like `CardView`.
- **EntryPoint**: Game bootstrap logic and scene setup.

Assemblies have fine-grained access:
| Assembly    | Reference to |
| -------- | ------- |
| Domain  |     |
| Application | Domain     |
| Infrastructure    | Domain, Application    |
| Presentation    | Domain, Application, Infrastructure, TextMeshPro    |
| EntryPoint    | Domain, Application, Infrastructure, Presentation    |

### 🗂️ Project Structure

```
Assets/
├── Animation/
├── Prefabs/
├── Scenes/
├── Scripts/
│ ├── Domain/
│ ├── Application/
│ ├── Infrastructure/
│ ├── Presentation/
│ └── EntryPoint/
├── Sounds/
└── Settings/
```

## 📦 Features

- Smooth gameplay with animations for card flipping and matching. 
- The system allows continuous card flipping without requiring users to wait for card
comparisons to finish before selecting additional cards.
- Save/load system to persist progress between game restarts.
- Sound effects for card flipping, matching, mismatching, and game over
- Scoring mechanism
- Clean separation of concerns via DDD
- Lightweight URP 2D support
- Clean and fair commit history

## 📄 License

MIT — see `LICENSE`.

---

Made with ❤️ for prototyping.
