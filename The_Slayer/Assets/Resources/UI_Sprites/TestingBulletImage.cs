using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingBulletImage : MonoBehaviour
{


    public GameObject myPanel;
    public Image myBulletImage;
    
    public int currentMag = 30;
    
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < currentMag; i++)
        {
            Instantiate(myBulletImage, myPanel.transform.position, Quaternion.identity, myPanel.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
