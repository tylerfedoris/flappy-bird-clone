using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FlappyBirdClone.Scripts
{
    public class Ground : MonoBehaviour
    {
        [System.Serializable]
        private struct GroundSpriteObject
        {
            public Sprite top;
            public Sprite bottom;
            public GameEvents.Season season;
        }
        
        [SerializeField] private List<GroundSpriteObject> groundSprites;
        [SerializeField] private GameObject groundPrefab;
        [SerializeField] private float yOffset;
        [SerializeField] private float xOffset;
        [SerializeField] private float xBoundary = 5f;
        
        private LinkedList<GameObject> groundObjects;
        private float yPosition;
        private GameEvents.Season currentTrackedSeason;
        private List<GroundSpriteObject> currentSeasonGroundSprites;
        private const float numGroundObjectsNeededMultiplier = 1.5f;

        private void Awake()
        {
            groundObjects = new LinkedList<GameObject>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            currentTrackedSeason = GameEvents.Current.CurrentSeason;
            UpdateCurrentSeasonGroundSprites();

            var numGroundObjectsNeeded = Convert.ToInt32(Math.Ceiling((GameEvents.Current.ScreenBounds.x * 2f + xOffset) / xOffset) * numGroundObjectsNeededMultiplier);
            yPosition = -GameEvents.Current.ScreenBounds.y + yOffset;

            float xPosition = -GameEvents.Current.ScreenBounds.x;
            var thisTransform = transform;
            thisTransform.position += new Vector3(0f, yPosition, 0f);

            for (var i = 1; i <= numGroundObjectsNeeded; i++)
            {
                var topSprite = currentSeasonGroundSprites[Random.Range(0, currentSeasonGroundSprites.Count)].top;
                
                var groundObject = Instantiate(groundPrefab, thisTransform, true);
                groundObject.transform.localPosition = new Vector3(xPosition, 0f, 0f);

                foreach (var spriteRenderer in groundObject.GetComponentsInChildren<SpriteRenderer>())
                {
                    spriteRenderer.sprite = spriteRenderer.gameObject.tag switch
                    {
                        "GroundTop" => topSprite,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }

                groundObjects.AddLast(groundObject);
                xPosition += xOffset;
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (currentTrackedSeason != GameEvents.Current.CurrentSeason)
            {
                currentTrackedSeason = GameEvents.Current.CurrentSeason;
                UpdateCurrentSeasonGroundSprites();
            }
            
            var headGroundObject = groundObjects.First;
            var headTransform = headGroundObject.Value.transform;
            
            if (!(headTransform.position.x < -GameEvents.Current.ScreenBounds.x - xBoundary)) return;
            
            headTransform.localPosition = groundObjects.Last.Value.transform.localPosition + new Vector3(xOffset, 0f, 0f);
            groundObjects.RemoveFirst();
            groundObjects.AddLast(headGroundObject);
        }

        private void UpdateCurrentSeasonGroundSprites()
        {
            currentSeasonGroundSprites = new List<GroundSpriteObject>(groundSprites.Where(groundSprite => groundSprite.season == currentTrackedSeason));
        }
    }
}
