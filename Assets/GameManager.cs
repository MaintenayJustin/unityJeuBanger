using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public int MaxHealth;
    public static GameManager Instance;
    public TMP_Text FruitText;
    public HealthBarBehaviour HealthBar;
    public PlayerBehaviour Player;
    private int _fruitCount = 0;

    public UnityEvent OnDeath;

    public List<GameObject> Fruits;
    private int NbFruitsACreer = 5;

    private int _currentHealth;

    private void Awake()
    {
        // Si jamais on charge une deuxième scène avec 
        // un autre game manager
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        _currentHealth = MaxHealth;
        // On crée 4 fruits pour le joueur
        for (int i = 0; i < NbFruitsACreer; i++)
        {
            CreateFruit();
        }
    }
    // Permet de créer un fruit sur un point random (possible)
    public void CreateFruit()
    {
        // On récupère un objet random dans la liste possible
        GameObject randomFruit = Fruits[Random.Range(0, Fruits.Count)];
        Vector3 pointSurLecran = GetPointSurEcran();
        Collider2D collided = Physics2D.CircleCast((Vector2)pointSurLecran, 0.01f, (Vector2)pointSurLecran).collider;
        while (collided != null)
        {
            // On vérifie qu'il n'y a pas de collider sur ce point
            pointSurLecran = GetPointSurEcran();
            collided = Physics2D.CircleCast((Vector2)pointSurLecran, 0.005f, (Vector2)pointSurLecran).collider;
        }
        Instantiate(randomFruit, pointSurLecran, Quaternion.identity);

    }
    // Permet de récupérer un point random sur l'écran visible par le joueur (peut être sur un collider)
    private Vector3 GetPointSurEcran()
    {
        // On prend un point random en fonction de la taille de l'écran et on fait spawn un fruit dessus
        Vector3 pointSurLecran = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height)));
        pointSurLecran.z = 0;
        return pointSurLecran;
    }
    // Ajoute un nombre au score du joueur
    public void AddScore(int score)
    {
        _fruitCount += score;
        FruitText.text = $"Score : {_fruitCount}";
    }
    public void TakeDamage(TrapData trapData, Collision2D collision)
    {
        // On applique un effet de KNOCKBACK au joueur
        // On récupère le Vecteur pour savoir d'où le joueur a touché le piège
        Vector2 VecteurPointTouche = collision.GetContact(0).normal;
        // TODO, changer le calcul de knockback quand on touche depuis le haut
        // (SI LE VECTEUR EN Y EST NEGATIF, ALORS ON VA PRENDRE EN COMPTE CECI)
        if(VecteurPointTouche.y < 0){
            Debug.Log("TU AS TOUCHE DEPUIS LE HAUT ZEBI");
        }
        // On inverse les valeurs et on les double
        VecteurPointTouche.x *= -trapData.KnockbackX;
        VecteurPointTouche.y = trapData.KnockbackY;
        
        // On applique le knockback au joueur
        Player.PutKnockback(VecteurPointTouche);
        Player.SetTakingDamage(true);
        StartCoroutine(DisableTakingDamage(0.2f));

        _currentHealth -= trapData.DamageAmount;
        HealthBar.SetHealth(_currentHealth, MaxHealth);
        if (_currentHealth <= 0)
            OnDeath?.Invoke();

        
    }
    private IEnumerator DisableTakingDamage(float time)
    {
        yield return new WaitForSeconds(time);
        Player.SetTakingDamage(false);
    }
}
