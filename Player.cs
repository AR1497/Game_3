using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections.Generic;
using Hmmmm;

public abstract class Player : MonoBehaviour
{
    public float Speed = 3.0f;
    private Rigidbody _rigidbody;
    private float _speedRotate = 70f;
    private Transform _target;

    public Text textCountBonuses;
    private GoodBonus goodBonus;
    public int countBonus;
    public float MovementSpeedWithBonus { get; set; }
    private float oldSpeedP;

    private SavedData _saveData;
    private SaveDataRepository _saveDataRepository;
    private readonly KeyCode _savePlayer = KeyCode.F5;
    private readonly KeyCode _loadPlayer = KeyCode.F9;

    private List<CollectNecessary> _collectNecessary;
    private List<SpeedAcc> _goodBonus;
    private List<BadBonus1> _badBonus;

    void Start()
    {
        _collectNecessary = FindObjectsOfType<CollectNecessary>().ToList();
        _goodBonus = FindObjectsOfType<SpeedAcc>().ToList();
        _badBonus = FindObjectsOfType<BadBonus1>().ToList();

        _saveDataRepository = new SaveDataRepository();

        oldSpeedP = Speed;
        if (textCountBonuses == null)
        {
            throw new Exception("Не перетащили объект textCountBonuses");
        }
        textCountBonuses.text = "Таблетки: ";

        Debug.Log("Количество 'к' в строке:");
        Debug.Log("Кар кар кар".CharCount('к'));
        
        List<int> list = new List<int>() {7, 8, 9, 1, 7, 2, 9};
        var dupList = list.NumberOfDuplicateListItems();
        foreach (var item in dupList)
        {
            Debug.Log($"{item.Key} повторяется {item.Value} раз");
        }

        _rigidbody = GetComponent<Rigidbody>();


    }

    public void Execute()
    {
        if (Input.GetKeyDown(_savePlayer))
        {
            _saveData = new SavedData
            {
                Name = transform.gameObject.name,
                Position = transform.position,
                IsEnabled = transform.gameObject.activeSelf,
                CollectNessesary = _collectNecessary.Select(i => i.transform.position).ToList(),
                BadBonus = _badBonus.Select(i => i.transform.position).ToList(),
                GoodBonus = _goodBonus.Select(i => i.transform.position).ToList()
            };
            _saveDataRepository.Save(_saveData);
        }
        if (Input.GetKeyDown(_loadPlayer))
        {
            var loadData = _saveDataRepository.Load();

            transform.position = loadData.Position;
        }
    }

    protected void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        _rigidbody.AddForce(movement * Speed);
    }

    protected void GetBonus()
    {
        textCountBonuses.text = $"Таблетки: {countBonus}";
    }

    protected void CheckedSpeedP()
    {
        if (MovementSpeedWithBonus > 0)
        {
            Invoke("SetSpeedP", MovementSpeedWithBonus);
        }
    }

    private void SetSpeedP()
    {
        MovementSpeedWithBonus = 0;
        Speed = oldSpeedP;
    }
}
