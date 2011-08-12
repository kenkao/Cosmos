﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using System.ComponentModel.Design;

namespace Cosmos.Cosmos_VS_Windows
{
    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    ///
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane, 
    /// usually implemented by the package implementer.
    ///
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its 
    /// implementation of the IVsUIElementPane interface.
    /// </summary>
    [Guid("f019fb29-c2c2-4d27-9abf-739533c939be")]
    public class AssemblyTW : ToolWindowPane
    {
        public static AssemblyUC mUC;

        public AssemblyTW() : base(null)
        {
            //this.ToolBar = new CommandID(GuidList.guidAsmToolbarCmdSet, (int)PkgCmdIDList.AsmToolbar);
            this.Caption = "Cosmos Assembly Window";

            // Set the image that will appear on the tab of the window frame
            // when docked with an other window
            // The resource ID correspond to the one defined in the resx file
            // while the Index is the offset in the bitmap strip. Each image in
            // the strip being 16x16.
            this.BitmapResourceID = 301;
            this.BitmapIndex = 1;

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on 
            // the object returned by the Content property.
            mUC = new AssemblyUC();
            base.Content = mUC;
        }
    }
}