#pragma strict
private var audioBarLength : float;
private var maxAudio : float;
private var curAudio : float;
private var maxBarWidth : float;

public var foregroundTexture : Texture2D;
public var backgroundTexture : Texture2D;
public var guiFont : Font;
public var roosterQuickness : float;
public var originalRoosterQuickness : float;

private var foregroundStyle : GUIStyle;
private var backgroundStyle : GUIStyle;
private var textStyle : GUIStyle;

private var audioC : PlayerAudioController;
private var roosterScript : GameObject;
private var checkAudio : boolean;

private var deathTime : float;

private var anim : Animator;

private var death : boolean;



function Start () {
	audioC = GameObject.FindGameObjectWithTag("MainCamera").GetComponent(PlayerAudioController);
	roosterScript = GameObject.FindGameObjectWithTag("Rooster");
	audioBarLength = 0;
	maxAudio = 200;
	curAudio = audioC.GetCurrentSample();
	roosterQuickness = 2000;
	originalRoosterQuickness = roosterQuickness;
	foregroundStyle = GUIStyle();
	foregroundStyle.stretchWidth = true;
	foregroundStyle.stretchHeight = true;
	foregroundStyle.normal.background = foregroundTexture;
	backgroundStyle = GUIStyle();
	backgroundStyle.stretchWidth = true;
	backgroundStyle.stretchHeight = true;
	backgroundStyle.normal.background = backgroundTexture;
	textStyle = GUIStyle();
	textStyle.alignment = TextAnchor.MiddleCenter;
	textStyle.richText = true;
	textStyle.font = guiFont;
	checkAudio=false;
	deathTime = 0;
	death = false;
}

function Update () {
	if(checkAudio){
		audioBarLengthUpdate();
	}
}

function OnGUI() {
	//Audio
	GUI.Label(new Rect(Screen.width-35, 10, 25, maxAudio), GUIContent.none, backgroundStyle);
	GUI.Label(new Rect(Screen.width-35, maxAudio-audioBarLength+10, 25, Mathf.Lerp(5, audioBarLength, Time.time)), GUIContent.none, foregroundStyle);
}

function audioBarLengthUpdate(){
	curAudio = audioC.GetCurrentSample();
	var previousLength : float = audioBarLength;
	audioBarLength = (maxAudio/roosterQuickness)*curAudio;
	if(audioBarLength<=0){
		audioBarLength = Mathf.Lerp(previousLength, 5, Time.time);
	}
	if(audioBarLength>maxAudio){
		audioBarLength = maxAudio;
	}
	if(audioBarLength>=900){
		roosterQuickness-=6;
	} else if(audioBarLength>=700){
		roosterQuickness-=5;
	} else if(audioBarLength>=500){
		roosterQuickness-=4; 
	} else if(audioBarLength>=300){
		roosterQuickness-=3;
	} else if(audioBarLength>=100){
		roosterQuickness-=2;
	} else if(audioBarLength>5){
		roosterQuickness-=1;
	}
	if(roosterQuickness<=0){
		Debug.Log("In here. Death Time: "+deathTime);
		if(deathTime >= 1){
			this.SendMessage("RevertToCheckpoint");
			roosterScript.SendMessage("RevertToCheckpoint");
			ResetRoosterTime();
			deathTime = 0;
		} else if(deathTime==0){
			anim = this.GetComponent(Animator);
			anim.SetBool("death",true);
			deathTime = Time.deltaTime+deathTime;
		} else {
			deathTime = Time.deltaTime+deathTime;
		}
	}
}

function ResetRoosterTime(){
	roosterQuickness=originalRoosterQuickness;
}

function monitorAudio(thisValue:boolean){
	checkAudio = thisValue;
}