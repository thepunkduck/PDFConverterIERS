using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ApplicationNiceties;
using ChartLib;
using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;
using Hjg.Pngcs;
using Hjg.Pngcs.Chunks;
using IERSInterface;
using Utilities;

namespace Docotron
{
    public partial class Form1 : Form, ITimeLicenceProtected
    {
        string _regDPI = "DPI";
        string _regOVW = "Overwrite";
        string _regMulti = "Multi";
        string _regAllPage = "AllPage";
        string _regRangePage = "RangePage";
        string _regFormat = "Format";
        string _regPreviewSize = "PreviewSize";

        static int PREV_SMALL = 100;
        static int PREV_MEDIUM = 400;
        static int PREV_LARGE = 800;



        public Form1()
        {
            InitializeComponent();


            doubleBuffer1.MouseWheel += DoubleBuffer1_MouseWheel;

            _lastInstalledVersion = GhostscriptVersionInfo.GetLastInstalledVersion();


            BaseForm.ApplicationIcon = this.Icon;
            fileFieldAndBrowser1.RecentFileRegistry = "RecentInput";

            toolStripTextBoxWithLabelDPI.LabelText = "DPI";
            int dpi = RegistryAccess.Int_FromRegistry(_regDPI);
            if (dpi == 0) dpi = 100;
            if (dpi < 10) dpi = 10;
            toolStripTextBoxWithLabelDPI.TextBoxText = dpi.ToString();
            overwriteToolStripMenuItem.Checked = RegistryAccess.Boolean_FromRegistry(_regOVW);
            oneFilePerPageToolStripMenuItem.Checked = RegistryAccess.Boolean_FromRegistry(_regMulti);
            checkBoxPAGES.Checked = RegistryAccess.Boolean_FromRegistry(_regAllPage);
            textBoxPAGES.Text = RegistryAccess.LoadEntryValue_FromRegistry(_regRangePage);
            checkBoxPAGES_CheckStateChanged(null, null);

            string tmpSiz = RegistryAccess.LoadEntryValue_FromRegistry(_regPreviewSize);
            smallToolStripMenuItem.Checked = true;// default
            if (tmpSiz == "medium") mediumToolStripMenuItem.Checked = true;
            if (tmpSiz == "large") largeToolStripMenuItem.Checked = true;

            string tmpExt = RegistryAccess.LoadEntryValue_FromRegistry(_regFormat);
            pNGToolStripMenuItem.Checked = true;//default
            if (tmpExt != null)
            {
                switch (tmpExt)
                {
                    case ".tif":
                        tIFFToolStripMenuItem.Checked = true;
                        break;
                    case ".jpg":
                        jPGToolStripMenuItem.Checked = true;
                        break;
                    case ".bmp":
                        bMPToolStripMenuItem.Checked = true;
                        break;
                    case ".png":
                        pNGToolStripMenuItem.Checked = true;
                        break;
                    case ".gif":
                        gIFToolStripMenuItem.Checked = true;
                        break;
                }
            }

          //  this.Text = ApplicationSetup.GetFormattedTitleText(null);
        }

        MouseWheelTranslator mwtVertScroll = new MouseWheelTranslator(10, 0, 0, 30);
        private void DoubleBuffer1_MouseWheel(object sender, MouseEventArgs e)
        {
            mwtVertScroll.ProcessEvent(e);
            int yDelta = (int)(20 * mwtVertScroll.Value);
            YOffset -= yDelta;
            doubleBuffer1.Refresh();
        }

        private readonly GhostscriptVersionInfo _lastInstalledVersion;

        // record output history
        string _path;
        int _dpi;
        bool _overwrite;
        bool _multiFiles;
        bool _allPages;
        string _imageExt;
        ImageFormat _imageFmt;
        string _prevSizeText;
        int _prevWidth = PREV_MEDIUM;
        List<int> _pagesToConvert = new List<int>();
        ThreadSafeList<object> _outputList = new ThreadSafeList<object>();
        // output destination
        // remember settings

