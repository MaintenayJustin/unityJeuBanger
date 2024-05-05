using UnityEngine;

public class SawBehaviour : MonoBehaviour
{
    public TrapData Data;
    public GameManager GameManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // On regarde si on a bien touché le joueur
        // On envoie la data du trap et la collision pour
        // Que le game manager se charge de 
        if (collision.collider.CompareTag("Player"))
        {
            GameManager.Instance.TakeDamage(Data, collision);
        }
    }
}
