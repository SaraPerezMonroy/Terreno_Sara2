using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    private static ObjectPool instance;
    static Dictionary<int, Queue<GameObject>> pool = new Dictionary<int, Queue<GameObject>>(); // Diccionario con los objetos, tenemos el número del objeto como clave
    static Dictionary<int, GameObject> parentsPool = new Dictionary<int, GameObject>(); // Diccionario para la jerarquía, se crea una piscina en la que se meten los objetos de antes y así se quedan ordenados dependiendo de los tipos (si tengo varios tipos de balas)

    private void Awake() // Singleton
    {
        if (instance == null) // Si la instancia de este script está vacía, IE si no se usa el script, usamos este script
        {
            instance = this; 
        }
        else // Si este script está siendo usado en otro lado, destruimos este :)
        {
            Destroy(this); 
        }

    }

    public static void PreLoad(GameObject prefab, int amount) // Con esto precargamos los objetos en la escena, necesitamos el prefab y la cantidad de objetos a crear
    {
        int id = prefab.GetInstanceID(); // Función de Unity que sirve para devolver el id del objeto, viendo que es único y almacenamos en id el orden en el que se creó el objeto
        GameObject parentPool = new GameObject(); // Esto es lo de antes, el objeto del pool que almacenará después todas las balas
        parentPool.name = prefab.name + " ParentPool"; // Lo nombramos bien
        parentsPool.Add(id, parentPool); // Añadir al diccionario, el de la jerarquía, para luego poder meterle las cositas dentro y estar ordenadas

        pool.Add(id, new Queue<GameObject>()); // Este sirve ahora para crear una nueva cola (en vez de stackearlas) donde almacenaremos las balas P.EJ 

        for (int i = 0; i < amount; i++) // Cantidad de objetos que tendremos en las piscinas
        {
            CreateObject(prefab);
        }
    }

    //El paramentro que se le pasa es el ObjetoPrimigeneo, ya que clonaremos el resto de objetos de ahí 
    static void CreateObject(GameObject prefab) // Pasamos el prefab, porque vamos a coger ese prefab para clonarlo
    {
        int id = prefab.GetInstanceID(); // Instancio el ID, al coger el identificador del prefab elijo en qué piscina lo meto
        GameObject copiaPrefab = Instantiate(prefab) as GameObject; // Hacemos copia del objeto que le pasé, del prefab
        copiaPrefab.transform.SetParent(Getparent(id).transform); // Aquí le decimos qué piscina es, hacemos padre el ParentPool y hacemos hijo al objeto que acabamos de copiar
        copiaPrefab.SetActive(false); // No queremos que se vean todos en la escena, los desactivamos
        pool[id].Enqueue(copiaPrefab); // Lo añadimos en la cola, es como el push
    }
    static GameObject Getparent(int parentID) // Devuelve el identificador del padre y se lo pasa como un parámetro a la clave del diccionario
    {
        GameObject parent; // Esto es para crear un sitio donde almacenar el ID del padre, en la jerarquía se ve como un empty
        parentsPool.TryGetValue(parentID, out parent); // Intenta obtener el valor asociado a la clave, si no hay, no pasa nada
        return parent; // Le pasamos al diccionario el identificador
    }

    public static GameObject GetObject(GameObject prefab) // Para coger los objetos y sacarlos
    {
        int id = prefab.GetInstanceID(); // Almacenamos el identificador del objeto para saber en qué piscina está

        if (pool[id].Count == 0) // Si la pool está vacía crea un objeto, después (o si no está vacía) lo saca de la cola
        {
            CreateObject(prefab);
        }
        GameObject copiaPrefab = pool[id].Dequeue(); // Sacamos el primer objeto de la cola, es como el pop
        copiaPrefab.SetActive(true);
        return copiaPrefab;
    }

    public static void RecicleObject(GameObject prefab, GameObject objectToRecicle) // Metemos el objeto activado a la cola, necesitamos el prefab para ver qué ID tiene y a qué pool meterlo; y el objeto que queremos meter en la cola
    {
        int id = prefab.GetInstanceID(); // Miramos el ID para saber en qué piscina ponerlo
        pool[id].Enqueue(objectToRecicle); // Lo metemos en la cola, como pushearlo
        objectToRecicle.SetActive(false); 
    }
    public static void ClearPool()
    {
        foreach (var m_FirstDictionary in pool)
        {
            Queue<GameObject> queue = m_FirstDictionary.Value;
            foreach (GameObject m_Obj in queue)
            {
                Destroy(m_Obj);
            }
            queue.Clear();
        }

        pool.Clear();

        foreach (var m_SecondDictionary in parentsPool)
        {
            GameObject parent = m_SecondDictionary.Value;
            Destroy(parent);
        }

        parentsPool.Clear();
    }
  }

