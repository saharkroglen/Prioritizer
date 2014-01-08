using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Commands;
using DevExpress.XtraRichEdit.Model;
using System.Text.RegularExpressions;
using System.IO;
using DevExpress.XtraRichEdit;
using System.Data.Objects;
//using Netformx.Online.Services.PrioritizerService.Contracts.Data;
using PrioritizerService.Model;

namespace Prioritizer2._0
{
    public enum enTaskType
    {
        ActionItem,
        Decision
    } 
    public partial class MeetingSummaryControl : UserControl
    {
        private int LastCaretPositionInRTFDocument;
        NewPrioritizer _parent;
        public MeetingSummaryControl(NewPrioritizer parent)
        {
            InitializeComponent();            
                
            _parent = parent;
            richEditControl1.Enabled = isMeetingOwner();
        }

        public void saveCaretPosition()
        {
            LastCaretPositionInRTFDocument = richEditControl1.Document.CaretPosition.ToInt();
        }

        public void createNewDocument()
        {
            richEditControl1.CreateNewDocument();
        }

        public void loadRTFDocument(MemoryStream ms)
        {            
            richEditControl1.LoadDocument(ms, DocumentFormat.Rtf);
            syncActionItems();
            richEditControl1.Enabled = isMeetingOwner();

            btnCreateAI.Enabled = btnCreateDecision.Enabled = btnInsertExistingAI.Enabled = btnSaveMeeting.Enabled = btnSyncActionItems.Enabled = isMeetingOwner();
        }

        
        private void btnCreateAI_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            createTask(enTaskType.ActionItem);
        }

