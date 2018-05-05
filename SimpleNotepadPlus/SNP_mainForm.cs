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

namespace TextEditor
{
    public partial class te_form : Form
    {
        const string TITLE = "Редактор";
        const string QUANTITY = "Колличество символов: ";
        const string PATH = "Путь: ";
        const string LINE = "Строка: ";
        const string POSITION = "Позиция: ";

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

            te_rtb_editor.TextChanged += Te_rtb_editor_TextChanged;

            te_rtb_editor.SelectionChanged += Te_rtb_editor_SelectionChanged;

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

            this.WindowState = FormWindowState.Maximized;
           
        }

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

        #region MDIWindow

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

        /// <summary>
        /// Завершение работы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

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
                te_rtb_editor.BackColor = cd.Color;
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
                te_rtb_editor.ForeColor = cd.Color;
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
                te_rtb_editor.Font = fd.Font;
            }
        }
        #endregion

        #region Edit
        /// <summary>
        /// Выбрать все в окне редактора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_selectAll_Click(object sender, EventArgs e)
        {
            te_rtb_editor.Focus();
            te_rtb_editor.SelectAll();
        }

        /// <summary>
        /// Вернуть предыдущее действие 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_redo_Click(object sender, EventArgs e)
        {
            if (te_rtb_editor.CanRedo == true)
            {
                if (te_rtb_editor.RedoActionName != "Delete")
                {
                    te_rtb_editor.Redo();
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
            if (te_rtb_editor.CanUndo == true)
            {
                te_rtb_editor.Undo();
                te_rtb_editor.ClearUndo();
            }
        }

        /// <summary>
        /// Вставить скопированный фрагмент
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_insert_Click(object sender, EventArgs e)
        {
            te_rtb_editor.Paste();
        }

        /// <summary>
        /// Вырезать выделенный фрагмент
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_cut_Click(object sender, EventArgs e)
        {
            if (te_rtb_editor.SelectionLength > 0)
            {
                te_rtb_editor.Cut();
            }
        }

        /// <summary>
        /// Копирование выделенного фрагмента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_copy_Click(object sender, EventArgs e)
        {
            if (te_rtb_editor.SelectionLength > 0)
            { 
                te_rtb_editor.Copy();
            }
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
                te_tsb_copy.Enabled = true;
                te_tsb_cut.Enabled = true;
                te_cm_copy.Enabled = true;
                te_cm_cut.Enabled = true;
            }
            else
            {
                te_menu_copy.Enabled = false;
                te_menu_cut.Enabled = false;
                te_tsb_copy.Enabled = false;
                te_tsb_cut.Enabled = false;
                te_cm_copy.Enabled = false;
                te_cm_cut.Enabled = false;
            }
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
        /// Открытие файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Te_menu_openFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Все файлы(*.*)|*.*|Текстовые файлы(*txt)|*.txt|Файлы cs(*cs)|*.cs";
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

            sfd.Filter = "Все файлы(*.*)|*.*|Текстовые файлы(*txt)|*.txt|Файлы cs(*cs)|*.cs";
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

                EnableDisableElements(true);
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
        /// Включение/выключение элементов
        /// </summary>
        /// <param name="status"></param>
        private void EnableDisableElements(bool status)
        {
            te_menu_saveFile.Enabled = status;
            te_tsb_save.Enabled = status;
            te_rtb_editor.Enabled = status;
            te_menu_closeFile.Enabled = status;
            te_menu_saveAs.Enabled = status;

            te_menu_undo.Enabled = status;
            te_menu_redo.Enabled = status;
            te_menu_insert.Enabled = status;
            te_menu_selectAll.Enabled = status;

            te_tsb_undo.Enabled = status;
            te_tsb_redo.Enabled = status;
            te_tsb_insert.Enabled = status;

            te_cm_undo.Enabled = status;
            te_cm_redo.Enabled = status;
            te_cm_insert.Enabled = status;

            if (!status)
            {
                te_sb_path.Text = PATH;
                this.Text = TITLE;
            }
        }
    }
}
