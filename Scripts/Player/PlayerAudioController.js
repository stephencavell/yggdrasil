#pragma strict

public var hurtClip : AudioClip;
public var deathClip : AudioClip;
public var breathingClip : AudioClip;
public var ravenClip : AudioClip;
public var jumpStartClip : AudioClip;
public var jumpLandClip : AudioClip;
public var roosterCrowClip : AudioClip;
public var walkingClip : AudioClip;
public var trottingClip : AudioClip;
public var runningClip : AudioClip;
public var backgroundClip : AudioClip;

private var hurtAudio : AudioSource;
private var deathAudio : AudioSource;
private var breathingAudio : AudioSource;
private var ravenAudio : AudioSource;
private var jumpStartAudio : AudioSource;
private var jumpLandAudio : AudioSource;
private var roosterCrowAudio : AudioSource;
private var walkingAudio : AudioSource;
private var trottingAudio : AudioSource;
private var runningAudio : AudioSource;
private var backgroundAudio : AudioSource;

var qSamples: int = 1024;  // array size
var refValue: float = 0.1; // RMS value for 0 dB
var threshold = 0.02;      // minimum amplitude to extract pitch
var rmsValue: float;   // sound level - RMS
var dbValue: float;    // sound level - dB
var pitchValue: float; // sound pitch - Hz
 
private var samples: float[]; // audio samples
private var spectrum: float[]; // audio spectrum
private var fSample: float;
private var sources: Array = new Array();

function Awake() {
    hurtAudio = gameObject.AddComponent(AudioSource);
    hurtAudio.clip = hurtClip;
    hurtAudio.loop = false;
    hurtAudio.playOnAwake = false;
    hurtAudio.volume = 0.8;
    hurtAudio.dopplerLevel = 0.0;
    sources.Push(hurtAudio);

    deathAudio = gameObject.AddComponent(AudioSource);
    deathAudio.clip = deathClip;
    deathAudio.loop = false;
    deathAudio.playOnAwake = false;
    deathAudio.volume = 0.8;
    deathAudio.dopplerLevel = 0.0;
    sources.Push(deathAudio);
    
    breathingAudio = gameObject.AddComponent(AudioSource);
    breathingAudio.clip = breathingClip;
    breathingAudio.loop = false;
    breathingAudio.playOnAwake = false;
    breathingAudio.volume = 0.8;
    breathingAudio.dopplerLevel = 0.0;
    sources.Push(breathingAudio);
    
    ravenAudio = gameObject.AddComponent(AudioSource);
    ravenAudio.clip = ravenClip;
    ravenAudio.loop = false;
    ravenAudio.playOnAwake = false;
    ravenAudio.volume = 0.8;
    ravenAudio.dopplerLevel = 0.0;
    sources.Push(ravenAudio);

    jumpStartAudio = gameObject.AddComponent(AudioSource);
    jumpStartAudio.clip = jumpStartClip;
    jumpStartAudio.loop = false;
    jumpStartAudio.playOnAwake = false;
    jumpStartAudio.volume = 0.5;
    jumpStartAudio.dopplerLevel = 0.0;
    sources.Push(jumpStartAudio);

    jumpLandAudio = gameObject.AddComponent(AudioSource);
    jumpLandAudio.clip = jumpLandClip;
    jumpLandAudio.loop = false;
    jumpLandAudio.playOnAwake = false;
    jumpLandAudio.volume = 0.4;
    jumpLandAudio.dopplerLevel = 0.0;
    sources.Push(jumpLandAudio);

    roosterCrowAudio = gameObject.AddComponent(AudioSource);
    roosterCrowAudio.clip = roosterCrowClip;
    roosterCrowAudio.loop = false;
    roosterCrowAudio.playOnAwake = false;
    roosterCrowAudio.volume = 0.8;
    roosterCrowAudio.dopplerLevel = 0.0;
    sources.Push(roosterCrowAudio);

    walkingAudio = gameObject.AddComponent(AudioSource);
    walkingAudio.clip = walkingClip;
    walkingAudio.loop = false;
    walkingAudio.playOnAwake = false;
    walkingAudio.volume = 0.8;
    walkingAudio.dopplerLevel = 0.0;
    sources.Push(walkingAudio);

    trottingAudio = gameObject.AddComponent(AudioSource);
    trottingAudio.clip = trottingClip;
    trottingAudio.loop = false;
    trottingAudio.playOnAwake = false;
    trottingAudio.volume = 0.8;
    trottingAudio.dopplerLevel = 0.0;
    sources.Push(trottingAudio);

    runningAudio = gameObject.AddComponent(AudioSource);
    runningAudio.clip = runningClip;
    runningAudio.loop = false;
    runningAudio.playOnAwake = false;
    runningAudio.volume = 10.6;
    runningAudio.dopplerLevel = 0.0;
    sources.Push(runningAudio);
    
    backgroundAudio = gameObject.AddComponent(AudioSource);
    backgroundAudio.clip = backgroundClip;
    backgroundAudio.loop = true;
    backgroundAudio.volume = 2.0;
    backgroundAudio.dopplerLevel = 0.0;
    sources.Push(backgroundAudio);
    backgroundAudio.Play();
    
    samples = new float[qSamples];
    spectrum = new float[qSamples];
    fSample = AudioSettings.outputSampleRate;
}

