using UnityEngine;

public class QRCodeInfo
{
    public string ID;
    public Vector2 Position;
    public float Angle;
    public int Floor;
    public string Node;
    public string Zone;
    public string Type;

    public override string ToString()
    {
        return $"ID={ID}, pos={Position}, angle={Angle}, floor={Floor}, node={Node}, zone={Zone}, type={Type}";
    }
}
