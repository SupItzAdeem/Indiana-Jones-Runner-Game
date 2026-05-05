using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private int rotateSpeed = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (this.tag == "RedCoin")
        {
            SoundManager.PlaySound(SoundType.COLLECT);
            GameManager.pointsCount += 5;
            this.gameObject.SetActive(false);

        }

        if (this.tag == "BlueCoin")
        {
            SoundManager.PlaySound(SoundType.COLLECT);
            GameManager.pointsCount += 3;
            this.gameObject.SetActive(false);
        }
        
    }
}