        BackgroundWorker worker;
        bool working = false;
        bool cancel = false;
        private void _startConversionJob()
        {
            if (working) return;

            working = true;
            cancel = false;

            buttonCONVERT.Visible = false;
            buttonCANCEL.Visible = true;

            doubleBuffer1.Refresh();
            _outputList.Clear();
            YOffset = 0;
            _outputToInfo("Start Conversion:");
            _getExtension();
            _getPreviewSize();
            _allPages = checkBoxPAGES.Checked;
            _pagesToConvert = new List<int>();
            if (!_allPages)
            {
                foreach (int p in _decodePageRange(textBoxPAGES.Text))
                    _pagesToConvert.Add(p);
            }

            try
            {
                _path = fileFieldAndBrowser1.FileName;
                _dpi = int.Parse(toolStripTextBoxWithLabelDPI.TextBoxText);
                _multiFiles = oneFilePerPageToolStripMenuItem.Checked;
                _overwrite = overwriteToolStripMenuItem.Checked;

                RegistryAccess.SaveEntryValue_ToRegistry(_regDPI, _dpi);
                RegistryAccess.SaveEntryValue_ToRegistry(_regOVW, _overwrite);
                RegistryAccess.SaveEntryValue_ToRegistry(_regMulti, _multiFiles);
                RegistryAccess.SaveEntryValue_ToRegistry(_regAllPage, _allPages);
                RegistryAccess.SaveEntryValue_ToRegistry(_regRangePage, textBoxPAGES.Text);

                RegistryAccess.SaveEntryValue_ToRegistry(_regFormat, _imageExt);
                RegistryAccess.SaveEntryValue_ToRegistry(_regPreviewSize, _prevSizeText);

            }
            catch (Exception ex)
            {
                _outputToInfo("Error");
                _outputToInfo(ex.ToString());
                return;
            }


            worker = new BackgroundWorker();
            worker.DoWork += (sender, args) =>
            {
                if (Directory.Exists(_path))
                {
                    string[] files = Directory.GetFiles(_path, "*.pdf");
                    foreach (string file in files) _convert(file);
                }
                else
                {
                    _convert(_path);
                }
            };

            worker.RunWorkerCompleted += (sender, args) =>
            {
                if (cancel)
                {
                    _outputToInfo("Cancelled");
                    _outputToInfo();
                }
                else
                {
                    _outputToInfo("Done");
                    _outputToInfo();
                }

                doubleBuffer1.Refresh();

                buttonCONVERT.Visible = true;
                buttonCANCEL.Visible = false;
                working = false;
            };

            worker.RunWorkerAsync(); // starts the background worker

            // execution continues here in parallel to the background worker
            // update messages

        }

        private void buttonCONVERT_Click(object sender, EventArgs e)
        {
            _startConversionJob();
        }
        private void buttonCANCEL_Click(object sender, EventArgs e)
        {
            if (working)
            {
                cancel = true;
                return;
            }
        }
        private void _getExtension()
        {
            if (tIFFToolStripMenuItem.Checked)
            {
                _imageExt = ".tif";
                _imageFmt = ImageFormat.Tiff;
            }
            if (jPGToolStripMenuItem.Checked)
            {
                _imageExt = ".jpg";
                _imageFmt = ImageFormat.Jpeg;
            }
            if (bMPToolStripMenuItem.Checked)
            {
                _imageExt = ".bmp";
                _imageFmt = ImageFormat.Bmp;
            }
            if (pNGToolStripMenuItem.Checked)
            {
                _imageExt = ".png";
                _imageFmt = ImageFormat.Png;
            }
            if (gIFToolStripMenuItem.Checked)
            {
                _imageExt = ".gif";
                _imageFmt = ImageFormat.Gif;
            }
        }


