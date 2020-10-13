using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceController : MonoBehaviour
{
    public static bool BlueprintIsSelected { get; set; }
    public static GameObject SelectedCanvas { get; set; }

    [SerializeField] private GameObject baseBuildingButtonGroup;
    private GameObject currentButtonGroup;

    [SerializeField] private GameObject waveInfoUI;
    private Text waveInfoText;
    private float timeToPrepare;

    private int enemiesLeftInWave;

    [Header("Textboxes")]
    [SerializeField] private Text ironText;
    [SerializeField] private Text aluminumText;
    [SerializeField] private Text copperText;
    [SerializeField] private Text leadText;
    [SerializeField] private Text powerText;
    [SerializeField] private Text livesText;
    [SerializeField] private Text commandText;

    [Header("Prefabs")]
    [SerializeField] private GameObject ballisticTurretPrefab;
    [SerializeField] private GameObject energyTurretPrefab;
    [SerializeField] private GameObject missileTurretPrefab;
    [SerializeField] private GameObject solarPanelsPrefab;
    [SerializeField] private GameObject windTurbinePrefab;
    [SerializeField] private GameObject minerPrefab;

    private void Awake()
    {
        currentButtonGroup = baseBuildingButtonGroup;
        BlueprintIsSelected = false;
        waveInfoText = waveInfoUI.GetComponent<Text>();
        EventCoordinator.RegisterListener<PreparationStageStartEvent>(OnPreparationStart);
        EventCoordinator.RegisterListener<WaveStartEvent>(OnWaveStart);
        EventCoordinator.RegisterListener<WaveClearedEvent>(OnWaveCleared);
        EventCoordinator.RegisterListener<LivesLostEvent>(OnLivesLost);
        EventCoordinator.RegisterListener<CommandForceChangedEvent>(OnCommandChange);
        EventCoordinator.RegisterListener<EnemyDiedEvent>(OnEnemyDeath);
    }

    private void Update()
    {
        ironText.text = "Iron: " + Inventory.GetResourceAmount(Resource.ResourceType.Iron).ToString("n2");
        aluminumText.text = "Aluminum: " + Inventory.GetResourceAmount(Resource.ResourceType.Aluminum).ToString("n2");
        copperText.text = "Copper: " + Inventory.GetResourceAmount(Resource.ResourceType.Copper).ToString("n2");
        leadText.text = "Lead: " + Inventory.GetResourceAmount(Resource.ResourceType.Lead).ToString("n2");
        powerText.text = "Power: " + PowerGrid.storedPower.ToString("n2") + "/" + PowerGrid.capacity.ToString("n2");
        if(timeToPrepare > 0)
        {
            timeToPrepare -= Time.deltaTime;
            if(timeToPrepare < 0) {
                timeToPrepare = 0;
            }
            else
            {
                waveInfoText.text = "Next wave spawns in: " + timeToPrepare.ToString("n2") + " seconds.";
            }  
        }
        else
        {
            waveInfoText.text = "Remaining enemies in this wave: " + enemiesLeftInWave;
        }
    }

    public void OnPreparationStart(PreparationStageStartEvent preparationStageStartEvent)
    {
        timeToPrepare = preparationStageStartEvent.preparationTime;
        waveInfoUI.SetActive(true);
        waveInfoText.text = "Next wave spawns in: " + timeToPrepare.ToString("n2") + " seconds.";
    }

    public void OnWaveStart(WaveStartEvent waveStart) { enemiesLeftInWave = waveStart.numberOfEnemiesInWave; }
    public void OnWaveCleared(WaveClearedEvent waveCleared) { SwitchToBaseButtonGroup(); }
    public void OnEnemyDeath(EnemyDiedEvent enemyDied) { enemiesLeftInWave--; }
    public void OnLivesLost(LivesLostEvent livesLost) { livesText.text = "Remaining Lives: " + Base.GetLives(); }
    public void OnCommandChange(CommandForceChangedEvent commandForceChanged) { commandText.text = "Command: " + commandForceChanged.currentCommandForce + "/" + Base.GetCommandLimit(); }

    public void InstantiateBlueprint(GameObject blueprint)
    {
        if (!BlueprintIsSelected)
        {
            if(blueprint == ballisticTurretPrefab)
            {
                BallisticTurret ballisticTurret = ObjectPooler.Instance.ballisticTurretPool.Retrieve();
                ballisticTurret.Relocate();
            }
            else if(blueprint == energyTurretPrefab)
            {
                EnergyTurret energyTurret = ObjectPooler.Instance.energyTurretPool.Retrieve();
                energyTurret.Relocate();
            }
            else if(blueprint == missileTurretPrefab)
            {
                MissileTurret missileTurret = ObjectPooler.Instance.missileTurretPool.Retrieve();
                missileTurret.Relocate();
            }
            else if(blueprint == solarPanelsPrefab)
            {
                SolarPanels solarPanels = ObjectPooler.Instance.solarPanelsPool.Retrieve();
                solarPanels.Relocate();
            }
            else if(blueprint == windTurbinePrefab)
            {
                WindTurbine windTurbine = ObjectPooler.Instance.windTurbinePool.Retrieve();
                windTurbine.Relocate();
            }
            else if(blueprint == minerPrefab)
            {
                Miner miner = ObjectPooler.Instance.minerPool.Retrieve();
                miner.Relocate();
            }
        }
    }

    public void SwitchButtonGroup(GameObject buttonGroup)
    {
        currentButtonGroup.SetActive(false);
        buttonGroup.SetActive(true);
        currentButtonGroup = buttonGroup;
    }

    public void SwitchToBaseButtonGroup() { SwitchButtonGroup(baseBuildingButtonGroup); }
}