        private void createTask(enTaskType type)
        {
            saveCaretPosition();
            string selectedLineText = getSelectedLineText();

            System.Text.RegularExpressions.Match match = FindActionItemPatternInText(selectedLineText);
            if (match.Captures.Count > 0)
            {
                MessageBox.Show(string.Format("This line is already attached with Action Item: '{0}'", match.ToString()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            StartOfLineCommand startOfLineCommand = new StartOfLineCommand(richEditControl1);
            startOfLineCommand.Execute();
            DocumentPosition beginOfLine = richEditControl1.Document.CaretPosition;

            //create task
            TaskForm newTaskForm = new TaskForm(_parent, new Tasks() { taskName = selectedLineText.Substring(0, Math.Min(128, selectedLineText.Length)), remarks = selectedLineText, updateRequester = false, projectID = Guid.Parse("00000000-0000-0000-0000-000000000000"), requesterID = Guid.Parse("00000000-0000-0000-0000-000000000000") }, formMode.add, type);
            _parent.openTaskForm(newTaskForm);

            if (newTaskForm.DialogResult == DialogResult.OK)
            {
                using (var model = new DocumentModel())
                {
                    string aiCaption = addActionItemText(beginOfLine, newTaskForm._task.ID, newTaskForm._task.getUserName());
                    startOfLineCommand.Execute(); //Go back to begining of line and make the inserted text as hyperlink
                    ChangeTextToHyperlink(aiCaption, newTaskForm._task.ID);
                    SaveMeetingRTF();
                }

                //DocumentImageCollection dic = richEditControl1.Document.GetImages(richEditControl1.Document.Range);

                syncActionItems();
            }
        }

        public void syncActionItems()
        {
            HyperlinkCollection hlc = richEditControl1.Document.Hyperlinks;
            List<Hyperlink> linksRequireUpdate = new List<Hyperlink>();
            foreach (var h in hlc)
            {
                Guid taskID;
                bool taskIDFound = extractTaskIDFromHyperlink(h, out taskID);
                if (taskIDFound == true)
                {
                    //Hyperlink currentHyperlink = h;
                    Tasks t = _parent.getTaskByTaskID(taskID);
                    if (t == null)
                        continue;

                    h.ToolTip = t.getTaskSummary();
                    string requiredAIText = getAIText(t.getUserName());
                    string currentAIText = richEditControl1.Document.GetText(h.Range);
                    if (requiredAIText != currentAIText)
                    {
                        linksRequireUpdate.Add(h);                        
                    }

                    DevExpress.XtraRichEdit.API.Native.CharacterProperties characterProperties = richEditControl1.Document.BeginUpdateCharacters(h.Range);
                    characterProperties.BackColor = _parent.getColorForStatus(t.taskStatusID.ToString());// .Italic = true;
                    richEditControl1.Document.EndUpdateCharacters(characterProperties);
                }
            }

            foreach (Hyperlink h in linksRequireUpdate)
            {
                Guid taskID;
                bool taskIDFound = extractTaskIDFromHyperlink(h, out taskID);
                Tasks t = _parent.getTaskByTaskID(taskID);
                richEditControl1.Document.Replace(h.Range, getAIText(t.getUserName()));
                //int hyperlinkStartLocation = h.Range.Start.ToInt()-1;
                //richEditControl1.Document.Delete(h.Range);
                //DocumentPosition beginingOfHyperLink = richEditControl1.Document.CreatePosition(hyperlinkStartLocation);
                //string AIText = addActionItemText(beginingOfHyperLink, t.ID, t.getUserName());
                //richEditControl1.Document.CaretPosition = beginingOfHyperLink;
                //ChangeTextToHyperlink(AIText, t.ID);
            }
        }

        private readonly string AI_URL_PATTERN = "prioritizeURI:{0}";
        private Hyperlink ChangeTextToHyperlink(string aiCaption, Guid taskID)
        {            
            Hyperlink hyperlink = richEditControl1.Document.CreateHyperlink(richEditControl1.Document.CaretPosition, aiCaption.Length);
            hyperlink.ToolTip = "";
            hyperlink.NavigateUri = string.Format(AI_URL_PATTERN, SimplerAES.Encrypt(taskID.ToString()));
            return hyperlink;
        }

        private readonly string AI_PATTERN = "-AI@{0}-";
        private string addActionItemText(DocumentPosition position, Guid taskID, string userName)
        {            
            richEditControl1.Document.CaretPosition = position;
            //richEditControl1.Document.InsertImage(richEditControl1.Document.CaretPosition, DocumentImageSource.FromFile(@"c:\temp\actionItem.png"));
            string aiCaption = getAIText(userName);
            richEditControl1.Document.InsertText(richEditControl1.Document.CaretPosition, aiCaption);
            return aiCaption;
        }

        private string getAIText(string userName)
        {
            string aiCaption = string.Format(AI_PATTERN, userName);
            return aiCaption;
        }

        private static System.Text.RegularExpressions.Match FindActionItemPatternInText(string selectedLineText)
        {
            string pattern = "-AI@(.*?)-"; //non greedy regex search (using the ? mark) which search for -AIxxx- matches
            System.Text.RegularExpressions.Match match = Regex.Match(selectedLineText, pattern);
            return match;
        }

        private static System.Text.RegularExpressions.Match FindActionItemPatternInNavigateURL(string selectedLineText)
        {
            string pattern = "prioritizeURI:(.*)"; //non greedy regex search (using the ? mark) which search for -AIxxx- matches
            System.Text.RegularExpressions.Match match = Regex.Match(selectedLineText, pattern);
            return match;
        }

        private string getSelectedLineText()
        {
            SelectCurrentLine();
            DocumentRange dr = richEditControl1.Document.Selection;
            return richEditControl1.Document.GetText(dr);
        }

        private void SelectCurrentLine()
        {
            StartOfLineCommand startOfLineCommand = new StartOfLineCommand(richEditControl1);
            EndOfLineCommand endOfLineCommand = new EndOfLineCommand(richEditControl1);

            startOfLineCommand.Execute();

            int start = richEditControl1.Document.CaretPosition.ToInt();

            endOfLineCommand.Execute();

            int length = richEditControl1.Document.CaretPosition.ToInt() - start;

            DocumentRange AITextRange = richEditControl1.Document.CreateRange(start, length);
            DocumentRange AITextRangeWithNewLine = richEditControl1.Document.CreateRange(start, length + 1);

            string text = richEditControl1.Document.GetText(AITextRangeWithNewLine);

            if (text.EndsWith(Environment.NewLine))
                richEditControl1.Document.Selection = AITextRangeWithNewLine;
            else
                richEditControl1.Document.Selection = AITextRange;
        }

        private void richEditControl1_HyperlinkClick(object sender, DevExpress.XtraRichEdit.HyperlinkClickEventArgs e)
        {
            Guid taskID;
            bool taskIDFound = extractTaskIDFromHyperlink(e.Hyperlink, out taskID);

            if (taskIDFound == true)
            {
                _parent.selectRowByTaskID(taskID);
                e.Handled = true;
                return;
            }
            //else
            //    MessageBox.Show(string.Format("Could not find selected task"), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool extractTaskIDFromHyperlink(Hyperlink hl, out Guid taskID)
        {
            taskID = Guid.Parse("00000000-0000-0000-0000-000000000000");
            //string hyperLinkText = richEditControl1.Document.GetText(hl.Range);
            System.Text.RegularExpressions.Match match = FindActionItemPatternInNavigateURL(hl.NavigateUri);
            
            if (match.Groups[1].Value.Length == 0)
                return false;

            string decryptedTaskID = SimplerAES.Decrypt(match.Groups[1].Value);
            return Guid.TryParse(decryptedTaskID, out taskID);
        }


        private void btnSaveMeeting_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveMeetingRTF();
        }

        public void focusOnHyperlink(Guid taskID)
        {
            HyperlinkCollection hlc = richEditControl1.Document.Hyperlinks;

            StartOfLineCommand startOfLineCommand = new StartOfLineCommand(richEditControl1);
            startOfLineCommand.Execute();

            foreach (var h in hlc)
            {
                Guid extractedTaskID;
                bool taskIDFound = extractTaskIDFromHyperlink(h, out extractedTaskID);
                if (taskIDFound == true && extractedTaskID == taskID)
                {                    
                    richEditControl1.Document.CaretPosition = richEditControl1.Document.CreatePosition(h.Range.Start.ToInt());
                    richEditControl1.ScrollToCaret();
                    richEditControl1.Document.Selection = h.Range;
                }
            }
        }

        public List<Guid> getTasksInDocument()
        {
            HyperlinkCollection hlc = richEditControl1.Document.Hyperlinks;
            List<Guid> tasksIDs = new List<Guid>();
            foreach (var h in hlc)
            {
                Guid extractedTaskID;
                bool taskIDFound = extractTaskIDFromHyperlink(h, out extractedTaskID);
                if (taskIDFound == true)
                {
                    tasksIDs.Add(extractedTaskID);
                }
            }
            return tasksIDs;
        }

        public void SaveMeetingRTF()
        {
            try
            {
                if (isMeetingOwner())
                {
                    if (_parent.selectedMeeting == null)
                        return;

                    MemoryStream ms = new MemoryStream();
                    richEditControl1.SaveDocument(ms, DocumentFormat.Rtf);
                    _parent.selectedMeeting.MeetingSummaryRTF = ms.ToArray();
                    _parent.selectedMeeting.updateDate = DateTime.Now;
                    NewPrioritizer.ProxyClient.applyChangesMeetings(_parent.selectedMeeting,null);
                    _parent.setSelectedMeeting(_parent.selectedMeeting.ID, false);//get fresh copy of selected meeting for concurrency matters.
                }
                //NewPrioritizer.repository.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Entities may have been modified or deleted since entities were loaded."))
                {                    
                    richEditControl1.SelectAll();
                    richEditControl1.Copy();
                    //System.Windows.Forms.Clipboard.SetText(richEditControl1.RtfText);
                    MessageBox.Show("Failed to save Meeting Summary document due to concurrent update by another user\nYour version of the document was saved into the clipboard_\nPlease refresh the meeting first to load latest version to complete saving action\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //NewPrioritizer.repository.Refresh(RefreshMode.StoreWins, NewPrioritizer.repository.Meetings);
                }
                else
                    throw ex;
            }
        }

        private bool isMeetingOwner()
        {
            if (_parent.selectedMeeting == null)
                return false;

            return _parent.selectedMeeting.MeetingOwner == NewPrioritizer.loggedInUserID;
        }

        public string getDocumentAsHtml()
        {
            return richEditControl1.HtmlText;
        }
        private void btnSyncActionItems_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            syncActionItems();
        }

        private void btnInsertExistingAI_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveCaretPosition();      
            if (_parent.SelectedTask != null)
            {
                //locate the caret in the last known location
                richEditControl1.Document.CaretPosition = richEditControl1.Document.CreatePosition(LastCaretPositionInRTFDocument);
                //insert text AI text
                string aiCaption = addActionItemText(richEditControl1.Document.CaretPosition, _parent.SelectedTask.ID, _parent.SelectedTask.getUserName());
                //go back to begining of text
                richEditControl1.Document.CaretPosition = richEditControl1.Document.CreatePosition(richEditControl1.Document.CaretPosition.ToInt() - aiCaption.Length);
                //change to hypertext
                ChangeTextToHyperlink(aiCaption, _parent.SelectedTask.ID);
                //locate caret at the end of the hyper link
                richEditControl1.Document.CaretPosition = richEditControl1.Document.CreatePosition(richEditControl1.Document.CaretPosition.ToInt() + aiCaption.Length + 1);
                //add task name
                richEditControl1.Document.InsertText(richEditControl1.Document.CaretPosition, _parent.SelectedTask.taskName);
                
            }
            syncActionItems();
        }

        private void btnCreateDecision_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            createTask(enTaskType.Decision);
        }
    }
}