        private void _getPreviewSize()
        {
            if (smallToolStripMenuItem.Checked)
            {
                _prevSizeText = "small";
                _prevWidth = PREV_SMALL;
            }
            if (mediumToolStripMenuItem.Checked)
            {
                _prevSizeText = "medium";
                _prevWidth = PREV_MEDIUM;
            }
            if (largeToolStripMenuItem.Checked)
            {
                _prevSizeText = "large";
                _prevWidth = PREV_LARGE;
            }
        }

        private delegate void AppendTextCallback(string text);
        private void _outputToInfo(string text)
        {

            try
            {

                _outputList.Add(text);

                doubleBuffer1.Invalidate();
                // panelPREV.Invalidate(true);
            }
            catch (ObjectDisposedException)
            {
                // object was disposed (usually on exit phase)
            }


        }
        private void _outputToInfo()
        {

            try
            {
                _outputList.Add(" ");
                doubleBuffer1.Invalidate();
                //panelPREV.Invalidate(true);
            }
            catch (ObjectDisposedException)
            {
                // object was disposed (usually on exit phase)
            }

        }
        private void _outputToInfo(Image image)
        {
            var size = _getPreviewSize(image, _prevWidth);
            _outputList.Add(resizeImage(image, size));
            doubleBuffer1.Invalidate();
            //  _updateHeight();
            // panelPREV.Invalidate(true);
        }





        static IEnumerable<int> _decodePageRange(string str)
        {
            foreach (string s in str.Split(','))
            {
                // try and get the number
                int num;
                if (int.TryParse(s, out num))
                {
                    yield return num;
                    continue; // skip the rest
                }

                // otherwise we might have a range
                // split on the range delimiter
                string[] subs = s.Split('-');
                int start, end;

                // now see if we can parse a start and end
                if (subs.Length > 1 &&
                    int.TryParse(subs[0], out start) &&
                    int.TryParse(subs[1], out end) &&
                    end >= start)
                {
                    // create a range between the two values
                    int rangeLength = end - start + 1;
                    foreach (int i in Enumerable.Range(start, rangeLength))
                    {
                        yield return i;
                    }
                }
            }
        }

        //private void _convertFAST_underconstruction(string inputPdfPath, string outputFolder)
        //{
        //    __outputToInfo(inputPdfPath);

        //    try
        //    {
        //        if (string.IsNullOrEmpty(outputFolder)) outputFolder = Path.Combine(Path.GetDirectoryName(inputPdfPath), "converted pdf folder");
        //        if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);

        //        string outputSingleFile = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(inputPdfPath) + ".png");

        //        if (!_overwrite && !_multiFiles && File.Exists(outputSingleFile))
        //        {
        //            __outputToInfo("Already converted, skip " + outputSingleFile);
        //            __outputToInfo("");
        //            return;
        //        }

        //        List<Bitmap> bmpList = new List<Bitmap>();
        //        using (GhostscriptRasterizer rasterizer = new GhostscriptRasterizer())
        //        {
        //            rasterizer.Open(inputPdfPath, _lastInstalledVersion, false);

        //            if (_multiFiles)
        //            {
        //                string outputPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(inputPdfPath));
        //                for (int pageNumber = 1; pageNumber <= rasterizer.PageCount; pageNumber++)
        //                {
        //                    string pageFilePath = outputPath + "_" + pageNumber.ToString() + ".png";
        //                    if (!_overwrite && File.Exists(pageFilePath))
        //                    {
        //                        __outputToInfo("Already converted, skip " + pageFilePath);
        //                        continue;
        //                    }

        //                    Image img = rasterizer.GetPage(_dpi, _dpi, pageNumber);

        //                    img.Save(pageFilePath, ImageFormat.Png);
        //                    __outputToInfo("OUTPUT: " + pageFilePath);
        //                    __outputToInfo("");
        //                    _history(inputPdfPath, pageFilePath);
        //                }
        //            }
        //            else
        //            {
        //                //  string outputPath = Path.GetTempPath();
        //                // string prefix = Guid.NewGuid().ToString().Substring(0, 7);
        //                //List<string> pathList = new List<string>();

