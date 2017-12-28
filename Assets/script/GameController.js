#pragma strict


var  IsGameOver : boolean = false;
static var instance : GameController;

function Awake(){
	if(instance == null){
		instance = this;
	}
	if(instance != this){
		Destroy(gameObject);
	}
}
function Start () {

}

function Update () {

	if(IsGameOver && Input.GetKeyDown(KeyCode.Space)){
		Application.LoadLevel(Application.loadedLevel);
	}	
}