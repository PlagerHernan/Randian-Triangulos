using UnityEngine;

public class StarParticle : MonoBehaviour
{
    static Object prefab;

    public static StarParticle Create()
    {
        prefab = Resources.Load("StarParticle");
        GameObject newObject = Instantiate(prefab) as GameObject;

        StarParticle starParticleObject = newObject.GetComponent<StarParticle>();
        return starParticleObject;
    }

    public static StarParticle Create(Vector3 UIposition)
    {
        prefab = Resources.Load("StarParticle");
        GameObject newObject = Instantiate(prefab) as GameObject;
        
        PrepareUIMode(newObject, UIposition);
        TriangleCanvas.ActivateBlockingCover();

        StarParticle starParticleObject = newObject.GetComponent<StarParticle>();
        return starParticleObject;
    }

    void Start()
    {
        AudioHandler.PlaySound("Stars");
    }

    static void PrepareUIMode(GameObject newObject, Vector3 position)
    {
        newObject.transform.position = position;

        SpriteRenderer sprite = newObject.GetComponent<SpriteRenderer>();

        Color darkColor;
        ColorUtility.TryParseHtmlString("#C0B375", out darkColor);
        sprite.color = darkColor;

        newObject.transform.localScale = new Vector3(0.4f, 0.4f);

        sprite.sortingLayerName = "Triangle Canvas";
    }

    //llamado desde animación
    public void DeactivateBlockingCover()
    {
        TriangleCanvas.DeactivateBlockingCover();
    }

    //llamado desde animación
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
