# Simulation-Game
We developed the prototype of a multiplayer simulation game for our university graduation project. 

Multiple lobbies can be created, and players can interact with each other or experience the game individually. In the game, players can assign tasks to their individually owned characters based on their professions like woodsman, miner. Additionally, players can craft items for themselves from the crafting menu, such as chests, beds, walls, and doors. This part is currently only functioning on the client side.

We used the following in the project:

▪ Utilized the Lobby and Relay features provided by Unity Game Service for multiplayer, and developed the multiplayer functionality using Netcode. It includes more features such as public/private settings, maximum players, player information, and many others.

▪ Created our map using procedural map generation by determining seven different regions. While creating our map, we can adjust various properties such as width/height, noise scale, octaves, persistence, seed, lacunarity, and many more.

![image](https://github.com/furkanyuksell/Simulation-Game/assets/81265340/115826ba-f4fb-466a-a663-c46b78ef2dfb)

▪ There are 7 different regions in the game, each with its own characteristics. These regions are visible on the map and have their own unique features such as hunger rate, thirst rate, chill rate, which animal and raw material can instantiated.

![image](https://github.com/furkanyuksell/Simulation-Game/assets/81265340/b10de9cc-478c-42fd-917b-dd894d00b399)

▪ The generic object pool was implemented, and all objects on the map were instantiated using this pool. In addition, these objects are network objects, meaning they are instantiated on the server-side.

![Adsız](https://github.com/furkanyuksell/Simulation-Game/assets/81265340/a692ffb4-384d-41ae-93a7-f3e536d9e8bc)

▪ Characters like woodsman, miner, etc., in the game are instantiated on the server-side upon request from the connecting clients. In the game, the task system assigns tasks to characters based on the tasks selected from the submenu. The characters then start working on the assigned tasks.

![image](https://github.com/furkanyuksell/Simulation-Game/assets/81265340/eb1c9fd6-a111-4ae0-b298-c75b44cf7c2e)
![image](https://github.com/furkanyuksell/Simulation-Game/assets/81265340/dcdba0a6-24a8-4402-b78e-7d35bfeb86a3)


▪ We have A* pathfinding system in game to allow characters to reach their destinations. This system helps them find the most suitable path to the objects in the given tasks. During the map generation, our grid system marks non-walkable areas as false, preventing characters from finding paths over water, objects, and other obstacles.

▪ In the game, there are collectible resources such as wood, stone, food, and herbs. These resources can be used to craft different items and add them to the map.



![image](https://github.com/furkanyuksell/Simulation-Game/assets/81265340/0d2344fa-7a46-4014-b099-38a3765caceb)
![image](https://github.com/furkanyuksell/Simulation-Game/assets/81265340/3f27fc17-58cc-410a-93fa-5f1f373754b3)
![image](https://github.com/furkanyuksell/Simulation-Game/assets/81265340/a60c676e-1e60-4d9a-b736-3d1309c02106)
![image](https://github.com/furkanyuksell/Simulation-Game/assets/81265340/16b96fc9-2a42-4019-8d06-3841caeb3ba2)
![image](https://github.com/furkanyuksell/Simulation-Game/assets/81265340/fca62e17-2daa-49ff-b550-c909eb9c67a6)
![image](https://github.com/furkanyuksell/Simulation-Game/assets/81265340/4876f576-d5d5-4779-a473-28e2c2660dae)
![image](https://github.com/furkanyuksell/Simulation-Game/assets/81265340/33be9114-cd54-4fdb-ae61-4551230253d2)
![image](https://github.com/furkanyuksell/Simulation-Game/assets/81265340/5c0f4296-34e4-4c93-ae12-7aaedc2ae7e4)
