using UnityEngine;

public class PlayerModel
{
    private string _name { get; set; }
    private int _str { get; set; }
    private int _dex { get; set; }
    private int _int { get; set; }
    private int _luk { get; set; }

    public PlayerModel(string name, int str, int dex, int Int, int luk)
    {
        _name = name;
        _str = str;
        _dex = dex;
        _int = Int;
        _luk = luk;
    }
}
