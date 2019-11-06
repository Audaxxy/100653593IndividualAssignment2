using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Bonus - make this class a Singleton!

[System.Serializable]

public class BulletPoolManager
{
	private Queue<GameObject> bullets;
	private Queue<GameObject> bullets2;

	public GameObject bullet;
	public GameObject bullet2;

	public Transform containerPos;

	private GameObject newBullet;
	private GameObject newBullet2;
	private GameObject oldBullet;


	static BulletPoolManager bulletManager;
	//TODO: create a structure to contain a collection of bullets

	// Start is called before the first frame update
	public static BulletPoolManager getInstance()
	{
		if (bulletManager == null)
		{
			
			GameObject bullet=GameObject.Find("Player").GetComponent<PlayerController>().bullet;
			GameObject bullet2 =GameObject.Find("Player").GetComponent<PlayerController>().bullet2;
			bulletManager = new BulletPoolManager(bullet,bullet2);
		}
			
		return bulletManager;
	}
   private BulletPoolManager(GameObject bullet,GameObject bullet2)
    {
		containerPos = GameObject.Find("containerPos").GetComponent<Transform>();
		bullets = new Queue<GameObject>();
		bullets2 = new Queue<GameObject>();
		// TODO: add a series of bullets to the Bullet Pool
		for (int i = 0; i <= 20; i++)
		{
			
			newBullet = MonoBehaviour.Instantiate(bullet, containerPos.position, containerPos.rotation);
			bullets.Enqueue(newBullet);

			newBullet2 = MonoBehaviour.Instantiate(bullet2, containerPos.position, containerPos.rotation);
			bullets2.Enqueue(newBullet2);


		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO: modify this function to return a bullet from the Pool
    public GameObject GetBullet()
    {

		return bullets.Dequeue();
    }

	public GameObject GetBullet2()
	{

		return bullets2.Dequeue();
	}

		//TODO: modify this function to reset/return a bullet back to the Pool 
		public void ResetBullet(GameObject bullet)
    {
		bullets.Enqueue(bullet);
		
		bullet.SetActive(false);
	}
	public void ResetBullet2(GameObject bullet)
	{
		bullets2.Enqueue(bullet);

		bullet.SetActive(false);
	}
}
