using LkDicomView.AnnObjects;
using LkDicomView.AnnObjects.Enums;
using LkDicomView.Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace LkDicomView.Modules
{
    public class AnnObjectContainer : ICollection<AnnObject>, IList<AnnObject>
    {
        private List<AnnObject> annObjects;

        public AnnObjectContainer()
        {
            annObjects = new List<AnnObject>();
        }

        public AnnObject this[int index]
        {
            get
            {
                return annObjects[index];
            }
            set
            {
                annObjects[index] = value;
            }
        }

        public int Count => annObjects.Count();

        public bool IsReadOnly => false;

        public void Add(AnnObject item)
        {
            annObjects.Add(item);
        }

        public void Clear()
        {
            annObjects.Clear();
        }

        public bool Contains(AnnObject item)
        {
            var find = annObjects.Where(a => a == item);
            if (find.Any())
            {
                return true;
            }
            return false;
        }

        public void CopyTo(AnnObject[] array, int arrayIndex)
        {
            foreach(var i in annObjects)
            {
                array[arrayIndex] = i;
                arrayIndex++;
            }
        }

        public IEnumerator<AnnObject> GetEnumerator()
        {
            return annObjects.GetEnumerator();
        }

        public int IndexOf(AnnObject item)
        {
            return annObjects.IndexOf(item);
        }

        public void Insert(int index, AnnObject item)
        {
            annObjects.Insert(index, item);
        }

        public bool Remove(AnnObject item)
        {
            return annObjects.Remove(item);
        }

        public void RemoveAt(int index)
        {
            annObjects.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void ForEach(Action<AnnObject> action)
        {
            foreach(var i in annObjects)
            {
                action(i);
            }
        }

        public static AnnObject CreateAnnObject(AnnObjectType annObjectType, Point p1, Point p2)
        {
            switch (annObjectType)
            {
                case AnnObjectType.None:
                    return null;
                case AnnObjectType.Ruler:
                    return new LineAnnObject(p1, p2);
                case AnnObjectType.Rectangle:
                    return new RectangleAnnObject(p1, p2);
                case AnnObjectType.Ellipse:
                    return new EllipseAnnObject(p1, p2);
                default:
                    throw new NotSupportedException();
            }
        }

        public void SaveAnnObjects(string savePath)
        {
            var jsonData = JsonConvert.SerializeObject(annObjects);//序列化
            File.WriteAllText(savePath, jsonData);
        }

        public void LoadAnnObjects(string path)
        {
            if (!File.Exists(path))
            {
                return;
            }
            var js = new JavaScriptSerializer();
            var jArray = JsonConvert.DeserializeObject(File.ReadAllText(path)) as JArray;
            foreach (var i in jArray)
            {
                switch ((AnnObjectType)i["Type"].Value<int>())
                {

                    case AnnObjectType.Ruler:
                        annObjects.Add(i.ToObject<LineAnnObject>());
                        break;
                    case AnnObjectType.Ellipse:
                        annObjects.Add(i.ToObject<EllipseAnnObject>());
                        break;
                    case AnnObjectType.Rectangle:
                        annObjects.Add(i.ToObject<RectangleAnnObject>());
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
            OnLoaded?.Invoke();
        }

        public event Action OnLoaded;
    }
}
