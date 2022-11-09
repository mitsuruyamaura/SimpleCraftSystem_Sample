using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 【Unity】マインクラフト風の地形を作りSTYLYへアップロードする《前編》
// https://styly.cc/ja/tips/texturedvoxel_iwase_minecraft/


public class VoxelGround : MonoBehaviour
{
    private float sizeX = 50.0f;  // X 軸上に並べるブロックの数
    private float sizeY = 10.0f;  // 地形の最大の高さ
    private float sizeZ = 50.0f;  // Z 軸上に並べるブロックの数
    private float sizeW = 17.0f;  // 起伏のサイズ(山の頂上から平地までの距離。17ブロックで１つの山を作るため、50ブロック内におおよそ３つの山が出来る計算)
                                  // 小さい数値にするほど起伏の激しい地形になる


    void Awake()
    {
        var material = GetComponent<MeshRenderer>().material;

        // 50 * 50 で2500個のブロックを生成
        for (float x = 0; x < sizeX; x++) {
            for (float z = 0; z < sizeZ; z++ ) {

                // ブロック生成
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.SetParent(transform);

                cube.GetComponent<MeshRenderer>().material = material;

                // パーリンノイズの引数の範囲は 0 ～ 1 の間
                // X、Z 軸に沿って滑らかに変化する数値を生成(PerlinNoise を利用することでブロックの高さを滑らかに変化させる)
                float noise = Mathf.PerlinNoise(x / sizeW, z / sizeW);

                // 生成した数値をブロックの Y 軸の高さ(位置)に設定
                float y = Mathf.Round(sizeY * noise);

                // ブロックの位置を設定
                cube.transform.localPosition = new(x, y, z);

                SetUV(cube);
            }
        }

        // 親のオブジェクトを移動し、子のオブジェクトがゲーム画面の中央にくるように配置する
        transform.localPosition = new(-sizeX / 2, 0, -sizeZ / 2);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cube"></param>
    private void SetUV(GameObject cube) {
        // 代入した変数では上書きできないので、毎回 GetComponent する
        //Vector2[] uv = cube.GetComponent<MeshFilter>().mesh.uv;
        cube.GetComponent<MeshFilter>().mesh.uv = GetBlockUVs(2, 15);  // 草

        if (cube.transform.position.y > sizeY * 0.3f) {
            cube.GetComponent<MeshFilter>().mesh.uv = GetBlockUVs(0, 15);  // 山
        } else if (cube.transform.position.y > sizeY * 0.2f) {
            cube.GetComponent<MeshFilter>().mesh.uv = GetBlockUVs(1, 2);  // 水面
        } else if (cube.transform.position.y > sizeY * 0.1f) {
            cube.GetComponent<MeshFilter>().mesh.uv = GetBlockUVs(15, 0); // 溶岩
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tileX"></param>
    /// <param name="tileY"></param>
    /// <returns></returns>
    private Vector2[] GetBlockUVs(float tileX, float tileY) {
        float pixelSize = 16;
        float tilePerc = 1 / pixelSize;

        float umin = tilePerc * tileX;
        float umax = tilePerc * (tileX + 1);
        float vmin = tilePerc * tileY;
        float vmax = tilePerc * (tileY + 1);

        Vector2[] blockUVs = new Vector2[24];

        // -X
        blockUVs[2] = new(umax, vmax);
        blockUVs[3] = new(umin, vmax);
        blockUVs[0] = new(umax, vmin);
        blockUVs[1] = new(umin, vmin);

        // +Y
        blockUVs[4] = new(umin, vmin);
        blockUVs[5] = new(umax, vmin);
        blockUVs[8] = new(umin, vmax);
        blockUVs[9] = new(umax, vmax);

        // -Z
        blockUVs[23] = new(umax, vmin);
        blockUVs[21] = new(umin, vmax);
        blockUVs[20] = new(umin, vmin);
        blockUVs[22] = new(umax, vmax);

        // +Z
        blockUVs[19] = new(umax, vmin);
        blockUVs[17] = new(umin, vmax);
        blockUVs[16] = new(umin, vmin);
        blockUVs[18] = new(umax, vmax);

        // +Y
        blockUVs[15] = new(umax, vmin);
        blockUVs[13] = new(umin, vmax);
        blockUVs[12] = new(umin, vmin);
        blockUVs[14] = new(umax, vmax);

        // +X
        blockUVs[6] = new(umin, vmin);
        blockUVs[7] = new(umax, vmin);
        blockUVs[10] = new(umin, vmax);
        blockUVs[11] = new(umax, vmax);

        return blockUVs;
    }
}