        //                int w = 0, h = 0;
        //                int depth = 24;
        //                for (int pageNumber = 1; pageNumber <= rasterizer.PageCount; pageNumber++)
        //                {
        //                    Image img = rasterizer.GetPage(_dpi, _dpi, pageNumber);
        //                    h += img.Height;
        //                    w = Math.Max(w, img.Width);
        //                    depth = System.Drawing.Bitmap.GetPixelFormatSize(img.PixelFormat);
        //                }


        //                depth = depth / 3;

        //                // combine!!
        //                //   PngReader pngr = FileHelper.CreatePngReader(pathList[0]); // or you can use the constructor
        //                ImageInfo ii = new ImageInfo(w, h, depth, false, false, false);
        //                PngWriter pngw = FileHelper.CreatePngWriter(outputSingleFile, ii, true);
        //                int chunkBehav = ChunkCopyBehaviour.COPY_ALL_SAFE; // tell to copy all 'safe' chunks
        //                //  pngw.CopyChunksFirst(pngr, chunkBehav);          // copy some metadata from reader 
        //                //  pngr.End();

        //                int destRow = 0;
        //                int ic = 1;
        //                for (int pageNumber = 1; pageNumber <= rasterizer.PageCount; pageNumber++)
        //                {
        //                    // string pageFilePath = Path.Combine(outputPath, prefix + "_" + pageNumber.ToString() + ".png");
        //                    Image img = rasterizer.GetPage(_dpi, _dpi, pageNumber);
        //                    LockBitmap lockBitmap = new LockBitmap(img.Clone() as Bitmap);

        //                    for (int i = 0; i < img.Height; i++)
        //                    {
        //                        byte[] rowByte = lockBitmap.GetRow(i);
        //                        pngw.WriteRowByte(rowByte, destRow++);
        //                    }
        //                    __outputToInfo("BUILDING:" + ic + " of " + rasterizer.PageCount);
        //                    ic++;
        //                }

        //                //pngw.CopyChunksLast(pngr, chunkBehav); // metadata after the image pixels? can happen
        //                pngw.End(); // dont forget this

        //                __outputToInfo("OUTPUT:" + outputSingleFile);
        //                __outputToInfo("");
        //                _history(inputPdfPath, outputSingleFile);
        //                AutoCrop(outputSingleFile);
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        __outputToInfo("Error");
        //        __outputToInfo(ex.ToString());
        //        throw;
        //    }


        //}

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
        string outputFolder;

