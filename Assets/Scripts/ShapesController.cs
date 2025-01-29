using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ShapesController : MonoBehaviour
{
    [SerializeField] private GameObject[] shapes;
    public ShapeController CurrentShape { get; private set; }

    private List<GameObject> _shuffled;

    public void Initialize(Category category)
    {
        //TODO add handling for different categories
        System.Random rand = new();
        _shuffled = shapes.ToList();

        //temp disable randomization
        //for (int i = 0; i < shapes.Length / 2; i++)
        //{
        //    var randNum = rand.Next(i, _shuffled.Count);
        //    var temp = _shuffled[randNum];
        //    _shuffled[randNum] = _shuffled[i];
        //    _shuffled[i] = temp;
        //}

        InitShape();
    }

    public void InitShape()
    {
        // remove previous shape
        if (CurrentShape != null)
        {
            Destroy(CurrentShape.gameObject);
        }

        if (_shuffled.Count == 0)
        {
            CurrentShape = null;
            return;
        }


        // init new randomized shape
        var randomShape = _shuffled.First();
        _shuffled.Remove(randomShape);
        GameObject shapeObj = Instantiate(randomShape, transform);
        CurrentShape = shapeObj.GetComponent<ShapeController>();
    }
}
