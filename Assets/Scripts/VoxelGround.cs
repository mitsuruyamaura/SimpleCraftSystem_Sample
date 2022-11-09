using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �yUnity�z�}�C���N���t�g���̒n�`�����STYLY�փA�b�v���[�h����s�O�ҁt
// https://styly.cc/ja/tips/texturedvoxel_iwase_minecraft/


public class VoxelGround : MonoBehaviour
{
    private float sizeX = 50.0f;  // X ����ɕ��ׂ�u���b�N�̐�
    private float sizeY = 10.0f;  // �n�`�̍ő�̍���
    private float sizeZ = 50.0f;  // Z ����ɕ��ׂ�u���b�N�̐�
    private float sizeW = 17.0f;  // �N���̃T�C�Y(�R�̒��ォ�畽�n�܂ł̋����B17�u���b�N�łP�̎R����邽�߁A50�u���b�N���ɂ����悻�R�̎R���o����v�Z)
                                  // ���������l�ɂ���قǋN���̌������n�`�ɂȂ�


    void Awake()
    {
        var material = GetComponent<MeshRenderer>().material;

        // 50 * 50 ��2500�̃u���b�N�𐶐�
        for (float x = 0; x < sizeX; x++) {
            for (float z = 0; z < sizeZ; z++ ) {

                // �u���b�N����
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.SetParent(transform);

                cube.GetComponent<MeshRenderer>().material = material;

                // �p�[�����m�C�Y�̈����͈̔͂� 0 �` 1 �̊�
                // X�AZ ���ɉ����Ċ��炩�ɕω����鐔�l�𐶐�(PerlinNoise �𗘗p���邱�ƂŃu���b�N�̍��������炩�ɕω�������)
                float noise = Mathf.PerlinNoise(x / sizeW, z / sizeW);

                // �����������l���u���b�N�� Y ���̍���(�ʒu)�ɐݒ�
                float y = Mathf.Round(sizeY * noise);

                // �u���b�N�̈ʒu��ݒ�
                cube.transform.localPosition = new(x, y, z);

                SetUV(cube);
            }
        }

        // �e�̃I�u�W�F�N�g���ړ����A�q�̃I�u�W�F�N�g���Q�[����ʂ̒����ɂ���悤�ɔz�u����
        transform.localPosition = new(-sizeX / 2, 0, -sizeZ / 2);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cube"></param>
    private void SetUV(GameObject cube) {
        // ��������ϐ��ł͏㏑���ł��Ȃ��̂ŁA���� GetComponent ����
        //Vector2[] uv = cube.GetComponent<MeshFilter>().mesh.uv;
        cube.GetComponent<MeshFilter>().mesh.uv = GetBlockUVs(2, 15);  // ��

        if (cube.transform.position.y > sizeY * 0.3f) {
            cube.GetComponent<MeshFilter>().mesh.uv = GetBlockUVs(0, 15);  // �R
        } else if (cube.transform.position.y > sizeY * 0.2f) {
            cube.GetComponent<MeshFilter>().mesh.uv = GetBlockUVs(1, 2);  // ����
        } else if (cube.transform.position.y > sizeY * 0.1f) {
            cube.GetComponent<MeshFilter>().mesh.uv = GetBlockUVs(15, 0); // �n��
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
