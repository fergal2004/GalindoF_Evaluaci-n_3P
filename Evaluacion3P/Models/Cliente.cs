using SQLite;

namespace Evaluacion3P.Models;

public class Cliente
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }

    public string Nombre { get; set; }
    public string Empresa { get; set; }
    public int AntiguedadMeses { get; set; }
    public bool Activo { get; set; }

    [Ignore]
    public int AntiguedadDias => AntiguedadMeses * 10;
}