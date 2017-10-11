using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	public CanvasGroup fadeCanvasGroup;							//Canvas group con el que haremos el oscurecido de la pantalla
	public float fadeDuration = 1f;								//Duración del fade
	public string startingSceneName = "BlockScene";				//Valor por defecto que utilizaremos si no está definida la escena actual en el DataManager
	public string initialStartingPosition;		//Valor por defecto de la posición de la entrada a la escena, por si no está definida en el DataManager

	private bool isFading;										//Bool para indicar cuándo se está realizando el fade
	public bool blockAction;

	public static SceneController SC;

	void Awake(){
		if (SC == null) {
			SC = GetComponent<SceneController> ();
		}
	}

	private IEnumerator Start(){
		//Ponemos la pantalla en negro
		fadeCanvasGroup.alpha = 1f;

		//Si existe un actualScene definido en el DataManager lo usamos, en caso contrario, usamos el que hemos definido por defecto
		startingSceneName = DataManager.DM.data.actualScene != "" ? DataManager.DM.data.actualScene : startingSceneName;

		//Si existe un startPosition definido en el DataManager lo usamos, en caso contrario, usamos el que hemos definido por defecto
		initialStartingPosition = DataManager.DM.data.startPosition != "" ? DataManager.DM.data.startPosition : initialStartingPosition;

		//Cargamos la escena y la ponemos activa
		yield return StartCoroutine (LoadSceneAndSetActive (startingSceneName));
		blockAction = true;

		//Hacemos un Face In
		StartCoroutine (Fade (0f));
	}

	/// <summary>
	/// Llamada pública para el cambio de escenas
	/// </summary>
	/// <param name="sceneName">Scene name.</param>
	/// <param name="startPosition">Start position.</param>
	public void FadeAndLoadScene(string sceneName, string startPosition) { //, AudioClip sceneMusic){
		//Actualizamos el valor de la escena actual
		DataManager.DM.data.actualScene = sceneName;

		//Actualizamos el punto de posicionamiento del jugador
		DataManager.DM.data.startPosition = startPosition;

		//MusicManager.MM.music.clip = sceneMusic;
		//MusicManager.MM.music.Play();

		//Guardamos al realizar el cambio de escena

		//MEJOR TODAVIA NO -- DataManager.DM.Save ();

		//Si no está haciendo un fade
		if (!isFading) {
			//Llamamos la corrutina para fade y cambio de escena
			StartCoroutine (FadeAndSwitchScenes (sceneName));
		}
	}

	/// <summary>
	/// Cambia de escena
	/// </summary>
	/// <returns>The and switch scenes.</returns>
	/// <param name="sceneName">Scene name.</param>
	private IEnumerator FadeAndSwitchScenes(string sceneName){
		//Hacemos Fade Out
		yield return StartCoroutine (Fade (1f));

		//En cuanto termina el Fade, descargamos la escena activa
		yield return SceneManager.UnloadSceneAsync (SceneManager.GetActiveScene ().buildIndex);

		//Una vez descargada la escena, llamamos a la corrutina que cargará la nueva escena
		yield return StartCoroutine (LoadSceneAndSetActive (sceneName));

		//Una vez terminada la carga, haremos el fade in
		yield return StartCoroutine(Fade(0f));
	}

	/// <summary>
	/// Carga y activa la escena
	/// </summary>
	/// <returns>The scene and set active.</returns>
	/// <param name="sceneName">Scene name.</param>
	private IEnumerator LoadSceneAndSetActive(string sceneName){

		int y = SceneManager.GetActiveScene().buildIndex;

		if (y == 0) {

			//Cargamos la escena de forma "Aditiva", sin destruir la escena actual
			yield return SceneManager.LoadSceneAsync (sceneName, LoadSceneMode.Additive);

			//Para recuperar la última escena añadida, tomamos el número actual de escenas menos 1, esto dará el índice de la última escena añadida
			Scene newlyLoadedScene = SceneManager.GetSceneAt (SceneManager.sceneCount - 1);

			//Ponemos esta última escena como activa
			SceneManager.SetActiveScene (newlyLoadedScene);

		} else {


			// A MEJORAR
			yield return null;

			//Para recuperar la última escena añadida, tomamos el número actual de escenas menos 1, esto dará el índice de la última escena añadida
			Scene newlyLoadedScene = SceneManager.GetSceneAt (SceneManager.sceneCount - 1);

			//Ponemos esta última escena como activa
			SceneManager.SetActiveScene (newlyLoadedScene);
		}
	}

	/// <summary>
	/// Fundido en negro
	/// </summary>
	/// <param name="finalAlpha">Final alpha.</param>
	public IEnumerator Fade(float finalAlpha){
		//Indicamos que se está realizando un fade
		isFading = true;

		//Bloqueamos todo raycast para evitar que se interactúe durante el fade
		fadeCanvasGroup.blocksRaycasts = true;

		//Calculamos la velocidad del fade
		float fadeSpeed = Mathf.Abs (fadeCanvasGroup.alpha - finalAlpha) / fadeDuration;

		//Mientras el alpha no sea SIMILAR al alpha objetivo, ejecutamos acciones
		while (!Mathf.Approximately (fadeCanvasGroup.alpha, finalAlpha)) {
			//Aumentamos el valor del alpha mediante MoveTowards
			fadeCanvasGroup.alpha = Mathf.MoveTowards (fadeCanvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);

			//Hacemos que la corrutina se ejecute en cada frame
			yield return null;
		}

		//Si llega hasta este punto, es que el fade ha terminado
		isFading = false;
		blockAction = false;

		//Volvemos a permitir nuevamente los raycasts
		fadeCanvasGroup.blocksRaycasts = false;
	}
}