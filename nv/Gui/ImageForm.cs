/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     imageform.cs
Description:   image form
Copyright:     Copyright (c) Martin A. Schnell, 2012
Licence:       GNU General Public License
               This program is free software; you can redistribute it and/or
               modify it under the terms of the GNU General Public License
               as published by the Free Software Foundation.

               This program is free software: you can redistribute it and/or modify
               it under the terms of the GNU General Public License as published by
               the Free Software Foundation, either version 3 of the License, or
               (at your option) any later version.
History:

* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using System;
using System.Collections.Generic;  // list
using System.Drawing;  // Bitmap
using System.Diagnostics;  // Debug
using System.IO;   // directory
using System.Linq;   // OfType
using System.Threading;  // Thread
using System.Windows.Forms;  // form
using Next_View.Properties;
using WeifenLuo.WinFormsUI.Docking;
using ProXoft.WinForms;   // Scollbar

namespace Next_View
{
    /// <summary>
    /// Description of StatsForm.
    /// </summary>
    public partial class frmImage : DockContent
    {
        Form _fM;
        ImgList _il = new ImgList();
        int _scHeight = 0;
        int _scWidth = 0;
        int _mainWidth = 0;
        int _mainHeight = 0;
        int _borderHeight = 0;
        int _borderWidth = 0;
        int _currentWidth = 0;
        int _currentHeight = 0;
        string _picSelection = "";    // directory, history, sarch, selection
        string _currentPath = "";
        string _orientationStr = "";
        int _oriInitial = -100;
        int _oriCurrent = -100;
        int _exifType = 0;
        string _lastSearchStr = "";
        Image _myImg;
        GifImage _gifImage = null;     // to load frames
        bool _loadNextPic = true;
        bool _dirChanged  = true;
        bool _isGif  = false;
        bool _setDark = false;
        bool _processHide = true;
        
        int _currentScrollPos = 0;
        Color[] _colors = {Color.Aqua, Color.Magenta, Color.Blue, Color.Lime, Color.Yellow, Color.Red};
        List<int> _posList = new List<int>();           //  set scrollbar marks outside background worker
        string _posListSelector = "+";
        List<int> _priorList = new List<int>();
        Dictionary<int, DateTime> _rangeDict = new Dictionary<int, DateTime>();
        int _rangeType = 0;
        DateTime _r0Date = DateTime.MinValue;
        bool _barClick = false;     // for scrollbar change

        WinType _wType;   // normal, full, second
        public bool _ndRunning {get;set;}
        public bool _fullRunning {get;set;}
        string _priorPath = "";


        ExifForm m_Exif;

        public frmImage  m_Image2;
        public frmImage  m_ImageF;

        public event HandleStatusMainChange  StatusChanged;

        public event HandleWindowMainChange  WindowChanged;

        public event HandleWindowSize WindowSize;

        public event HandleCommandChange  CommandChanged;

        public event HandleSelfChange  SelfChanged;

        public frmImage(int mainWidth, int mainHeight, WinType wType, ImgList il)
        // 2nd screen, full screen
        {
            InitializeComponent();
            _wType = wType;
            _il = il;
            picBox.Left = 0;
            Scrollbar1.Visible = false;
                
            _mainWidth = mainWidth;
            _mainHeight = mainHeight;
        }

        public frmImage(Form fM, WinType wType)
        // main image
        {
            InitializeComponent();
            _wType = wType;

            _fM = fM;
            // use _fM.Width
            //     _fM.Width
            _mainWidth = _fM.Width;
            _mainHeight = _fM.Height;
        }



        //------------------------------   events form ----------------------------------------------------------

        private void HandleKey(object sender, SetKeyEventArgs e)
        // HandleKeyChange for exif, rename form
        {
            int kVal = e.kValue;
            bool alt = e.alt;
            bool ctrl = e.ctrl;
            KDown(kVal, ctrl, alt);
        }

        private void HandleSelf(object sender, SetSelfEventArgs e)
        // return from full to normal ImageForm, setSelf
        {
          if (e.Fname == "d"){
              DarkPic2();
          } 
          else if (e.Fname == "x"){
              //Debug.WriteLine("handle x: " + _wType);
              SetCommand('n', "");    // show main
          }
          else
          {
              _currentPath = e.Fname;
              PicLoadPos(_currentPath, true);
              CalcBar();
              DrawBar();
          }
        }

        void FrmImageLoad(object sender, EventArgs e)
        {
            if (_wType == WinType.normal){
                _ndRunning = false;
                _fullRunning = false;
                
                _scWidth = Screen.FromControl(this).Bounds.Width;
                _scHeight = Screen.FromControl(this).Bounds.Height;               
                picBox.BackColor = SystemColors.Control;
                
            }
            if (_wType == WinType.full){
                _ndRunning = false;
                _fullRunning = true;
                _scWidth = Screen.FromControl(this).Bounds.Width;
                _scHeight = Screen.FromControl(this).Bounds.Height;
                //Debug.WriteLine("Full Image W / H: {0}/{1}", _scWidth, _scHeight);
                ScollbarVis(false);
                //this.Width = _scWidth;
                //this.Height = _scHeight;
                //this.Top = 0;
                //this.Left = 0;

                picBox.BackColor = Color.Black;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }

            TranslateImageForm();

            if (_wType == WinType.second){
                popClose.Text = T._("Close");    // not Exit

                int wX;
                int wY;
                int wW = Settings.Default.SecondW;
                int wH = Settings.Default.SecondH;
                Multi.SecondLoad(out wX, out wY);

                bool visible;
                // menu bar visible
                Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);
                int titleHeight = screenRectangle.Top - this.Top;
                Multi.FormShowVisible(out visible, ref wX, ref wY, wW, titleHeight);
                if (!visible){
                    this.Left = wX;
                    this.Top = wY;
                }
                else {
                    Multi.FormShowVisible(out visible, ref wX, ref wY, wW, wH);
                    this.Left = wX;
                    this.Top = wY;
                }
                this.Width = wW;
                this.Height = wH;
                this.Icon = Icon1.Icon;
                //Debug.WriteLine("open 2nd y: {0} ", Settings.Default.SecondY);

                _ndRunning = true;
                _fullRunning = false;
                _mainWidth = picBox.Width;
                _mainHeight = picBox.Height;
                _scWidth = this.Width;
                _scHeight = this.Height;
                picBox.BackColor = SystemColors.Control;
            }  // end 2nd
        }

        void FrmImageShown(object sender, EventArgs e)
        {
            CalcBorderSize();
        }

        void FrmImageEnter(object sender, EventArgs e)
        {
            if (_currentWidth > 0){
                //RefreshDir();  msn
                SetWindowSize(_currentWidth, _currentHeight, _exifType);
            }
        }

        void FrmImageLeave(object sender, EventArgs e)
        {
            //Debug.WriteLine("img: leave");
        }

        void FrmImageHelpRequested(object sender, HelpEventArgs hlpevent)
        {
            var c = this.ActiveControl;
            if(c!=null)
                MessageBox.Show(c.Name);
        
            //Help.ShowHelp(this, "Next-View.chm", "Fieldlist.htm");
            //MessageBox.Show("Help not yet done", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        void FrmImageFormClosing(object sender, FormClosingEventArgs e)
        {
            if (_wType == WinType.second){
                _ndRunning = false;
                int wX = this.Left;
                int wY = this.Top;
                Multi.SecondSave(wX, wY);
                Settings.Default.SecondW = this.Width;
                Settings.Default.SecondH = this.Height;
                Settings.Default.Save( );
                //Debug.WriteLine("close 2nd y: {0} ", Settings.Default.SecondY);
            }
            if (_wType == WinType.normal){
                e.Cancel = true;
                this.Hide();
                //Debug.WriteLine("hide img ");
            }
            if (_wType == WinType.full){
                _fullRunning = false;
            }
            if (CanShowExif()){
                m_Exif.Close();
            }
        }

        void FrmImageFormClosed(object sender, FormClosedEventArgs e)
        {

        }

        void RClose()
        // remote close
        {
            //this.Hide();
            this.Close();
        }

        //------------------------------   drop  ----------------------------------------------------------

        void FrmImageDragDrop(object sender, DragEventArgs e)
        {
            bool allDirs = false;
            if ((e.KeyState & 8) == 8){
                //Debug.WriteLine("ctrl");
                allDirs = true;
            }

            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Copy;
                ProcessDrop((string[])e.Data.GetData(DataFormats.FileDrop), allDirs);

            }
            else {
                e.Effect = DragDropEffects.None;
            }
        }

