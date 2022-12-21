using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;
using System.Windows.Forms;

namespace System.Windows.Forms;

//[System.ComponentModel.Designer("System.Windows.Forms.Design.DocumentDesigner, System.Windows.Forms.Design",
//typeof(System.ComponentModel.Design.IRootDesigner)),
//System.ComponentModel.DesignerCategory("")]
//[Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner)), System.ComponentModel.DesignerCategory("")]
public partial class ListViewEx : System.Windows.Forms.ListView
{
    public string DummyName { get; set; } = "Dummy";
    private System.Windows.Forms.ListViewItem heldDownItem;
    //private System.Windows.Forms.ListViewGroup heldDownGroup;
    private System.Drawing.Point heldDownPoint;

    public ListViewEx()
    {
        InitializeComponent();

        typeof(Control).GetProperty("DoubleBuffered",
                         System.Reflection.BindingFlags.NonPublic |
                         System.Reflection.BindingFlags.Instance)?
           .SetValue(this, true, null);


        this.AllowDrop = true;
        this.FullRowSelect = true;
        this.HideSelection = false;
        this.UseCompatibleStateImageBehavior = false;
        this.View = System.Windows.Forms.View.LargeIcon;

    }

    protected override void OnPaint(PaintEventArgs pe)
    {
        base.OnPaint(pe);
    }

    protected override void OnDragDrop(DragEventArgs e)
    {
        var localPoint = this.PointToClient(new System.Drawing.Point(e.X, e.Y));
        var group = this.GetItemAt(localPoint.X, localPoint.Y);
        var item = e.Data.GetData(DataFormats.Text).ToString();
        this.Items.Add(new System.Windows.Forms.ListViewItem { Group = group.Group, Text = item });
    }

    protected override void OnDragEnter(DragEventArgs e)
    {
        e.Effect = System.Windows.Forms.DragDropEffects.Move;
    }

    // https://www.codeproject.com/articles/14487/manual-reordering-of-items-inside-a-listview
    // 
    protected override void OnMouseDown(MouseEventArgs e)
    {
        //listView1.AutoArrange = false;

        heldDownItem = this.GetItemAt(e.X, e.Y);

        //heldDownGroup = this.gett
        if (heldDownItem != null)
        {
            heldDownPoint = new System.Drawing.Point(e.X - heldDownItem.Position.X,
                                      e.Y - heldDownItem.Position.Y);
        }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        if (heldDownItem != null)
        {
            heldDownItem.Position = new System.Drawing.Point(e.Location.X - heldDownPoint.X,
                                              e.Location.Y - heldDownPoint.Y);
        }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        //var localPoint = listViewA.PointToClient(new Point(e.X, e.Y));
        if (heldDownItem != null && heldDownItem.Name != DummyName)
        {
            var group = this.GetItemAt(e.X, e.Y);
            if (group != null)
            {
                if (this.Groups[heldDownItem.Group.Header].Items.Count == 1)
                {
                    var emptyItem = new System.Windows.Forms.ListViewItem(String.Empty) { Name = DummyName };
                    emptyItem.Group = heldDownItem.Group;
                    //emptyItem.Tag = heldDownItem.Group.Name;
                    this.Items.Add(emptyItem);
                }
                heldDownItem.Group = group.Group;
                this.RemoveEmptyItems(group.Group.Header);

                //heldDownItem.Group.Items.RemoveByKey(System.String.Empty);
                //listViewA.Groups[listViewA.Groups.Count - 1].Items.RemoveByKey(String.Empty);
            }
        }
        heldDownItem = null;
        //listView1.AutoArrange = true; 
    }

    /// <summary>
    /// Append a group to the list view with a generic header name
    /// </summary>
    public void AddGroup(string str = "Task")
    {
        AddGroup(this.Groups.Count, str);
    }

