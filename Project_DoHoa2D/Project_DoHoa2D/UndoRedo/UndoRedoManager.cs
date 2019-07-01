using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DoHoa2D.UndoRedo
{
    class UndoRedoManager
    {
        const int maxStatus = 10;
        private int currentStatus = -1;
        private List<string> statusStack;

        public UndoRedoManager()
        {
            statusStack = new List<string>(maxStatus);
        }

        public bool AddNewStatus(List<MyShape> myShapes)
        {
            string listShape = "";
            for (int i = 0; i < myShapes.Count; i++)
            {
                string data = myShapes[i].getData();
                listShape = listShape + data;
            }

            if (statusStack.Count > maxStatus)
            {
                shiftLeftOnePosition();
                statusStack[statusStack.Count - 1] = listShape;
                currentStatus = maxStatus;
            }
            else
            {
                statusStack.Add(listShape);
                currentStatus++;
            }

            return true;
        }

        public List<MyShape> HandleUndo()
        {
            currentStatus--;
            if (currentStatus < 0)
                currentStatus = 0;
            Console.WriteLine(currentStatus.ToString());
            for (int i = 0; i < statusStack.Count; i++)
                Console.WriteLine(statusStack[i].ToString());
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            return parseStringDataToListShapeObject(statusStack[currentStatus]);
        }

        public List<MyShape> HandleRedo()
        {
            currentStatus++;
            if (currentStatus > maxStatus -1)
                currentStatus = maxStatus -1;
            if (currentStatus > statusStack.Count - 1)
                currentStatus = statusStack.Count - 1;
            Console.WriteLine(currentStatus.ToString());
            return parseStringDataToListShapeObject(statusStack[currentStatus]);
        }

        public void setDefauleStatus()
        {
            string temp = statusStack[currentStatus];
            currentStatus = 0;
            statusStack.Clear();
            statusStack.Add(temp);
        }
        private void shiftLeftOnePosition()
        {
            for (int i = 0; i < statusStack.Count - 1; i++)
            {
                statusStack[i] = statusStack[i + 1];
            }
        }

        private List<MyShape> parseStringDataToListShapeObject(string data)
        {
            List<MyShape> myShapes = new List<MyShape>();
            string firstWord = null;
            string[] listShape = data.Split('\n');
            for (int i = 0; i < listShape.Length; i++)
            {
                if (listShape[i] != "")
                    firstWord = listShape[i].Substring(0, listShape[i].IndexOf(" "));
                switch (firstWord)
                {

                    case "Line":
                        MyShape myLine = new MyLine();
                        myLine.Open(listShape[i]);
                        myShapes.Add(myLine);
                        break;
                    case "Rectangle":
                        MyShape myRectangle = new MyRectangle();
                        myRectangle.Open(listShape[i]);
                        myShapes.Add(myRectangle);
                        break;
                    case "Circle":
                        MyShape myCircle = new MyCircle();
                        myCircle.Open(listShape[i]);
                        myShapes.Add(myCircle);
                        break;
                    case "Ellipse":
                        MyShape myEllipse = new MyEllipse();
                        myEllipse.Open(listShape[i]);
                        myShapes.Add(myEllipse);
                        break;
                    case "Polygon":
                        MyShape myPolygon = new MyPolygon();
                        myPolygon.Open(listShape[i]);
                        myShapes.Add(myPolygon);
                        break;
                    case "Bezier":
                        MyShape myBezier = new MyBezier();
                        myBezier.Open(listShape[i]);
                        myShapes.Add(myBezier);
                        break;
                    case "Polyline":
                        MyShape myPolyline = new MyPolyline();
                        myPolyline.Open(listShape[i]);
                        myShapes.Add(myPolyline);
                        break;
                }
                firstWord = "";
            }
            return myShapes;
        }
    }
}
