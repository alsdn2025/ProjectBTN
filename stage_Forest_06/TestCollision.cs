using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Occur. \n" 
            + $"Opponent Object : {collision.gameObject.name}");
    }

    private void OnTriggerEnter(Collider other)
    {
        string opponentName = other.gameObject.name; 
        Debug.Log("Trigger Occur \n"
            + $"Opponent Object : {opponentName}");

        // 트리거로 텔레포트 기능을 구현해보자
        if(opponentName.StartsWith("Teleport"))
        {
            string gateNumber = opponentName.Substring(opponentName.Length-1);
            if (gateNumber == "1")
            {
                GameObject gate = GameObject.Find("Teleport_2");
                this.transform.position = gate.transform.position + gate.transform.TransformDirection(Vector3.forward);
            }
            if (gateNumber == "2")
            {
                GameObject gate = GameObject.Find("Teleport_1");
                this.transform.position = gate.transform.position + gate.transform.TransformDirection(Vector3.forward);
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        // Screen 좌표
        // Debug.Log(Input.mousePosition);
        //Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition));

        // 왼쪽 마우스 버튼 클릭시
        if (Input.GetMouseButtonDown(0)) {

            //Vector3 ScreenPos = new Vector3(
            //        Input.mousePosition.x,
            //        Input.mousePosition.y,
            //        Camera.main.nearClipPlane // z값은 카메라와 화면사이의 거리
            //        );
            //// 마우스의 스크린상 위치를 World 좌표로 변환
            //Vector3 mousePos = Camera.main.ScreenToWorldPoint(ScreenPos);

            //Vector3 dir = mousePos - Camera.main.transform.position;
            //dir = dir.normalized; // (카메라 -> 평면에 찍은 점)의 방향벡터


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(Camera.main.transform.position, ray.direction*100.0f, Color.red, 2.0f);

            //int mask = (1 << 8) | (1 << 9);
            LayerMask mask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall");

            if(Physics.Raycast(ray, out hit, 100.0f, mask))
            {
                Debug.Log($"RayCast Camera @ {hit.collider.gameObject.tag}");
            }



            //// 레이캐스트
            //Debug.DrawRay(Camera.main.transform.position, dir*100.0f, Color.red, 3.0f);
            //if (Physics.Raycast(Camera.main.transform.position, dir, out hit))
            //{
            //    Debug.Log($"RayCast Camera @ {hit.collider.gameObject.name}");
            //}

        }




    }


/*    void Update()
    {
        Vector3 lookDir = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position + Vector3.up, lookDir * 5, Color.red);


        RaycastHit[] hits = Physics.RaycastAll(transform.position + Vector3.up, lookDir, 5);
        foreach (RaycastHit h in hits)
        {
            Debug.Log($"RayCast {h.collider.gameObject.name}");
        }

        Debug.Log(Input.mousePosition);
    }*/
}
