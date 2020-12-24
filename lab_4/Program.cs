using System;
using static System.Console;

namespace asd_list_single_linked_ring_head_pointer
{
    /*
    Команды для редактирования списка :
    `AddFirst` - добавить первым элементом
    `AddLast` -  добавить последним элементом
    `AddAtPosition` - добавить элемент на позицию, но если нужно в конец, то использовать `AddLast`

    `DeleteFirst` - удалить первый элемент
    `DeleteLast` - удалить последний элемент
    `DeleteAtPosition` - удалить элемент с позиции

    `Print` - вывести "главный" список

    `` или `End` - закончить исполнение программы 

    Команды для редактирования списка за заданием:
    `AddAfterHead` - добавить элемент после головы
    `DeleteEvenPositions` - удалить элемент, который стоит на парной позиции

    Дополнительные команды, не предусметренные заданием:
    `Clear` - очистить списки
    `PrintSl` - вывести список удаленных элементов

    Уточнение: 
        -список с удаленными элементами не очищаю автоматически, 
    а новые удалённые элементы будут записываться дальше 
        -все команды пишутся без пробелов или знаков пунктуации
    */

    public class CircularLinkedList
    {
        public Node head;
        public Node tail;

        public class Node
        {
            public string data;
            public Node next;

            public Node(string data)
            {
                this.data = data;
            }

            public Node(string data, Node next)
            {
                this.data = data;
                this.next = next;
            }
        }
        public int count;  // количество элементов в списке


        public SLList headSl;
        public SLList tailSl;
        public class SLList
        {
            public string data;
            public SLList next;

            public SLList(string data)
            {
                this.data = data;
            }
        }
        public int slCount;

        public void Print()    //вывод списка
        {

            Node current = head;
            int counter = count;
            if (counter == 0)
            {
                Console.Write(current.data);
            }
            if (head == null)
            {
                return;
            }
            else
            {
                WriteLine();
                Write("Current list: ");

                while (counter != 0)
                {
                    Console.Write($"[{current.data}]");
                    if (counter != 1) Console.Write(" -> ");
                    current = current.next;
                    counter--;
                }
            }
            Console.WriteLine();
        }

        // добавление элемента в конец
        public void AddLast(string data)
        {
            Node node = new Node(data);
            if (head == null)
            {
                head = node;
                tail = node;
                tail.next = head;
            }
            else
            {
                node.next = head;
                tail.next = node;
                tail = node;
            }
            count++;
        }

        //добавление элемента в начало
        public void AddFirst(string data)
        {
            Node newNode = new Node(data);
            if (head == null)
            {
                head = newNode;
                tail = newNode;
                tail.next = head;
            }
            else
            {
                newNode.next = head;
                tail.next = newNode;
                head = newNode;
            }
            count++;
        }

        //добавление элемента после головы
        public void AddAfterHead(string data)
        {
            Node newNode = new Node(data);
            if (head == null)
            {
                head = newNode;
                tail = newNode;
                tail.next = head;
            }
            else
            {
                newNode.next = head.next;
                head.next = newNode;
            }
            count++;
        }

        //добавление на позицию, но не в конец
        public bool AddAtPosition(string data, int pos)
        {
            Node current = head;
            Node previous = null;
            Node newNode = new Node(data);
            int counter = 1;
            do
            {
                if (pos == counter)
                {
                    // Если узел в середине 
                    if (previous != null)
                    {
                        previous.next = newNode;
                        newNode.next = current;
                    }
                    else    // если добавляется первый элемент
                    {
                        if (head == null)
                        {
                            head = newNode;
                            tail = newNode;
                            tail.next = head;
                        }
                        else
                        {
                            newNode.next = head;
                            tail.next = newNode;
                            head = newNode;
                        }
                    }
                    count++;
                    return true;
                }

                previous = current;
                current = current.next;
                counter++;
            } while (current != head);
            return false;
        }

        //удаление элемента с позиции
        public bool DeleteAtPosition(int pos)
        {
            if (head == null) return false;
            Node current = head;
            Node previous = null;
            int counter = 1;
            if (IsEmpty) return false;
            if (count < pos) return false;
            do
            {
                if (pos == counter)
                {
                    // Если узел в середине или в конце
                    if (previous != null)
                    {
                        previous.next = current.next;
                        if (current == tail)
                            tail = previous;
                    }
                    else // если удаляется первый элемент
                    {
                        if (count == 1)
                        {
                            head = tail = null;
                        }
                        else
                        {
                            head = current.next;
                            tail.next = current.next;
                        }
                    }
                    count--;
                    return true;
                }

                previous = current;
                current = current.next;
                counter++;
            } while (current != head);

            return false;
        }

        //удаление последнего элемента
        public void DeleteLast()
        {
            if (head != null && head.next != null) // перевіряємо, чи містить список вузли та чи не є голова останнім вузлом
            {
                Node current = head;

                while (current.next.next != tail.next)
                {
                    current = current.next;
                }
                current.next = null;
                tail = current;
                tail.next = head;

            }
            else WriteLine("Error: list is empty\n");
            count--;
        }

