# blog-server

#### 介绍
基于.Net5的博客api，主要包含功能有：博客的创建发布，公告/时间轴等发布，后台用户角色权限管理，

#### 软件模块
- **使用Mysql作为数据库** 
- **使用EF Core+CodeFirst模式进行数据库的迁移映射，以及数据的添加修改，SqlSugur进行数据的查询**
- **使用redis作为缓存** 
- **使用Swagger做API接口文档** 
- **使用Automapper处理对象映射** 
- **使用AutoFac进行依赖注入**，没有使用.NetCore自带的依赖注入是因为还是用了Aop，所以直接使用AotoFac方便些
- **使用NLog的日志框架** ，集成原生 ILogger 接口做日志记录
- **使用MiniProfiler进行接口性能分析** 
- **使用NPOI进行Excel导出** 
- **使用 RabbitMQ 消息队列**
- **支持 CORS 跨域；**
- **使用JWT 策略授权**，也可以使用IdentityServer4进行授权



#### 使用说明

1.  拉取/下载代码，编译代码
2.  修改配置文件App.config中数据库配置即connectionStrings节点中的值
3.  直接运行编译后生成exe即可打开程序（应用程序启动时会自动执行迁移文件生成数据库，并生成种子数据）
4.  内置登录密码为123456
#### 基本功能展示


