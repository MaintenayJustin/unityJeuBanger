using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{

    public int MaxHealth;
    public static GameManager Instance;
    public TMP_Text FruitText;
    public HealthBarBehaviour HealthBar;
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
        Collider2D collided = Physics2D.CircleCast((Vector2)pointSurLecran, 0.005f, (Vector2)pointSurLecran).collider;
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
    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        HealthBar.SetHealth(_currentHealth, MaxHealth);
        if (_currentHealth <= 0)
            OnDeath?.Invoke();
    }
}
