using UnityEngine;
using UnityEngine.Pool;

public class GeneradorConPool : MonoBehaviour
{
    [SerializeField] FrutaPool prefab;
    [SerializeField] float intervalo = 1f;
    [SerializeField] float rangoX = 2.2f;

    ObjectPool<FrutaPool> pool;
    float reloj;

    void Awake()
    {
        pool = new ObjectPool<FrutaPool>(
            createFunc: () =>
            {
                var f = Instantiate(prefab);
                f.Pool = pool;
                return f;
            },
            actionOnGet: f => f.gameObject.SetActive(true),
            actionOnRelease: f => f.gameObject.SetActive(false),
            actionOnDestroy: f => Destroy(f.gameObject),
            defaultCapacity: 10, maxSize: 30);
    }

    void Update()
    {
        reloj += Time.deltaTime;
        if (reloj < intervalo) return;
        reloj = 0f;

        var fruta = pool.Get();
        fruta.transform.position = new Vector3(
            Random.Range(-rangoX, rangoX), transform.position.y, 0f);
    }
}
