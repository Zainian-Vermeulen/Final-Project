using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroyMusic : MonoBehaviour
{
    //private void Awake()
    //{
    //    GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");

    //    if (musicObj.Length > 1)
    //    {
    //        Destroy(this.gameObject);
    //    }
    //    DontDestroyOnLoad(this.gameObject);

    //}

    private static DoNotDestroyMusic instance = null;
    public static DoNotDestroyMusic Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }


    /*
     * music:
     * https://youtu.be/RLXr_miMxHU
     * 
     * game win:
     * https://freesound.org/people/jobro/sounds/60444/
     * 
     * game loose:
     * https://freesound.org/people/Jofae/sounds/364929/
     * 
     * monster die:
     * https://freesound.org/people/vox_artist/sounds/513621/
     * 
     * shoot:
     * https://freesound.org/people/igroglaz/sounds/593857/
     * 
     * click button:
     * https://freesound.org/people/Snapper4298/sounds/183473/
     */
}
