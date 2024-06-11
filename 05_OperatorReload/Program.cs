Vector2 v1 = new Vector2(1, 1);
Vector2 v2 = new Vector2(2, 1);
Vector2 v3 = new Vector2(2, 1);
Console.WriteLine(v1 == v2);
Console.WriteLine(v3 == v2);
class Vector2
{
    public int x, y;

    public Vector2(int x, int y )
    {
        this.x = x;
        this.y = y;
    }

    public static bool operator ==( Vector2 a, Vector2 b)
    {
        if(a.x == b.x && a.y == b.y) { return true; }
        return false;
    }

    public static bool operator !=(Vector2 a, Vector2 b)
    {
        if (a.x != b.x || a.y != b.y) { return false; }
        return true;
    }
}