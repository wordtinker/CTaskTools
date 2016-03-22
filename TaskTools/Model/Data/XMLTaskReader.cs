using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TaskTools.Models;

namespace TaskTools.Data
{
    public class XMLTaskReader : TaskReader
    {
        private string fileName;
        private XDocument xDoc;
        private int lastId;
        private int lastRoutineId;

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
                        new XElement("TaskId", 0),
                        new XElement("RoutineId", 0),
                        new XElement("Tasks"),
                        new XElement("Routines")
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
                lastId = int.Parse(xDoc.Root.Element("TaskId").Value);
                lastRoutineId = int.Parse(xDoc.Root.Element("RoutineId").Value);
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
                xDoc.Root.Element("TaskId").SetValue(lastId);

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

        private XElement GetTaskElementById(int id)
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
                XElement selectedElem = GetTaskElementById(task.Id.GetValueOrDefault());
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
                XElement selectedElem = GetTaskElementById(task.Id.GetValueOrDefault());
                selectedElem.Remove();
                xDoc.Save(fileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override List<Routine> GetRoutines()
        {
            try
            {
                IEnumerable<Routine> routines =
                    xDoc.Root.Element("Routines").Elements("Routine")
                    .Select(t => t.FromXElement<Routine>());
                return routines.ToList();
            }
            catch (Exception)
            {
                return new List<Routine>();
            }
        }

        public override bool SaveRoutine(Routine newRoutine)
        {
            try
            {
                lastRoutineId++;
                newRoutine.Id = lastRoutineId;
                xDoc.Root.Element("RoutineId").SetValue(lastRoutineId);

                XElement elem = newRoutine.ToXElement<Routine>();
                xDoc.Root.Element("Routines").Add(elem);

                xDoc.Save(fileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private XElement GetRoutineElementById(int id)
        {
            XElement selectedElem =
                (from t in xDoc.Root.Element("Routines").Elements("Routine").Elements("Id")
                 where t.Value == id.ToString()
                 select t.Parent).FirstOrDefault();
            return selectedElem;
        }

        public override bool UpdateRoutine(Routine routine)
        {
            try
            {
                XElement selectedElem = GetRoutineElementById(routine.Id.GetValueOrDefault());
                selectedElem.ReplaceWith(routine.ToXElement<Routine>());
                xDoc.Save(fileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool DeleteRoutine(Routine routine)
        {
            try
            {
                XElement selectedElem = GetRoutineElementById(routine.Id.GetValueOrDefault());
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
