using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Bebidas y clientes")]
    [SerializeField] LayerMask _beverageLayer;
    [SerializeField] LayerMask _clientLayer;
    public string currentRequest;
    private string _selectedDrink = null;

    [Header("Puntos y Cordura")]
    public int _score = 0;
    //public int _cordura = 100;
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _selectionText;
    //[SerializeField] TMP_Text _corduraText;
    //public Image _corduraImageFill;

    //[Header("Vignette")]
    //[SerializeField] Volume volume;
    //[SerializeField] Vignette vignette;
    //[SerializeField] private float vignetteOscStrength = 0.03f;
    //[SerializeField] private float vignetteOscSpeed = 1f;

    //[Header("Aberration")]
    //private ChromaticAberration chromAberration;
    //[SerializeField] private float aberrationOscStrength = 0.03f;
    //[SerializeField] private float aberrationOscSpeed = 1f;

    //[Header("Depth of Field")]
    //private DepthOfField depthOfField;
    //private Coroutine depthCoroutine;

    [Header("SHOTGUN")]
    //[SerializeField] MeshRenderer meshPumpBar;
    //[SerializeField] MeshRenderer meshPumpHand;
    bool _usePump;
    Pump _pumpCode = null;
    //[SerializeField] ParticleSystem _smokeParticle;

    [Header("NO SE XD")]
    [SerializeField] Scene _sceneName;
    [HideInInspector] public ClientOG _client;
    public TalksTeme _talkTheme;
    //public BarManager barManager;
    public bool help;



    //[SerializeField] Animator _pumpHandAni, _pumpBarAni;

    private void Awake()
    {
        //_pumpBarAni.SetBool("Bar gets it", true);


        //if (volume.profile.TryGet(out vignette))
        //{
        //   // Debug.Log("Viñeta Encontrada");
        //}
        //else
        //{
        //  //  Debug.LogWarning("No hay Nada");
        //}
        //if (volume.profile.TryGet(out chromAberration))
        //{
        // //   Debug.Log("Una Aberración en la mira");
        //}
        //else
        //{
        ////    Debug.LogWarning("Aca menos");
        //}
        //if (volume.profile.TryGet(out depthOfField))
        //{
        // //   Debug.Log("mira");
        //}

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, _beverageLayer))
            {
               // MoveDrinks moveDrinks = hit.collider.GetComponent<MoveDrinks>();
                //_moveDrinks = moveDrinks;
                Pump pump = hit.collider.GetComponent<Pump>();
                _pumpCode = pump;

                //if (moveDrinks != null)
                //    if (moveDrinks.isDraggingDrink)
                //    {
                        //_moveDrinks.drinkName = _moveDrinks._currentRequest;
                        PumpOff();
                        _pumpCode = null;
                    //}


                if (_pumpCode != null)
                {
                    if (!_usePump)
                    {
                        PumpOn();
                    }
                    else PumpOff();

                }
            }
            else if (Physics.Raycast(ray, out hit, 100f, _clientLayer))
            {
                ClientOG client = hit.collider.GetComponent<ClientOG>();

                _client = client;

                Pump();

            }
          
        }


        if (_score < 0) _score = 0;
        _scoreText.text = "$ " + _score;
        _selectionText.text = "Tienes: " + _selectedDrink;

        //if (_cordura <= 0) 
        //{
        //    StopAllCoroutines();
        //    SceneManager.LoadScene("Lose");
        //}
        //if (_cordura > 100) _cordura = 100;
        //_corduraText.text = "Cordura: " + _cordura;

        if (_score >= 1000)
        {
            //void LoadScene(string sceneName)
            //{
               StopAllCoroutines();
               SceneManager.LoadScene("Win");
            //}

        }

        if (Input.GetKeyDown(KeyCode.A)) help = !help;

        //vinetta(); 


        //_corduraImageFill.fillAmount = _cordura / 100f;

    }

    //void vinetta()
    //{

    //    if (vignette != null)
    //    {
    //        float baseVignette = 1f - (_cordura / 100f);// Inversamente proporcional a la vida      
    //        float oscillation = Mathf.Sin(Time.time * vignetteOscSpeed) * vignetteOscStrength;
    //        vignette.intensity.value = Mathf.Clamp01(baseVignette + oscillation);
    //    }
    //    if (chromAberration != null)
    //    {
    //        float baseAberration = 1f - (_cordura / 100f);// Inversamente proporcional a la vida  
    //        float oscillation = Mathf.Sin(Time.time * aberrationOscSpeed) * aberrationOscStrength;

    //        chromAberration.intensity.value = Mathf.Clamp01(baseAberration + oscillation);
    //    }

    //}

    void Pump()
    {

        if (_usePump)
        {
            _client.isDeath = true;
            if (_client.imposter)
            {
                _score += 50;
               // _cordura += 10;
                StartCoroutine(correct());
            }
            else
            {
                _score -= 200;
               // _cordura -= 15;
                StartCoroutine(Incorrect());
            }
            StartCoroutine(Shoot());
            //PumpOff();
        }

    }

    void PumpOn()
    {
      //  _pumpHandAni.SetBool("Hand gets it", true);
      // meshPumpBar.enabled = false;
       // _pumpBarAni.SetBool("Bar gets it", false);
       // meshPumpHand.enabled = true;
        _usePump = true;
        _selectedDrink = null;
    }

    void PumpOff()
    {
      //  _pumpHandAni.SetBool("Hand gets it", false);
        //meshPumpBar.enabled = true;
       // _pumpBarAni.SetBool("Bar gets it", true);
        //meshPumpHand.enabled = false;
        _usePump = false;
        _pumpCode = null;
    }

    IEnumerator Shoot()
    {
       // _smokeParticle.Play();
        yield return new WaitForSeconds(0.1f);
       // ActivateDepthOfField(3f, 0.5f);
        PumpOff();
    }

    //private void ActivateDepthOfField(float duration, float startDistance)
    //{
    //    if (depthOfField == null) return;
    //    //StopAllCoroutines();
    //    StartCoroutine(Activate(duration, startDistance));
    //}

    //private IEnumerator Activate(float duration, float startDistance)
    //{
    //    depthOfField.active = true;
    //    depthOfField.focusDistance.value = startDistance;

    //    float elapsed = 0f;

    //    while (elapsed < duration)
    //    {
    //        elapsed += Time.deltaTime;
    //        depthOfField.focusDistance.value = Mathf.Lerp(startDistance, 9f, elapsed / duration);
    //        yield return null;
    //    }
    //    depthOfField.active = false;
    //    depthOfField.focusDistance.value = 9f;
    //}
    IEnumerator Incorrect()
    {
        //_client.visor.GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        //_client.visor.GetComponent<MeshRenderer>().material.color = Color.white;
    }

    IEnumerator correct()
    {
      //  _client.visor.GetComponent<MeshRenderer>().material.color = Color.green;
        yield return new WaitForSeconds(0.3f);
       // _client.visor.GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
