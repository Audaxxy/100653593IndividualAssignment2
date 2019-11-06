using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class PlayerController : MonoBehaviour
{
    public Speed speed;
    public Boundary boundary;
    public GameController gameController;
    public Transform bulletSpawn;
    public GameObject bullet;
	public GameObject bullet2;

    // private instance variables
    private AudioSource _thunderSound;
    private AudioSource _yaySound;
    private AudioSource _bulletSound;
	private GameObject nextBullet;
	private int bulletType;

    private bool isFiring = false;

    //TODO: create a reference to the BulletPoolManager here

    // Start is called before the first frame update
    void Start()
    {
        _thunderSound = gameController.audioSources[(int)SoundClip.THUNDER];
        _yaySound = gameController.audioSources[(int)SoundClip.YAY];
        _bulletSound = GetComponent<AudioSource>();

        // Shoots bullet on a delay if button is pressed
        StartCoroutine(FireBullet());
    }

    // Update is called once per frame
    void Update()
    {
        // Move player
        Move();

        // Checks if shoot button is pressed
        ActionCheck();

        // Destroys bullet when it's off screen
        CheckBounds();
    }

    public void Move()
    {
        Vector2 newPosition = transform.position;

        if(Input.GetAxis("Horizontal") > 0.0f)
        {
            newPosition += new Vector2(speed.max, 0.0f);
        }

        if (Input.GetAxis("Horizontal") < 0.0f)
        {
            newPosition += new Vector2(speed.min, 0.0f);
        }

        transform.position = newPosition;
    }

    private void CheckBounds()
    {
        // check right boundary
        if(transform.position.x > boundary.Right)
        {
            transform.position = new Vector2(boundary.Right, transform.position.y);
        }

        // check left boundary
        if (transform.position.x < boundary.Left)
        {
            transform.position = new Vector2(boundary.Left, transform.position.y);
        }
    }

    private void ActionCheck()
    {
		// see Edit -> Project Settings -> Input
		if (Input.GetKey(KeyCode.Mouse0))
		{
			isFiring = true;
			bulletType = 1;
		}
		else if (Input.GetKey(KeyCode.Mouse1))
		{
			isFiring = true;
			bulletType = 2;
		}

		else
		{
			isFiring = false;
		}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Cloud":
                _thunderSound.Play();
                gameController.Lives -= 1;
                break;
            case "Island":
                _yaySound.Play();
                gameController.Score += 100;
                break;
        }
    }

    IEnumerator FireBullet()
    { 
        while (true)
        {
            // Check every 0.2 seconds if shoot button is pressed
            yield return new WaitForSeconds(0.15f);
            if (isFiring)
            {
                _bulletSound.Play();

				//TODO: this code needs to change to user the BulletPoolManager's
				if (bulletType == 1)
				{
					nextBullet = BulletPoolManager.getInstance().GetBullet();
					nextBullet.transform.position = bulletSpawn.transform.position;
					nextBullet.SetActive(true);
				}
				else if (bulletType == 2)
				{
					nextBullet =  BulletPoolManager.getInstance().GetBullet2();
					nextBullet.transform.position = bulletSpawn.transform.position;
					nextBullet.SetActive(true);
				}

				
                //TODO: bullet object. 
                //TODO: Ensure you position the new bullet at the bulletSpawn position
                
            }

        }
    }

}
