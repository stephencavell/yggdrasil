#pragma strict
private var audioBarLength : float;
private var maxAudio : float;
private var curAudio : float;
private var maxBarWidth : float;
private var originalRoosterQuickness : float;

public var foregroundTexture : Texture2D;
public var backgroundTexture : Texture2D;
public var guiFont : Font;
public var roosterQuickness : float;

private var foregroundStyle : GUIStyle;
private var backgroundStyle : GUIStyle;
private var textStyle : GUIStyle;

private var audioC : PlayerAudioController;

function Start () {
	audioC = GetComponent(PlayerAudioController);
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
}

function Update () {
	audioBarLengthUpdate();
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
	roosterQuickness-=1;
}

function ResetRoosterTime(){
	roosterQuickness=originalRoosterQuickness;
}