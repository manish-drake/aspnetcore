using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public enum enumDBTransaction
    {
        spSelect = 0,
        spAdd = 1,
        spEdit = 2,
        spDelete = 3,
        spNull = -1
    }
    public interface IDBObject
    {
        string Connection { get; }

        //Identity Identity { get; }

        String Transaction(enumDBTransaction argTransactionType);

        String Transaction(enumDBTransaction argTransactionType, SqlTransaction argTransaction);

        DataTable Items();

    }
}
