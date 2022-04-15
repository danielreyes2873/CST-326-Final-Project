

put the GameManager prefab into the scene,and in the player's script(maybe playerController?)

in the Awake()

(I put CharacterStats in the CharacterStats file)
GameManager.Instance.RigisterPlayer(CharacterStats);

(I create CharacterStats script, you can just put it on the player, and getComponent in the playerController)

or you can just put CharacterStats on the player, and put data into the character

and after that, every script can access playerInformation by using

GameManager.Instance.playerStats.gameObject;

or using 

GameManager.Instance.playerStats.currentHealth....

