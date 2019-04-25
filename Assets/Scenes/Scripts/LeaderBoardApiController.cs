using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LeaderBoardApiController : MonoBehaviour
{

    public GameObject RowPrefab;
    public GameObject Panel;

    private void Start()
    {
        StartCoroutine(GetHighScores());
    }
    // Update is called once per frame
    IEnumerator GetHighScores()
    {
        var GetHighScoresEndpoint = UnityWebRequest.Get("https://cis174finalbm.azurewebsites.net/api/v1/HighScores");
        yield return GetHighScoresEndpoint.SendWebRequest();
        if(GetHighScoresEndpoint.isNetworkError || GetHighScoresEndpoint.isHttpError)
        {
            Debug.Log(GetHighScoresEndpoint.error);
        }
        else
        {
            Debug.Log("Web Stts Req: " + GetHighScoresEndpoint.responseCode);
            var jsonres = GetHighScoresEndpoint.downloadHandler.text;

            var scores = JsonConvert.DeserializeObject<List<LeaderboardViewModel>>(jsonres);

            foreach(var score in scores)
            {
                var row = GameObject.Instantiate(RowPrefab, Panel.transform);
                row.GetComponent<RowController>().SetAllFields(

                    score.PersonId.ToString(),
                    score.Score.ToString(),
                    score.DateAttained.ToString());
                /*score.FirstName.ToString(),
                score.PersonId.ToString(),
                score.DateCreated.ToString());
                */
            }
        }
    }

}
