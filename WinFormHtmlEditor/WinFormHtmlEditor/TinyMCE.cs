﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WinFormHtmlEditor
{
  public partial class TinyMCE : UserControl
  {
    public TinyMCE()
    {
      InitializeComponent();
    }

      public object ActiveXInstance
      {
          get { return webBrowserControl.ActiveXInstance; }
      }

    public string HtmlContent
    {
      get
      {
        string content = string.Empty;
        if (webBrowserControl.Document != null)
        {
          object html = webBrowserControl.Document.InvokeScript("GetContent");
          content = html as string;
        }
        return content;
      }
      set
      {
          webBrowserControl.Document?.InvokeScript("SetContent", new object[] { value });
      }
    }

    public void CreateEditor()
    {
      // Check if the main script file exist being used by the HTML page
      if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"tinymce\js\tinymce\tinymce.min.js")))
      {
        webBrowserControl.Url = new Uri(@"file:///" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"tinymce/tinymce.htm").Replace('\\', '/'));
      }
      else
      {
        MessageBox.Show("Could not find the tinyMCE script directory. Please ensure the directory is in the same location as tinymce.htm", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}