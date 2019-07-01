using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DoHoa2D.UndoRedo
{
    class UndoRedoManager
    {
        private int currentStatus = -1;
        private List<string> statusStack;

        public UndoRedoManager()
        {
            statusStack = new List<string>(10);
        }

        public bool AddNewStatus(List<MyShape> myShapes)
        {
            string listShape = "";
            for (int i = 0; i < myShapes.Count; i++)
            {
                string data = myShapes[i].getData();
                listShape = listShape + data;
            }

            if (statusStack.Count > 10)
            {
                shiftLeftOnePosition();
                statusStack[statusStack.Count - 1] = listShape;
                currentStatus = 9;
            }
            else
            {
                statusStack.Add(listShape);
                currentStatus++;
            }
            for (int i=0; i<statusStack.Count; i++)
                Console.WriteLine(statusStack[i].ToString());
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            return true;
        }

        public List<MyShape> HandleUndo()
        {
            return null;
        }

        public List<MyShape> HandleRedo()
        {
            return null;
        }

        private void shiftLeftOnePosition()
        {
            for (int i=0; i< statusStack.Count - 1; i++)
            {
                statusStack[i] = statusStack[i + 1];
            }
        }
    }
}
