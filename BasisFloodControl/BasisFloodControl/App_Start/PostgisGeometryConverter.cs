﻿//using Npgsql;
//using NpgsqlTypes;

using System.Data;
using ServiceStack.OrmLite;

namespace BasisFloodControl
{
    public class SqlGeometryConverter : OrmLiteConverter
    {
        public override string ColumnDefinition
        {
            get { return "geometry"; }
        }

        public override DbType DbType
        {
            get { return DbType.Object; }
        }

        //public override void InitDbParam(IDbDataParameter p, Type fieldType)
        //{
        //    ((NpgsqlParameter)p).NpgsqlDbType = NpgsqlDbType.Geometry;
        //}
    }
}