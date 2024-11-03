
# Tetris Game

This project is an implementation of the classic Tetris game using **SDL** and **C#**, following **Clean Architecture** principles and incorporating various **design patterns** (Singleton, Factory, Strategy, Observer). The game includes advanced features for piece management, flexible user interfaces, and an extensible architecture.

## Table of Contents

- [Features](#features)
- [Architecture](#architecture)
- [Design Patterns](#design-patterns)
- [Setup](#setup)
- [Controls](#controls)
- [Technologies Used](#technologies-used)
- [Credits](#credits)
- [License](#license)

## Features

- **Piece Control**: Move, rotate, and hold pieces.
- **Scoring System**: Calculates score based on the number of rows cleared.
- **Win Condition**: Victory screen when reaching a specified score.
- **User Interface**: Menus (main, credits, controls) with options selectable via keyboard or mouse.
- **Game Over Condition**: Game switches to `Game Over` if there's no space for a new piece.
- **Event Handling**: Includes keyboard and mouse input handling through configurable strategies.

## Architecture

The project follows **Clean Architecture**, which ensures a clear separation between business rules, application logic, and infrastructure.

- **Domain**: Contains the main game entities (pieces, configurations).
- **Application**: Includes the game logic (`GameLogicService`), design patterns, and input handling.
- **Infrastructure**: Implements input infrastructure (SDL) and rendering, SDL initialization, and environment configuration.

## Design Patterns

### Singleton

<div align="center">
<img src="https://lh3.googleusercontent.com/pw/AP1GczPg9MafyX63StksCGhwaM69lk7GK_-kWGPILTqvfOUOzeMZ6UvR06Tst5C8iV8aXWI5fgKNio5IgK6yZMr3RKLhgJev2OxeFWbAfKYtjNAtsfbqDZbfH1BJeDKHxx35x4NJdQbT8NpR9iu1l4FlLWPz=w331-h301-s-no-gm?authuser=0">
</div>

Used to ensure a single instance of classes such as `GameManager`, `RenderManager`, `MovementManager`, and `SoundManager`. This ensures a single access point for each, avoiding synchronization issues and unnecessary resource usage.

### Factory

<div align="center">
<img src="https://lh3.googleusercontent.com/pw/AP1GczNHczUWSfmioER3qJNfUF9uZYXdBkUNj_fCq5xf_RuVg08QIv0S-JCtZLrW-o9oTmE685dx4xNor-S9D56a-lvhqkuTQgpmrx87mS7J3ayrBi66OzwjijAnwaErTyYpHsDPx361AWBrI2id9epa5ECc=w867-h951-s-no-gm?authuser=0">
</div>

<div align="center">
<img src="https://lh3.googleusercontent.com/pw/AP1GczO1UO3rp2weODl7kpgma1bdjyes3JjADYpqzSnO0nZTU1rUb1VLhl5K6BhHVcABuUN7Tp7ogZbbEw81cCbO_kN-Lyh4lrRWc42vH2BkSuRiI595AfPsCnVcIly0CB5s2i3J4tet50Gr2jAKeKv2xnhM=w951-h950-s-no-gm?authuser=0">
</div>

`PieceFactory` and `MenuFactory` manage the creation of game pieces and menus, encapsulating creation logic and allowing easy addition of new types of pieces or menus.

### Strategy

<div align="center">
<img src="https://lh3.googleusercontent.com/pw/AP1GczMlA-rRTkQH8_leBhYuekrm-43fUZdFIju9m-YrI7Z-gh2HoGmXNwKw0INN6M3YEZ-05eLEjBIdv_UkN2BXjq40zuvO0XNFxVL1cAY9daQMzlWzaSSdkuK4iwvsBOe5sHWpnGWLL0xXaTpBg36sxEja=w798-h951-s-no-gm?authuser=0">
</div>

Input handling is achieved using the Strategy pattern. `InputManager` uses `KeyboardInputStrategy` or `MouseInputStrategy` to handle keyboard or mouse inputs, allowing dynamic input strategy changes.

### Observer

<div align="center">
<img src="https://lh3.googleusercontent.com/pw/AP1GczN4vinySvkcq0LdgcvHTxWtIqpXZEwHnV0GdR6OjE9TGves2ikdazXkfm83RDGGlmFr3-B2OjkWi_hW79QIiB8sQ62jbAASQxIBU6os1kzRRdUvC-_N8KiVonfxgcQZ04Bmnenczm4YrXfYih7L758A=w958-h675-s-no-gm?authuser=0">
</div>

`GameManager` notifies `SoundManager`, `RenderManager`, and `MovementManager` of state changes, allowing each to react independently to these changes. This keeps the code modular and facilitates the addition of new observers in the future.

## Setup

### Prerequisites

- **SDL**: Ensure SDL is installed and configured on your system.
- **.NET Core SDK**: Make sure you have the compatible version installed.

### Setup Steps

1. Clone the repository to your local machine.
   ```bash
   git clone https://emilianobz546@dev.azure.com/emilianobz546/Utn-Programacion/_git/Tetris-Tsl-Tao
   ```
2. Navigate to the project directory.
   ```bash
   cd tetris-sdl
   ```
3. Restore NuGet packages.
   ```bash
   dotnet restore
   ```
4. Set up the icon file and assets in the `Infrastructure/Assets` path.
5. Build and run the project.
   ```bash
   dotnet run
   ```

## Controls

- **Keyboard**:
  - `R` : Rotate the piece.
  - `A` : Move left.
  - `D` : Move right.
  - `S` : Drop the piece faster.
  - `Q` : Hold the current piece.
- **Mouse**:
  - `Left Click`: Select menu options.
  - `Hover`: Highlights menu options when hovering over them.

## Technologies Used

- **C#** and **.NET Core**: Programming language and framework.
- **SDL (Simple DirectMedia Layer)**: Library for graphics and input event handling.
- **Tao.Sdl**: Wrapper to use SDL with C#.

## Credits

- Developer: Emiliano Bazán-Zapata
- University: UTN Buenos Aires

## License
This project is licensed under the MIT License. See the [MIT](https://choosealicense.com/licenses/mit/) file for more details.