        private void _convert(string inputPdfPath)
        {
            _outputToInfo(inputPdfPath);

            // make a sub path in the same folder as this

            // put file(s) in there

            // single file has NAME.png

            // multiples are NAME_000.png

            // overwrite just uses folder always

            // otherwise, append output folder if it exists and is NOT empty

            try
            {
                outputFolder = _createOutputFolder(inputPdfPath, _overwrite);
                if (outputFolder == null)
                {
                    _outputToInfo("Error");
                    return;
                }

                string historyFile = Path.Combine(outputFolder, "history.txt");

                _outputToInfo("Output to: " + outputFolder);
                _outputToInfo(" ");
                using (GhostscriptRasterizer rasterizer = new GhostscriptRasterizer())
                {
                    rasterizer.Open(inputPdfPath, _lastInstalledVersion, false);

                    if (_multiFiles)
                    {
                        string outputPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(inputPdfPath));
                        int nFiles = 0;
                        for (int pageNumber = 1; pageNumber <= rasterizer.PageCount; pageNumber++)
                        {
                            if (cancel) break;
                            if (_isPageNumberRequired(pageNumber))
                            {
                                string pageFilePath = outputPath + "_" + pageNumber.ToString("000") + _imageExt;
                                Image img = rasterizer.GetPage(_dpi, pageNumber);
                                img.Save(pageFilePath, _imageFmt);
                                _outputToInfo(img);
                                _outputToInfo("  " + Path.GetFileName(pageFilePath));
                                _outputToInfo("  Dimensions:" + img.Width + " x " + img.Height);
                                _outputToInfo();

                                _history(historyFile, inputPdfPath, pageFilePath);
                                nFiles++;
                            }
                        }
                        _outputToInfo("  complete, " + nFiles + " files in:");
                        _outputToInfo("     " + outputFolder);
                        _outputToInfo();
                    }
                    else
                    {
                        // work in PNG here
                        string outputPath = Path.GetTempPath();
                        string prefix = Guid.NewGuid().ToString().Substring(0, 7);
                        List<string> pathList = new List<string>();
                        int w = 0, h = 0;
                        for (int pageNumber = 1; pageNumber <= rasterizer.PageCount; pageNumber++)
                        {
                            if (cancel) break;
                            if (_isPageNumberRequired(pageNumber))
                            {
                                string pageFilePath = Path.Combine(outputPath, prefix + "_" + pageNumber.ToString() + ".png");
                                Image img = rasterizer.GetPage(_dpi, pageNumber);
                                img.Save(pageFilePath, ImageFormat.Png);
                                _outputToInfo(img);
                                pathList.Add(pageFilePath);
                                _outputToInfo("  building page: " + pageNumber + " of " + rasterizer.PageCount);
                                _outputToInfo();
                                h += img.Height;
                                w = Math.Max(w, img.Width);
                            }
                        }

                        // combine!!
                        if (cancel == false && pathList.Count > 0)
                        {
                            string bigImagePath = Path.Combine(outputPath, prefix + "_BIG" + ".png");
                            PngReader pngr = FileHelper.CreatePngReader(pathList[0]); // or you can use the constructor
                            ImageInfo ii = new ImageInfo(w, h, pngr.ImgInfo.BitDepth, pngr.ImgInfo.Alpha, false, false);
                            PngWriter pngw = FileHelper.CreatePngWriter(bigImagePath, ii, true);
                            int chunkBehav = ChunkCopyBehaviour.COPY_ALL_SAFE; // tell to copy all 'safe' chunks
                            pngw.CopyChunksFirst(pngr, chunkBehav);          // copy some metadata from reader 
                            pngr.End();

                            int destRow = 0;
                            int ic = 1;
                            foreach (string path in pathList)
                            {
                                pngr = FileHelper.CreatePngReader(path);
                                ImageLines iLines = pngr.ReadRowsInt(0, pngr.ImgInfo.Rows, 1);

                                int nrr = iLines.Nrows;
                                for (int i = 0; i < nrr; i++)
                                    pngw.WriteRow(iLines.GetImageLineAtMatrixRow(i), destRow++);
                                _outputToInfo(" combining page:" + ic + " of " + pathList.Count);
                                ic++;
                                pngr.End();
                                File.Delete(path); // remove them as we go
                            }

                            pngw.CopyChunksLast(pngr, chunkBehav); // metadata after the image pixels? can happen
                            pngw.End(); // dont forget this
                            _autoCrop(bigImagePath);


                            // convert to required type if not PNG
                            string outputSingleFile = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(inputPdfPath) + _imageExt);
                            Bitmap bm;
                            if (_imageExt != ".png")
                            {
                                bm = new Bitmap(bigImagePath);
                                bm.Save(outputSingleFile, _imageFmt);
                            }
                            else
                            {
                                bm = new Bitmap(bigImagePath);
                                _moveFile(bigImagePath, outputSingleFile);
                            }

                            _outputToInfo(bm);
                            _outputToInfo("  Dimensions:" + bm.Width + " x " + bm.Height);
                            _outputToInfo("  Complete: ");
                            _outputToInfo("   " + outputSingleFile);
                            _history(historyFile, inputPdfPath, outputSingleFile);
                        }
                        else
                        {
                            _outputToInfo("  NO PAGES set! Try again.");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _outputToInfo("Error");
                _outputToInfo(ex.ToString());
                throw;
            }


        }
        private Size _getPreviewSize(Image img, int prevWidth)
        {
            return (new Size(prevWidth, prevWidth * img.Height / img.Width));
        }

        private bool _isPageNumberRequired(int pageNumber)
        {
            if (_allPages) return (true);
            return (_pagesToConvert.Contains(pageNumber));
        }

        string ALL_IMAGES_PATTERN = "*.png|*.bmp|*.gif|*.jpeg|*.jpg|*.tif";
        private string _createOutputFolder(string path, bool overwrite)
        {
            try
            {
                if (string.IsNullOrEmpty(path)) return (null);

                string folder = Path.GetDirectoryName(path);
                string subFolder = Path.GetFileNameWithoutExtension(path);
                string outputFolderBase = Path.Combine(folder, subFolder);
                string outputFolder = outputFolderBase;

                int ic = 0;
                while (true)
                {
                    if (!Directory.Exists(outputFolder))
                    {
                        Directory.CreateDirectory(outputFolder);
                        return (outputFolder);
                    }
                    else
                    {
                        // it exists
                        if (overwrite) return (outputFolder);
                        string[] files = GetFiles(outputFolder, ALL_IMAGES_PATTERN, SearchOption.TopDirectoryOnly);
                        if (files.Length == 0) // empty, can use
                            return (outputFolder);
                    }
                    ic++;
                    outputFolder = outputFolderBase + "_" + ic.ToString("000");
                }
            }
            catch (Exception ex)
            {
                _outputToInfo("Error");
                _outputToInfo(ex.Message);
                return (null);

            }


        }



        public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            string[] searchPatterns = searchPattern.Split('|');
            List<string> files = new List<string>();
            foreach (string sp in searchPatterns)
                files.AddRange(System.IO.Directory.GetFiles(path, sp, searchOption));
            files.Sort();
            return files.ToArray();
        }


