using System;
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
            // TODO
            throw new NotImplementedException();
        }

        public override List<TDTask> GetTasks()
        {
            // TODO make it actual
            try
            {
                IEnumerable<XElement> elements = xDoc.Root.Element("Tasks").Elements("TDTask");
                IEnumerable<TDTask> tasks = elements.Select(p => p.FromXElement<TDTask>());
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

        public override bool UpdateTask(TDTask task)
        {
            try
            {
                XElement selectedElem =
                (from t in xDoc.Root.Element("Tasks").Elements("TDTask").Elements("Id")
                 where t.Value == task.Id.ToString()
                 select t.Parent).FirstOrDefault();
                selectedElem.ReplaceWith(task.ToXElement<TDTask>());

                xDoc.Save(fileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override void DeleteTask(TDTask task)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