    /// <summary>
    /// Append a group to the list view with a specific header name
    /// </summary>
    /// <param name="index">Group number</param>
    public void AddGroup(int index, string str = "Task")
    {
        var strHeader = $"{str} {((char)('A' + index)).ToString()}";
        var _index = this.Groups.Add(new ListViewGroup(strHeader, strHeader));
        //var _index = this.Groups.Add(new ListViewGroup(strHeader) { Name = strHeader });
        AddEmptyItem(_index);
    }

    /// <summary>
    /// Adds a dummy item in a group
    /// </summary>
    /// <param name="group">Zero-index group where the item will be added</param>
    public void AddEmptyItem(int group)
    {
        var emptyItem = new ListViewItem(String.Empty)
        {
            Group = this.Groups[group],
            Name = DummyName,
            Tag = this.Groups[group].Name
        };
        this.Items.Add(emptyItem);
    }

    /// <summary>
    /// Shows all groups by adding an empty item whenever some group does not have any item
    /// </summary>
    public void ShowAllGroups()
    {
        int i = 0;
        foreach(ListViewGroup group in this.Groups)
        {
            if (group.Items.Count == 0)
                AddEmptyItem(i);
            i++;
        }
    }
    /// <summary>
    /// Deletes all empty items in the ListView
    /// </summary>
    public void RemoveEmptyItems()
    {
        for (int i = 0; i < this.Groups.Count; i++)
        {
            RemoveEmptyItems(i);
        }
        // this.Items.RemoveByKey(DummyName);
    }
    /// <summary>
    /// Delete any empty item in a given group
    /// </summary>
    /// <param name="GroupIndex">Group index</param>
    public void RemoveEmptyItems(int GroupIndex)
    {
        int totalItems = this.Groups[GroupIndex].Items.Count;
        for (int i = totalItems; i > 0; i--)
        {
            if (totalItems > 1 && this.Groups[GroupIndex].Items[i - 1].Name == DummyName)
                this.Items.Remove(this.Groups[GroupIndex].Items[i - 1]);
        }
    }

    /// <summary>
    /// Delete any empty item in a given group
    /// </summary>
    /// <param name="header">Group header</param>
    public void RemoveEmptyItems(string header)
    {
        for (int i = 0; i < this.Groups.Count; i++)
        {
            if (this.Groups[i].Header == header)
            {
                RemoveEmptyItems(i);
            }
        }
    }

    /// <summary>
    /// Removes all groups in the Groups collection containing only "Dummy" items
    /// </summary>
    public void RemoveEmptyGroups()
    {
        for (int i = this.Groups.Count; i > 0; i--)
        {
            if (this.Groups[i - 1].Items.Count <= 1)
            {
                if (this.Groups[i - 1].Items[0].Name == DummyName)
                    this.RemoveGroup(i - 1);
            }
        }
    }

    /// <summary>
    /// Removes the last group in the Groups collection
    /// </summary>
    public void RemoveLastGroup()
    {
        if (this.Groups.Count >= 1)
            RemoveGroup(this.Groups.Count - 1);
    }

    /// <summary>
    /// Remove all gropus from the ListView
    /// </summary>
    public void RemoveGroups()
    {
        for (int i = 0; i < this.Groups.Count; i++)
            RemoveGroup(i);
    }

    /// <summary>
    /// Delete one group from the ListView. It moves the items to the previous group if there is one available
    /// </summary>
    /// <param name="index">0-based index of the group to be deleted</param>
    public void RemoveGroup(int index)
    {
        // First, move items to the previous group if there is at least 2 groups
        if (index > 0)
        {
            for (int i = this.Groups[index].Items.Count; i > 0; i--)
            {
                // If a "Dummy" item, delete. Otherwise, move it to the lower group
                if (this.Groups[index].Items[i - 1].Name == DummyName)
                    this.Items.Remove(this.Groups[index].Items[i - 1]);
                else
                    this.Groups[index].Items[i - 1].Group = this.Groups[index - 1];
            }
            RemoveEmptyItems(index - 1);
        }

        // Finally, delete group
        this.Groups.RemoveAt(index);
    }
}
