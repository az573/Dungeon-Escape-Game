using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootBlank : MonoBehaviour
{
    //Variables
    public GameObject projectilePrefab;
    public Transform muzzlePoint;
    public GameObject projectileParent;
    public float projectileLifespan = 3f;
    public float projectileSpeed = 20f;

    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    // Update is called once per frame
    void Update()
    {
        //check if player hit our shoot button
        if(Input.GetMouseButtonDown(0)&& Time.time >= nextFireTime)
        {   
            nextFireTime = Time.time + fireRate;
            //instantiate our prefab projectile
            GameObject spawnedProjectile = Instantiate(projectilePrefab, muzzlePoint.position, muzzlePoint.rotation);
            //set the parent of the projectile to a null object so it is not impaced by our character movement
            spawnedProjectile.transform.SetParent(projectileParent.transform);
            //add force to the projectile
            spawnedProjectile.GetComponent<Rigidbody>().AddForce(muzzlePoint.up * projectileSpeed);
            //destroy the projectile after time has passed
            Destroy(spawnedProjectile, projectileLifespan);
        }






       
    }
}
