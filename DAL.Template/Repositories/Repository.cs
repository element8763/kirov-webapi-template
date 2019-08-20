using DAL.Template.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
using Extension.Template;
using NAutowired.Core.Attributes;
using Snowflake.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Template.Repositories
{
    public abstract class Repository<TEntity> where TEntity : Entity, new()
    {

        [Autowired]
        protected readonly IdWorker idWorker;
        //默认60S的数据库执行超时时间
        private readonly int commandTimeout = 60;

        /// <summary>
        /// 
        /// </summary>
        [Autowired]
        protected DbConnectionFactory dbConnectionFactory;

        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName => GetTableName(typeof(TEntity)) ?? typeof(TEntity).Name.ToSnakeCase();


        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="id">实体Id</param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            return this.dbConnectionFactory.Connection.Delete(new TEntity { id = id });
        }

        /// <summary>
        /// 根据ID集合删除所有符合条件的对象
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool Delete(IList<long> ids)
        {
            if (ids == null || !ids.Any())
            {
                return true;
            }
            string sql = $"delete from {TableName} where id = ANY(@ids)";
            var parameters = new { ids = ids };
            return this.dbConnectionFactory.Connection.Execute(sql, parameters) > 0;
        }

        /// <summary>
        /// 根据Id获取对象
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>对象</returns>
        public TEntity Get(long id)
        {
            string sql = $"select * from {TableName} where id = @id and delete_at is null";
            var parameters = new { id = id };
            return this.dbConnectionFactory.Connection.QueryFirstOrDefault<TEntity>(sql, parameters);
        }

        /// <summary>  
        /// 获取表名  
        /// </summary>  
        /// <param name="type"></param>  
        /// <returns></returns>  
        public static string GetTableName(Type type)
        {
            string tableName = null;
            object[] attributes = type.GetCustomAttributes(false);
            foreach (var attr in attributes)
            {
                if (attr is TableAttribute)
                {
                    TableAttribute tableAttribute = attr as TableAttribute;
                    tableName = tableAttribute.Name;
                    break;
                }
            }
            return tableName;
        }
    }
}
