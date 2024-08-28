Estoty Survivors:
The project is made utilizing Zenject. The project is written in a manner designed to follow SOLID principles and heavily favours composition over inheritance.

The project divides most scripts into three categories - Interfaces, Implementations and Consumers. Interfaces are abstractions which define method contracts. Implementations are their concrete Implementations, as the name suggests. Consumers are Mono-Behaviours which utilize logic from Implementations. Majority of the game logic is split between Implementations and Consumers. Implementations are easier to test, and therefore preferable to Consumers in most cases for logic implemenation, but some logic requires a Mono-Behaviour class to execure or is very inconvenicient to move into an Implementation layer, and therefore a significant amount of game logic lies in them as well.

All spawned GameObjects are spawned utilizing Factories. Bullets are spawned utilizing the recommended Zenject factory implemenation, but the Items and Enemies were implemented differently due to them having being spawned as a variety of different Prefabs which share the same Mono-Behaviour scipt attached to them. Enemies have a lot of Consumer scripts attached to them and therefore are even harder to implement using the main approach utilized in Zenject documentation, and therefore they are implemented without BindFactory<>() utilization. In retrospect, utilization of a Fascade pattern during development could have resulted in a code which follows approach utilized in Zenject documentation more closely.

Game components communicate with each using Signals, which are one of the approaches recommended in Zenject documentation.

MonoScriptInstaller is the main installer script, which implements most of Zenject DI bindings.

For Input, the project utilizes unity Input System, which is the modern default approach for Unity input.