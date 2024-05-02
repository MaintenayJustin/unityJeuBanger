using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public TMP_Text FruitText;
    private int _fruitCount = 0;
    public List<GameObject> Fruits;


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
        for (int i = 0; i < 4; i++)
        {
            CreateFruit();
        }
    }
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
    private Vector3 GetPointSurEcran()
    {
        // On prend un point random en fonction de la taille de l'écran et on fait spawn un fruit dessus
        Vector3 pointSurLecran = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height)));
        pointSurLecran.z = 0;
        return pointSurLecran;
    }
    public void AddScore(int score)
    {
        _fruitCount += score;
        FruitText.text = $"Score : {_fruitCount}";
    }
}
