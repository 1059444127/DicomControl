using LkDicomView.AnnObjects;
using LkDicomView.AnnObjects.AnnObjects;
using LkDicomView.AnnObjects.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static AnnObject CreateAnnObject(AnnObjectType annObjectType, Point start, Point end)
        {
            switch (annObjectType)
            {
                case AnnObjectType.Ruler:
                    return new LineAnnObject(start, end);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
