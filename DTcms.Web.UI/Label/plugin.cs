using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DTcms.Web.UI
{
    public partial class BasePage : System.Web.UI.Page
    {
        /// <summary>
        /// 利用反射调用插件方法
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        /// <param name="objParas">参数</param>
        /// <returns>DataTable</returns>
        public DataTable get_plugin_method(string assemblyName, string className, string methodName, params object[] objParas)
        {
            DataTable dt = new DataTable();
            try
            {
                Assembly assembly = Assembly.Load(assemblyName);
                object obj = assembly.CreateInstance(assemblyName + "." + className);
                Type t = obj.GetType();
                //查找匹配的方法
                foreach (MethodInfo m in t.GetMethods(BindingFlags.Instance | BindingFlags.Public))
                {
                    if (m.Name == methodName && m.GetParameters().Length == objParas.Length)
                    {
                        object obj2 = m.Invoke(obj, objParas);
                        dt = obj2 as DataTable;
                        return dt;
                    }
                }
            }
            catch
            {
                //插件方法获取失败
            }
            return dt;
        }
    }
}
