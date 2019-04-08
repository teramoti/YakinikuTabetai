using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStar : MonoBehaviour
{
    [SerializeField]
    int maxRarity = 5;

    public Momoya.Player player;

    public GameObject star001;
    public GameObject star002;
    [SerializeField]
    float width;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxRarity; i++)
        {
            if (i  < player.Rarity)
            {
                GameObject go = Instantiate(star001) as GameObject;
                go.transform.position = new Vector3(transform.position.x + (i * width), transform.position.y, transform.position.z);
                
            }
            else
            {
                GameObject go = Instantiate(star002) as GameObject;
                go.transform.position = new Vector3(transform.position.x + (i * width), transform.position.y, transform.position.z);
           
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
  
    }
}