        private bool _autoCrop(string filename)
        {
            PngReader pngr = FileHelper.CreatePngReader(filename);
            int nrr = pngr.ImgInfo.Rows;
            int nc = pngr.ImgInfo.Cols;
            int firstNonBlank = -1;
            int lastNonBlank = -1;
            for (int i = 0; i < nrr; i++)
            {
                ImageLine iLine = pngr.ReadRow(i);

                // is it a blank?
                Boolean isBlank = true;
                for (int j = 0; j < nc; j++) { if (iLine.Scanline[j] != 255) { isBlank = false; break; } }

                if (!isBlank)
                {
                    if (firstNonBlank == -1) firstNonBlank = i;
                    lastNonBlank = i;
                }
            }

            int minBdr = 20;

            int row0 = firstNonBlank - minBdr;
            if (row0 < 0) row0 = 0;

            int row1 = lastNonBlank + minBdr;
            if (row1 >= nrr) row1 = nrr - 1;
            pngr.End();


            if (row0 == 0 && row1 == (nrr - 1)) return (false);

            // start over
            string tmp = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename) + "_cropped.png");
            pngr = FileHelper.CreatePngReader(filename);
            int h = row1 - row0 + 1;
            ImageInfo ii = new ImageInfo(nc, h, pngr.ImgInfo.BitDepth, pngr.ImgInfo.Alpha, false, false);
            PngWriter pngw = FileHelper.CreatePngWriter(tmp, ii, true);
            int chunkBehav = ChunkCopyBehaviour.COPY_ALL_SAFE; // tell to copy all 'safe' chunks
            pngw.CopyChunksFirst(pngr, chunkBehav);          // copy some metadata from reader 

            int ic = 0;
            for (int i = row0; i <= row1; i++)
            {
                ImageLine iLine = pngr.ReadRow(i);
                pngw.WriteRow(iLine, ic++);
            }

