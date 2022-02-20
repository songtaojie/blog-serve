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
&nbsp;
&nbsp;

**微服务模块：**
- 可配合 Docker 实现容器化；
- 可配合 Jenkins 实现CI / CD；
- 可配合 Consul 实现服务发现；
- 可配合 Ocelot 实现网关处理；
- 可配合 Nginx  实现负载均衡；
- 可配合 Ids4   实现认证中心；


本项目是 .netCore 后端接口部分，前端部分请看另外几个项目
 
&nbsp;
&nbsp;

|个人博客Vue版本|VueAdmin权限管理后台|
|-|-|-|
|[https://gitee.com/songtaojie/blog-client](https://gitee.com/songtaojie/blog-client)|[https://gitee.com/songtaojie/blog-admin](https://gitee.com/songtaojie/blog-admin)|


&nbsp;

### 初始化项目
 

下载项目后，编译如果没问题，直接运行即可，会自动生成种子数据，数据库是`MySql`，接口文档是`swagger`。    




