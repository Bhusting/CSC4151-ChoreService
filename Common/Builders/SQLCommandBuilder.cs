﻿using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Common.Builders
{
    public class SqlCommandBuilder
    {

        public static string GetRecords(Type type)
        {
            return $"SELECT * FROM {type.Name}";
        }

        public static string GetRecordsByField(Type type, Type fieldType, string fieldName, string value)
        {            
            if (fieldType == typeof(string) || fieldType == typeof(Guid))
                value = $"'{value}'";

            return $"SELECT * FROM {type.Name} WHERE {fieldName} = {value}";
        }
        
        public static string GetIndividualRecordBuilder(Type type, Guid id)
        {
            return $"SELECT * FROM {type.Name} WHERE {type.Name}Id = \'{id}\'";
        }

        public static string GetIndividualRecordFromNameBuilder(Type type, string name)
        {
            return $"SELECT * FROM {type.Name} WHERE {type.Name}Name = \"{name}\"";
        }

        public static string GetIndividualRecordFromIdBuilder(Type type, Type typeToRetrieve, Guid id)
        {
            return $"SELECT * FROM {type.Name} WHERE {typeToRetrieve.Name}Id = \'{id}\'";
        }

        public static string GetIndividualRecordFromNameBuilder(Type type, Type typeToRetrieve, string name)
        {
            return $"SELECT * FROM {type.Name} WHERE {typeToRetrieve.Name}Name = {name}";
        }

        public static string GetIndividualParentFromId(Type type, Type typeToRetrieve, Guid id)
        {
            return $"SELECT * FROM {typeToRetrieve.Name} WHERE {typeToRetrieve.Name}Id = (SELECT {typeToRetrieve.Name}Id FROM {type.Name} WHERE {type.Name}Id = \'{id}\')";
        }

        public static string GetIndividualParentFromName(Type type, Type typeToRetrieve, string name)
        {
            return $"SELECT * FROM {typeToRetrieve.Name} WHERE {typeToRetrieve.Name}Id = (SELECT {typeToRetrieve.Name}Id FROM {type.Name} WHERE {type.Name}Name = \"{name}\")";
        }

        public static string GetLike(Type type, string phrase)
        {
            return $"SELECT * FROM {type.Name} WHERE {type.Name}Name LIKE \"%{phrase}%\";";
        }

        public static string InsertRecord<T>(T obj)
        {

            var str = new StringBuilder($"INSERT INTO {obj.GetType().Name} (");

            var props = obj.GetType().GetProperties();

            foreach (var prop in props)
            {
                // As discussed that we need to pass the ID manually
                //if (!prop.Name.Contains($"{obj.GetType().Name}Id"))
                //{
                    str.Append($"{prop.Name},");
                //}
            }
            str.Remove(str.Length - 1, 1);
            str.Append(") VALUES (");

            foreach (var prop in props)
            {
                //if (prop.Name != $"{obj.GetType().Name}Id")
                //{
                    var value = prop.GetValue(obj);
                    if (value.GetType() == typeof(string) || value.GetType() == typeof(Guid) || value.GetType() == typeof(DateTime))
                        str.Append($"'{value.ToString()}',");
                    else
                        str.Append($"{value.ToString()},");
                //}
            }
            str.Remove(str.Length - 1, 1);
            str.Append(");");

            return str.ToString();
        }

        public static string DeleteRecord(Type type, Guid id)
        {
            var props = type.GetProperties();

            foreach (var prop in props)
            {
                if (prop.Name.Contains($"{type.Name}Id"))
                {
                    return $"DELETE FROM {type.Name} WHERE {prop.Name}='{id.ToString()}'";
                }
            }
            return null;
        }

        public static string UpdateRecord(Type type, Guid id, string fieldName, string value)
        {

            var str =$"UPDATE {type.Name} SET {fieldName} = '{value}' WHERE ChoreId ='{id.ToString()}'";
            return str; 
        }

        //public static string UpdateRecord<T>(T obj)
        //{

        //    var str=  $"UPDATE {obj.GetType().Name} SET ChoreName = {obj.GetType().Name} WHERE ChoreId = {obj.GetType().Name}Id ";
        //    return str; 
        //}

           //var str = new StringBuilder($"UPDATE {obj.GetType().Name} ");
           // var strWhere = new StringBuilder($" WHERE {obj.GetType().Name }Id = ");

           // str.Append($"SET ");
           //var props = obj.GetType().GetProperties();

           // foreach (var prop in props)
           // {
           //     var value = prop.GetValue(obj);
           //     if (value.GetType() == typeof(string) || value.GetType() == typeof(Guid) || value.GetType() == typeof(DateTime))
           //         value = $"'{value.ToString()}',";
           //     else
           //         value = $"{value.ToString()},";

           //     if (prop.Name.Contains($"{obj.GetType().Name}Id"))
           //     {
           //         strWhere.Append($"{value};");                   
           //     }
           //     else
           //     {
           //         str.Append($"{prop.Name} = {value},");
           //     }
           // }
           // str.Remove(str.Length - 1, 1);
           // str.Append(strWhere);            

           // return str.ToString();
        
    }

}
