using Lvr_Land_Maker.Models.Configuartion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvr_Land_Maker
{
    public class PropertyConfigTool
    {
        private static string SQL_SELECT = @"SELECT 
	                                    g.GroupPropertyId
	                                    ,g.GroupPropertyName
	                                    ,g.GroupPropertyNameCH
	                                    ,p.PropertyId
	                                    ,p.PropertyName
	                                    ,p.PropertyNameCH
                                    FROM LvrLand.dbo.LvrLandProperty as p
                                    INNER JOIN LvrLand.dbo.GroupProperty as g
	                                    ON p.GroupPropertyId = g.GroupPropertyId
                                    Order by p.GroupPropertyId, p.PropertyId";


        public static void Create()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = SQL_SELECT;
            List<LvrLandProperties> result = GetCollectionFromDataBase<LvrLandProperties>(cmd);

            if (result == null)
            {
                throw new Exception("取得Property出錯。");
            }

            AttributeConfig config = new AttributeConfig();
            config.Attributes = new List<LvrLandAttribute>();
            foreach (var group in result.GroupBy(r => r.GroupPropertyId))
            {
                LvrLandAttribute tmpAttr = new LvrLandAttribute
                {
                    GroupId = group.First().GroupPropertyId,
                    Name = group.First().GroupPropertyName,
                    Name_CH = group.First().GroupPropertyNameCH,
                    Items = new List<Item>()
                };

                foreach (var item in group)
                {
                    tmpAttr.Items.Add(new Item
                    {
                        GroupId = item.GroupPropertyId,
                        Name = item.PropertyName,
                        Name_CH = item.PropertyNameCH,
                        PropertyId = item.PropertyId
                    });
                }

                config.Attributes.Add(tmpAttr);
            }

            SaveXml<AttributeConfig>(config, string.Empty, @"Configuration\Attribute.Config");
        }

        private static List<T> GetCollectionFromDataBase<T>(SqlCommand cmd)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = @"server=TON-PC\SQLEXPRESS;database=LvrLand;uid=tong;pwd=123456";
                ////conn.ConnectionString = ConfigurationManager.ConnectionStrings["Lvr_Land"].ConnectionString;
                try
                {
                    cmd.Connection = conn;
                    conn.Open();

                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());

                    return BindDataCollection<T>(dt);
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    return null;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private static T GetObjectFromDataBase<T>(SqlCommand cmd)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = @"server=TON-PC\SQLEXPRESS;database=LvrLand;uid=tong;pwd=123456";
                ////conn.ConnectionString = ConfigurationManager.ConnectionStrings["Lvr_Land"].ConnectionString;
                try
                {
                    cmd.Connection = conn;
                    conn.Open();

                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());

                    return BindData<T>(dt);
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    return default(T);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private static List<T> BindDataCollection<T>(DataTable dt)
        {
            List<T> result = new List<T>();

            foreach (DataRow dr in dt.Rows)
            {
                // Get all columns' name
                List<string> columns = new List<string>();
                foreach (DataColumn dc in dt.Columns)
                {
                    columns.Add(dc.ColumnName);
                }

                // Create object
                var ob = Activator.CreateInstance<T>();

                // Get all fields
                var fields = typeof(T).GetFields();
                foreach (var fieldInfo in fields)
                {
                    if (columns.Contains(fieldInfo.Name))
                    {
                        // Fill the data into the field
                        fieldInfo.SetValue(ob, dr[fieldInfo.Name]);
                    }
                }

                // Get all properties
                var properties = typeof(T).GetProperties();
                foreach (var propertyInfo in properties)
                {
                    if (columns.Contains(propertyInfo.Name))
                    {
                        // Fill the data into the property
                        propertyInfo.SetValue(ob, dr[propertyInfo.Name]);
                    }
                }

                result.Add(ob);
            }

            return result;
        }

        private static T BindData<T>(DataTable dt)
        {
            DataRow dr = dt.Rows[0];

            // Get all columns' name
            List<string> columns = new List<string>();
            foreach (DataColumn dc in dt.Columns)
            {
                columns.Add(dc.ColumnName);
            }

            // Create object
            var ob = Activator.CreateInstance<T>();

            // Get all fields
            var fields = typeof(T).GetFields();
            foreach (var fieldInfo in fields)
            {
                if (columns.Contains(fieldInfo.Name))
                {
                    // Fill the data into the field
                    fieldInfo.SetValue(ob, dr[fieldInfo.Name]);
                }
            }

            // Get all properties
            var properties = typeof(T).GetProperties();
            foreach (var propertyInfo in properties)
            {
                if (columns.Contains(propertyInfo.Name))
                {
                    // Fill the data into the property
                    propertyInfo.SetValue(ob, dr[propertyInfo.Name]);
                }
            }

            return ob;
        }

        private static void SaveXml<T>(T obj, string path, string fileName)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using (TextWriter writer = new StreamWriter(Path.Combine(path, fileName)))
            {
                serializer.Serialize(writer, obj);
            } 
        }
    }

    class LvrLandProperties
    {
        public int GroupPropertyId { get; set; }

        public string GroupPropertyName { get; set; }

        public string GroupPropertyNameCH { get; set; }

        public int PropertyId { get; set; }

        public string PropertyName { get; set; }

        public string PropertyNameCH { get; set; }
    }


}
