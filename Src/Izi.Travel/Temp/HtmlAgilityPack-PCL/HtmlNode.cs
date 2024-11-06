// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlNode
// Assembly: HtmlAgilityPack-PCL, Version=1.4.6.0, Culture=neutral, PublicKeyToken=null
// MVID: A611BE5D-A211-439D-AF0B-7D7BA44DC844
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;

#nullable disable
namespace HtmlAgilityPack
{
  /// <summary>Represents an HTML node.</summary>
  [DebuggerDisplay("Name: {OriginalName}")]
  public class HtmlNode
  {
    internal HtmlAttributeCollection _attributes;
    internal HtmlNodeCollection _childnodes;
    internal HtmlNode _endnode;
    private bool _changed;
    internal string _innerhtml;
    internal int _innerlength;
    internal int _innerstartindex;
    internal int _line;
    internal int _lineposition;
    private string _name;
    internal int _namelength;
    internal int _namestartindex;
    internal HtmlNode _nextnode;
    internal HtmlNodeType _nodetype;
    internal string _outerhtml;
    internal int _outerlength;
    internal int _outerstartindex;
    private string _optimizedName;
    internal HtmlDocument _ownerdocument;
    internal HtmlNode _parentnode;
    internal HtmlNode _prevnode;
    internal HtmlNode _prevwithsamename;
    internal bool _starttag;
    internal int _streamposition;
    /// <summary>
    /// Gets the name of a comment node. It is actually defined as '#comment'.
    /// </summary>
    public static readonly string HtmlNodeTypeNameComment = "#comment";
    /// <summary>
    /// Gets the name of the document node. It is actually defined as '#document'.
    /// </summary>
    public static readonly string HtmlNodeTypeNameDocument = "#document";
    /// <summary>
    /// Gets the name of a text node. It is actually defined as '#text'.
    /// </summary>
    public static readonly string HtmlNodeTypeNameText = "#text";
    /// <summary>
    /// Gets a collection of flags that define specific behaviors for specific element nodes.
    /// The table contains a DictionaryEntry list with the lowercase tag name as the Key, and a combination of HtmlElementFlags as the Value.
    /// </summary>
    public static Dictionary<string, HtmlElementFlag> ElementsFlags = new Dictionary<string, HtmlElementFlag>();

