using UnityEngine;
using NewHorizons.Utility;
using NewHorizons.Components.Quantum;

namespace TheStrangerTheyAre
{
    public class EnigmaPlanetFix : MonoBehaviour
    {
        private QuantumPlanet distantEnigma; // to store distant enigma
        private EffectRuleset enigmaRuleset; // to store the ruleset of distant enigma
        private Sector[] enigmaSectors = new Sector[3]; // to store all of distant enigma's sectors
        private PlayerSectorDetector playerSectorDetector; // to store the player's sector detector
        private bool areAllSectorsLoaded; // to check whether or not all sectors are loaded in
        private bool isOnDistantEnigma; // to check if player is inside one of the distant enigma sectors

        void Start()
        {
            // distant enigma object to save searchutilities resources
            GameObject distantEnigmaObject = SearchUtilities.Find("DistantEnigma_Body");
            distantEnigma = distantEnigmaObject.GetComponent<QuantumPlanet>();

            // get player sector detector and enigma sectors
            playerSectorDetector = Locator.GetPlayerSectorDetector();
            enigmaSectors[0] = distantEnigmaObject.transform.Find("Sector").gameObject.GetComponent<Sector>();
            enigmaSectors[1] = distantEnigmaObject.transform.Find("Sector-2").gameObject.GetComponent<Sector>();
            enigmaSectors[2] = distantEnigmaObject.transform.Find("Sector-3").gameObject.GetComponent<Sector>();

            // get rid of distortion because it makes it harder to see the interface and scout for that one puzzle
            enigmaRuleset = distantEnigmaObject.transform.Find("Volumes/Ruleset").gameObject.GetComponent<EffectRuleset>();
            enigmaRuleset._underwaterDistortScale = 0;
            enigmaRuleset._underwaterMaxDistort = 0;
            enigmaRuleset._underwaterMinDistort = 0;

            // set all sectors loaded to false at start of loop
            areAllSectorsLoaded = false;
            isOnDistantEnigma = false;
        }

        void Update()
        {
            if (distantEnigma.IsPlayerEntangled() && !areAllSectorsLoaded) // only runs if the sectors are all not loaded in.
            {
                for (int i = 0; i < enigmaSectors.Length; i++) // loops through all three distant enigma sectors
                {
                    if (!playerSectorDetector.IsWithinSector(enigmaSectors[i].name)) // only runs if boolean is true, and if the current sector is not in the player's sector list.
                    {
                        playerSectorDetector.AddSector(enigmaSectors[i]); // adds player to sector
                        TheStrangerTheyAre.WriteLine("Player added to sector: " + enigmaSectors[i].name.ToString());
                    }
                }
                areAllSectorsLoaded = true; // confirms all sectors are loaded so it doesn't keep running in update for the rest of the loop
            }
        }
    }
}