        public void ProcessDrop(string[] files, bool allDirs)
        {
            int picCount = 0;
            int dirCount = 0;
            //Array.Sort(files);

            string loadFile = "";
            _il.DirClear();
            foreach (string dropFile in files)
            {
                string dropDir = "";
                if (File.Exists(dropFile)) {
                    if (_il.FileIsValid(dropFile)){
                        picCount++;
                        _il.DirPicAdd(dropFile);     // for multi file drop
                        dropDir = Path.GetDirectoryName(dropFile);
                        loadFile = dropFile;
                    }
                    else{
                        string ext = Path.GetExtension(dropFile).ToLower();
                        MessageBox.Show(String.Format(T._("File type {0} not supported"), ext), T._("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else if (Directory.Exists(dropFile)){ // is dir
                    dirCount++;
                    dropDir = dropFile;
                    loadFile = dropFile;
                    //Debug.WriteLine("drop dir " + dropDir);
                }
                else if (dropDir  == ""){
                    MessageBox.Show(T._("No drop dir"), T._("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }  // end for

            if (picCount == 1) {
                _picSelection = T._("Directory:");
                PicScan(loadFile, allDirs, 0);
            }
            else if (picCount > 0){
                _picSelection = T._("Selection:");
                // pic list already loaded
            }
            else if (dirCount > 0){
                _picSelection = T._("Directory:");
                PicScan(loadFile, allDirs, 1);  // rescan for lower
            }
            else {
                //MessageBox.Show("No drop selection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (loadFile != ""){
                if (File.Exists(loadFile)) {
                    PicLoadPos(loadFile, true);
                    CalcBar();
                    DrawBar();
                    SetCommand('r', loadFile);   // recent
                }
                // else load with RunWorkerCompleted
            }
            else {
                picBox.Image = null;
                SetStatusText(0, T._("No image loaded"));
            }
        }

        void FrmImageDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        void FrmImageDragOver(object sender, DragEventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control)) {
                // control is pressed. Copy.
                e.Effect = DragDropEffects.Copy;    // + sign for sub-dirs
            }
            else {
                e.Effect = DragDropEffects.Move;
            }
        }

        //------------------------------   key functions  ----------------------------------------------------------

        void FrmImageKeyDown(object sender, KeyEventArgs e)
        {

            //Debug.WriteLine(" ");
            //Debug.WriteLine("key: " + e.KeyValue.ToString());

            bool alt = false;
            if (e.Modifiers == Keys.Alt){
                alt = true;
            }
            bool ctrl = false;
            if (e.Modifiers == Keys.Control){
                ctrl = true;
            }
            KDown(e.KeyValue, ctrl, alt);
            //else Debug.WriteLine("eat up1: " + e.KeyValue);
        }

        void FrmImagePreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;     // triggers keydown for arrow keys
        }

        void Scollbar1KeyDown(object sender, KeyEventArgs e)
        {
            bool alt = false;
            if (e.Modifiers == Keys.Alt){
                alt = true;
            }
            bool ctrl = false;
            if (e.Modifiers == Keys.Control){
                ctrl = true;
            }
            KDown(e.KeyValue, ctrl, alt);
        }

        void Scollbar1PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;     // triggers keydown for arrow keys
        }

        void SplitContainer1KeyDown(object sender, KeyEventArgs e)
        {
            bool alt = false;
            if (e.Modifiers == Keys.Alt){
                alt = true;
            }
            bool ctrl = false;
            if (e.Modifiers == Keys.Control){
                ctrl = true;
            }
            KDown(e.KeyValue, ctrl, alt);
        }

        void SplitContainer1KeyUp(object sender, KeyEventArgs e)
        {

        }

        public bool KDown(int kValue, bool ctrl, bool alt)
        {
          //Debug.WriteLine("char: " + kValue);
            switch(kValue)
            {
                case 39:  //  ->
                    if (alt){
                        ForwardPic();
                    }
                    else if (ctrl){
                        NextPicDir();
                    }
                    else {
                        NextPic();
                    }
                    break;
                case 34:  //  pd
                case 32:  //  space
                    NextPic();
                    break;
                case 37:  // <-
                    if (alt){
                        BackPic();
                    }
                    else if (ctrl){
                        PriorPicDir();
                    }
                    else {
                        PriorPic();
                    }
                    break;
                case 33:  // pu
                    PriorPic();
                    break;
                case 36:    // pos 1
                    FirstPic();
                    break;
                case 35:    // end
                    LastPic();
                    break;
                case 8:    // back
                    BackPic();
                    break;

                case 66:    // 'b'   boss
                case 68:    // 'd'   dark
                    if (!alt){   // key press here
                        _setDark = true;
                    }
                    DarkPic();
                    break;
                case 113:    // F2
                    RenamePic();
                    break;
                case 116:    // F5
                    RefreshDir();
                    break;
                case 122:    // F11
                    CopyDirString();
                    break;                  
                case 46:     // del
                    DelPic();
                    break;
                case 79:     // 'o'
                    if (ctrl){
                        OpenPic();
                    }
                    break;
                case 70:    // ctrl 'f'
                    if (ctrl){
                        SearchPic();
                    }
                    break;
                case 50:    // '2'
                    if (ctrl){
                        Start2ndScreen();
                    }
                    break;

                case 107:    // +
                case 187:    // +
                    RenamePicPlus();
                    break;
                case 109:    // -
                case 189:    // -
                    RemovePicPlus();
                    break;
                case 84:    // 't'
                    if (alt){
                        TempmarkDelete();
                    }
                    else if (ctrl){
                        TempmarkGo();
                    }
                    else {
                        TempmarkPic();
                    }
                    break;

                case 13:    // enter    start / end full screen
                case 27:    // esc
                    ShowFullScreen();
                    break;
                case 76:    // 'l
                    RotateLeft();
                    break;
                case 82:    //  'r'
                    if (alt){
                        RefreshPic();
                    }
                    else if (ctrl){ 
                        _setDark = false;
                        RefreshPic();   
                    }
                    else RotateRight();
                    break;
                case 83:    // ctrl 's'
                    if (ctrl){
                        SaveOri();
                    }
                    break;
                case 87:    // ctrl w     end program
                    if (ctrl){
                        SetCommand('w', "");
                    }
                    break;
                case 65:    // 'a'  for test
                    Test();
                    break;
                case 69:    // 'e'  for exif
                case 73:    // 'i'  for info
                    if (alt){
                        SetCommand('e', _currentPath);    //ShowExifDash();
                    }
                    else if (ctrl){
                        ShowExif0();
                    }
                    else {
                        StartExif();
                    }
                    break;
            }
            return true;
            //  ctrl 17
        }

        //------------------------------   scrollbar events  ----------------------------------------------------------

        void Scollbar1ValueChanged(object sender, EventArgs e)
        {
            if (_loadNextPic){
                _loadNextPic = false;           // eat up clicks
                string pPath = "";
                int scrollPos = (int) Scrollbar1.Value;
                _currentScrollPos = scrollPos;
                //Debug.WriteLine("scroll change val: " + scrollPos.ToString());
                if (_il.DirPathPos(ref pPath, scrollPos)){
                    if (_barClick){
                        int picPos = 0;
                        int picAll = 0;
                        _il.DirPosPath(ref picPos, ref picAll, pPath);
                        SetStatusText(-2, String.Format(_picSelection + " {0}/{1}", picPos, picAll));
                        SetWindowText(pPath);
                        _priorPath =_currentPath;
                        _currentPath = pPath;
                        PicLoad(pPath);
                    }
                    else _barClick = true;
                }
                _loadNextPic = true;
            }
                //else Debug.WriteLine("eat up click1: ");
        }



        void Scollbar1ToolTipNeeded(object sender, TooltipNeededEventArgs e)
        {
            if (e.Bookmarks.Count > 0) {
                //get topmost bookmark
                ScrollBarBookmark bookmark = e.Bookmarks[e.Bookmarks.Count - 1];
                if (bookmark is BasicShapeScrollBarBookmark) {
                    if (bookmark is ValueRangeScrollBarBookmark) {
                        BasicShapeScrollBarBookmark shapeBookmark = (BasicShapeScrollBarBookmark)bookmark;
                        e.ToolTip = string.Format("Range start, picture {0:###,##0} - ", shapeBookmark.Value) + shapeBookmark.Name;
                    }
                    else{
                        BasicShapeScrollBarBookmark shapeBookmark = (BasicShapeScrollBarBookmark)bookmark;
                        e.ToolTip = string.Format("Marked picture {0:###,##0} ", shapeBookmark.Value);
                    }
                }

            }
            else {
                e.ToolTip = string.Format("Image {0:###,##0}", e.Value);
            }
        }


        //------------------------------   pic functions  ----------------------------------------------------------

        public void NextGif()
        {
            if (_isGif){    
                int fCount;
                int fIndex;
                picBox.Enabled = false;
                picBox.Image = _gifImage.GetNextFrame(out fCount, out fIndex);
                SetStatusText(-3, String.Format(" Gif: ({0}/{1})", fIndex, fCount));
            }

        }

        public void AnimateGif()
        {
            if (_isGif){
              picBox.Enabled = true;
              SetStatusText(-3, " Gif: (Loop)");
            }

        }

        public void PriorGif()
        {
            if (_isGif){
                int fCount;
                int fIndex;
                picBox.Enabled = false;
                picBox.Image = _gifImage.GetPriorFrame(out fCount, out fIndex);
                SetStatusText(-3, String.Format(" Gif: ({0}/{1})", fIndex, fCount));
          }
        }

        void OnFrameChanged(object sender, EventArgs e)
        {
            // frame change
        }

        public bool PicLoadPos(string pPath, bool log)
        {
            int picPos = 0;
            int picAll = 0;
            if (log) {
                _il.DirPosPath(ref picPos, ref picAll, pPath);
                SetStatusText(-2, String.Format(_picSelection + " {0}/{1}", picPos, picAll));
                _il.LogPic(pPath);
            }
            else {
                _il.LogPos(ref picPos, ref picAll);
                SetStatusText(-2, String.Format(T._("History: {0}/{1}"), picPos, picAll));
            }

            SetWindowText(pPath);
            _priorPath =_currentPath;
            _currentPath = pPath;

            _barClick = false;              // scrollbar change only
            if (picAll > 0){
                Scrollbar1.Value = picPos;
            }
            PicLoad(pPath);
            return true;
        }

        public bool PicLoad(string pPath)
        {
            //Debug.WriteLine("pic load: " + pPath);
            try
            {
                if (!File.Exists(pPath)){
                    picBox.SizeMode = PictureBoxSizeMode.CenterImage;
                    picBox.Image = picBox.ErrorImage;
                    SetStatusText(-3, "");
                    return false;
                }

                //Stopwatch sw1 = new Stopwatch();
                //sw1.Start();
                bool showOk = ShowExif();
                //sw1.Stop();
                if (!showOk){   // exif form not shown
                    ExifRead.ExifOrient(ref _exifType, ref _orientationStr, _currentPath);
                }

                //Debug.WriteLine("Ticks: " + sw1.Elapsed.Ticks.ToString());
                //var t = new DateTime(sw1.Elapsed.Ticks);
                //Debug.WriteLine("Exif time: {0:D2}s:{1:D5}ms", t.Second, t.Millisecond);

                //Image myImg;
                _oriInitial = -100;
                using (FileStream stream = new FileStream(pPath, FileMode.Open, FileAccess.Read))
                {
                    _myImg = Image.FromStream(stream);  // abort for gif
                    if (_orientationStr.Equals("right side, top (rotate 90 cw)")){
                        _myImg.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        _oriInitial = 1;
                    }
                    else if (_orientationStr.Equals("bottom, right side (rotate 180)")){
                        _myImg.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        _oriInitial = 2;
                    }
                    else if (_orientationStr.Equals("left side, bottom (rotate 270 cw)")){
                        _myImg.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        _oriInitial = 3;
                    }
                    else if (_orientationStr.Equals("top, left side (horizontal / normal)")){
                        _oriInitial = 0;
                    }

                    stream.Close();
                }
                _oriCurrent = _oriInitial;
                GC.Collect();
                Application.DoEvents();      // like delphi processMessages

                //using (Image bmpTemp = new Bitmap(pPath))      // abort for invalid jpg
                //{
                //  _myImg = new Bitmap(bmpTemp);
                //  if(bmpTemp != null)
                //    ((IDisposable)bmpTemp).Dispose();
                //}
                //GC.Collect();

                string ext = Path.GetExtension(pPath).ToLower();
                picBox.Enabled = true;
                if (ext == ".gif"){
                    SetCommand('g', "");     // show gif menu
                    
                    FileStream  fs = new System.IO.FileStream(pPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
		            MemoryStream ms = new System.IO.MemoryStream();
		            fs.CopyTo(ms);
		            fs.Close();
		            ms.Position = 0;                             
		            picBox.Image = Image.FromStream(ms);          // gif need memory stream
			
 
                    _gifImage = new GifImage(pPath);
                    _isGif = true;
                }
                else {
                    SetCommand('h', "");       // hide gif menu
                    picBox.Image = _myImg;
                    _isGif = false;
                }

                int scalePerc = PicSetSize();
                //Debug.WriteLine("Size: {0}x{1}", picBox.Image.Width, picBox.Image.Height);
                SetStatusText(-3, String.Format(" Size: {0} x {1} ({2} %)", picBox.Image.Width, picBox.Image.Height, scalePerc));

                if (_wType != WinType.second){
                    Settings.Default.LastImage = pPath;
                }

                Show2ndPic(_priorPath);
                //Debug.WriteLine("pic end " + pPath);
                _barClick = true;
                _setDark = false;
                return true;
            }
            catch (Exception e)
            {
              SetStatusText(-3, "");
                picBox.Image = null;
                MessageBox.Show(T._("File is invalid") + "\n "  + pPath + "\n " + e.Message, T._("Invalid file"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        public int PicSetSize( )
        // called by: picLoad, L, R, ScollbarVis
        {
            if (_wType == WinType.normal){
                _scWidth = Screen.FromControl(this).Bounds.Width;
                _scHeight = Screen.FromControl(this).Bounds.Height;
            }
            float scWinFactor = Util.getScalingFactor( );
            float sfW = _scWidth * scWinFactor;
            float sfH = _scHeight * scWinFactor;
            int scWidthScale = (int)sfW;
            int scHeightScale = (int)sfH;
            //Debug.WriteLine("Screen size w: {0} h: {1}", _scWidth, _scHeight);
            //Debug.WriteLine("Screen scale size w: {0} h: {1} scale: {2} ", scWidthScale, scHeightScale, scWinFactor);
                
            int imHeight = _myImg.Height;
            int imWidth = _myImg.Width;
            float scaleFactor = 0;
            int scalePerc = 100;
            if (_wType == WinType.full){
                if ((imWidth + _borderWidth > _scWidth) || (imHeight + _borderHeight > _scHeight)){
                    picBox.SizeMode = PictureBoxSizeMode.Zoom;
                    scaleFactor = (float) (imWidth * imHeight) / ((scWidthScale - _borderWidth) * (scHeightScale - _borderHeight));
                    scalePerc = (int) Math.Round(100 / scaleFactor);
                }
                else {
                    picBox.SizeMode = PictureBoxSizeMode.CenterImage;
                     scalePerc = 100;
                }
                return scalePerc;
            }

            //Debug.WriteLine("Image W / H: {0}/{1}", imWidth, imHeight);
            int scWidth = 0;
            if (Scrollbar1.Visible){
                scWidth = 0; // Scrollbar1.Width;
            }
            CalcBorderSize();
            if ((imWidth + _borderWidth > _scWidth) || (imHeight + _borderHeight > _scHeight)){
                picBox.SizeMode = PictureBoxSizeMode.Zoom;
                picBox.BackColor = SystemColors.Control;
                float scFactor = (float) (_scWidth - _borderWidth) / (_scHeight - _borderHeight);
                float imFactor = (float) imWidth / imHeight;
                if (imFactor > scFactor){            // wide img
                    int ih = (imHeight * (_scWidth - _borderWidth) / imWidth);
                    //splitContainer1.SplitterDistance = _scWidth;
                    SetWindowSize(_scWidth, ih + _borderHeight, _exifType);
                    scaleFactor = (float) imWidth / (scWidthScale - _borderWidth);
                    scalePerc = (int) Math.Round(100 / scaleFactor);
                }
                else {          // high img
                    int iw = (imWidth * (_scHeight - _borderHeight) / imHeight);// + _borderWidth;
                    //splitContainer1.SplitterDistance = iw;
                    SetWindowSize(iw + _borderWidth + scWidth, _scHeight, _exifType);
                    scaleFactor = (float) imHeight / (scHeightScale - _borderHeight);   
                    scalePerc = (int) Math.Round(100 / scaleFactor);
                }
            }
            else {  // small img
                picBox.SizeMode = PictureBoxSizeMode.CenterImage;
                //splitContainer1.Panel2.Width = 12;
                //splitContainer1.SplitterDistance = imWidth;
                SetWindowSize(imWidth + _borderWidth + scWidth, imHeight + _borderHeight, _exifType);
                scalePerc = 100;
            }
            return scalePerc;
        }


        public void PicScan(string  pPath, bool allDirs, int postAction)
        // called by: Next/PriorPicDir, open, refresh, drop; main: show, recent, MessageReceived
        {
            object oPath = pPath;
            object oDirs = allDirs;
            object oAction = postAction;
            object[] parameters = new object [] { oPath, oDirs, oAction };

            if (backgroundWorker1.IsBusy == true){
              //Debug.WriteLine("bw1: busy - cancel ");
              backgroundWorker1.CancelAsync();
            }

            while (backgroundWorker1.IsBusy)
              Application.DoEvents();

            //Debug.WriteLine("bw1: start: ");
            backgroundWorker1.RunWorkerAsync(parameters);
        }

        public void DarkPic0()
        // called by: MainDeactivate
        {
            if (_processHide)
            {
                Debug.WriteLine("dark0: ");
                picBox.Image = null;
            }
        }
        
        public void DarkPic()
        // called by: 'b,d'; main.SearchKeyDown
        {
            //Debug.WriteLine("dark: ");
            picBox.Image = null;
            SetWindowText("");
            SetStatusText(0, "");
            Dark2nd();
            SetSelf("d");
        }

        void DarkPic2()
        // called by: HandleSelf - from full screen
        {
            picBox.Image = null;
            SetWindowText("");
            SetStatusText(0, "");
            Dark2nd();
        }

        public void RefreshPic()
        // called by: FrmMainActivated, 'r'
        {
            if (_processHide)
            {
                if (_setDark == false)
                {
                    string ext = Path.GetExtension(_currentPath).ToLower();
                    if (ext == ".gif"){         
                        PicLoadPos(_currentPath, true);
                    }
                    else {
                        if (_myImg != null)  
                            picBox.Image = _myImg;
                    }
                }
            }
        }
        
        public void NextPic()
        {
            string pPath = "";
            if (_il.DirPicNext(ref pPath)){
                PicLoadPos(pPath, true);
            }
            else {
                SetStatusText(0, T._("No image loaded"));
            }
        }

        public void NextSearchPic(string pSearch)
        {
            string pPath = "";
            if (_il.DirPicSearchNext(pSearch, ref pPath)){
                PicLoadPos(pPath, true);
            }
            else {
                SetStatusText(0, T._("No image found"));
            }
        }

        public void NextPicDir()
        {
            PicScan(_currentPath, false, 2);
        }

        public void PriorPic()
        {
            string pPath = "";
            if (_il.DirPicPrior(ref pPath)){
                PicLoadPos(pPath, true);
            }
            else {
                SetStatusText(0, T._("No image loaded"));
            }
        }

        public void PriorSearchPic(string pSearch)
        {
            string pPath = "";
            if (_il.DirPicSearchPrior(pSearch, ref pPath)){
                PicLoadPos(pPath, true);
            }
            else {
                SetStatusText(0, T._("No image found"));
            }
        }

        public void PriorPicDir()
        {
            PicScan(_currentPath, false, 3);
        }

        public void FirstPic()
        {
            string pPath = "";
            if (_il.DirPicFirst(ref pPath)){
                PicLoadPos(pPath, true);
            }
            else {
                SetStatusText(0, T._("No image loaded"));
            }
        }

        public void LastPic()
        {
            string pPath = "";
            if (_il.DirPicLast(ref pPath)){
                PicLoadPos(pPath, true);
            }
            else {
                SetStatusText(0, T._("No image loaded"));
            }
        }

        public void BackPic()
        {
            string pPath = "";
            if (_il.LogBack(ref pPath)){
                PicLoadPos(pPath, false);
            }
        }

        public void ForwardPic()
        {
            string pPath = "";
            if (_il.LogForward(ref pPath)){
                PicLoadPos(pPath, false);
            }
        }

        public void RefreshDir()
        {
            _il.DirClear();
            PicScan(_currentPath, false, 0);
            PicLoadPos(_currentPath, true);
        }

        public void CopyDirString()
        {
            string fullPath = Path.GetDirectoryName(_currentPath);
            //string copyText = Path.GetFileName(fullPath);  
            Clipboard.SetText(fullPath);
        }
        
        //------------------------------   sort functions  ----------------------------------------------------------

        public void NameSort()
        {
            _il.SortName();
            int picPos = 0;
            int picAll = 0;
            _il.DirPosPath(ref picPos, ref picAll, _currentPath);
            SetStatusText(-2, String.Format(_picSelection + " {0}/{1}", picPos, picAll));
            SetStatusText(-3, "");
      
            Scrollbar1.Bookmarks.Clear();
            _barClick = false;              // scrollbar change only
            if (picAll > 0){
                Scrollbar1.Value = picPos;
            }
            CalcBar();
            DrawBar();
        }

        public void FDateSort()
        {
            _il.SortFDate();
            int picPos = 0;
            int picAll = 0;
            _il.DirPosPath(ref picPos, ref picAll, _currentPath);
            SetStatusText(-2, String.Format(_picSelection + " {0}/{1}", picPos, picAll));
            SetStatusText(-3, "");
      
            Scrollbar1.Bookmarks.Clear();
            _barClick = false;              // scrollbar change only
            if (picAll > 0){
                Scrollbar1.Value = picPos;
            }
            CalcBar();
            DrawBar();
        }

        public void ExifSort()
        {
            _il.SortExifDate();
            int picPos = 0;
            int picAll = 0;
            _il.DirPosPath(ref picPos, ref picAll, _currentPath);
            SetStatusText(-2, String.Format(_picSelection + " {0}/{1}", picPos, picAll));
            SetStatusText(-3, "");
      
            Scrollbar1.Bookmarks.Clear();
            _barClick = false;              // scrollbar change only
            if (picAll > 0){
                Scrollbar1.Value = picPos;
            }
            CalcBar();
            DrawBar();
        }

        //------------------------------   file functions  ----------------------------------------------------------

        public void RenamePic()
        {
            string newPath = "";
            frmRename frm = new frmRename(_currentPath);
            frm.KeyChanged += new HandleKeyChange(HandleKey);   // subscribed to this event
            var result = frm.ShowDialog();
            if (frm._ReturnPath != "") {
                newPath = frm._ReturnPath;
                _il.RenameListLog(_currentPath, newPath);
                _currentPath = newPath;
                SetWindowText(_currentPath);
                PicLoadPos(_currentPath, true);
            }
        }

        public void RenamePicPlus()
        {
            string fname = Path.GetFileNameWithoutExtension(_currentPath);
            string fext = Path.GetExtension(_currentPath);
            string newPath = Path.GetDirectoryName(_currentPath) + @"\" + fname + "+" + fext;
            if (FileRename2(_currentPath, newPath)) {
                _il.RenameListLog(_currentPath, newPath);
                _currentPath = newPath;
                SetWindowText(_currentPath);
                BasicShapeScrollBarBookmark bookmarkBS = new BasicShapeScrollBarBookmark(" ", _currentScrollPos, ScrollBarBookmarkAlignment.LeftOrTop, 1, 1, ScrollbarBookmarkShape.Rectangle, Color.Green, true, true, null);
                Scrollbar1.Bookmarks.Add(bookmarkBS);
            }
            else {
                //Debug.WriteLine("no rename");
            }
        } 

        public void RemovePicPlus()
        {
            string fname = Path.GetFileNameWithoutExtension(_currentPath);
            string fext = Path.GetExtension(_currentPath);
            if (fname.EndsWith("+")){
                fname = fname.Substring(0, fname.Length - 1);
                string newPath = Path.GetDirectoryName(_currentPath) + @"\" + fname + fext;
                if (FileRename2(_currentPath, newPath)) {
                    _il.RenameListLog(_currentPath, newPath);
                    _currentPath = newPath;
                    SetWindowText(_currentPath);
                    RemoveBookmark(_currentScrollPos);
                }
                else {
                    //Debug.WriteLine("no rename");
                }
            }
        }

        public void RemoveBookmark(int bPos)
        {
            int i = 0;
            foreach (ScrollBarBookmark bm in Scrollbar1.Bookmarks)
            {
                if (bm is BasicShapeScrollBarBookmark){
                    if (! (bm is ValueRangeScrollBarBookmark)) {
                        if (bPos == (int)bm.Value){
                          Scrollbar1.Bookmarks.RemoveAt(i);
                            break;
                        }
                    }
                }
                i++;
            }
        }

        public void DelPic()
        {
            _processHide = false;  // DelFile is a Windows process with an own window, so this form is deactivated, and activated again after delete. Avoid windows flicker  
            DelFile.MoveToRecycleBin(_currentPath);
            string nextPath = "";
            int imgCount = 0;
            if (_il.DeleteListLog(_currentPath, ref nextPath, ref imgCount)){
                Scrollbar1.Maximum = imgCount;
                AdjustBookmark(_currentScrollPos);
                PicLoadPos(nextPath, true);
                _currentPath = nextPath;
            }
            else {  // last img in selection deleted
                picBox.Image = null;
                SetStatusText(0, T._("No image loaded"));
            }
            _processHide = true;
        }

        public void OpenPic()
        {
            var dialog = new OpenFileDialog();
            string lastPath = Settings.Default.LastImage;
            if (File.Exists(lastPath)){
                if (Directory.Exists(Path.GetDirectoryName(lastPath))) {
                    dialog.InitialDirectory = Path.GetDirectoryName(lastPath);
                }
            }
            dialog.Filter = "All images |*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.ico;*.tif;*.wmf;*.emf|JPEG files |*.jpg;*.jpeg;*.jfif|PNG files |*.png|GIF files |*.gif|Bitmap files |*.bmp|Icon files |*.ico|TIF files |*.tif|WMF files |*.wmf|EMF files |*.emf";
            dialog.Title = T._("Select image");

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                string picPath = dialog.FileName;
                PicScan(picPath, false, 0);
                PicLoadPos(picPath, true);
                SetCommand('r', picPath);  // recent
            }
        }

        public bool SaveOri()
        {
            try
            {
                string mess1 = "";
                if (_oriCurrent == -100){
                    mess1 = T._("This file has no Exif orientation.");
                    MessageBox.Show(mess1, T._("Save not possible"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                if (_oriCurrent == _oriInitial){
                    mess1 = T._("Orientation not changed");
                    MessageBox.Show(mess1, T._("Save not possible"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                byte oriByte;
                switch(_oriCurrent)
                {
                    case 1:  oriByte = 6;      //  90 l
                    break;
                    case 2:  oriByte = 3;      // 180
                    break;
                    case 3:  oriByte = 8;      //  270
                    break;
                    default: oriByte = 1;      // 0
                    break;
                }
                DateTime dtChanged = File.GetLastWriteTime(_currentPath);
                ushort ori = 0;
                using (var reader = new ExifReader(_currentPath))
                {
                    if (reader.GetTagValue(ExifTags.Orientation, out ori)) {
                        reader.SaveOrient(oriByte);
                    }
                }
                //  set original date after update
                DateTime dtOriginal = DateTime.MinValue;
                ExifRead.ExifODate(out dtOriginal, _currentPath);
                if (dtOriginal == DateTime.MinValue)
                    dtOriginal = dtChanged;
                System.IO.File.SetLastWriteTime(_currentPath, dtOriginal);

                //Debug.WriteLine("Orient ini: {0}, current {1}, byte {2} ", _oriInitial, _oriCurrent, ori);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(T._("Error for update") + "\n"  + e.Message, T._("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        //------------------------------   tempmark functions  ----------------------------------------------------------
        public void TempmarkDelete()
        {
            if (!_il.MarkDelete(_currentPath)){
                MessageBox.Show(T._("This image is not marked"), T._("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void TempmarkGo()
        {
            string markPath = "";
            if (_il.MarkGo(ref markPath)){
                PicLoadPos(markPath, true);
                _currentPath = markPath;
            }
            else {
                MessageBox.Show(T._("No image is marked yet"), T._("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void TempmarkPic()
        {
            _il.MarkPic(_currentPath);
        }

        bool FileRename2(string nameFrom, string nameTo)
        {
            try {  
                File.Move(nameFrom, nameTo);
                return true;
            }
            catch (Exception e) {
                Debug.WriteLine("Rename error:  " +  e.Message);
                return true;  // copyied file used, but old file not deleted   false;
            }
        }


        //------------------------------   pop up  ----------------------------------------------------------

        void PopOpenClick(object sender, EventArgs e)
        {
            OpenPic();
        }

        void PopRenameClick(object sender, EventArgs e)
        {
            RenamePic();
        }
        
        void PopCopyPathClick(object sender, EventArgs e)
        {
            CopyDirString();
        }
        
        void PopDeleteClick(object sender, EventArgs e)
        {
            DelPic();
        }

        void PopSearchClick(object sender, EventArgs e)
        {
            SearchPic();
        }

        void PopStartEditorClick(object sender, EventArgs e)
        {
            StartEditor();
        }

        void PopNextClick(object sender, EventArgs e)
        {
            NextPic();
        }

        void PopPriorClick(object sender, EventArgs e)
        {
            PriorPic();
        }

        void PopRefreshClick(object sender, EventArgs e)
        {
            RefreshDir();
        }

        void PopFullscreenClick(object sender, EventArgs e)
        {
            ShowFullScreen();
        }

        void PopCloseClick(object sender, EventArgs e)
        {
            if (_wType == WinType.second){
                this.Close();
            }
            if (_wType == WinType.normal){
                Application.Exit();
                Environment.Exit(0);
            }
        }

        //------------------------------   other functions   ----------------------------------------------------------

        public void SearchPic()
        {
            SearchForm frm = new SearchForm(_currentPath, _lastSearchStr, _il);
            frm.KeyChanged += new HandleKeyChange(HandleKey);
            frm.ShowDialog();

            _lastSearchStr = frm._lastSearchStr;
            if (frm._SearchReturn) {
                string selImg = frm._selImg;
                int picPos = 0;
                int picAll = 0;
                if (selImg != ""){
                    _il.DirPosPath(ref picPos, ref picAll, selImg);
                }
                else {
                    _il.DirPicFirst(ref selImg);
                }
                _currentPath = selImg;
                _picSelection = T._("Search:");
                PicLoadPos(selImg, true);
                Bw2Run( );
            }
        }

        public void ScollbarVis(bool sVisible)
        {
            if (sVisible){
                picBox.Left = Scrollbar1.Width;
                picBox.Width = picBox.Width - Scrollbar1.Width;
                Scrollbar1.Visible = true;
            }
            else {
                picBox.Left = 0;
                picBox.Width = picBox.Width + Scrollbar1.Width;
                Scrollbar1.Visible = false;
            }
            //PicSetSize();  // abort for invalid first image
        }

        public void ShowExifImages(List<ImgFile> exImgList, string selImg)
        // called by: dash via main
        {
            _il.DirClear();
            exImgList.ForEach((item)=>   // deep copy
            {
            _il._imList.Add(new ImgFile(item.fName, item.fDate, item.fDateOriginal));
            });

            int picPos = 0;
            int picAll = 0;
            _il.DirPosPath(ref picPos, ref picAll, selImg);

            _currentPath = selImg;
            _picSelection = T._("Search:");
            PicLoadPos(_currentPath, true);
            Bw2Run( );
        }


        public void ShowFullScreen()
        // called by: menu, pop, key
        {
            if (CanStartFull()){
                m_ImageF  = new frmImage(0, 0, WinType.full, _il);
                m_ImageF.SelfChanged += new HandleSelfChange(HandleSelf);         // send event for full screen close
                m_ImageF.PicLoadPos(_currentPath, false);
                m_ImageF.Show();
                m_ImageF.BringToFront();
                SetCommand('f', "");    // hide main
            }
            else {
              if (FullRunning()){
                m_ImageF.PicLoadPos(_currentPath, false);
                m_ImageF.Show();
                m_ImageF.BringToFront();                
              }
            }
            if (_wType == WinType.full) {
                //Debug.WriteLine("close full " + _currentPath);
                SetSelf(_currentPath);     // handled by HandleSelf
                SetSelf("x");
                this.Close();
            }
        }

        public void RotateLeft()
        {
            _myImg.RotateFlip(RotateFlipType.Rotate270FlipNone);
            picBox.Image = _myImg;
            if (_oriCurrent != -100){
                _oriCurrent--;
                if (_oriCurrent < 0) _oriCurrent = 3;
            }
            PicSetSize();
        }

        public void RotateRight()
        {
            _myImg.RotateFlip(RotateFlipType.Rotate90FlipNone);
            picBox.Image = _myImg;
            if (_oriCurrent != -100){
                _oriCurrent++;
                if (_oriCurrent > 3) _oriCurrent = 0;
            }
            PicSetSize();
        }

        public void StartEditor()
        {
            string editorPath = Settings.Default.Editor;
            if ((editorPath == "")||(!File.Exists(editorPath))) {    // extension default editor
                Util.StartEditor(_currentPath, "");
            }
            else {
                Util.StartEditor(editorPath, _currentPath);
            }
        }

        public void Test()
        {
            m_Exif.Close();
        }

        public void TranslateImageForm( )
        {
          _picSelection = T._("Directory:");
          popOpen.Text = T._("Open...");
          popRename.Text = T._("Rename...");
          popDelete.Text = T._("Delete");
          popSearch.Text = T._("Search...");
          popStartEditor.Text = T._("Start editor...");
          popNext.Text = T._("Next image");
          popPrior.Text = T._("Prior image");
          popRefresh.Text = T._("Refresh");
          popFullscreen.Text = T._("Full screen");
          popClose.Text = T._("Exit");
        }

        public void CalcBorderSize()
        {
            if (_wType == WinType.normal){
                int pbWidth = picBox.Width;
                int pbHeight = picBox.Height;
                _mainWidth = _fM.Width;
                _mainHeight = _fM.Height;
                _borderWidth = _mainWidth - pbWidth;
                _borderHeight = _mainHeight - pbHeight;
                //Debug.WriteLine("picbox border size W / H: {0}/{1}; pic box {2} / {3}; form {4} / {5} ", _borderWidth, _borderHeight, pbWidth, pbHeight, this.Width, this.Height);
            }
        }

        //------------------------------   bar functions    ----------------------------------------------------------

        public void ScanImagesBar( )
        {
            List<ImgFile> imList;
            _il.ImgListOut(out imList);

            DateTime dtOriginal = DateTime.MinValue;
            foreach (ImgFile imf in imList)
            {
                string picPath = imf.fName;
                DateTime dtCreation = File.GetCreationTime(picPath);
                DateTime dtChanged = File.GetLastWriteTime(picPath);
                DateTime dtFile;
                if (dtCreation < dtChanged){
                    dtFile = dtCreation;
                }
                else {
                    dtFile = dtChanged;
                }
                imf.fDate = dtFile;

                ExifRead.ExifODate(out dtOriginal, picPath);
                imf.fDateOriginal = dtOriginal;
            }
        }

        public void CalcBar( )
        // calculate bar ranges
        {
            List<ImgFile> imList;
            _il.ImgListOut(out imList);

            DateTime dtOriginal = DateTime.MinValue;
            DateTime nullDate = DateTime.MinValue;
            DateTime minDate = DateTime.MaxValue;
            DateTime maxDate = DateTime.MinValue;

            int fCount = 0;
            int fCountExif = -1;
            int dateCount = 0;
            DateTime priorDate = DateTime.MinValue;
            var spanDict = new Dictionary<int, int>();
            _posList.Clear();
            _priorList.Clear();
            _rangeDict.Clear();
            // dict for time gaps
            foreach (ImgFile imf in imList)
            {
                string picPath = imf.fName;
                dtOriginal = imf.fDateOriginal;
                fCount++;

                string fName = Path.GetFileName(picPath);
                int pPos = fName.IndexOf(_posListSelector);
                if (pPos > -1){
                    _posList.Add(fCount);
                }

                if (dtOriginal != nullDate){
                    //Debug.WriteLine("path: " + picPath + " " + dtOriginal.ToString());
                    fCountExif = fCount;
                    dateCount++;
                    imf.fDateOriginal = dtOriginal;
                    if (priorDate > dtOriginal){
                        //Debug.WriteLine("prior date: " + fCount);
                        _priorList.Add(fCount);
                    }
                    if (minDate > dtOriginal) minDate = dtOriginal;
                    if (maxDate < dtOriginal) maxDate = dtOriginal;

                    if (dateCount > 1){
                        TimeSpan span = dtOriginal.Subtract(priorDate);
                        int spanSec = Math.Abs((int) span.TotalSeconds);
                        spanDict.Add(fCount, spanSec);
                    }
                    priorDate = dtOriginal;
                }
                else {   // no exif date
                    if (fCountExif == fCount - 1){        // gap to exif date
                        spanDict.Add(fCount, Int32.MaxValue);
                    }
                }
            }

            // span values
            if (dateCount > 0){
                TimeSpan imgSpan = maxDate.Subtract(minDate);
                //Debug.WriteLine("min: " + minDate.ToString() + " max: " + maxDate.ToString());
                //Debug.WriteLine("range: " + imgSpan.ToString());

                int mean = (int) imgSpan.TotalSeconds / dateCount;
                long sumVar = 0;
                foreach (KeyValuePair<int, int> sd in spanDict)
                {
                    if (sd.Value != Int32.MaxValue){
                        long var = (long) Math.Pow((mean - sd.Value), 2);
                        sumVar += var;
                        //Debug.WriteLine("F Num: " + dfn.Key + " " + dfn.Value);
                    }
                }
                int stdDev = (int) Math.Sqrt(sumVar / dateCount);
                //Debug.WriteLine("mean / std: {0}/{1}", mean, stdDev);

                int breakVal = mean + stdDev * 2;
                //int wi = 2;

                if (imgSpan.TotalDays > 730){
                    _rangeType = 1;      // years
                }
                else if (imgSpan.TotalDays > 60){
                    _rangeType = 2;      // months
                }
                else if (imgSpan.TotalDays > 1){
                    _rangeType = 3;      // days
                }
                else {
                    _rangeType = 4;      // hours
                }

                int i = 0;
                // largest breaks
                string p0Path = "";
                if (_il.DirPathPos(ref p0Path, 1)){
                    ExifRead.ExifODate(out _r0Date, p0Path);
                }
                foreach (KeyValuePair<int, int> sd in spanDict.OrderByDescending(key=> key.Value))
                {
                    i++;
                    //Debug.WriteLine("pic no / dist: {0}/{1}", sd.Key, sd.Value);
                    if (sd.Value < breakVal) break;

                    string piPath = "";
                    DateTime dOriginal = nullDate;
                    if (_il.DirPathPos(ref piPath, sd.Key)){
                        ExifRead.ExifODate(out dOriginal, piPath);
                    }
                    _rangeDict.Add(sd.Key, dOriginal);
                }
            }
            // bar end check 
            if (_posList.Count == imList.Count && _posListSelector == "+"){
                _posListSelector = "++";
                // again
                CalcBar();   
            }
            else _posListSelector = "+";
        }

        public void AdjustBookmark(int bPos)
        {
            foreach (ScrollBarBookmark bm in Scrollbar1.Bookmarks)
            {
                int scPos = (int) bm.Value;
                if (bPos <= scPos){
                    bm.Value = --scPos;
                }

                if (bm is ValueRangeScrollBarBookmark) {
                    int raPos = (int) ((ValueRangeScrollBarBookmark)bm).EndValue;
                    if (bPos <= raPos){
                        ((ValueRangeScrollBarBookmark)bm).EndValue = --raPos;
                    }
                }
            }
        }

        //------------------------------   BackgroundWorker    ----------------------------------------------------------

        void BackgroundWorker1DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        // called by backgroundWorker1.RunWorkerAsync  (picScan)
        {
            object[] parameters = e.Argument as object[];
            //Debug.WriteLine("para " + parameters[0]);
            string picPath = (string) parameters[0];
            bool allDirs = (bool) parameters[1];
            int postAction = (int) parameters[2];

            int pCount;
            _dirChanged = _il.DirScan(out pCount, picPath, allDirs);
            if (pCount == 0 && postAction == 1) {        // rescan for lower
                _il.DirScan(out pCount, picPath, true);
            }

            object oPath = picPath;
            object oAction = postAction;
            object[] results = new object [] { oPath, oAction };
            e.Result = results;
        }

        void BackgroundWorker1RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            object[] results = e.Result as object[];
            string pPath = (string) results[0];
            int postAction = (int) results[1];
            _currentPath = pPath;

            int picPos = 0;
            int picAll = 0;
            switch(postAction)
            {
                case 1:        //  scan lower
                    _il.DirPicFirst(ref _currentPath);
                    _il.DirPosPath(ref picPos, ref picAll, _currentPath);
                    SetStatusText(-2, String.Format(_picSelection + " {0}/{1}", picPos, picAll));
                    PicLoadPos(_currentPath, true);
                    SetCommand('r', _currentPath);         // recent
                    break;

                case 2:       // next pic dir
                    _il.DirPosPath(ref picPos, ref picAll, _currentPath);
                    _picSelection = T._("Directory:");
                    NextPic();
                    break;

                case 3:       // prior pic dir
                    _il.DirPosPath(ref picPos, ref picAll, _currentPath);
                    _picSelection = T._("Directory:");
                    PriorPic();
                    break;

                default:    // single pic
                    _il.DirPosPath(ref picPos, ref picAll, _currentPath);
                    SetStatusText(-2, String.Format(_picSelection + " {0}/{1}", picPos, picAll));
                    break;
            }
            SetStatusText(-3, "");
            if (picAll > 0) {
                Scrollbar1.Value = picPos;
            }

            // scan for scroll bar, after 'normal' scan
            if (_dirChanged){
                Bw2Run( );
            }
        }

        void Bw2Run( )
        {
          Scrollbar1.Bookmarks.Clear();
            if (bw2.IsBusy == true){
              //Debug.WriteLine("bw2: busy - cancel ");
              backgroundWorker1.CancelAsync();
            }

            while (bw2.IsBusy)
              Application.DoEvents();

            bw2.RunWorkerAsync( );
        }

        void Bw2DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        // called by:
        {
            ScanImagesBar( );
            CalcBar();
        }

        void Bw2RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //Debug.WriteLine("bw2: complete ");
            DrawBar();
        }

        void DrawBar( )
        {
            Scrollbar1.SuspendLayout();     //   same thread
            int dirCount = _il.DirCount();
            if (dirCount == 0) dirCount = 1;
            Scrollbar1.Maximum = dirCount;
            // scrollbar marks, line
            foreach (int pNo in _posList)
            {
                BasicShapeScrollBarBookmark bookmarkBS = new BasicShapeScrollBarBookmark(" ", pNo, ScrollBarBookmarkAlignment.LeftOrTop, 1, 1, ScrollbarBookmarkShape.Rectangle, Color.Green, true, true, null);
                Scrollbar1.Bookmarks.Add(bookmarkBS);
                //Debug.WriteLine("bookmark: {0}", pNo);
            }
            // image, prior mark 
            Image barImg = barIcon.Icon.ToBitmap();
            foreach (int pNo in _priorList)
            {
                ImageScrollBarBookmark ibookmark = new ImageScrollBarBookmark(" ", pNo, barImg, ScrollBarBookmarkAlignment.RightOrBottom, null);
                Scrollbar1.Bookmarks.Add(ibookmark);
            }

            // range
            int start1 = 1;
            int end1 = 0;
            int depth1 = 6;
            int colIndex = 0;
            string rangeText = GetRangeText(_r0Date);
            foreach (KeyValuePair<int, DateTime> rd in _rangeDict.OrderBy(key=> key.Key))
            {
                end1 = rd.Key;
                ValueRangeScrollBarBookmark bookmarkVR = new ValueRangeScrollBarBookmark(rangeText , start1, end1, ScrollBarBookmarkAlignment.LeftOrTop, depth1, _colors[colIndex], true, false, null);
                Scrollbar1.Bookmarks.Add(bookmarkVR);
                rangeText = GetRangeText(rd.Value);
                //Debug.WriteLine("bookrange: {0}, {1}", start1, end1 );
                start1 = end1;
                colIndex++;
                if (colIndex > _colors.Length - 1) colIndex = 0;
            }
            end1 = dirCount;
            ValueRangeScrollBarBookmark bookmarkVR2 = new ValueRangeScrollBarBookmark(rangeText , start1, end1, ScrollBarBookmarkAlignment.LeftOrTop, depth1, _colors[colIndex], true, false, null);
            Scrollbar1.Bookmarks.Add(bookmarkVR2);    // last range
            //Debug.WriteLine("bookrange-end: {0}, {1}", start1, end1 );
            Scrollbar1.ResumeLayout();
        }

        public String GetRangeText(DateTime rangeStart)
        {
            string rText;
            switch(_rangeType)
            {
                case 1:  // year
                    rText = String.Format("{0:yyyy-MM}", rangeStart);
                    break;

                case 2:  // month
                    rText = String.Format("{0:yyyy-MM}", rangeStart);
                    break;

                case 3:  // days
                    rText = String.Format("{0:MMM dd}", rangeStart);
                    break;

                default:    // hours
                    rText = String.Format("{0:HH:mm}", rangeStart);
                    break;
            }

            return rText;
        }

        //------------------------------   2nd screen    ----------------------------------------------------------

        public void Start2ndScreen()
        {
            //Debug.WriteLine("start-2nd: ");
            string prPath = _priorPath;
            if (prPath == ""){
                prPath = _currentPath;
            }

            if (CanStart2nd()){
                m_Image2  = new frmImage(0, 0, WinType.second, _il);
                m_Image2.PicLoadPos(prPath, false);
                m_Image2.Show();

            }
            else {    // img to foreground
                if (CanShow2nd()){
                    m_Image2.BringToFront();
                }
            }
        }

        public void Show2ndPic(string prPath)
        // called by: PicLoad
        {
            if (CanShow2nd()){
                if (prPath == ""){
                    prPath = _currentPath;
                }
                if (File.Exists(prPath)){
                    m_Image2.PicLoadPos(prPath, false);
                }
            }
        }

        bool CanStart2nd()
        {
            if (_wType == WinType.second){
                return false;
            }
            if (_wType == WinType.full){
                return false;
            }
            if (m_Image2 == null){
                return true;
            }
            return !m_Image2._ndRunning;
        }

        bool CanStartFull()
        {
            if (_wType == WinType.second){
                return false;
            }
            if (_wType == WinType.full){
                return false;
            }
            if (m_ImageF == null){
                return true;
            }
            return !m_ImageF._fullRunning;
        }

        bool FullRunning()
        {
            if (m_ImageF == null){
                return false;
            }
            return m_ImageF._fullRunning;
        }
        
        bool CanShow2nd()
        {
            if (_wType == WinType.second){
                return false;
            }
            if (m_Image2 == null){
                return false;
            }
            return m_Image2._ndRunning;
        }

        public void Dark2nd()
        {
            if (CanShow2nd()){
                m_Image2.picBox.Image = null;
                m_Image2.Text = "";
            }
        }

        public void Close2nd()
        {
            if (CanShow2nd()){
                m_Image2.RClose();
            }
            if (CanShowExif()){
                    m_Exif.Close();
            }
        }

        //------------------------------   Exif screen    ----------------------------------------------------------


        public void ShowExifDash()
        {

//                  string selImg = frmDash._exifImg;
//                  int picPos = 0;
//                  int picAll = 0;
//                  if (selImg != ""){
//                      _il.DirPosPath(ref picPos, ref picAll, selImg);
//                  }
//                  else {
//                      _il.DirPicFirst(ref selImg);
//                  }
//                  _currentPath = selImg;
//                  _picSelection = T._("Exif Search:");
//                  PicLoadPos(_currentPath, true);


        }

        public void ShowExif0()
        {
            if (File.Exists(_currentPath)) {
                ExifForm0 frmExif0 = new ExifForm0();
                frmExif0.CheckFile0(_currentPath);
                frmExif0.ShowDialog();
            }
        }


        public bool StartExif()   //  with e, i
        {
            try
            {
                if (!File.Exists(_currentPath))
                    return false;

                if (CanStartExif()){
                    m_Exif = new ExifForm();
                    m_Exif.KeyChanged += new HandleKeyChange(HandleKey);
                    m_Exif.CheckFile(ref _exifType, ref _orientationStr, _currentPath);
                    m_Exif.Show();
                }
                else {    // img to foreground
                    if (CanShowExif()){
                        m_Exif.Show();
                        m_Exif.BringToFront();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Exif", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        public bool ShowExif()
        // for each pic
        {
            bool showRet = CanShowExif();
            if (showRet){
                m_Exif.CheckFile(ref _exifType, ref _orientationStr, _currentPath);
            }
            return showRet;
        }

        bool CanStartExif()
        {
            if (m_Exif == null){
                return true;
            }
            return false;
        }

        bool CanShowExif()
        {
            if (m_Exif == null){
                return false;
            }
            return true;
        }


        //------------------------------   delegates   ----------------------------------------------------------

        public void SetWindowText(string text2)
        {
            // called by: PicLoad, RenamePic, RenamePicPlus, RemovePicPlus
            // output: main.HandleWindow
            this.Text = Path.GetFileName(text2);

            OnWindowChanged(new SetTitleEventArgs(text2));
            Application.DoEvents();
        }

        protected virtual void OnWindowChanged(SetTitleEventArgs e)
        {
            if(this.WindowChanged != null)     // nothing subscribed to this event
            {
                this.WindowChanged(this, e);
            }
        }


        public void SetWindowSize(int w, int h, int exifType)
        {
            try
            {
                // called by: PicLoad 3*, Enter
                // output: main.SetWindowSize
                //Debug.WriteLine("Window size W / H: {0}/{1}", w, h);
                OnWindowSize(new SetSizeEventArgs(w, h, exifType));
                _currentWidth = w;
                _currentHeight = h;
                Application.DoEvents();
            }
            catch (Exception e)
            {
                picBox.Image = null;
                MessageBox.Show(T._("File is invalid") + "\n "  + e.Message, T._("Invalid file"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        protected virtual void OnWindowSize(SetSizeEventArgs  e)
        {
            if(this.WindowSize != null)
            {
                this.WindowSize(this, e);
            }
        }


        public void SetStatusText(int sVal, string sText)
        {
            // called by: PicLoad, or 5* 'no img loaded'
            // output: main.HandleStatus
            OnStatusChanged(new SetStatusMainEventArgs(sVal, sText));
            Application.DoEvents();
        }

        protected virtual void OnStatusChanged(SetStatusMainEventArgs e)
        {
            if(this.StatusChanged != null)
            {
                this.StatusChanged(this, e);
            }
        }


        public void SetCommand(char comm, string fName)
        {
        // called by: recent: Filename: openPic, FrmImageDragDrop
        // dash: kdown e

        // main command, HandleCommand
            OnCommandChanged(new SetCommandEventArgs(comm, fName));
            Application.DoEvents();
        }

        protected virtual void OnCommandChanged(SetCommandEventArgs e)
        {
            if(this.CommandChanged != null)
            {
                this.CommandChanged(this, e);
            }
        }

        public void SetSelf(string fName)
        {
        // called by: recent: full screen

            OnSelfChanged(new SetSelfEventArgs(fName));
            Application.DoEvents();
        }

        protected virtual void OnSelfChanged(SetSelfEventArgs e)
        {
            if(this.SelfChanged != null)
            {
                this.SelfChanged(this, e);
            }
        }
        
        void Scrollbar1Click(object sender, EventArgs e)
        {
    
        }
        void PicBoxPaint(object sender, PaintEventArgs e)
        {
    
        }


    }  // end frmImage

    public enum WinType
    {
        normal = 0,
        full = 1,
        second = 2
    }

        //------------------------------   delegates   ----------------------------------------------------------

    public delegate void HandleKeyChange(object sender, SetKeyEventArgs e);
    public delegate void HandleSelfChange(object sender, SetSelfEventArgs e);
}