    /// <summary>
    /// Initialize HtmlNode. Builds a list of all tags that have special allowances
    /// </summary>
    static HtmlNode()
    {
      HtmlNode.ElementsFlags.Add("script", HtmlElementFlag.CData);
      HtmlNode.ElementsFlags.Add("style", HtmlElementFlag.CData);
      HtmlNode.ElementsFlags.Add("noxhtml", HtmlElementFlag.CData);
      HtmlNode.ElementsFlags.Add("base", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("link", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("meta", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("isindex", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("hr", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("col", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("img", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("param", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("embed", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("frame", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("wbr", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("bgsound", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("spacer", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("keygen", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("area", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("input", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("basefont", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("form", HtmlElementFlag.Empty | HtmlElementFlag.CanOverlap);
      HtmlNode.ElementsFlags.Add("option", HtmlElementFlag.Empty);
      HtmlNode.ElementsFlags.Add("br", HtmlElementFlag.Empty | HtmlElementFlag.Closed);
      HtmlNode.ElementsFlags.Add("p", HtmlElementFlag.Empty | HtmlElementFlag.Closed);
    }

    /// <summary>
    /// Initializes HtmlNode, providing type, owner and where it exists in a collection
    /// </summary>
    /// <param name="type"></param>
    /// <param name="ownerdocument"></param>
    /// <param name="index"></param>
    public HtmlNode(HtmlNodeType type, HtmlDocument ownerdocument, int index)
    {
      this._nodetype = type;
      this._ownerdocument = ownerdocument;
      this._outerstartindex = index;
      switch (type)
      {
        case HtmlNodeType.Document:
          this.Name = HtmlNode.HtmlNodeTypeNameDocument;
          this._endnode = this;
          break;
        case HtmlNodeType.Comment:
          this.Name = HtmlNode.HtmlNodeTypeNameComment;
          this._endnode = this;
          break;
        case HtmlNodeType.Text:
          this.Name = HtmlNode.HtmlNodeTypeNameText;
          this._endnode = this;
          break;
      }
      if (this._ownerdocument.Openednodes != null && !this.Closed && -1 != index)
        this._ownerdocument.Openednodes.Add(index, this);
      if (-1 != index || type == HtmlNodeType.Comment || type == HtmlNodeType.Text)
        return;
      this.SetChanged();
    }

    /// <summary>
    /// Gets the collection of HTML attributes for this node. May not be null.
    /// </summary>
    public HtmlAttributeCollection Attributes
    {
      get
      {
        if (!this.HasAttributes)
          this._attributes = new HtmlAttributeCollection(this);
        return this._attributes;
      }
      internal set => this._attributes = value;
    }

    /// <summary>Gets all the children of the node.</summary>
    public HtmlNodeCollection ChildNodes
    {
      get => this._childnodes ?? (this._childnodes = new HtmlNodeCollection(this));
      internal set => this._childnodes = value;
    }

    /// <summary>
    /// Gets a value indicating if this node has been closed or not.
    /// </summary>
    public bool Closed => this._endnode != null;

    /// <summary>
    /// Gets the collection of HTML attributes for the closing tag. May not be null.
    /// </summary>
    public HtmlAttributeCollection ClosingAttributes
    {
      get
      {
        return this.HasClosingAttributes ? this._endnode.Attributes : new HtmlAttributeCollection(this);
      }
    }

    internal HtmlNode EndNode => this._endnode;

    /// <summary>Gets the first child of the node.</summary>
    public HtmlNode FirstChild => this.HasChildNodes ? this._childnodes[0] : (HtmlNode) null;

    /// <summary>
    /// Gets a value indicating whether the current node has any attributes.
    /// </summary>
    public bool HasAttributes => this._attributes != null && this._attributes.Count > 0;

    /// <summary>
    /// Gets a value indicating whether this node has any child nodes.
    /// </summary>
    public bool HasChildNodes => this._childnodes != null && this._childnodes.Count > 0;

    /// <summary>
    /// Gets a value indicating whether the current node has any attributes on the closing tag.
    /// </summary>
    public bool HasClosingAttributes
    {
      get
      {
        return this._endnode != null && this._endnode != this && this._endnode._attributes != null && this._endnode._attributes.Count > 0;
      }
    }

    /// <summary>
    /// Gets or sets the value of the 'id' HTML attribute. The document must have been parsed using the OptionUseIdAttribute set to true.
    /// </summary>
    public string Id
    {
      get
      {
        if (this._ownerdocument.Nodesid == null)
          throw new Exception(HtmlDocument.HtmlExceptionUseIdAttributeFalse);
        return this.GetId();
      }
      set
      {
        if (this._ownerdocument.Nodesid == null)
          throw new Exception(HtmlDocument.HtmlExceptionUseIdAttributeFalse);
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        this.SetId(value);
      }
    }

    /// <summary>
    /// Gets or Sets the HTML between the start and end tags of the object.
    /// </summary>
    public virtual string InnerHtml
    {
      get
      {
        if (this._changed)
        {
          this.UpdateHtml();
          return this._innerhtml;
        }
        if (this._innerhtml != null)
          return this._innerhtml;
        return this._innerstartindex < 0 ? string.Empty : this._ownerdocument.Text.Substring(this._innerstartindex, this._innerlength);
      }
      set
      {
        HtmlDocument htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(value);
        this.RemoveAllChildren();
        this.AppendChildren(htmlDocument.DocumentNode.ChildNodes);
      }
    }

    /// <summary>
    /// Gets or Sets the text between the start and end tags of the object.
    /// </summary>
    public virtual string InnerText
    {
      get
      {
        if (this._nodetype == HtmlNodeType.Text)
          return ((HtmlTextNode) this).Text;
        if (this._nodetype == HtmlNodeType.Comment)
          return ((HtmlCommentNode) this).Comment;
        if (!this.HasChildNodes)
          return string.Empty;
        string innerText = (string) null;
        foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) this.ChildNodes)
          innerText += childNode.InnerText;
        return innerText;
      }
    }

    /// <summary>Gets the last child of the node.</summary>
    public HtmlNode LastChild
    {
      get => this.HasChildNodes ? this._childnodes[this._childnodes.Count - 1] : (HtmlNode) null;
    }

    /// <summary>Gets the line number of this node in the document.</summary>
    public int Line
    {
      get => this._line;
      internal set => this._line = value;
    }

    /// <summary>Gets the column number of this node in the document.</summary>
    public int LinePosition
    {
      get => this._lineposition;
      internal set => this._lineposition = value;
    }

    /// <summary>Gets or sets this node's name.</summary>
    public string Name
    {
      get
      {
        if (this._optimizedName == null)
        {
          if (this._name == null)
            this.Name = this._ownerdocument.Text.Substring(this._namestartindex, this._namelength);
          this._optimizedName = this._name != null ? this._name.ToLower() : string.Empty;
        }
        return this._optimizedName;
      }
      set
      {
        this._name = value;
        this._optimizedName = (string) null;
      }
    }

    /// <summary>Gets the HTML node immediately following this element.</summary>
    public HtmlNode NextSibling
    {
      get => this._nextnode;
      internal set => this._nextnode = value;
    }

    /// <summary>Gets the type of this node.</summary>
    public HtmlNodeType NodeType
    {
      get => this._nodetype;
      internal set => this._nodetype = value;
    }

    /// <summary>The original unaltered name of the tag</summary>
    public string OriginalName => this._name;

    /// <summary>Gets or Sets the object and its content in HTML.</summary>
    public virtual string OuterHtml
    {
      get
      {
        if (this._changed)
        {
          this.UpdateHtml();
          return this._outerhtml;
        }
        if (this._outerhtml != null)
          return this._outerhtml;
        return this._outerstartindex < 0 ? string.Empty : this._ownerdocument.Text.Substring(this._outerstartindex, this._outerlength);
      }
    }

    /// <summary>
    /// Gets the <see cref="T:HtmlAgilityPack.HtmlDocument" /> to which this node belongs.
    /// </summary>
    public HtmlDocument OwnerDocument
    {
      get => this._ownerdocument;
      internal set => this._ownerdocument = value;
    }

    /// <summary>
    /// Gets the parent of this node (for nodes that can have parents).
    /// </summary>
    public HtmlNode ParentNode
    {
      get => this._parentnode;
      internal set => this._parentnode = value;
    }

    /// <summary>Gets the node immediately preceding this node.</summary>
    public HtmlNode PreviousSibling
    {
      get => this._prevnode;
      internal set => this._prevnode = value;
    }

    /// <summary>
    /// Gets the stream position of this node in the document, relative to the start of the document.
    /// </summary>
    public int StreamPosition => this._streamposition;

    /// <summary>Gets a valid XPath string that points to this node</summary>
    public string XPath
    {
      get
      {
        return (this.ParentNode == null || this.ParentNode.NodeType == HtmlNodeType.Document ? "/" : this.ParentNode.XPath + "/") + this.GetRelativeXpath();
      }
    }

    /// <summary>Determines if an element node can be kept overlapped.</summary>
    /// <param name="name">The name of the element node to check. May not be <c>null</c>.</param>
    /// <returns>true if the name is the name of an element node that can be kept overlapped, <c>false</c> otherwise.</returns>
    public static bool CanOverlapElement(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      return HtmlNode.ElementsFlags.ContainsKey(name.ToLower()) && (HtmlNode.ElementsFlags[name.ToLower()] & HtmlElementFlag.CanOverlap) != (HtmlElementFlag) 0;
    }

    /// <summary>
    /// Creates an HTML node from a string representing literal HTML.
    /// </summary>
    /// <param name="html">The HTML text.</param>
    /// <returns>The newly created node instance.</returns>
    public static HtmlNode CreateNode(string html)
    {
      HtmlDocument htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(html);
      return htmlDocument.DocumentNode.FirstChild;
    }

    /// <summary>Determines if an element node is a CDATA element node.</summary>
    /// <param name="name">The name of the element node to check. May not be null.</param>
    /// <returns>true if the name is the name of a CDATA element node, false otherwise.</returns>
    public static bool IsCDataElement(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      return HtmlNode.ElementsFlags.ContainsKey(name.ToLower()) && (HtmlNode.ElementsFlags[name.ToLower()] & HtmlElementFlag.CData) != (HtmlElementFlag) 0;
    }

    /// <summary>Determines if an element node is closed.</summary>
    /// <param name="name">The name of the element node to check. May not be null.</param>
    /// <returns>true if the name is the name of a closed element node, false otherwise.</returns>
    public static bool IsClosedElement(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      return HtmlNode.ElementsFlags.ContainsKey(name.ToLower()) && (HtmlNode.ElementsFlags[name.ToLower()] & HtmlElementFlag.Closed) != (HtmlElementFlag) 0;
    }

    /// <summary>Determines if an element node is defined as empty.</summary>
    /// <param name="name">The name of the element node to check. May not be null.</param>
    /// <returns>true if the name is the name of an empty element node, false otherwise.</returns>
    public static bool IsEmptyElement(string name)
    {
      switch (name)
      {
        case null:
          throw new ArgumentNullException(nameof (name));
        case "":
          return true;
        default:
          if ('!' == name[0] || '?' == name[0])
            return true;
          return HtmlNode.ElementsFlags.ContainsKey(name.ToLower()) && (HtmlNode.ElementsFlags[name.ToLower()] & HtmlElementFlag.Empty) != (HtmlElementFlag) 0;
      }
    }

    /// <summary>
    /// Determines if a text corresponds to the closing tag of an node that can be kept overlapped.
    /// </summary>
    /// <param name="text">The text to check. May not be null.</param>
    /// <returns>true or false.</returns>
    public static bool IsOverlappedClosingElement(string text)
    {
      if (text == null)
        throw new ArgumentNullException(nameof (text));
      return text.Length > 4 && text[0] == '<' && text[text.Length - 1] == '>' && text[1] == '/' && HtmlNode.CanOverlapElement(text.Substring(2, text.Length - 3));
    }

    /// <summary>
    /// Returns a collection of all ancestor nodes of this element.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<HtmlNode> Ancestors()
    {
      HtmlNode node = this.ParentNode;
      if (node != null)
      {
        yield return node;
        for (; node.ParentNode != null; node = node.ParentNode)
          yield return node.ParentNode;
      }
    }

    /// <summary>Get Ancestors with matching name</summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IEnumerable<HtmlNode> Ancestors(string name)
    {
      for (HtmlNode n = this.ParentNode; n != null; n = n.ParentNode)
      {
        if (n.Name == name)
          yield return n;
      }
    }

    /// <summary>
    /// Returns a collection of all ancestor nodes of this element.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<HtmlNode> AncestorsAndSelf()
    {
      for (HtmlNode n = this; n != null; n = n.ParentNode)
        yield return n;
    }

    /// <summary>Gets all anscestor nodes and the current node</summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IEnumerable<HtmlNode> AncestorsAndSelf(string name)
    {
      for (HtmlNode n = this; n != null; n = n.ParentNode)
      {
        if (n.Name == name)
          yield return n;
      }
    }

    /// <summary>
    /// Adds the specified node to the end of the list of children of this node.
    /// </summary>
    /// <param name="newChild">The node to add. May not be null.</param>
    /// <returns>The node added.</returns>
    public HtmlNode AppendChild(HtmlNode newChild)
    {
      if (newChild == null)
        throw new ArgumentNullException(nameof (newChild));
      this.ChildNodes.Append(newChild);
      this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
      this.SetChanged();
      return newChild;
    }

    /// <summary>
    /// Adds the specified node to the end of the list of children of this node.
    /// </summary>
    /// <param name="newChildren">The node list to add. May not be null.</param>
    public void AppendChildren(HtmlNodeCollection newChildren)
    {
      if (newChildren == null)
        throw new ArgumentNullException(nameof (newChildren));
      foreach (HtmlNode newChild in (IEnumerable<HtmlNode>) newChildren)
        this.AppendChild(newChild);
    }

    /// <summary>Gets all Attributes with name</summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IEnumerable<HtmlAttribute> ChildAttributes(string name)
    {
      return this.Attributes.AttributesWithName(name);
    }

    /// <summary>Creates a duplicate of the node</summary>
    /// <returns></returns>
    public HtmlNode Clone() => this.CloneNode(true);

    /// <summary>
    /// Creates a duplicate of the node and changes its name at the same time.
    /// </summary>
    /// <param name="newName">The new name of the cloned node. May not be <c>null</c>.</param>
    /// <returns>The cloned node.</returns>
    public HtmlNode CloneNode(string newName) => this.CloneNode(newName, true);

    /// <summary>
    /// Creates a duplicate of the node and changes its name at the same time.
    /// </summary>
    /// <param name="newName">The new name of the cloned node. May not be null.</param>
    /// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself.</param>
    /// <returns>The cloned node.</returns>
    public HtmlNode CloneNode(string newName, bool deep)
    {
      if (newName == null)
        throw new ArgumentNullException(nameof (newName));
      HtmlNode htmlNode = this.CloneNode(deep);
      htmlNode.Name = newName;
      return htmlNode;
    }

    /// <summary>Creates a duplicate of the node.</summary>
    /// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself.</param>
    /// <returns>The cloned node.</returns>
    public HtmlNode CloneNode(bool deep)
    {
      HtmlNode node = this._ownerdocument.CreateNode(this._nodetype);
      node.Name = this.Name;
      switch (this._nodetype)
      {
        case HtmlNodeType.Comment:
          ((HtmlCommentNode) node).Comment = ((HtmlCommentNode) this).Comment;
          return node;
        case HtmlNodeType.Text:
          ((HtmlTextNode) node).Text = ((HtmlTextNode) this).Text;
          return node;
        default:
          if (this.HasAttributes)
          {
            foreach (HtmlAttribute attribute in (IEnumerable<HtmlAttribute>) this._attributes)
            {
              HtmlAttribute newAttribute = attribute.Clone();
              node.Attributes.Append(newAttribute);
            }
          }
          if (this.HasClosingAttributes)
          {
            node._endnode = this._endnode.CloneNode(false);
            foreach (HtmlAttribute attribute in (IEnumerable<HtmlAttribute>) this._endnode._attributes)
            {
              HtmlAttribute newAttribute = attribute.Clone();
              node._endnode._attributes.Append(newAttribute);
            }
          }
          if (!deep || !this.HasChildNodes)
            return node;
          foreach (HtmlNode childnode in (IEnumerable<HtmlNode>) this._childnodes)
          {
            HtmlNode newChild = childnode.Clone();
            node.AppendChild(newChild);
          }
          return node;
      }
    }

    /// <summary>
    /// Creates a duplicate of the node and the subtree under it.
    /// </summary>
    /// <param name="node">The node to duplicate. May not be <c>null</c>.</param>
    public void CopyFrom(HtmlNode node) => this.CopyFrom(node, true);

    /// <summary>Creates a duplicate of the node.</summary>
    /// <param name="node">The node to duplicate. May not be <c>null</c>.</param>
    /// <param name="deep">true to recursively clone the subtree under the specified node, false to clone only the node itself.</param>
    public void CopyFrom(HtmlNode node, bool deep)
    {
      if (node == null)
        throw new ArgumentNullException(nameof (node));
      this.Attributes.RemoveAll();
      if (node.HasAttributes)
      {
        foreach (HtmlAttribute attribute in (IEnumerable<HtmlAttribute>) node.Attributes)
          this.SetAttributeValue(attribute.Name, attribute.Value);
      }
      if (deep)
        return;
      this.RemoveAllChildren();
      if (!node.HasChildNodes)
        return;
      foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) node.ChildNodes)
        this.AppendChild(childNode.CloneNode(true));
    }

    /// <summary>
    /// Gets all Descendant nodes for this node and each of child nodes
    /// </summary>
    /// <returns></returns>
    [Obsolete("Use Descendants() instead, the results of this function will change in a future version")]
    public IEnumerable<HtmlNode> DescendantNodes()
    {
      foreach (HtmlNode node in (IEnumerable<HtmlNode>) this.ChildNodes)
      {
        yield return node;
        foreach (HtmlNode descendant in node.DescendantNodes())
          yield return descendant;
      }
    }

    /// <summary>
    /// Returns a collection of all descendant nodes of this element, in document order
    /// </summary>
    /// <returns></returns>
    [Obsolete("Use DescendantsAndSelf() instead, the results of this function will change in a future version")]
    public IEnumerable<HtmlNode> DescendantNodesAndSelf() => this.DescendantsAndSelf();

    /// <summary>Gets all Descendant nodes in enumerated list</summary>
    /// <returns></returns>
    public IEnumerable<HtmlNode> Descendants()
    {
      foreach (HtmlNode node in (IEnumerable<HtmlNode>) this.ChildNodes)
      {
        yield return node;
        foreach (HtmlNode descendant in node.Descendants())
          yield return descendant;
      }
    }

    /// <summary>Get all descendant nodes with matching name</summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IEnumerable<HtmlNode> Descendants(string name)
    {
      name = name.ToLowerInvariant();
      foreach (HtmlNode node in this.Descendants())
      {
        if (node.Name.Equals(name))
          yield return node;
      }
    }

    /// <summary>
    /// Returns a collection of all descendant nodes of this element, in document order
    /// </summary>
    /// <returns></returns>
    public IEnumerable<HtmlNode> DescendantsAndSelf()
    {
      yield return this;
      foreach (HtmlNode n in this.Descendants())
      {
        HtmlNode el = n;
        if (el != null)
          yield return el;
      }
    }

    /// <summary>Gets all descendant nodes including this node</summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IEnumerable<HtmlNode> DescendantsAndSelf(string name)
    {
      yield return this;
      foreach (HtmlNode node in this.Descendants())
      {
        if (node.Name == name)
          yield return node;
      }
    }

    /// <summary>Gets first generation child node matching name</summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public HtmlNode Element(string name)
    {
      foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) this.ChildNodes)
      {
        if (childNode.Name == name)
          return childNode;
      }
      return (HtmlNode) null;
    }

    /// <summary>
    /// Gets matching first generation child nodes matching name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IEnumerable<HtmlNode> Elements(string name)
    {
      foreach (HtmlNode node in (IEnumerable<HtmlNode>) this.ChildNodes)
      {
        if (node.Name == name)
          yield return node;
      }
    }

    /// <summary>
    /// Helper method to get the value of an attribute of this node. If the attribute is not found, the default value will be returned.
    /// </summary>
    /// <param name="name">The name of the attribute to get. May not be <c>null</c>.</param>
    /// <param name="def">The default value to return if not found.</param>
    /// <returns>The value of the attribute if found, the default value if not found.</returns>
    public string GetAttributeValue(string name, string def)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (!this.HasAttributes)
        return def;
      HtmlAttribute attribute = this.Attributes[name];
      return attribute == null ? def : attribute.Value;
    }

    /// <summary>
    /// Helper method to get the value of an attribute of this node. If the attribute is not found, the default value will be returned.
    /// </summary>
    /// <param name="name">The name of the attribute to get. May not be <c>null</c>.</param>
    /// <param name="def">The default value to return if not found.</param>
    /// <returns>The value of the attribute if found, the default value if not found.</returns>
    public int GetAttributeValue(string name, int def)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (!this.HasAttributes)
        return def;
      HtmlAttribute attribute = this.Attributes[name];
      if (attribute == null)
        return def;
      try
      {
        return Convert.ToInt32(attribute.Value);
      }
      catch
      {
        return def;
      }
    }

    /// <summary>
    /// Helper method to get the value of an attribute of this node. If the attribute is not found, the default value will be returned.
    /// </summary>
    /// <param name="name">The name of the attribute to get. May not be <c>null</c>.</param>
    /// <param name="def">The default value to return if not found.</param>
    /// <returns>The value of the attribute if found, the default value if not found.</returns>
    public bool GetAttributeValue(string name, bool def)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (!this.HasAttributes)
        return def;
      HtmlAttribute attribute = this.Attributes[name];
      if (attribute == null)
        return def;
      try
      {
        return Convert.ToBoolean(attribute.Value);
      }
      catch
      {
        return def;
      }
    }

    /// <summary>
    /// Inserts the specified node immediately after the specified reference node.
    /// </summary>
    /// <param name="newChild">The node to insert. May not be <c>null</c>.</param>
    /// <param name="refChild">The node that is the reference node. The newNode is placed after the refNode.</param>
    /// <returns>The node being inserted.</returns>
    public HtmlNode InsertAfter(HtmlNode newChild, HtmlNode refChild)
    {
      if (newChild == null)
        throw new ArgumentNullException(nameof (newChild));
      if (refChild == null)
        return this.PrependChild(newChild);
      if (newChild == refChild)
        return newChild;
      int num = -1;
      if (this._childnodes != null)
        num = this._childnodes[refChild];
      if (num == -1)
        throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
      if (this._childnodes != null)
        this._childnodes.Insert(num + 1, newChild);
      this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
      this.SetChanged();
      return newChild;
    }

    /// <summary>
    /// Inserts the specified node immediately before the specified reference node.
    /// </summary>
    /// <param name="newChild">The node to insert. May not be <c>null</c>.</param>
    /// <param name="refChild">The node that is the reference node. The newChild is placed before this node.</param>
    /// <returns>The node being inserted.</returns>
    public HtmlNode InsertBefore(HtmlNode newChild, HtmlNode refChild)
    {
      if (newChild == null)
        throw new ArgumentNullException(nameof (newChild));
      if (refChild == null)
        return this.AppendChild(newChild);
      if (newChild == refChild)
        return newChild;
      int index = -1;
      if (this._childnodes != null)
        index = this._childnodes[refChild];
      if (index == -1)
        throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
      if (this._childnodes != null)
        this._childnodes.Insert(index, newChild);
      this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
      this.SetChanged();
      return newChild;
    }

    /// <summary>
    /// Adds the specified node to the beginning of the list of children of this node.
    /// </summary>
    /// <param name="newChild">The node to add. May not be <c>null</c>.</param>
    /// <returns>The node added.</returns>
    public HtmlNode PrependChild(HtmlNode newChild)
    {
      if (newChild == null)
        throw new ArgumentNullException(nameof (newChild));
      this.ChildNodes.Prepend(newChild);
      this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
      this.SetChanged();
      return newChild;
    }

    /// <summary>
    /// Adds the specified node list to the beginning of the list of children of this node.
    /// </summary>
    /// <param name="newChildren">The node list to add. May not be <c>null</c>.</param>
    public void PrependChildren(HtmlNodeCollection newChildren)
    {
      if (newChildren == null)
        throw new ArgumentNullException(nameof (newChildren));
      foreach (HtmlNode newChild in (IEnumerable<HtmlNode>) newChildren)
        this.PrependChild(newChild);
    }

    /// <summary>Removes node from parent collection</summary>
    public void Remove()
    {
      if (this.ParentNode == null)
        return;
      this.ParentNode.ChildNodes.Remove(this);
    }

    /// <summary>
    /// Removes all the children and/or attributes of the current node.
    /// </summary>
    public void RemoveAll()
    {
      this.RemoveAllChildren();
      if (this.HasAttributes)
        this._attributes.Clear();
      if (this._endnode != null && this._endnode != this && this._endnode._attributes != null)
        this._endnode._attributes.Clear();
      this.SetChanged();
    }

    /// <summary>Removes all the children of the current node.</summary>
    public void RemoveAllChildren()
    {
      if (!this.HasChildNodes)
        return;
      if (this._ownerdocument.OptionUseIdAttribute)
      {
        foreach (HtmlNode childnode in (IEnumerable<HtmlNode>) this._childnodes)
          this._ownerdocument.SetIdForNode((HtmlNode) null, childnode.GetId());
      }
      this._childnodes.Clear();
      this.SetChanged();
    }

    /// <summary>Removes the specified child node.</summary>
    /// <param name="oldChild">The node being removed. May not be <c>null</c>.</param>
    /// <returns>The node removed.</returns>
    public HtmlNode RemoveChild(HtmlNode oldChild)
    {
      if (oldChild == null)
        throw new ArgumentNullException(nameof (oldChild));
      int index = -1;
      if (this._childnodes != null)
        index = this._childnodes[oldChild];
      if (index == -1)
        throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
      if (this._childnodes != null)
        this._childnodes.Remove(index);
      this._ownerdocument.SetIdForNode((HtmlNode) null, oldChild.GetId());
      this.SetChanged();
      return oldChild;
    }

    /// <summary>Removes the specified child node.</summary>
    /// <param name="oldChild">The node being removed. May not be <c>null</c>.</param>
    /// <param name="keepGrandChildren">true to keep grand children of the node, false otherwise.</param>
    /// <returns>The node removed.</returns>
    public HtmlNode RemoveChild(HtmlNode oldChild, bool keepGrandChildren)
    {
      if (oldChild == null)
        throw new ArgumentNullException(nameof (oldChild));
      if (oldChild._childnodes != null && keepGrandChildren)
      {
        HtmlNode previousSibling = oldChild.PreviousSibling;
        foreach (HtmlNode childnode in (IEnumerable<HtmlNode>) oldChild._childnodes)
          this.InsertAfter(childnode, previousSibling);
      }
      this.RemoveChild(oldChild);
      this.SetChanged();
      return oldChild;
    }

    /// <summary>Replaces the child node oldChild with newChild node.</summary>
    /// <param name="newChild">The new node to put in the child list.</param>
    /// <param name="oldChild">The node being replaced in the list.</param>
    /// <returns>The node replaced.</returns>
    public HtmlNode ReplaceChild(HtmlNode newChild, HtmlNode oldChild)
    {
      if (newChild == null)
        return this.RemoveChild(oldChild);
      if (oldChild == null)
        return this.AppendChild(newChild);
      int index = -1;
      if (this._childnodes != null)
        index = this._childnodes[oldChild];
      if (index == -1)
        throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
      if (this._childnodes != null)
        this._childnodes.Replace(index, newChild);
      this._ownerdocument.SetIdForNode((HtmlNode) null, oldChild.GetId());
      this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
      this.SetChanged();
      return newChild;
    }

    /// <summary>
    /// Helper method to set the value of an attribute of this node. If the attribute is not found, it will be created automatically.
    /// </summary>
    /// <param name="name">The name of the attribute to set. May not be null.</param>
    /// <param name="value">The value for the attribute.</param>
    /// <returns>The corresponding attribute instance.</returns>
    public HtmlAttribute SetAttributeValue(string name, string value)
    {
      HtmlAttribute htmlAttribute = name != null ? this.Attributes[name] : throw new ArgumentNullException(nameof (name));
      if (htmlAttribute == null)
        return this.Attributes.Append(this._ownerdocument.CreateAttribute(name, value));
      htmlAttribute.Value = value;
      return htmlAttribute;
    }

    /// <summary>
    /// Saves all the children of the node to the specified TextWriter.
    /// </summary>
    /// <param name="outText">The TextWriter to which you want to save.</param>
    public void WriteContentTo(TextWriter outText)
    {
      if (this._childnodes == null)
        return;
      foreach (HtmlNode childnode in (IEnumerable<HtmlNode>) this._childnodes)
        childnode.WriteTo(outText);
    }

    /// <summary>Saves all the children of the node to a string.</summary>
    /// <returns>The saved string.</returns>
    public string WriteContentTo()
    {
      StringWriter outText = new StringWriter();
      this.WriteContentTo((TextWriter) outText);
      outText.Flush();
      return outText.ToString();
    }

    /// <summary>Saves the current node to the specified TextWriter.</summary>
    /// <param name="outText">The TextWriter to which you want to save.</param>
    public void WriteTo(TextWriter outText)
    {
      switch (this._nodetype)
      {
        case HtmlNodeType.Document:
          if (this._ownerdocument.OptionOutputAsXml)
          {
            outText.Write("<?xml version=\"1.0\" encoding=\"" + this._ownerdocument.GetOutEncoding().WebName + "\"?>");
            if (this._ownerdocument.DocumentNode.HasChildNodes)
            {
              int count = this._ownerdocument.DocumentNode._childnodes.Count;
              if (count > 0)
              {
                if (this._ownerdocument.GetXmlDeclaration() != null)
                  --count;
                if (count > 1)
                {
                  if (this._ownerdocument.OptionOutputUpperCase)
                  {
                    outText.Write("<SPAN>");
                    this.WriteContentTo(outText);
                    outText.Write("</SPAN>");
                    break;
                  }
                  outText.Write("<span>");
                  this.WriteContentTo(outText);
                  outText.Write("</span>");
                  break;
                }
              }
            }
          }
          this.WriteContentTo(outText);
          break;
        case HtmlNodeType.Element:
          string name = this._ownerdocument.OptionOutputUpperCase ? this.Name.ToUpper() : this.Name;
          if (this._ownerdocument.OptionOutputOriginalCase)
            name = this.OriginalName;
          if (this._ownerdocument.OptionOutputAsXml)
          {
            if (name.Length <= 0 || name[0] == '?' || name.Trim().Length == 0)
              break;
            name = HtmlDocument.GetXmlName(name);
          }
          outText.Write("<" + name);
          this.WriteAttributes(outText, false);
          if (this.HasChildNodes)
          {
            outText.Write(">");
            bool flag = false;
            if (this._ownerdocument.OptionOutputAsXml && HtmlNode.IsCDataElement(this.Name))
            {
              flag = true;
              outText.Write("\r\n//<![CDATA[\r\n");
            }
            if (flag)
            {
              if (this.HasChildNodes)
                this.ChildNodes[0].WriteTo(outText);
              outText.Write("\r\n//]]>//\r\n");
            }
            else
              this.WriteContentTo(outText);
            outText.Write("</" + name);
            if (!this._ownerdocument.OptionOutputAsXml)
              this.WriteAttributes(outText, true);
            outText.Write(">");
            break;
          }
          if (HtmlNode.IsEmptyElement(this.Name))
          {
            if (this._ownerdocument.OptionWriteEmptyNodes || this._ownerdocument.OptionOutputAsXml)
            {
              outText.Write(" />");
              break;
            }
            if (this.Name.Length > 0 && this.Name[0] == '?')
              outText.Write("?");
            outText.Write(">");
            break;
          }
          outText.Write("></" + name + ">");
          break;
        case HtmlNodeType.Comment:
          string comment = ((HtmlCommentNode) this).Comment;
          if (this._ownerdocument.OptionOutputAsXml)
          {
            outText.Write("<!--" + HtmlNode.GetXmlComment((HtmlCommentNode) this) + " -->");
            break;
          }
          outText.Write(comment);
          break;
        case HtmlNodeType.Text:
          string text = ((HtmlTextNode) this).Text;
          outText.Write(this._ownerdocument.OptionOutputAsXml ? HtmlDocument.HtmlEncode(text) : text);
          break;
      }
    }

    /// <summary>Saves the current node to the specified XmlWriter.</summary>
    /// <param name="writer">The XmlWriter to which you want to save.</param>
    public void WriteTo(XmlWriter writer)
    {
      switch (this._nodetype)
      {
        case HtmlNodeType.Document:
          writer.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"" + this._ownerdocument.GetOutEncoding().WebName + "\"");
          if (!this.HasChildNodes)
            break;
          using (IEnumerator<HtmlNode> enumerator = ((IEnumerable<HtmlNode>) this.ChildNodes).GetEnumerator())
          {
            while (enumerator.MoveNext())
              enumerator.Current.WriteTo(writer);
            break;
          }
        case HtmlNodeType.Element:
          string localName = this._ownerdocument.OptionOutputUpperCase ? this.Name.ToUpper() : this.Name;
          if (this._ownerdocument.OptionOutputOriginalCase)
            localName = this.OriginalName;
          writer.WriteStartElement(localName);
          HtmlNode.WriteAttributes(writer, this);
          if (this.HasChildNodes)
          {
            foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) this.ChildNodes)
              childNode.WriteTo(writer);
          }
          writer.WriteEndElement();
          break;
        case HtmlNodeType.Comment:
          writer.WriteComment(HtmlNode.GetXmlComment((HtmlCommentNode) this));
          break;
        case HtmlNodeType.Text:
          string text = ((HtmlTextNode) this).Text;
          writer.WriteString(text);
          break;
      }
    }

    /// <summary>Saves the current node to a string.</summary>
    /// <returns>The saved string.</returns>
    public string WriteTo()
    {
      using (StringWriter outText = new StringWriter())
      {
        this.WriteTo((TextWriter) outText);
        outText.Flush();
        return outText.ToString();
      }
    }

    internal void SetChanged()
    {
      this._changed = true;
      if (this.ParentNode == null)
        return;
      this.ParentNode.SetChanged();
    }

    private void UpdateHtml()
    {
      this._innerhtml = this.WriteContentTo();
      this._outerhtml = this.WriteTo();
      this._changed = false;
    }

    internal static string GetXmlComment(HtmlCommentNode comment)
    {
      string comment1 = comment.Comment;
      return comment1.Substring(4, comment1.Length - 7).Replace("--", " - -");
    }

    internal static void WriteAttributes(XmlWriter writer, HtmlNode node)
    {
      if (!node.HasAttributes)
        return;
      foreach (HtmlAttribute htmlAttribute in node.Attributes.Hashitems.Values)
        writer.WriteAttributeString(htmlAttribute.XmlName, htmlAttribute.Value);
    }

    internal void CloseNode(HtmlNode endnode)
    {
      if (!this._ownerdocument.OptionAutoCloseOnEnd && this._childnodes != null)
      {
        foreach (HtmlNode childnode in (IEnumerable<HtmlNode>) this._childnodes)
        {
          if (!childnode.Closed)
          {
            HtmlNode endnode1 = new HtmlNode(this.NodeType, this._ownerdocument, -1);
            endnode1._endnode = endnode1;
            childnode.CloseNode(endnode1);
          }
        }
      }
      if (this.Closed)
        return;
      this._endnode = endnode;
      if (this._ownerdocument.Openednodes != null)
        this._ownerdocument.Openednodes.Remove(this._outerstartindex);
      if (Utilities.GetDictionaryValueOrNull<string, HtmlNode>(this._ownerdocument.Lastnodes, this.Name) == this)
      {
        this._ownerdocument.Lastnodes.Remove(this.Name);
        this._ownerdocument.UpdateLastParentNode();
      }
      if (endnode == this)
        return;
      this._innerstartindex = this._outerstartindex + this._outerlength;
      this._innerlength = endnode._outerstartindex - this._innerstartindex;
      this._outerlength = endnode._outerstartindex + endnode._outerlength - this._outerstartindex;
    }

    internal string GetId()
    {
      HtmlAttribute attribute = this.Attributes["id"];
      return attribute != null ? attribute.Value : string.Empty;
    }

    internal void SetId(string id)
    {
      HtmlAttribute htmlAttribute = this.Attributes[nameof (id)] ?? this._ownerdocument.CreateAttribute(nameof (id));
      htmlAttribute.Value = id;
      this._ownerdocument.SetIdForNode(this, htmlAttribute.Value);
      this.SetChanged();
    }

    internal void WriteAttribute(TextWriter outText, HtmlAttribute att)
    {
      string str1 = att.QuoteType == AttributeValueQuote.DoubleQuote ? "\"" : "'";
      if (this._ownerdocument.OptionOutputAsXml)
      {
        string str2 = this._ownerdocument.OptionOutputUpperCase ? att.XmlName.ToUpper() : att.XmlName;
        if (this._ownerdocument.OptionOutputOriginalCase)
          str2 = att.OriginalName;
        outText.Write(" " + str2 + "=" + str1 + HtmlDocument.HtmlEncode(att.XmlValue) + str1);
      }
      else
      {
        string str3 = this._ownerdocument.OptionOutputUpperCase ? att.Name.ToUpper() : att.Name;
        if (this._ownerdocument.OptionOutputOriginalCase)
          str3 = att.OriginalName;
        if (att.Name.Length >= 4 && att.Name[0] == '<' && att.Name[1] == '%' && att.Name[att.Name.Length - 1] == '>' && att.Name[att.Name.Length - 2] == '%')
          outText.Write(" " + str3);
        else if (this._ownerdocument.OptionOutputOptimizeAttributeValues)
        {
          if (att.Value.IndexOfAny(new char[4]
          {
            '\n',
            '\r',
            '\t',
            ' '
          }) < 0)
            outText.Write(" " + str3 + "=" + att.Value);
          else
            outText.Write(" " + str3 + "=" + str1 + att.Value + str1);
        }
        else
          outText.Write(" " + str3 + "=" + str1 + att.Value + str1);
      }
    }

    internal void WriteAttributes(TextWriter outText, bool closing)
    {
      if (this._ownerdocument.OptionOutputAsXml)
      {
        if (this._attributes == null)
          return;
        foreach (HtmlAttribute att in this._attributes.Hashitems.Values)
          this.WriteAttribute(outText, att);
      }
      else if (!closing)
      {
        if (this._attributes != null)
        {
          foreach (HtmlAttribute attribute in (IEnumerable<HtmlAttribute>) this._attributes)
            this.WriteAttribute(outText, attribute);
        }
        if (!this._ownerdocument.OptionAddDebuggingAttributes)
          return;
        this.WriteAttribute(outText, this._ownerdocument.CreateAttribute("_closed", this.Closed.ToString()));
        this.WriteAttribute(outText, this._ownerdocument.CreateAttribute("_children", this.ChildNodes.Count.ToString()));
        int num = 0;
        foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) this.ChildNodes)
        {
          this.WriteAttribute(outText, this._ownerdocument.CreateAttribute("_child_" + (object) num, childNode.Name));
          ++num;
        }
      }
      else
      {
        if (this._endnode == null || this._endnode._attributes == null || this._endnode == this)
          return;
        foreach (HtmlAttribute attribute in (IEnumerable<HtmlAttribute>) this._endnode._attributes)
          this.WriteAttribute(outText, attribute);
        if (!this._ownerdocument.OptionAddDebuggingAttributes)
          return;
        this.WriteAttribute(outText, this._ownerdocument.CreateAttribute("_closed", this.Closed.ToString()));
        this.WriteAttribute(outText, this._ownerdocument.CreateAttribute("_children", this.ChildNodes.Count.ToString()));
      }
    }

    private string GetRelativeXpath()
    {
      if (this.ParentNode == null)
        return this.Name;
      if (this.NodeType == HtmlNodeType.Document)
        return string.Empty;
      int num = 1;
      foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) this.ParentNode.ChildNodes)
      {
        if (!(childNode.Name != this.Name))
        {
          if (childNode != this)
            ++num;
          else
            break;
        }
      }
      return this.Name + "[" + (object) num + "]";
    }
  }
}
