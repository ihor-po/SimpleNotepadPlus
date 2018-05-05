using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleNotepadPlus
{
    public partial class te_form : Form
    {
        const string TITLE = "Редактор";
        const string QUANTITY = "Колличество символов: ";
        const string PATH = "Путь: ";
        const string LINE = "Строка: ";
        const string POSITION = "Позиция: ";

        string[]  dictionary = {
            "using", "namespace", "public", "private", "protected",
            "partial", "class", "const",
            "string", "int", "boole", "float", "double", "char",
            "get", "set", "value",
            "new", "object", "void", "var", "switch", "case", "default",
            "break","true", "false",
            "for", "if", "foreach", "in",  "as", "is",
            "else", "this", "try", "catch", "throw", "override"
        };

        RichTextBox virtualRtb;

        public te_form()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            virtualRtb = new RichTextBox();

            te_sb_quantity.Text = QUANTITY + 0.ToString();
            te_sb_path.Text = PATH;

            te_menu_newFile.Click += Te_menu_newFile_Click;

            te_tsb_newFile.Click += Te_menu_newFile_Click;

            te_menu_saveFile.Click += Te_menu_saveFile_Click;

            te_tsb_save.Click += Te_menu_saveFile_Click;

            te_menu_openFile.Click += Te_menu_openFile_Click;

            te_tsb_open.Click += Te_menu_openFile_Click;

            te_menu_closeFile.Click += Te_menu_closeFile_Click;

            te_menu_saveAs.Click += Te_menu_saveAs_Click;

            //te_rtb_editor.TextChanged += Te_rtb_editor_TextChanged;

            //te_rtb_editor.SelectionChanged += Te_rtb_editor_SelectionChanged;

            te_menu_copy.Click += Te_menu_copy_Click;
            te_tsb_copy.Click += Te_menu_copy_Click;
            te_cm_copy.Click += Te_menu_copy_Click;

            te_menu_cut.Click += Te_menu_cut_Click;
            te_tsb_cut.Click += Te_menu_cut_Click;
            te_cm_cut.Click += Te_menu_cut_Click;

            te_menu_insert.Click += Te_menu_insert_Click;
            te_tsb_insert.Click += Te_menu_insert_Click;
            te_cm_insert.Click += Te_menu_insert_Click;

            te_menu_undo.Click += Te_menu_undo_Click;
            te_tsb_undo.Click += Te_menu_undo_Click;
            te_cm_undo.Click += Te_menu_undo_Click;

            te_menu_redo.Click += Te_menu_redo_Click;
            te_tsb_redo.Click += Te_menu_redo_Click;
            te_cm_redo.Click += Te_menu_redo_Click;

            te_menu_selectAll.Click += Te_menu_selectAll_Click;

            te_menu_font.Click += Te_menu_font_Click;
            te_tsb_font.Click += Te_menu_font_Click; ;

            te_menu_fontColor.Click += Te_menu_fontColor_Click;
            te_tsb_fontColor.Click += Te_menu_fontColor_Click;

            te_tsb_backColor.Click += Te_menu_backColor_Click;
            te_tsb_backColor.Click += Te_menu_backColor_Click;

            te_menu_exit.Click += Te_menu_exit_Click;

            te_menu_closeAll.Click += Te_menu_closeAll_Click;
            te_menu_minAll.Click += Te_menu_minAll_Click;
            te_menu_normalAll.Click += Te_menu_normalAll_Click;
            te_menu_horizontal.Click += Te_menu_horizontal_Click;
            te_menu_vertical.Click += Te_menu_vertical_Click;
            te_menu_cascade.Click += Te_menu_cascade_Click;
            te_menu_reorgAll.Click += Te_menu_reorgAll_Click;

            te_menu_delete.Click += Te_menu_delete_Click;
            te_tsb_delete.Click += Te_menu_delete_Click;

            te_menu_dateTime.Click += Te_menu_dateTime_Click;
            te_tsb_dateTime.Click += Te_menu_dateTime_Click;

            te_menu_find.Click += Te_menu_find_Click;
            te_tsb_search.Click += Te_menu_find_Click;
            te_menu_replace.Click += Te_menu_find_Click;
            te_tsb_replace.Click += Te_menu_find_Click;

            te_menu_comment.Click += Te_menu_comment_Click;
            te_tsb_comment.Click += Te_menu_comment_Click;

            te_menu_uncomment.Click += Te_menu_uncomment_Click;
            te_tsb_uncomment.Click += Te_menu_uncomment_Click;

            te_tscb_editorStyle.SelectedIndexChanged += Te_tscb_editorStyle_SelectedIndexChanged;

            te_tscb_editorStyle.SelectedIndex = 0;

            this.WindowState = FormWindowState.Maximized;
           
        }

        private void Te_tscb_editorStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox cb = sender as ToolStripComboBox;
            SNP_EditorForm child = null;

            if (this.ActiveMdiChild != null)
            {
                child = this.ActiveMdiChild as SNP_EditorForm;
            }

            switch(cb.SelectedIndex)
            {
                case 0:
                    if (child != null)
                    {
                        ColorizeProcess(1, child.editorRtb);
                    }
                    break;
                case 1:
                    if (child != null)
                    {
                        ColorizeProcess(2, child.editorRtb);
                    }
                    break;
            }
        }

        #region MDIWindow

        /// <summary>
        /// Переопределение события формы при активировании дочернего окна
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMdiChildActivate(EventArgs e)
        {
            SNP_EditorForm child = this.ActiveMdiChild as SNP_EditorForm;
            if (child != null)
            {
                te_sb_path.Text = "Путь: " + child.filePath;
                Point cPosition = GetCursorPosition(child.editorRtb);

                te_sb_quantity.Text = QUANTITY + child.editorRtb.TextLength.ToString();
                te_sbl_line.Text = LINE + cPosition.X.ToString();
                te_sbl_position.Text = POSITION + cPosition.Y.ToString();
            }
            else
            {
                EnableDisableElements(false);
            }
        }

        /// <summary>
        /// Реорганизовать дочерние окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_reorgAll_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.ArrangeIcons);
        }

        /// <summary>
        /// Расположить дочерние окна каскадом
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_cascade_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        /// <summary>
        /// Расположить дочерние окна вертикально
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_vertical_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        /// <summary>
        /// Расположить дочерние окна горизонтально
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_horizontal_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        /// <summary>
        /// Развернуть все дочерние окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_normalAll_Click(object sender, EventArgs e)
        {
            foreach (Form f in MdiChildren)
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        /// <summary>
        /// Сворачивание всех дочерних окон
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_minAll_Click(object sender, EventArgs e)
        {
            foreach (Form f in MdiChildren)
            {
                f.WindowState = FormWindowState.Minimized;
            }
        }
        /// <summary>
        /// Закрытие дочерних окон
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_closeAll_Click(object sender, EventArgs e)
        {
            foreach (Form f in MdiChildren)
            {
                f.Close();
            }
        }

        #endregion

        #region Settings
        /// <summary>
        /// Изменение цвета фона
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_backColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            if (cd.ShowDialog() == DialogResult.OK)
            {
                //te_rtb_editor.BackColor = cd.Color;
            }
        }

        /// <summary>
        /// Изменение цвета шрифта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_fontColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            if (cd.ShowDialog() == DialogResult.OK)
            {
                //te_rtb_editor.ForeColor = cd.Color;
            }
        }

        /// <summary>
        /// Именение шрифта в редакторе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_font_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();

            if (fd.ShowDialog() == DialogResult.OK)
            {
               // te_rtb_editor.Font = fd.Font;
            }
        }
        #endregion

        #region Edit

        /// <summary>
        /// Поиск и замена тескта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_find_Click(object sender, EventArgs e)
        {
            string srch = "";
            string rplc = "";

            SNP_EditorForm child = this.ActiveMdiChild as SNP_EditorForm;

            if (sender.ToString() == "Найти")
            {
                SNP_searchForm sForm = new SNP_searchForm(false);

                if (sForm.ShowDialog() == DialogResult.OK)
                {
                    srch = sForm.SearchText;

                    if (child.editorRtb.Find(srch) == -1)
                    {
                        SNPMessage("Искомый текст не найден", false);
                    }
                }


            }
            else
            {
                SNP_searchForm sForm = new SNP_searchForm(true);

                if (sForm.ShowDialog() == DialogResult.OK)
                {
                    srch = sForm.SearchText;
                    rplc = sForm.ReplaceText;

                    while (child.editorRtb.Find(srch, RichTextBoxFinds.MatchCase) != -1)
                    {
                        child.editorRtb.SelectedText = rplc;
                    }
                }


            }
        }

        /// <summary>
        /// Вставляе текущую дату и время в позицию курсора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_dateTime_Click(object sender, EventArgs e)
        {
            SNP_EditorForm child = this.ActiveMdiChild as SNP_EditorForm;

            child.editorRtb.SelectedText = DateTime.Now.ToString();
        }

        /// <summary>
        /// Выбрать все в окне редактора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_selectAll_Click(object sender, EventArgs e)
        {
            SNP_EditorForm child = this.ActiveMdiChild as SNP_EditorForm;

            child.editorRtb.Focus();
            child.editorRtb.SelectAll();
        }

        /// <summary>
        /// Удалить выбраный фрагмент
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_delete_Click(object sender, EventArgs e)
        {
            SNP_EditorForm child = this.ActiveMdiChild as SNP_EditorForm;

            child.editorRtb.SelectedText = "";
        }

        /// <summary>
        /// Вставить скопированный фрагмент
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_insert_Click(object sender, EventArgs e)
        {
            SNP_EditorForm child = this.ActiveMdiChild as SNP_EditorForm;

            child.editorRtb.Paste();
        }

        /// <summary>
        /// Копирование выделенного фрагмента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_copy_Click(object sender, EventArgs e)
        {
            SNP_EditorForm child = this.ActiveMdiChild as SNP_EditorForm;

            if (child.editorRtb.SelectionLength > 0)
            {
                child.editorRtb.Copy();
            }
        }

        /// <summary>
        /// Вырезать выделенный фрагмент
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_cut_Click(object sender, EventArgs e)
        {
            SNP_EditorForm child = this.ActiveMdiChild as SNP_EditorForm;

            if (child.editorRtb.SelectionLength > 0)
            {
                child.editorRtb.Cut();
            }
        }

        /// <summary>
        /// Вернуть предыдущее действие 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_redo_Click(object sender, EventArgs e)
        {
            SNP_EditorForm child = this.ActiveMdiChild as SNP_EditorForm;

            if (child.editorRtb.CanRedo == true)
            {
                if (child.editorRtb.RedoActionName != "Delete")
                {
                    child.editorRtb.Redo();
                }
            }
        }

        /// <summary>
        /// Отменить предыдущее действие
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_undo_Click(object sender, EventArgs e)
        {
            SNP_EditorForm child = this.ActiveMdiChild as SNP_EditorForm;

            if (child.editorRtb.CanUndo == true)
            {
                child.editorRtb.Undo();
            }
        }
        #endregion

        #region File

        /// <summary>
        /// Раскомментировать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_uncomment_Click(object sender, EventArgs e)
        {
            SNP_EditorForm child = this.ActiveMdiChild as SNP_EditorForm;

            if (child.editorRtb.SelectedText.Substring(0, 2) == "/*" && child.editorRtb.SelectedText.Substring(child.editorRtb.SelectedText.Length - 2, 2) == "*/")
            {
                child.editorRtb.SelectionColor = Color.White;
                child.editorRtb.SelectedText = child.editorRtb.SelectedText.Substring(2, child.editorRtb.SelectedText.Length - 4);
            }
        }

        /// <summary>
        /// Закомментирровать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_comment_Click(object sender, EventArgs e)
        {
            SNP_EditorForm child = this.ActiveMdiChild as SNP_EditorForm;
            string comment = $"/*{child.editorRtb.SelectedText}*/";
            child.editorRtb.SelectionColor = Color.LightGray;
            child.editorRtb.SelectedText = comment;

        }

        /// <summary>
        /// Сохранить файл как ...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_saveAs_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                SNP_EditorForm child = this.ActiveMdiChild as SNP_EditorForm;


                SaveFileDialog sfd = new SaveFileDialog();

                sfd.Filter = "Все файлы(*.*)|*.*|Текстовые файлы(*txt)|*.txt|Файлы cs(*cs)|*.cs";
                sfd.FilterIndex = 2;
                sfd.Title = "Тестовый редактор :: Сохранить файла как ...";
                sfd.FileName = "new_name";              //начальное наименование файла
                sfd.DefaultExt = "txt";                 //задаем начальное раширение файла
                sfd.ValidateNames = true;               //валидация введенного имени

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string[] path = sfd.FileName.Split('\\');

                    child.fileName = path[path.Length - 1];
                    child.Text = child.fileName;
                    child.filePath = sfd.FileName;

                    te_sb_path.Text = PATH + child.filePath;

                    File.WriteAllText(child.filePath, child.editorRtb.Text);
                }


            }

        }

        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_saveFile_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                SNP_EditorForm child = this.ActiveMdiChild as SNP_EditorForm;
                File.WriteAllText(child.filePath, child.editorRtb.Text);
            }

        }

        /// <summary>
        /// Закрытие файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_closeFile_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();

                //Если дочерних окон больше нет то оключаем элементы управления
                if (this.MdiChildren.Length == 0)
                {
                    EnableDisableElements(false);
                }
            }

        }

        /// <summary>
        /// Открытие файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_openFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Все файлы(*.*)|*.*|Текстовые файлы(*.txt)|*.txt|Файлы cs(*.cs)|*.cs";
            ofd.FilterIndex = 2;
            ofd.Title = this.Text + " :: Открытие файла";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string[] path = ofd.FileName.Split('\\');

                SNP_EditorForm editor = new SNP_EditorForm();

                //Подписка на события
                editor.editorRtb.SelectionChanged += Te_rtb_editor_SelectionChanged;
                editor.editorRtb.TextChanged += Te_rtb_editor_TextChanged;
                //Конец подписки

                editor.Text = path[path.Length - 1];
                editor.fileName = path[path.Length - 1];
                editor.filePath = ofd.FileName;
                editor.editorRtb.Text = File.ReadAllText(ofd.FileName);

                editor.MdiParent = this;
                editor.Show();

                if (editor.filePath.EndsWith("cs"))
                {
                    
                    editor.Enabled = false;

                    te_tscb_editorStyle.SelectedIndex = 1;
                    ColorizeProcess(2, editor.editorRtb);

                    editor.Enabled = true;
                }
                else
                {
                    te_tscb_editorStyle.SelectedIndex = 0;
                }

                te_sb_path.Text = "Путь: " + editor.filePath;

                EnableDisableElements(true);
            }
        }

        /// <summary>
        /// Создание нового файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_newFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Все файлы(*.*)|*.*|Текстовые файлы(*.txt)|*.txt|Файлы cs(*.cs)|*.cs";
            sfd.FilterIndex = 2;
            sfd.Title = this.Text + " :: Создание новго файла";
            sfd.FileName = "new_file";              //начальное наименование файла
            sfd.DefaultExt = "txt";                 //задаем начальное раширение файла
            sfd.ValidateNames = true;               //валидация введенного имени

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string[] path = sfd.FileName.Split('\\');


                SNP_EditorForm editor = new SNP_EditorForm();

                //Подписка на события
                editor.editorRtb.SelectionChanged += Te_rtb_editor_SelectionChanged;
                editor.editorRtb.TextChanged += Te_rtb_editor_TextChanged;
                //Конец подписки

                editor.Text = path[path.Length - 1];
                editor.fileName = path[path.Length - 1];
                editor.filePath = sfd.FileName;

                editor.MdiParent = this;
                editor.Show();

                StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.Default);
                sw.Write("");
                sw.Close();

                if (editor.filePath.EndsWith("cs"))
                {
                    te_tscb_editorStyle.SelectedIndex = 1;
                    
                }

                EnableDisableElements(true);
            }
        }

        /// <summary>
        /// Завершение работы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        /// <summary>
        /// Обработка измениения выделения в редакторе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_rtb_editor_SelectionChanged(object sender, EventArgs e)
        {
            if (((RichTextBox)sender).SelectionLength > 0)
            {
                te_menu_copy.Enabled = true;
                te_menu_cut.Enabled = true;
                te_menu_delete.Enabled = true;
                

                te_tsb_copy.Enabled = true;
                te_tsb_cut.Enabled = true;
                te_tsb_delete.Enabled = true;

                te_cm_copy.Enabled = true;
                te_cm_cut.Enabled = true;

                if (te_tscb_editorStyle.SelectedIndex == 1)
                {
                    te_menu_comment.Enabled = true;
                    te_menu_uncomment.Enabled = true;

                    te_tsb_comment.Enabled = true;
                    te_tsb_uncomment.Enabled = true;
                }
                
            }
            else
            {
                te_menu_copy.Enabled = false;
                te_menu_cut.Enabled = false;
                te_menu_delete.Enabled = false;
                

                te_tsb_copy.Enabled = false;
                te_tsb_cut.Enabled = false;
                te_tsb_delete.Enabled = false;

                te_cm_copy.Enabled = false;
                te_cm_cut.Enabled = false;

                if (te_tscb_editorStyle.SelectedIndex == 1)
                {
                    te_menu_comment.Enabled = false;
                    te_menu_uncomment.Enabled = false;

                    te_tsb_comment.Enabled = false;
                    te_tsb_uncomment.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Событие изменения текста в редакторе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_rtb_editor_TextChanged(object sender, EventArgs e)
        {
            RichTextBox rtb = sender as RichTextBox;

            Point cPosition = GetCursorPosition(rtb);

            te_sb_quantity.Text = QUANTITY + rtb.TextLength.ToString();
            te_sbl_line.Text = LINE + cPosition.X.ToString();
            te_sbl_position.Text = POSITION + cPosition.Y.ToString();

            if (te_tscb_editorStyle.SelectedIndex == 1)
            {
                if (rtb.Text[rtb.Text.Length - 1] == ' ' ||
                    rtb.Text[rtb.Text.Length - 1] == ',' ||
                    rtb.Text[rtb.Text.Length - 1] == ';' ||
                    rtb.Text[rtb.Text.Length - 1] == '(' ||
                    rtb.Text[rtb.Text.Length - 1] == ')' ||
                    rtb.Text[rtb.Text.Length - 1] == '[' ||
                    rtb.Text[rtb.Text.Length - 1] == ']' ||
                    rtb.Text[rtb.Text.Length - 1] == '!' ||
                    rtb.Text[rtb.Text.Length - 1] == '{' ||
                    rtb.Text[rtb.Text.Length - 1] == '}' ||
                    rtb.Text[rtb.Text.Length - 1] == ':' ||
                    rtb.Text[rtb.Text.Length - 1] == '\r'
                    )
                {
                    int start = rtb.Text.Length - cPosition.Y;
                    int length = rtb.Text.Length;

                    foreach (string word in dictionary)
                    {
                        int i = 0;

                        int res = length - word.Length;

                        if (res > 0)
                        {
                            while (i <= res)
                            {
                                i = rtb.Text.IndexOf(word, i);
                               

                                //MessageBox.Show(word + '\r' + $"res = {res}\ri = {i}\rLength = {length}\rlength = {length}\rstart = {start}");
                                if (i < 0)
                                {
                                    break;
                                }

                                rtb.Find(word, start, length, RichTextBoxFinds.WholeWord);

                                rtb.SelectionFont = new Font(rtb.Font, FontStyle.Bold);
                                rtb.SelectionColor = Color.Yellow;

                                i += word.Length;
                            }
                        }
                    }
                    rtb.SelectionLength = 0;
                    rtb.SelectionStart = length;
                }
            }

        }

        /// <summary>
        /// Получение позиции курсора
        /// </summary>
        /// <param name="rtb"></param>
        /// <returns>Возвращает позицию курсора</returns>
        private Point GetCursorPosition(RichTextBox rtb)
        {
            Point cPosition = new Point();
            cPosition.X = rtb.GetLineFromCharIndex(rtb.SelectionStart) + 1;
            cPosition.Y = rtb.SelectionStart - rtb.GetFirstCharIndexOfCurrentLine();

            return cPosition;
        }

        /// <summary>
        /// Вывод сообщения
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="isError"></param>
        private void SNPMessage(string msg, bool isError)
        {
            if (isError)
            {
                MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Изменение цвета кода
        /// </summary>
        /// <param name="child"></param>
        //private void ColorizeCode(SNP_EditorForm child)
        //{
        //    if (child != null)
        //    {
        //        if (te_tscb_editorStyle.SelectedIndex == 1)
        //        {
        //            ColorizeProcess(1, child.editorRtb);
        //        }
        //        else
        //        {
        //            child.editorRtb.BackColor = Color.White;
        //            child.editorRtb.ForeColor = Color.Black;
        //            child.editorRtb.SelectionFont = new Font(child.editorRtb.Font, FontStyle.Regular);
        //        }
        //    }
        //}

        private void ColorizeProcess(int state, RichTextBox rtb)
        {
            Color text = Color.Blue;
            Color background = Color.Blue;
            Color code = Color.Blue;
            Font font = null;

            switch (state)
            {
                case 1: //если перекрашиваем редактор при стандартной схеме
                    code = Color.Black;
                    text = Color.Black;
                    background = Color.White;
                    font = new Font(rtb.Font, FontStyle.Regular);
                    break;
                case 2: //если перекрашиваем редактор при схеме кода
                    code = Color.Yellow;
                    text = Color.White;
                    background = Color.Blue;
                    font = new Font(rtb.Font, FontStyle.Bold);
                    break;
            }

            rtb.BackColor = background;
            rtb.ForeColor = text;

            int textLength = rtb.Text.Length;

            foreach (string word in dictionary)
            {
                int i = 0;

                while (i <= textLength - word.Length)
                {
                    i = rtb.Text.IndexOf(word, i);

                    if (i < 0)
                    {
                        break;
                    }

                    rtb.Find(word, i, textLength, RichTextBoxFinds.WholeWord);

                    rtb.SelectionFont = font;
                    rtb.SelectionColor = code;

                    i += word.Length;
                }
            }

            rtb.SelectionLength = 0;
            rtb.SelectionStart = 0;
        }

        /// <summary>
        /// Включение/выключение элементов
        /// </summary>
        /// <param name="status"></param>
        private void EnableDisableElements(bool status)
        {
            te_menu_saveFile.Enabled = status;
            te_menu_closeFile.Enabled = status;
            te_menu_saveAs.Enabled = status;
            te_menu_dateTime.Enabled = status;
            te_menu_undo.Enabled = status;
            te_menu_redo.Enabled = status;
            te_menu_insert.Enabled = status;
            te_menu_selectAll.Enabled = status;
            te_menu_find.Enabled = status;
            te_menu_replace.Enabled = status;

            te_tsb_save.Enabled = status;
            te_tsb_undo.Enabled = status;
            te_tsb_redo.Enabled = status;
            te_tsb_insert.Enabled = status;
            te_tsb_dateTime.Enabled = status;
            te_tsb_search.Enabled = status;
            te_tsb_replace.Enabled = status;

            te_cm_undo.Enabled = status;
            te_cm_redo.Enabled = status;
            te_cm_insert.Enabled = status;

            if (!status)
            {
                te_sb_path.Text = PATH;
            }
        }
    }
}
