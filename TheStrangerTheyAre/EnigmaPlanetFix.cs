using UnityEngine;
using NewHorizons.Utility;
using NewHorizons.Components.Quantum;

namespace TheStrangerTheyAre
{
    public class EnigmaPlanetFix : MonoBehaviour
    {
        private QuantumPlanet distantEnigma; // to store distant enigma
        private EffectRuleset enigmaRuleset; // to store the ruleset of distant enigma
        private PlayerSectorDetector playerSectorDetector; // to store the player's sector detector

        void Start()
        {
            // distant enigma object to save searchutilities resources
            GameObject distantEnigmaObject = SearchUtilities.Find("DistantEnigma_Body");
            distantEnigma = distantEnigmaObject.GetComponent<QuantumPlanet>();

            // get player sector detector and enigma sectors
            playerSectorDetector = Locator.GetPlayerSectorDetector();

            // get rid of distortion because it makes it harder to see the interface and scout for that one puzzle
            enigmaRuleset = distantEnigmaObject.transform.Find("Volumes/Ruleset").gameObject.GetComponent<EffectRuleset>();
            enigmaRuleset._underwaterDistortScale = 0;
            enigmaRuleset._underwaterMaxDistort = 0;
            enigmaRuleset._underwaterMinDistort = 0;
        }
    }
}