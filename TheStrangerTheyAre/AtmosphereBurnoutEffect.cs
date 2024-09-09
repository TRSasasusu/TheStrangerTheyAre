using UnityEngine;
using OWML.Common;
using NewHorizons.Components.Stars;

namespace TheStrangerTheyAre;
public class AtmosphereBurnoutEffect : MonoBehaviour
{
    private static MaterialPropertyBlock s_matPropBlock_ShockLayer;

    private static int s_propID_Color;

    private static int s_propID_WorldToLocalShockMatrix;

    private static int s_propID_Dir;

    private static int s_propID_Length;

    private static int s_propID_Flare;

    private static int s_propID_TrailFade;

    private static int s_propID_GradientLerp;

    private static int s_propID_MainTex_ST;

    private PlanetaryFogController _fog;
    private Color _shockLayerColor = new Color(255f, 130f, 0f);
    private float _shockLayerStartRadius = 1000f;
    private float _shockLayerFullRadius = 2000f;
    private float _shockLayerTrailLength = 100f;
    private float _shockLayerTrailFlare = 100f;

    private float _ambientLightOrigIntensity;

    private LOD[] _atmosphereLODs;

    private Color _fogOrigTint;

    private Renderer _fogImpostor;

    private MeshRenderer _shockLayer;
    private Light _ambientLight;
    private LODGroup _atmosphere;

    // custom variables
    private GameObject atmosphere;
    private GameObject clouds;
    private GameObject hazard;
    private GameObject hazardLight;
    private StarController star;
    //bool runOnce;
    private void Awake()
    {
        //runOnce = false;

        // gets nh generated objects
        atmosphere = GameObject.Find("SizzlingSands_Body/Sector/Atmosphere");
        _atmosphere = GameObject.Find("SizzlingSands_Body/Sector/Atmosphere/Atmosphere").GetComponent<LODGroup>();
        _ambientLight = GameObject.Find("SizzlingSands_Body/Sector/AmbientLight").GetComponent<Light>();
        clouds = GameObject.Find("SizzlingSands_Body/Sector/Clouds");
        hazard = GameObject.Find("SizzlingSands_Body/Sector/HazardVolume");
        hazardLight = GameObject.Find("SizzlingSands_Body/Sector/AmbientLight");
        _shockLayer = GameObject.Find("SizzlingSands_Body/Sector/ShockLayer").GetComponent<MeshRenderer>();
        star = GameObject.Find("NearestNeighbor_Body/Sector/Star").GetComponent<StarController>();
        _fog = GameObject.Find("NearestNeighbor_Body/Sector/FogSphere").GetComponent<PlanetaryFogController>();

        // sets them active at start of loop
        atmosphere.SetActive(true);
        clouds.SetActive(true);
        hazard.SetActive(true);
        hazardLight.SetActive(true);

        if (s_matPropBlock_ShockLayer == null)
        {
            s_matPropBlock_ShockLayer = new MaterialPropertyBlock();
            s_propID_Color = Shader.PropertyToID("_Color");
            s_propID_WorldToLocalShockMatrix = Shader.PropertyToID("_WorldToShockLocalMatrix");
            s_propID_Dir = Shader.PropertyToID("_Dir");
            s_propID_Length = Shader.PropertyToID("_Length");
            s_propID_Flare = Shader.PropertyToID("_Flare");
            s_propID_TrailFade = Shader.PropertyToID("_TrailFade");
            s_propID_GradientLerp = Shader.PropertyToID("_GradientLerp");
            s_propID_MainTex_ST = Shader.PropertyToID("_MainTex_ST");
        }
        if (_ambientLight != null)
        {
            _ambientLightOrigIntensity = _ambientLight.intensity;
        }
        if (_atmosphere != null)
        {
            _atmosphereLODs = _atmosphere.GetLODs();
        }
        if (_fog != null)
        {
            _fogOrigTint = _fog.fogTint;
            _fogImpostor = _fog.fogImpostor;
        }
        if (_shockLayer != null)
        {
            _shockLayer.enabled = false;
        }
    }

    private void Update()
    {
        var burnDuration = TimeLoop.GetSecondsElapsed() > 1320 && TimeLoop.GetSecondsElapsed() <= 1330;
        var afterBurn = TimeLoop.GetSecondsElapsed() > 1330;


        if (afterBurn)
        {
            TheStrangerTheyAre.WriteLine("Atmosphere should be finished burning...", MessageType.Success); // debug message
            atmosphere.SetActive(false);
            clouds.SetActive(false);
            hazard.SetActive(false);
            hazardLight.SetActive(false);
        }
        else if (burnDuration)
        {
            TheStrangerTheyAre.WriteLine("Atmosphere should be burning...", MessageType.Success); // debug message
            // shock layer stuff
            if (_shockLayer != null)
            {
                if (!_shockLayer.enabled)
                {
                    _shockLayer.enabled = true;
                }
                Vector3 dir = Vector3.Normalize(transform.position - star.transform.position);
                s_matPropBlock_ShockLayer.SetColor(s_propID_Color, _shockLayerColor);
                s_matPropBlock_ShockLayer.SetMatrix(s_propID_WorldToLocalShockMatrix, Matrix4x4.TRS(transform.position, Quaternion.LookRotation(dir, Vector3.up), Vector3.one).inverse);
                s_matPropBlock_ShockLayer.SetVector(s_propID_Dir, dir);
                s_matPropBlock_ShockLayer.SetFloat(s_propID_Length, _shockLayerTrailLength);
                s_matPropBlock_ShockLayer.SetFloat(s_propID_Flare, _shockLayerTrailFlare);
                s_matPropBlock_ShockLayer.SetFloat(s_propID_TrailFade, 1f - Mathf.InverseLerp(_shockLayerStartRadius, _shockLayerFullRadius, star.transform.localScale.x));
                s_matPropBlock_ShockLayer.SetFloat(s_propID_GradientLerp, 0);
                s_matPropBlock_ShockLayer.SetVector(s_propID_MainTex_ST, _shockLayer.sharedMaterial.GetVector(s_propID_MainTex_ST) with { w = -Time.timeSinceLevelLoad });
                _shockLayer.SetPropertyBlock(s_matPropBlock_ShockLayer);
            }
        }
    }
}
