// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlNodeCollection
// Assembly: HtmlAgilityPack-PCL, Version=1.4.6.0, Culture=neutral, PublicKeyToken=null
// MVID: A611BE5D-A211-439D-AF0B-7D7BA44DC844
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.xml

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace HtmlAgilityPack
{
  /// <summary>
  /// Represents a combined list and collection of HTML nodes.
  /// </summary>
  public class HtmlNodeCollection : 
    IList<HtmlNode>,
    ICollection<HtmlNode>,
    IEnumerable<HtmlNode>,
    IEnumerable
  {
    private readonly HtmlNode _parentnode;
    private readonly List<HtmlNode> _items = new List<HtmlNode>();

    /// <summary>
    /// Initialize the HtmlNodeCollection with the base parent node
    /// </summary>
    /// <param name="parentnode">The base node of the collection</param>
    public HtmlNodeCollection(HtmlNode parentnode) => this._parentnode = parentnode;

    /// <summary>Gets a given node from the list.</summary>
    public int this[HtmlNode node]
    {
      get
      {
        int nodeIndex = this.GetNodeIndex(node);
        return nodeIndex != -1 ? nodeIndex : throw new ArgumentOutOfRangeException(nameof (node), "Node \"" + node.CloneNode(false).OuterHtml + "\" was not found in the collection");
      }
    }

    /// <summary>Get node with tag name</summary>
    /// <param name="nodeName"></param>
    /// <returns></returns>
    public HtmlNode this[string nodeName]
    {
      get
      {
        nodeName = nodeName.ToLower();
        for (int index = 0; index < this._items.Count; ++index)
        {
          if (this._items[index].Name.Equals(nodeName))
            return this._items[index];
        }
        return (HtmlNode) null;
      }
    }

    /// <summary>
    /// Gets the number of elements actually contained in the list.
    /// </summary>
    public int Count => this._items.Count;

    /// <summary>Is collection read only</summary>
    public bool IsReadOnly => false;

    /// <summary>Gets the node at the specified index.</summary>
    public HtmlNode this[int index]
    {
      get => this._items[index];
      set => this._items[index] = value;
    }

    /// <summary>Add node to the collection</summary>
    /// <param name="node"></param>
    public void Add(HtmlNode node) => this._items.Add(node);

    /// <summary>
    /// Clears out the collection of HtmlNodes. Removes each nodes reference to parentnode, nextnode and prevnode
    /// </summary>
    public void Clear()
    {
      foreach (HtmlNode htmlNode in this._items)
      {
        htmlNode.ParentNode = (HtmlNode) null;
        htmlNode.NextSibling = (HtmlNode) null;
        htmlNode.PreviousSibling = (HtmlNode) null;
      }
      this._items.Clear();
    }

    /// <summary>Gets existence of node in collection</summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Contains(HtmlNode item) => this._items.Contains(item);

    /// <summary>Copy collection to array</summary>
    /// <param name="array"></param>
    /// <param name="arrayIndex"></param>
    public void CopyTo(HtmlNode[] array, int arrayIndex) => this._items.CopyTo(array, arrayIndex);

    /// <summary>Get Enumerator</summary>
    /// <returns></returns>
    IEnumerator<HtmlNode> IEnumerable<HtmlNode>.GetEnumerator()
    {
      return (IEnumerator<HtmlNode>) this._items.GetEnumerator();
    }

    /// <summary>Get Explicit Enumerator</summary>
    /// <returns></returns>
    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this._items.GetEnumerator();

    /// <summary>Get index of node</summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int IndexOf(HtmlNode item) => this._items.IndexOf(item);

    /// <summary>Insert node at index</summary>
    /// <param name="index"></param>
    /// <param name="node"></param>
    public void Insert(int index, HtmlNode node)
    {
      HtmlNode htmlNode1 = (HtmlNode) null;
      HtmlNode htmlNode2 = (HtmlNode) null;
      if (index > 0)
        htmlNode2 = this._items[index - 1];
      if (index < this._items.Count)
        htmlNode1 = this._items[index];
      this._items.Insert(index, node);
      if (htmlNode2 != null)
        htmlNode2._nextnode = node != htmlNode2 ? node : throw new InvalidProgramException("Unexpected error.");
      if (htmlNode1 != null)
        htmlNode1._prevnode = node;
      node._prevnode = htmlNode2;
      node._nextnode = htmlNode1 != node ? htmlNode1 : throw new InvalidProgramException("Unexpected error.");
      node._parentnode = this._parentnode;
    }

    /// <summary>Remove node</summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Remove(HtmlNode item)
    {
      this.RemoveAt(this._items.IndexOf(item));
      return true;
    }

    /// <summary>
    /// Remove <see cref="T:HtmlAgilityPack.HtmlNode" /> at index
    /// </summary>
    /// <param name="index"></param>
    public void RemoveAt(int index)
    {
      HtmlNode htmlNode1 = (HtmlNode) null;
      HtmlNode htmlNode2 = (HtmlNode) null;
      HtmlNode htmlNode3 = this._items[index];
      if (index > 0)
        htmlNode2 = this._items[index - 1];
      if (index < this._items.Count - 1)
        htmlNode1 = this._items[index + 1];
      this._items.RemoveAt(index);
      if (htmlNode2 != null)
        htmlNode2._nextnode = htmlNode1 != htmlNode2 ? htmlNode1 : throw new InvalidProgramException("Unexpected error.");
      if (htmlNode1 != null)
        htmlNode1._prevnode = htmlNode2;
      htmlNode3._prevnode = (HtmlNode) null;
      htmlNode3._nextnode = (HtmlNode) null;
      htmlNode3._parentnode = (HtmlNode) null;
    }

    /// <summary>Get first instance of node in supplied collection</summary>
    /// <param name="items"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static HtmlNode FindFirst(HtmlNodeCollection items, string name)
    {
      foreach (HtmlNode first1 in (IEnumerable<HtmlNode>) items)
      {
        if (first1.Name.ToLower().Contains(name))
          return first1;
        if (first1.HasChildNodes)
        {
          HtmlNode first2 = HtmlNodeCollection.FindFirst(first1.ChildNodes, name);
          if (first2 != null)
            return first2;
        }
      }
      return (HtmlNode) null;
    }

    /// <summary>Add node to the end of the collection</summary>
    /// <param name="node"></param>
    public void Append(HtmlNode node)
    {
      HtmlNode htmlNode = (HtmlNode) null;
      if (this._items.Count > 0)
        htmlNode = this._items[this._items.Count - 1];
      this._items.Add(node);
      node._prevnode = htmlNode;
      node._nextnode = (HtmlNode) null;
      node._parentnode = this._parentnode;
      if (htmlNode == null)
        return;
      htmlNode._nextnode = htmlNode != node ? node : throw new InvalidProgramException("Unexpected error.");
    }

    /// <summary>Get first instance of node with name</summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public HtmlNode FindFirst(string name) => HtmlNodeCollection.FindFirst(this, name);

    /// <summary>Get index of node</summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public int GetNodeIndex(HtmlNode node)
    {
      for (int index = 0; index < this._items.Count; ++index)
      {
        if (node == this._items[index])
          return index;
      }
      return -1;
    }

    /// <summary>Add node to the beginning of the collection</summary>
    /// <param name="node"></param>
    public void Prepend(HtmlNode node)
    {
      HtmlNode htmlNode = (HtmlNode) null;
      if (this._items.Count > 0)
        htmlNode = this._items[0];
      this._items.Insert(0, node);
      node._nextnode = node != htmlNode ? htmlNode : throw new InvalidProgramException("Unexpected error.");
      node._prevnode = (HtmlNode) null;
      node._parentnode = this._parentnode;
      if (htmlNode == null)
        return;
      htmlNode._prevnode = node;
    }

    /// <summary>Remove node at index</summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool Remove(int index)
    {
      this.RemoveAt(index);
      return true;
    }

    /// <summary>Replace node at index</summary>
    /// <param name="index"></param>
    /// <param name="node"></param>
    public void Replace(int index, HtmlNode node)
    {
      HtmlNode htmlNode1 = (HtmlNode) null;
      HtmlNode htmlNode2 = (HtmlNode) null;
      HtmlNode htmlNode3 = this._items[index];
      if (index > 0)
        htmlNode2 = this._items[index - 1];
      if (index < this._items.Count - 1)
        htmlNode1 = this._items[index + 1];
      this._items[index] = node;
      if (htmlNode2 != null)
        htmlNode2._nextnode = node != htmlNode2 ? node : throw new InvalidProgramException("Unexpected error.");
      if (htmlNode1 != null)
        htmlNode1._prevnode = node;
      node._prevnode = htmlNode2;
      node._nextnode = htmlNode1 != node ? htmlNode1 : throw new InvalidProgramException("Unexpected error.");
      node._parentnode = this._parentnode;
      htmlNode3._prevnode = (HtmlNode) null;
      htmlNode3._nextnode = (HtmlNode) null;
      htmlNode3._parentnode = (HtmlNode) null;
    }

    /// <summary>Get all node descended from this collection</summary>
    /// <returns></returns>
    public IEnumerable<HtmlNode> Descendants()
    {
      foreach (HtmlNode item in this._items)
      {
        foreach (HtmlNode n in item.Descendants())
          yield return n;
      }
    }

    /// <summary>
    /// Get all node descended from this collection with matching name
    /// </summary>
    /// <returns></returns>
    public IEnumerable<HtmlNode> Descendants(string name)
    {
      foreach (HtmlNode item in this._items)
      {
        foreach (HtmlNode n in item.Descendants(name))
          yield return n;
      }
    }

    /// <summary>Gets all first generation elements in collection</summary>
    /// <returns></returns>
    public IEnumerable<HtmlNode> Elements()
    {
      foreach (HtmlNode item in this._items)
      {
        foreach (HtmlNode n in (IEnumerable<HtmlNode>) item.ChildNodes)
          yield return n;
      }
    }

    /// <summary>Gets all first generation elements matching name</summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IEnumerable<HtmlNode> Elements(string name)
    {
      foreach (HtmlNode item in this._items)
      {
        foreach (HtmlNode n in item.Elements(name))
          yield return n;
      }
    }

    /// <summary>All first generation nodes in collection</summary>
    /// <returns></returns>
    public IEnumerable<HtmlNode> Nodes()
    {
      foreach (HtmlNode item in this._items)
      {
        foreach (HtmlNode n in (IEnumerable<HtmlNode>) item.ChildNodes)
          yield return n;
      }
    }
  }
}
