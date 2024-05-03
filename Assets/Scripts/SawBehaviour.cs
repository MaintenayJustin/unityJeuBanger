using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBehaviour : MonoBehaviour
{
    public TrapData Data;
    public GameManager GameManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // TODO, Knockback
            // On récupère le rigidbody du joueur
            Rigidbody2D RbPlayer = collision.collider.GetComponent<Rigidbody2D>();
            // On récupère le Vecteur pour savoir d'où le joueur a touché la scie
            Vector2 VecteurPointTouche = collision.GetContact(0).normal;
            // On inverse les valeurs et on les double
            VecteurPointTouche *= -Data.Knockback;
            // On applique la force calculé au rigid Body du joueur
            RbPlayer.AddForce(VecteurPointTouche, ForceMode2D.Impulse);
            // Debug.Log(VecteurPointTouche);
            GameManager.Instance.TakeDamage(Data.DamageAmount);
        }
    }
}
