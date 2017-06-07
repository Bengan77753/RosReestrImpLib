﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace RosReestrImp.Rule
{
    /// <summary>
    /// Правила схемы, элемент Shema
    /// </summary>
    internal class ShemaRule
    {
        /// <summary>
        /// Имя схемы
        /// </summary>
        internal string _rootElem; 

        /// <summary>
        /// Список слоёв (правил)
        /// </summary>
        internal List<LayerRule> _LayerList;

        /// <summary>
        /// Загружаем правила схемы
        /// </summary>
        /// <param name="wEl"> Элемент Shema xml-файла правил </param>
        /// <exception cref="RuleLoadException"> Ошибка xml-файла правил </exception>
        internal ShemaRule(XmlElement wEl)
        {
            if (wEl.HasAttribute("rootElem"))
            {
                this._rootElem = wEl.GetAttribute("rootElem");
                this._LayerList = new List<LayerRule>();
                foreach (XmlElement ch in wEl.ChildNodes)
                {
                    this._LayerList.Add(new LayerRule(ch));
                }
            }
            else
            {
                throw new RuleLoadException("У Shema нет rootElem");
            }
        }

        /// <summary>
        /// Загружаем данные слоя
        /// </summary>
        /// <param name="wDoc"> xml-файл с данными </param>
        /// <param name="wRule"> правила слоя </param>
        /// <returns></returns>
        internal Data.Layer LoadData(XmlDocument wDoc, LayerRule wRule)
        {
            Data.Layer res = new Data.Layer(wRule);
            res.LoadData(wDoc);
            return res;
        }

        /// <summary>
        /// Загружаем данные схемы
        /// </summary>
        /// <param name="wDoc"> xml-файл с данными </param>
        /// <returns></returns>
        internal List<Data.Layer> LoadData(XmlDocument wDoc)
        {
            List<Data.Layer> res = new List<Data.Layer>();
            foreach (LayerRule r in this._LayerList)
            {
                res.Add(this.LoadData(wDoc, r));
            }
            return res;
        }

    }

}
