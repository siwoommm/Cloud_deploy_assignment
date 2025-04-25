using UnityEngine;

public class QRCodeParser
{
    public static QRCodeInfo Parse(string qrText)
    {
        QRCodeInfo info = new QRCodeInfo();

        string[] parts = qrText.Split(';');
        foreach (string part in parts)
        {
            if (part.StartsWith("ID="))
                info.ID = part.Substring(3);

            else if (part.StartsWith("pos="))
            {
                string coordText = part.Substring(4).Trim('(', ')'); // "(x,y)" → "x,y"
                string[] coords = coordText.Split(',');
                if (coords.Length == 2 &&
                    float.TryParse(coords[0], out float x) &&
                    float.TryParse(coords[1], out float y))
                {
                    info.Position = new Vector2(x, y);
                }
            }

            else if (part.StartsWith("angle="))
                float.TryParse(part.Substring(6), out info.Angle);

            else if (part.StartsWith("floor="))
                int.TryParse(part.Substring(6), out info.Floor);

            else if (part.StartsWith("node="))
                info.Node = part.Substring(5);

            else if (part.StartsWith("zone="))
                info.Zone = part.Substring(5);

            else if (part.StartsWith("type="))
                info.Type = part.Substring(5);
        }

        return info;
    }
}
