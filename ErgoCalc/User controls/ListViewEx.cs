using System;
//using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    //[System.ComponentModel.Designer("System.Windows.Forms.Design.DocumentDesigner, System.Windows.Forms.Design",
    //typeof(System.ComponentModel.Design.IRootDesigner)),
    //System.ComponentModel.DesignerCategory("")]
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner)), System.ComponentModel.DesignerCategory("")]
    public class ListViewEx : System.Windows.Forms.ListView
    {
        private System.Windows.Forms.ListViewItem heldDownItem;
        private System.Windows.Forms.ListViewGroup heldDownGroup;
        private System.Drawing.Point heldDownPoint;

        public ListViewEx()
        {
            typeof(Control).GetProperty("DoubleBuffered",
                             System.Reflection.BindingFlags.NonPublic |
                             System.Reflection.BindingFlags.Instance)
               .SetValue(this, true, null);


            this.AllowDrop = true;
            this.FullRowSelect = true;
            this.HideSelection = false;
            //this.listViewC.Location = new System.Drawing.Point(21, 53);
            //this.listViewC.Name = "listViewC";
            //this.listViewC.Size = new System.Drawing.Size(401, 392);
            //this.listViewC.TabIndex = 4;
            this.UseCompatibleStateImageBehavior = false;
            this.View = System.Windows.Forms.View.Details;

            // Set 1 group
            AddGroup();
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
            if (heldDownItem != null)
            {
                var group = this.GetItemAt(e.X, e.Y);
                if (group != null)
                {
                    if (this.Groups[heldDownItem.Group.Header].Items.Count == 1)
                    {
                        var emptyItem = new System.Windows.Forms.ListViewItem(String.Empty);
                        emptyItem.Group = heldDownItem.Group;
                        //emptyItem.Tag = heldDownItem.Group.Name;
                        this.Items.Add(emptyItem);
                    }
                    heldDownItem.Group = group.Group;
                    heldDownItem.Group.Items.RemoveByKey(System.String.Empty);
                    //listViewA.Groups[listViewA.Groups.Count - 1].Items.RemoveByKey(String.Empty);
                }
            }
            heldDownItem = null;
            //listView1.AutoArrange = true; 
        }

        public void DeleteEmptyItems()
        {
            this.Items.RemoveByKey("Dummy");
        }
        public void DeleteEmptyItems(int GroupIndex)
        {           
            this.Groups[GroupIndex].Items.RemoveByKey("Dummy");
        }

        public void AddGroup()
        {
            AddGroup(this.Groups.Count);
        }

        public void AddGroup(int index)
        {
            var strHeader = "Task " + ((char)('A' + index)).ToString();
            var _index = this.Groups.Add(new ListViewGroup(strHeader, strHeader));
            //var _index = this.Groups.Add(new ListViewGroup(strHeader) { Name = strHeader });
            var emptyItem = new ListViewItem(String.Empty)
            {
                Group = this.Groups[_index],
                Name = "Dummy",
                Tag = this.Groups[_index].Name
            };
            this.Items.Add(emptyItem);
        }

        public void RemoveGroup()
        {
            if (this.Groups.Count >= 1)
                RemoveGroup(this.Groups.Count - 1);
        }

        public void RemoveGroups()
        {

        }

        public void RemoveGroup(int index)
        {
            foreach (System.Windows.Forms.ListViewItem item in this.Groups[index].Items)
            {
                if (item.Name == "Dummy")
                {
                    this.Items.RemoveByKey("Dummy");
                }
                else
                {
                    item.Group = this.Groups[this.Groups.Count - 2];
                }
            }
            this.Groups.RemoveAt(index);
        }
    }
}
