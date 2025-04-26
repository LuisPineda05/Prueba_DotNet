using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class BaseRepository
{
    protected readonly IDbConnection _connection;

    public BaseRepository(IDbConnection connection)
    {
        _connection = connection;
    }
}
