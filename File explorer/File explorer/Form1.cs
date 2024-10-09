using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace File_explorer
{
	public partial class Form1 : Form
	{
		private ImageList imageList = new ImageList();
		private List<string> navigationHistory = new List<string>();
		private int currentHistoryIndex = -1;
		public Form1()
		{
			InitializeComponent();
			SetupListView();
			SetupImageList();
			InitializeContextMenu();
			LoadFilesAndDirectories("D:\\"); // Load thư mục gốc D: khi khởi động
		}
		// Thiết lập ImageList và thêm các icon mặc định
		private void SetupImageList()
		{
			imageList.ImageSize = new Size(16, 16);
			imageList.Images.Add("folder", SystemIcons.WinLogo.ToBitmap());
			imageList.Images.Add("file", SystemIcons.WinLogo.ToBitmap());
			listView1.SmallImageList = imageList;
		}
		// Thiết lập ListView với các cột
		private void SetupListView()
		{
			listView1.View = View.Details;
			listView1.FullRowSelect = true;
			listView1.Columns.Add("Name", 200);
			listView1.Columns.Add("Date modified", 150);
			listView1.Columns.Add("Type", 100);
			listView1.Columns.Add("Size", 100);

			listView1.MouseDoubleClick += listView1_MouseDoubleClick;
			listView1.MouseClick += listView1_MouseClick;
		}
		// Tải các thư mục và tập tin vào ListView	

		private void LoadFilesAndDirectories(string path)
		{
			listView1.Items.Clear();
			DirectoryInfo dirInfo = new DirectoryInfo(path);

			listView1.Items.Clear();

			try
			{
				foreach (var dir in dirInfo.GetDirectories())
				{
					ListViewItem item = new ListViewItem(dir.Name);
					item.ImageKey = "folder";
					item.SubItems.Add(dir.LastWriteTime.ToString());
					item.SubItems.Add("File folder");
					item.SubItems.Add("");
					item.Tag = dir.FullName;
					listView1.Items.Add(item);
				}

				foreach (var file in dirInfo.GetFiles())
				{
					ListViewItem item = new ListViewItem(file.Name);
					item.ImageKey = "file";
					item.SubItems.Add(file.LastWriteTime.ToString());
					item.SubItems.Add(file.Extension);
					item.SubItems.Add((file.Length / 1024).ToString() + " KB");
					item.Tag = file.FullName;
					listView1.Items.Add(item);
				}

				// Cập nhật lịch sử nếu đang duyệt thư mục mới
				if (currentHistoryIndex == -1 || navigationHistory[currentHistoryIndex] != path)
				{
					if (currentHistoryIndex < navigationHistory.Count - 1)
					{
						navigationHistory.RemoveRange(currentHistoryIndex + 1, navigationHistory.Count - currentHistoryIndex - 1);
					}
					navigationHistory.Add(path);
					currentHistoryIndex++;
				}

				txtPath.Text = path;
			}
			catch (UnauthorizedAccessException)
			{
				MessageBox.Show("Access to the path is denied.");
			}
		}

		private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (listView1.SelectedItems.Count > 0)
			{
				ListViewItem item = listView1.SelectedItems[0];
				string selectedPath = item.Tag.ToString();

				if (Directory.Exists(selectedPath))
				{
					LoadFilesAndDirectories(selectedPath);
				}
			}
		}
		private void listView1_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right && listView1.FocusedItem != null && listView1.FocusedItem.Bounds.Contains(e.Location))
			{
				ContextMenuStrip contextMenu = new ContextMenuStrip();
				contextMenu.Items.Add("Open", null, (s, ev) =>
				{
					if (Directory.Exists(listView1.FocusedItem.Tag.ToString()))
					{
						LoadFilesAndDirectories(listView1.FocusedItem.Tag.ToString());
					}
				});
				contextMenu.Items.Add("Delete", null, (s, ev) =>
				{
					var selectedPath = listView1.FocusedItem.Tag.ToString();
					if (Directory.Exists(selectedPath))
					{
						Directory.Delete(selectedPath);
					}
					else if (File.Exists(selectedPath))
					{
						File.Delete(selectedPath);
					}
					LoadFilesAndDirectories(Path.GetDirectoryName(selectedPath));
				});
				contextMenu.Show(listView1, e.Location);
			}
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void btnLeft_Click(object sender, EventArgs e)
		{
			if (currentHistoryIndex > 0)
			{
				currentHistoryIndex--;
				string previousPath = navigationHistory[currentHistoryIndex];
				LoadFilesAndDirectories(previousPath);
			}
		}

		private void btnRight_Click(object sender, EventArgs e)
		{
			if (currentHistoryIndex < navigationHistory.Count - 1)
			{
				currentHistoryIndex++;
				string nextPath = navigationHistory[currentHistoryIndex];
				LoadFilesAndDirectories(nextPath);
			}
		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			string path = txtPath.Text;
			if (Directory.Exists(path))
			{
				LoadFilesAndDirectories(path);
			}
			else
			{
				MessageBox.Show("The path does not exist.");
			}
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}
		private string clipboardPath = null;
		private bool isCutOperation = false;

		private void InitializeContextMenu()
		{
			ContextMenuStrip contextMenu = new ContextMenuStrip();

			// Tạo các mục menu
			ToolStripMenuItem copyItem = new ToolStripMenuItem("Copy");
			copyItem.Click += CopyItem_Click;
			contextMenu.Items.Add(copyItem);

			ToolStripMenuItem cutItem = new ToolStripMenuItem("Cut");
			cutItem.Click += CutItem_Click;
			contextMenu.Items.Add(cutItem);

			ToolStripMenuItem pasteItem = new ToolStripMenuItem("Paste");
			pasteItem.Click += PasteItem_Click;
			contextMenu.Items.Add(pasteItem);

			ToolStripMenuItem deleteItem = new ToolStripMenuItem("Delete");
			deleteItem.Click += DeleteItem_Click;
			contextMenu.Items.Add(deleteItem);

			ToolStripMenuItem newFolderItem = new ToolStripMenuItem("New Folder");
			newFolderItem.Click += NewFolderItem_Click;
			contextMenu.Items.Add(newFolderItem);

			listView1.ContextMenuStrip = contextMenu;
		}

		private void CopyItem_Click(object sender, EventArgs e)
		{
			if (listView1.SelectedItems.Count > 0)
			{
				clipboardPath = listView1.SelectedItems[0].Tag.ToString();
				isCutOperation = false;
			}
		}

		private void CutItem_Click(object sender, EventArgs e)
		{
			if (listView1.SelectedItems.Count > 0)
			{
				clipboardPath = listView1.SelectedItems[0].Tag.ToString();
				isCutOperation = true;
			}
		}

		private void PasteItem_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(clipboardPath))
			{
				string destinationPath = Path.Combine(txtPath.Text, Path.GetFileName(clipboardPath));

				try
				{
					if (File.Exists(clipboardPath))
					{
						if (isCutOperation)
						{
							File.Move(clipboardPath, destinationPath);
						}
						else
						{
							File.Copy(clipboardPath, destinationPath);
						}
					}
					else if (Directory.Exists(clipboardPath))
					{
						if (isCutOperation)
						{
							Directory.Move(clipboardPath, destinationPath);
						}
						else
						{
							CopyDirectory(clipboardPath, destinationPath);
						}
					}
					LoadFilesAndDirectories(txtPath.Text);
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred: " + ex.Message);
				}
				finally
				{
					clipboardPath = null;
					isCutOperation = false;
				}
			}
		}

		private void DeleteItem_Click(object sender, EventArgs e)
		{
			if (listView1.SelectedItems.Count > 0)
			{
				string selectedPath = listView1.SelectedItems[0].Tag.ToString();
				try
				{
					if (File.Exists(selectedPath))
					{
						File.Delete(selectedPath);
					}
					else if (Directory.Exists(selectedPath))
					{
						Directory.Delete(selectedPath, true);
					}
					LoadFilesAndDirectories(txtPath.Text);
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred: " + ex.Message);
				}
			}
		}

		private void NewFolderItem_Click(object sender, EventArgs e)
		{
			string newFolderPath = Path.Combine(txtPath.Text, "New Folder");
			int count = 1;
			while (Directory.Exists(newFolderPath))
			{
				newFolderPath = Path.Combine(txtPath.Text, $"New Folder ({count++})");
			}

			try
			{
				Directory.CreateDirectory(newFolderPath);
				LoadFilesAndDirectories(txtPath.Text);
			}
			catch (Exception ex)
			{
				MessageBox.Show("An error occurred: " + ex.Message);
			}
		}

		// Helper method to copy directories
		private void CopyDirectory(string sourceDir, string destinationDir)
		{
			Directory.CreateDirectory(destinationDir);
			foreach (var file in Directory.GetFiles(sourceDir))
			{
				string destFile = Path.Combine(destinationDir, Path.GetFileName(file));
				File.Copy(file, destFile);
			}

			foreach (var dir in Directory.GetDirectories(sourceDir))
			{
				string destDir = Path.Combine(destinationDir, Path.GetFileName(dir));
				CopyDirectory(dir, destDir);
			}
		}
	}
}
