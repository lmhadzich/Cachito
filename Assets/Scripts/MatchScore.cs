using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MatchScore : MonoBehaviour
{

    //Managers

    public GameManager gameMGR;
    public MatchManager matchMGR;

    private Transform entryContainer;
    private Transform entryTemplate;

    [SerializeField]
    public List<MatchScoreEntry> matchScoreEntryList;
    [SerializeField]
    public List<Transform> matchScoreEntryTransformList;

    public int maxPlayers;
    // Start is called before the first frame update
    void Awake()
    {

        entryContainer = transform.Find("MsEntries");
        entryTemplate = entryContainer.Find("MsEntryTemplate");
        entryTemplate.gameObject.SetActive(false);

    }

    public void PopulateMs(string action)
    {
        maxPlayers = gameMGR.maxPlayers;
        matchScoreEntryList = new List<MatchScoreEntry>();
        matchScoreEntryTransformList = new List<Transform>();
        if (action == "update")
        {
            matchScoreEntryList.Clear();
            matchScoreEntryTransformList.Clear();
            foreach(Transform entryTemplate in entryContainer)
            {
                if (entryTemplate.gameObject.activeSelf)
                {
                    Destroy(entryTemplate.gameObject);
                }
            }
        }

        maxPlayers = gameMGR.maxPlayers;
        matchScoreEntryList = new List<MatchScoreEntry>();
        
        for (int i = 0; i < maxPlayers; i++) {
            matchScoreEntryList.Add(new MatchScoreEntry { name = gameMGR.playerList[i].name, score = gameMGR.playerList[i].matchScore, fosforos = gameMGR.playerList[i].gamePoints });
        };

        if (action == "update")
        {
            for (int i = 0; i < matchScoreEntryList.Count; i++)
            {
                for (int j = i + 1; j < matchScoreEntryList.Count; j++)
                {
                    if (matchScoreEntryList[j].score > matchScoreEntryList[i].score)
                    {
                        Debug.Log(matchScoreEntryList[j].score +" > "+ matchScoreEntryList[i].score);
                        MatchScoreEntry tmp = matchScoreEntryList[i];
                        matchScoreEntryList[i] = matchScoreEntryList[j];
                        matchScoreEntryList[j] = tmp;
                        
                    }
                    if (matchScoreEntryList[j].score == matchScoreEntryList[i].score)
                    {
                        Debug.Log(matchScoreEntryList[j].score +" = "+ matchScoreEntryList[i].score);
                    }
                }
            }

        }



        foreach (MatchScoreEntry matchScoreEntry in matchScoreEntryList)
        {
            CreateMatchscoreEntryTransform(matchScoreEntry, matchScoreEntryTransformList);
        }
    }

   private void CreateMatchscoreEntryTransform(MatchScoreEntry matchScoreEntry, List<Transform> transformList)
    {
        maxPlayers = gameMGR.maxPlayers;

        float templateHeight = 20;

            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
            entryTransform.gameObject.SetActive(true);

            entryTransform.Find("txtMsPos").GetComponent<TextMeshProUGUI>().text = (transformList.Count + 1).ToString();
            entryTransform.Find("txtMsName").GetComponent<TextMeshProUGUI>().text = matchScoreEntry.name + " (" + matchScoreEntry.fosforos + ")";
            entryTransform.Find("txtMsScore").GetComponent<TextMeshProUGUI>().text = matchScoreEntry.score.ToString();

            transformList.Add(entryTransform);

    }
public class MatchScoreEntry
    {
        public string name;
        public float score;
        public int fosforos;
    }
}
