using UnityEngine;

public class Bullet : MonoBehaviour
{

private Transform target;

public GameObject impactEffect;

public float speed = 50f;

public float explosionRadius = 0f;

public int damage = 50;

 public void Seek(Transform _target) 
 {
    target = _target;
 }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;

        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
          Destroy(effectIns, 2f);

          if(explosionRadius > 0)
          {
            Explode();
          }
          else
          {
            Damage(target);
          }
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
            
        }
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
             e.takeDamage(damage);
        }
        else 
        {
            Debug.LogError("Pas de script Ennemy sur l'ennemi");
        }
       

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