        //удаление первого элемента
        public void DeleteFirst()
        {
            if (head != null && head.next != null) // перевіряємо, чи містить список вузли та чи не є голова останнім вузлом
            {
                Node current = head;
                head = current.next;
                tail.next = current.next;

            }
            else WriteLine("Error: list is empty\n");
            count--;
        }

        public bool IsEmpty { get { return count == 0; } }

        //очистка списка 
        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
            headSl = null;
            tailSl = null;
            slCount = 0;
        }


        public bool DeleteEvenPosition()        //redo
        {
            if (head == null) return false;
            Node current = head;
            Node previous = null;
            int counter = 1;
            bool ifDel = false;
            if (IsEmpty) return false;
            if (count < 2) return false;
            do
            {
                if (counter % 2 == 0)
                {
                    if (previous != null)
                    {
                        SLList newNode = new SLList(current.data);
                        if (headSl == null)
                        {
                            headSl = newNode;
                            tailSl = newNode;
                            slCount++;
                        }
                        else
                        {
                            tailSl.next = newNode;
                            tailSl = newNode;
                            slCount++;
                        }

                        previous.next = current.next;
                        if (current == tail)
                        {
                            tail = previous;
                        }
                        count--;
                        ifDel = true;
                    }
                }
                previous = current;
                current = current.next;
                counter++;
            } while (current != head);

            return ifDel;
        }

        public void PrintSl()
        {
            SLList current = headSl;
            int counter = slCount;
            if (counter == 0)
            {
                Write(current.data);
            }
            if (headSl == null)
            {
                return;
            }
            else
            {
                Write("Current list: ");

                while (counter != 0)
                {
                    Console.Write($"[{current.data}]");
                    if (counter != 1) Console.Write(" -> ");
                    current = current.next;
                    counter--;
                }
            }
            WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CircularLinkedList circularlist = new CircularLinkedList();
            do
            {
                WriteLine("Enter command: ");
                string str = ReadLine();
                if ((str == "") || (str == "End"))                                 //else unknown com
                {
                    WriteLine("Ending processing...");
                    Environment.Exit(0);
                }
                else if (str == "AddLast")
                {
                    WriteLine("Enter value: ");
                    string value = ReadLine();
                    circularlist.AddLast(value);
                    circularlist.Print();
                }
                else if (str == "DeleteLast")
                {
                    WriteLine("Value was deleted");
                    circularlist.DeleteLast();
                    circularlist.Print();
                }
                else if (str == "DeleteFirst")
                {
                    WriteLine("Value was deleted");
                    circularlist.DeleteFirst();
                    circularlist.Print();
                }
                else if (str == "DeleteAtPosition")
                {
                    WriteLine("Enter position: ");
                    int pos;
                    while (!int.TryParse(Console.ReadLine(), out pos))
                    {
                        WriteLine("Error: Enter integer number");
                    }
                    if (!circularlist.DeleteAtPosition(pos))
                    {
                        WriteLine("Error: Unable to delete item\n");
                    }
                    else
                    {
                        if (circularlist.head == null)
                        {
                            WriteLine("\nCurrent list: ");
                        }
                        else
                        {
                            circularlist.Print();
                        }
                    }
                }
                else if (str == "AddAtPosition")
                {
                    WriteLine("Enter value: ");
                    string value = ReadLine();
                    int pos;
                    WriteLine("Enter position: ");
                    while (!int.TryParse(Console.ReadLine(), out pos))
                    {
                        WriteLine("Error: Enter integer number");
                    }
                    if (!circularlist.AddAtPosition(value, pos))
                    {
                        WriteLine("Error: Unable to insert item. If you want to insert element in the end, use `AddLast`.\n");
                    }
                    else
                    {
                        circularlist.Print();
                    }
                }
                else if (str == "AddFirst")
                {
                    WriteLine("Enter value: ");
                    string value = ReadLine();
                    circularlist.AddFirst(value);
                    circularlist.Print();
                }
                else if (str == "Clear")
                {
                    circularlist.Clear();
                    WriteLine("Tip: Lists were cleared\n");
                }
                else if (str == "DeleteEvenPositions")
                {
                    if (!circularlist.DeleteEvenPosition())
                    {
                        WriteLine("Error: Unable to delete even positioned items.\n");
                    }
                    else
                    {
                        WriteLine("\nTip: List of deleted elements: ");
                        circularlist.PrintSl();
                    }
                    if (circularlist.head != null)
                    {
                        Write("\n\nTip: Main list: ");
                        circularlist.Print();
                    }
                    else
                    {
                        WriteLine("Error: List is empty");
                    }
                    WriteLine();
                }
                else if (str == "Print")
                {
                    if (circularlist.head == null)
                    {
                        WriteLine("\nTip: List is empty");
                    }
                    else
                    {
                        circularlist.Print();

                    }
                }
                else if (str == "PrintSl")
                {
                    if (circularlist.headSl == null)
                    {
                        WriteLine("\nTip: List is empty");
                    }
                    else
                    {
                        WriteLine();
                        circularlist.PrintSl();
                    }
                }
                else if (str == "AddAfterHead")
                {
                    WriteLine("Enter value: ");
                    string value = ReadLine();
                    circularlist.AddAfterHead(value);
                    circularlist.Print();
                }
                else
                {
                    WriteLine("Error: Unknown command `{0}`\n", str);
                }
            } while (true);
        }
    }
}

