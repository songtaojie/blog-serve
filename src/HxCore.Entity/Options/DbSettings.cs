namespace HxCore.Options
{
    /// <summary>
    /// 数据库连接设置
    /// </summary>
    public class DbSettings
    {
        /// <summary>
        /// 连接ID
        /// </summary>
        public string ConnId { get; set; }
        /// <summary>
        /// 连接ID
        /// </summary>
        public DbSettings_DbType? DbType { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 是否开启sql日志记录
        /// </summary>
        public bool EnableLog { get; set; }
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

    }
    /// <summary>
    /// 连接数据库的类型
    /// </summary>
    public enum DbSettings_DbType
    {
        /// <summary>
        /// 使用MySql数据库
        /// </summary>
        MySql = 0,
        /// <summary>
        /// 使用SqlServer数据库
        /// </summary>
        SqlServer = 1,
        /// <summary>
        /// 使用Oracle数据库
        /// </summary>
        Oracle = 2,
        /// <summary>
        /// 使用Sqlite数据库
        /// </summary>
        Sqlite = 3,
        /// <summary>
        /// 使用PostgreSQL数据库
        /// </summary>
        PostgreSQL = 4
    }
}
