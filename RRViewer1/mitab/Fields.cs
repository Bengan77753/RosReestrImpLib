﻿// $Id: Fields.cs,v 1.2 2005/03/24 17:02:06 dmorissette Exp $
//

using System.Collections.Generic;
using System.Text;

namespace MITAB
{
    /// <summary>Contains the set of fields belonging to a layer.</summary>
    /// <remarks>
    /// This class descends EnumImpl, meaning the fields in the
    /// set can be iterated using foreach.
    /// It also has an index property allowing any field between 0 and Fields.Count-1
    /// to be accessed directly with Fields[idx]
    /// </remarks>
    public class Fields : EnumImpl
    {
        private List<Field> fields = new List<Field>();

        /// <summary>Конструктор</summary>
        /// <param name="layer">Слой</param>
        protected internal Fields(MiLayer layer) : base(MiApi.mitab_c_get_field_count(layer.Handle))
        {
            for (int i = 0; i < Count; i++) fields.Add(CreateField(layer, i));
        }

        /// <summary>Override this to support descendants of the Field class.</summary>
        /// <returns>A Field, with the given index, belonging to the given Layer</returns>
        protected internal virtual Field CreateField(MiLayer layer, int index)
        {
            return new Field(layer, index);
        }

        /// <summary>Добавить поле</summary>
        /// <param name="layer">Слой</param>
        /// <param name="fieldName">Имя поля</param>
        /// <param name="fieldType">Тип поля</param>
        /// <param name="width">Ширина</param>
        /// <param name="precision">Точность</param>
        /// <param name="indexed"></param>
        /// <param name="unique"></param>
        public void AddField(MiLayer layer, 
            string fieldName, FieldType fieldType, int width, int precision, int indexed, int unique)
        {            //
            fields.Add(new Field(layer, fieldName, fieldType, width, precision, indexed, unique));
        }

        /// <summary>Получение поля по индексу</summary>
        /// <param name="index">Индекс</param>
        /// <returns></returns>
        public virtual Field this[int index] => index < Count ? fields[index] : null;

        /// <inheritdoc />
        public override object GetObj(int idx)
        {
            return this[idx];
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var str = new StringBuilder();
            str.Append("Columns:\n");
            foreach (Field field in this) str.Append($"{field}\t");
            return str.ToString();
        }
    }
}