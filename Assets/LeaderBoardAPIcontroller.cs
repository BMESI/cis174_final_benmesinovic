using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardAPIcontroller : MonoBehaviour
{
    // Start is called before the first frame update
    WWW myWww = new WWW("http://localhost:49758/api/v1/HighScores");
}
