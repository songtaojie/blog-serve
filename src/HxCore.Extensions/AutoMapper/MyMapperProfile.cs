using AutoMapper;
using Hx.Sdk.Entity.Dependency;
using Hx.Sdk.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HxCore.Extensions.AutoMapper
{
    /// <summary>
    /// automapper配置文件
    /// </summary>
    internal class MyMapperProfile : Profile
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MyMapperProfile(IEnumerable<Assembly> assemblyList)
        {
            if (assemblyList != null && assemblyList.Count() > 0)
            {
                Type mapperType = typeof(IAutoMapper<>);
                foreach (var assembly in assemblyList)
                {
                    try
                    {
                        var types = assembly.GetExportedTypes().Where(t => t.IsClass && !t.IsAbstract);
                        foreach (Type sourceType in types)
                        {
                            var genericTypes = sourceType.GetGenericInterfaces(mapperType);
                            if (genericTypes != null && genericTypes.Count() > 0)
                            {
                                foreach (Type t in genericTypes)
                                {
                                    Type destType = t.GetGenericElementType();
                                    if (destType.IsClass)
                                    {
                                        CreateMap(sourceType, destType);
                                        CreateMap(destType, sourceType);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception) { }
                }
            }
        }
    }

}
