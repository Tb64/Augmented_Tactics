#pragma strict
var cam : Transform;
var Head : Transform;
var Hight = 0.1;

function Start () {

}

function Update () {
//Head.transform.position = Head.transform.position + Vector3(0,Hight,0);
cam.LookAt(Head);
}