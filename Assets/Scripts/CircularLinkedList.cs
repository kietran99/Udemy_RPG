using System.Collections;
using System.Collections.Generic;

public class CircularLinkedList<T>
{
    public class Node
    {
        public T value { get; set; }
        public Node next, prev;

        public Node(T value, Node next, Node prev)
        {
            this.value = value;
            this.next = next;
            this.prev = prev;
        }
    }

    public Node head, current;

    public CircularLinkedList()
    {
        head = null;
        current = head;
    }

    public void Append(T value)
    {
        if (head == null)
        {
            head = new Node(value, null, null);
            this.current = head;
            return;
        }

        Node current = head;

        while (current.next != head && current.next != null)
        {
            current = current.next;
        }

        current.next = new Node(value, head, current);
        head.prev = current.next;
    }

    public void Append(T[] value)
    {
        int empty = 0;

        if (head == null)
        {
            head = new Node(value[0], null, null);
            this.current = head;
            empty = 1;
        }

        Node current = head;

        while (current.next != head && current.next != null)
        {
            current = current.next;
        }

        for (int i = empty; i < value.Length; i++)
        {
            current.next = new Node(value[i], head, current);
            head.prev = current.next;

            current = current.next;
        }
    }

    public void NextPos()
    {
        if (current.next != null) current = current.next;
    }

    public void PrevPos()
    {
        if (current.prev != null) current = current.prev;
    }

    public void RevertToDefault()
    {
        if (head != null) current = head;
    }
}
