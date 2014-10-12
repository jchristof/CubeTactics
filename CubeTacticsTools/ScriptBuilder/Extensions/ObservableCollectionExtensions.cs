using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptBuilder.Extensions {
    public static class ObservableCollectionExtensions {
        public static void Swap<T>(
           this ObservableCollection<T> collection, T obj1, T obj2) {
            if (!(collection.Contains(obj1) && collection.Contains(obj2))) return;

            var indexes = new List<int> { collection.IndexOf(obj1), collection.IndexOf(obj2) };
            if (indexes[0] == indexes[1]) return;
            indexes.Sort();

            var values = new List<T> { collection[indexes[0]], collection[indexes[1]] };

            collection.RemoveAt(indexes[1]);
            collection.RemoveAt(indexes[0]);
            collection.Insert(indexes[0], values[1]);
            collection.Insert(indexes[1], values[0]);
        }
    }
}