function Update() {
	pitchValue = -100000;
	for(var source : AudioSource in sources){
		AnalyzeSound(source);
	}
}

function PlayHurt() {
	if(hurtAudio.isPlaying==false){
    	hurtAudio.Play();
	}
}

function PlayDeath() {
    if(deathAudio.isPlaying==false){
    	deathAudio.Play();
	}
}

function PlayBreathing() {
	if(breathingAudio.isPlaying==false){
		breathingAudio.Play();
	}
}

function PlayRaven() {
	if(ravenAudio.isPlaying==false){
    	ravenAudio.Play();
    }
}

function PlayWalking() {
	//Debug.Log("Walking");
	if(trottingAudio.isPlaying==true){
		trottingAudio.Pause();
	}
	if(runningAudio.isPlaying==true){
		runningAudio.Pause();
	}
	if(walkingAudio.isPlaying==false){
    	walkingAudio.Play();
    }
}

function PlayTrotting() {
	//Debug.Log("Trotting");
	if(walkingAudio.isPlaying==true){
		walkingAudio.Pause();
	}
	if(runningAudio.isPlaying==true){
		runningAudio.Pause();
	}
	if(trottingAudio.isPlaying==false){
    	trottingAudio.Play();
    }
}

function PlayRunning() {
	//Debug.Log("Running");
	if(walkingAudio.isPlaying==true){
		walkingAudio.Pause();
	}
	if(trottingAudio.isPlaying==true){
		trottingAudio.Pause();
	}
	if(runningAudio.isPlaying==false){
    	runningAudio.Play();
    }
}

function PlayJumpStart() {
	//Debug.Log("Jumped");
	if(jumpStartAudio.isPlaying==false){
    	jumpStartAudio.Play();
    }
}

function PlayJumpLand() {
	//Debug.Log("Landed");
	if(jumpLandAudio.isPlaying==false){
    	jumpLandAudio.Play();
    }
}

function PlayRoosterCrow() {
	if(roosterCrowAudio.isPlaying==false){
    	roosterCrowAudio.Play();
    }
}

function PauseRoosterCrow() {
	if(roosterCrowAudio.isPlaying){
    	roosterCrowAudio.Pause();
    }
}

function AnalyzeSound(aud:AudioSource){
     aud.GetOutputData(samples, 0); // fill array with samples
     //GetComponent(AudioListener).GetOutputData(samples, 0);
     var i: int;
     var sum: float = 0;
     for (i=0; i < qSamples; i++){
         sum += samples[i]*samples[i]; // sum squared samples
     }
     rmsValue = Mathf.Sqrt(sum/qSamples); // rms = square root of average
     dbValue = 20*Mathf.Log10(rmsValue/refValue); // calculate dB
     if (dbValue < -160) dbValue = -160; // clamp it to -160dB min
     // get sound spectrum
     aud.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
     var maxV: float = 0;
     var maxN: int = 0;
     for (i=0; i < qSamples; i++){ // find max 
         if (spectrum[i] > maxV && spectrum[i] > threshold){
             maxV = spectrum[i];
             maxN = i; // maxN is the index of max
         }
     }
     var freqN: float = maxN; // pass the index to a float variable
     if (maxN > 0 && maxN < qSamples-1){ // interpolate index using neighbours
         var dL = spectrum[maxN-1]/spectrum[maxN];
         var dR = spectrum[maxN+1]/spectrum[maxN];
         freqN += 0.5*(dR*dR - dL*dL);
     }
     pitchValue = Mathf.Max(pitchValue, freqN*(fSample/2)/qSamples); // convert index to frequency
 }
 
 function GetCurrentSample () {
	return pitchValue;
}