            pngr.End();


            pngw.CopyChunksLast(pngr, chunkBehav); // metadata after the image pixels? can happen
            pngw.End(); // do

            _moveFile(tmp, filename);

            return (true);
        }

        void _moveFile(string src, string dst)
        {
            try
            {
                if (File.Exists(src))
                {
                    try { File.Delete(dst); } catch { };
                    File.Move(src, dst);
                }
            }
            catch { }
        }
        private void _history(string path, string input, string output)
        {
            File.AppendAllText(path, "\r\n");
            File.AppendAllText(path, DateTime.Now.ToString() + "\r\n");
            File.AppendAllText(path, "Converted " + input + " into " + output + "\r\n");
            File.AppendAllText(path, "dpi=" + _dpi + " " + (_multiFiles ? "Multiple Page Output" : "One Page Output") + "\r\n");
        }




        ToolStripMenuItem ITimeLicenceProtected.LicenceParentMenu()
        {
            return (helpToolStripMenuItem);
        }

        Form ITimeLicenceProtected.ProtectedForm()
        {
            return (this);
        }

        void ITimeLicenceProtected.LicenceUpdated()
        {
    //        this.Text = ApplicationSetup.GetFormattedTitleText(null);
        }

        public void ShutdownApp()
        {
            Close();
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new AboutForm
            {
                Text = "About " + Application.ProductName,
                Image = BitmapGenerator.GenerateAboutBitmap(),
                ProductName = Application.ProductName,
                CopyrightOwner = Application.CompanyName,
                Email = "jparsons@quantiseal.com",
                Credits = "Development Team:\n James Parsons",
                LicenseInfo = "Licenced to: " + TimeLicence.LicenceOwner + "\n\n" + TimeLicence.Report,
                UseAssemblyVersion = true,
            };
            dlg.ShowDialog(this);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileFieldAndBrowser1.Browse();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBoxPAGES_CheckStateChanged(object sender, EventArgs e)
        {
            labelPAGES.Enabled = !checkBoxPAGES.Checked;
            textBoxPAGES.Enabled = !checkBoxPAGES.Checked;
        }

        bool inPaint = false;
        int _yOffset = 0;
        int _yDataHeight;
        int _yEnd;
        private void doubleBuffer1_PaintEvent(object sender, PaintEventArgs e)
        {
            if (inPaint) return;

            inPaint = true;
            Graphics g = null;

            _yDataHeight = 0;
            _yEnd = 0;
            int maxY = doubleBuffer1.Height;
            int minY = -10;
            try
            {
                var cpyList = _outputList.Clone();
                //Console.Write("<<");
                g = e.Graphics;

                bool iDraw = true;
                while (iDraw)
                {
                    iDraw = false;
                    int x = 10;
                    int y = 10 - YOffset;
                    int _spacing = 10;
                    int ic = 0;
                    g.Clear(doubleBuffer1.BackColor);
                    foreach (var item in cpyList)
                    {
                        Image image = item as Image;
                        string text = item as string;

                        if (image != null)
                        {
                            var rect = new Rectangle(x, y, image.Width, image.Height);
                            if (rect.Bottom > minY && rect.Top < maxY)
                            {
                                rect.Offset(5, 5);
                                g.FillRectangle(Brushes.DimGray, rect);
                                rect.Offset(-5, -5);
                                g.DrawImage(image, x, y);
                                g.DrawRectangle(Pens.DimGray, rect);
                            }
                            y += image.Height;
                            y += _spacing;
                        }
                        else
                        {
                            if (y > minY && y < maxY)
                                g.DrawString(text, this.Font, Brushes.Yellow, x, y);
                            y += this.Font.Height;
                            y += 2;
                            //Console.Write("T" + ic++ + " ");
                        }
                    }
                    _yEnd = y;
                    _yDataHeight = y + YOffset;

                    // Console.WriteLine(">>");

                    if (y > maxY && working)
                    {
                        YOffset = _yDataHeight - maxY;
                        iDraw = true;
                    }
                    int mY = Math.Max(0, _yDataHeight - doubleBuffer1.Height);
                    if (_yOffset > mY)
                    { _yOffset = mY; iDraw = true; }
                }



                // fix up scrollbar
                double shownProp = ((double)maxY) / _yDataHeight;
                int large = (int)Math.Round(maxSB * shownProp);
                if (large < 10) large = 10;
                if (large > maxSB) large = maxSB;
                int maxVal = maxSB - large;
                int val = (int)Math.Round(QSGeometry.QSGeometry.Interpolate(YOffset, 0, _yDataHeight, 0, maxSB));
                if (val < 0) val = 0;
                if (val > maxVal) val = maxVal;
                vScrollBar1.Minimum = 0;
                vScrollBar1.Maximum = maxSB;
                vScrollBar1.SmallChange = large / 10;
                vScrollBar1.LargeChange = large;
                vScrollBar1.Value = val;


            }
            catch (Exception ex)
            {

            }
            finally
            {
            }

            inPaint = false;
        }

        int maxSB = 100000;
        int mouseDownY;
        int yOffsetAtMouseDown;
        bool mouseDown;

        public int YOffset
        {
            get { return (_yOffset); }
            set
            {
                _yOffset = value; if (_yOffset < 0) _yOffset = 0;
                int mY = Math.Max(0, _yDataHeight - doubleBuffer1.Height);
                if (_yOffset > mY)
                    _yOffset = mY;
            }
        }

        private void doubleBuffer1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            mouseDownY = e.Y;
            yOffsetAtMouseDown = YOffset;
        }

        private void doubleBuffer1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown == false) return;
            int dY = e.Y - mouseDownY;
            YOffset = yOffsetAtMouseDown - dY;
            doubleBuffer1.Refresh();
        }

        private void doubleBuffer1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            doubleBuffer1.Refresh();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            vScrollBar1_ValueChanged(sender, e);
        }

        private void vScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            if (inPaint) return;
            YOffset = (int)Math.Round(QSGeometry.QSGeometry.Interpolate(vScrollBar1.Value, 0, maxSB, 0, _yDataHeight));
            doubleBuffer1.Refresh();
        }

        private void browseToOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", outputFolder);
            }
            catch
            {

            }
        }
    }



    public class ThreadSafeList<T> : IList<T>
    {
        protected List<T> _interalList = new List<T>();

        // Other Elements of IList implementation

        public IEnumerator<T> GetEnumerator()
        {
            return Clone().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Clone().GetEnumerator();
        }

        protected static object _lock = new object();

        int ICollection<T>.Count => _interalList.Count;

        bool ICollection<T>.IsReadOnly => false;

        T IList<T>.this[int index] { get => _interalList[index]; set => _interalList[index] = value; }

        public List<T> Clone()
        {
            List<T> newList = new List<T>();

            lock (_lock)
            {
                _interalList.ForEach(x => newList.Add(x));
            }

            return newList;
        }

        int IList<T>.IndexOf(T item)
        {
            return (_interalList.IndexOf(item));
        }

        void IList<T>.Insert(int index, T item)
        {
            _interalList.Insert(index, item);
        }

        void IList<T>.RemoveAt(int index)
        {
            _interalList.RemoveAt(index);
        }

        public void Add(T item)
        {
            _interalList.Add(item);
        }

        public void Clear()
        {
            _interalList.Clear();
        }

        bool ICollection<T>.Contains(T item)
        {
            return (_interalList.Contains(item));
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            _interalList.CopyTo(array, arrayIndex);
        }

        bool ICollection<T>.Remove(T item)
        {
            return (_interalList.Remove(item));
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (_interalList.GetEnumerator());
        }
    }
}
