using UnityEngine;

public class SawBehaviour : MonoBehaviour
{
    public TrapData Data;
    public GameManager GameManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // On regarde si on a bien touché le joueur
        if (collision.collider.CompareTag("Player"))
        {
            // TODO, Knockback
            // On récupère le rigidbody du joueur
            Rigidbody2D RbPlayer = collision.collider.GetComponent<Rigidbody2D>();
            // On récupère le Vecteur pour savoir d'où le joueur a touché la scie
            Vector2 VecteurPointTouche = collision.GetContact(0).normal;
            // On inverse les valeurs et on les double
            VecteurPointTouche *= -Data.KnockbackX;
            VecteurPointTouche.y = Data.KnockbackY;
            Debug.Log(VecteurPointTouche.ToString());
            // On applique la force calculée au rigid Body du joueur
            RbPlayer.AddForce(VecteurPointTouche, ForceMode2D.Impulse);
            GameManager.Instance.TakeDamage(Data.DamageAmount);
        }
    }
}
