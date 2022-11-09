using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 【Unity】マイクラっぽい地形をパーリンノイズで再現してみた(perlin noise)
// https://dntf.hatenablog.com/entry/unity_perlinnoise


/// <summary>
/// 未使用。地面の中の部分を石にできるパターン
/// </summary>
public class PerlinNoise : MonoBehaviour {
    
    public GameObject player;
    public GameObject parent;
    public GameObject grass;
    public GameObject stone;
    
    [SerializeField]
    int fieldSize = 50;
    
    [SerializeField, Range(0.03f, 0.4f)]
    float scaler = 0.15f;

    //private float stoneChunk = 3;

    private void Awake() {
        Application.targetFrameRate = 30;
    }

    void Start() {
        Vector3 p_position = player.transform.localPosition;
        setBlocks(p_position, fieldSize);
    }

    void setBlocks(Vector3 p, int size) {
        int s = size;
        for (var i = 0; i < s; i++) {
            for (var j = 0; j < s; j++) {
                //パーリンノイズ
                var noise = Mathf.PerlinNoise(i * scaler, j * scaler);
                var y_noise = (int)(noise * 10f);
                //Debug.Log((int)(noise*10f));

                Instantiate(grass, new Vector3(p.x - i, y_noise, p.z + j), Quaternion.identity);

                for (int y = y_noise - 1; y >= 2; y--) {
                    Instantiate(stone, new Vector3(p.x - i, y, p.z + j), Quaternion.identity);
                }
            }
        }
    }
}