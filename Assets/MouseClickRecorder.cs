using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseClickRecorder : MonoBehaviour
{
    public bool isOpen = false;
    public Dictionary<PlayerType, int> playerClickNum = new Dictionary<PlayerType, int>();
    public Dictionary<PlayerType, Text> playerClickNumText = new Dictionary<PlayerType, Text>();
    public Text clickNumText_Player1;
    public Text clickNumText_Player2;
    // Start is called before the first frame update
    void Start()
    {
        playerClickNum[PlayerType.Player1] = 0;
        playerClickNum[PlayerType.Player2] = 0;
        playerClickNumText[PlayerType.Player1] = clickNumText_Player1;
        playerClickNumText[PlayerType.Player2] = clickNumText_Player2;
    }

    // Update is called once per frame
    void Update()
    {
		if (isOpen)
		{
			if (Input.GetButtonDown("Fire_Player1"))
			{
                playerClickNum[PlayerType.Player1] += 1;
                playerClickNumText[PlayerType.Player1].text= playerClickNum[PlayerType.Player1].ToString();
            }
            else if (Input.GetButtonDown("Fire_Player2"))
			{
                playerClickNum[PlayerType.Player2] += 1;
                playerClickNumText[PlayerType.Player2].text = playerClickNum[PlayerType.Player2].ToString();
            }
        }
    }
}
