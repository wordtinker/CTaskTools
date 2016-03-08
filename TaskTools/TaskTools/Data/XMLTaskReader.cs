﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TaskTools.Models;

namespace TaskTools.Data
{
    class XMLTaskReader : TaskReader
    {
        private string fileName;
        private XDocument xDoc;
        private int lastId;

        public override string Extension
        {
            get
            {
                return "XML task files (*.xtsk)|*.xtsk";
            }
        }

        public override List<TDTask> GetFinishedTasks()
        {
            try
            {
                IEnumerable<TDTask> tasks =
                    xDoc.Root.Element("Tasks").Elements("TDTask")
                    .Select(t => t.FromXElement<TDTask>())
                    .Where(t => t.Completed == true ||
                           t.ValidTill?.Date < DateTime.Today);
                return tasks.ToList();
            }
            catch (Exception)
            {
                return new List<TDTask>();
            }
        }

        public override List<TDTask> GetTasks()
        {
            try
            {
                IEnumerable<TDTask> tasks =
                    xDoc.Root.Element("Tasks").Elements("TDTask")
                    .Select(t => t.FromXElement<TDTask>())
                    .Where(t => (t.Completed == false && (t.ValidTill?.Date >= DateTime.Today || t.ValidTill == null)) ||
                                (t.Finish?.Date == DateTime.Today));
                return tasks.ToList();
            }
            catch (Exception)
            {
                return new List<TDTask>();
            }
        }

        public override bool InitializeFile(string fileName)
        {
            try
            {
                XDocument tree = new XDocument(
                    new XElement("Root",
                        new XElement("LastId", 0),
                        new XElement("Tasks")
                        )
                    );
                tree.Save(fileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool LoadFile(string fileName)
        {
            try
            {
                this.fileName = fileName;
                xDoc = XDocument.Load(fileName);
                lastId = int.Parse(xDoc.Root.Element("LastId").Value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool SaveTask(TDTask newTask)
        {
            try
            {
                lastId++;
                newTask.Id = lastId;
                xDoc.Root.Element("LastId").SetValue(lastId);

                XElement elem = newTask.ToXElement<TDTask>();
                xDoc.Root.Element("Tasks").Add(elem);

                xDoc.Save(fileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private XElement GetElementById(int id)
        {
            XElement selectedElem =
                (from t in xDoc.Root.Element("Tasks").Elements("TDTask").Elements("Id")
                 where t.Value == id.ToString()
                 select t.Parent).FirstOrDefault();
            return selectedElem;
        }

        public override bool UpdateTask(TDTask task)
        {
            try
            {
                XElement selectedElem = GetElementById(task.Id.GetValueOrDefault());
                selectedElem.ReplaceWith(task.ToXElement<TDTask>());
                xDoc.Save(fileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool DeleteTask(TDTask task)
        {
            try
            {
                XElement selectedElem = GetElementById(task.Id.GetValueOrDefault());
                selectedElem.Remove();
                xDoc.Save(fileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
